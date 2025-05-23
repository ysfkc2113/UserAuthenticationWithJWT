﻿using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationManager : IAuthenticationService
    {
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private User _user;

        public AuthenticationManager(ILoggerService loggerService, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _loggerService = loggerService;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto)
        {
            var user = _mapper.Map<User>(userForRegistrationDto);

            var result = await _userManager
                .CreateAsync(user, userForRegistrationDto.Password);

            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, userForRegistrationDto.Roles);
            return result;
        }

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuthDto)
        {
            _user = await _userManager.FindByNameAsync(userForAuthDto.UserName);
            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuthDto.Password));
            if (!result)
            {
                _loggerService.LogWarning($"{nameof(ValidateUser)} : Authentication failed. Wrong username or password.");
            }
            return result;
        }

        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
            var user = await _userManager.FindByNameAsync(principal.Identity.Name);

            if (user is null ||
                user.RefreshToken != tokenDto.RefreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.Now)
                throw new RefreshTokenBadRequestException();

            _user = user;
            return await CreateToken(populateExp: false);
        }


        //jwt token üretimi kullanıcı adı ve rolleri ile
        public async Task<TokenDto> CreateToken(bool populateExp)
        {
            var signinCredentials = GetSiginCredentials();//User bilgileri
            var claims = await GetClaims();//user claims(haklar)
            var tokenOptions = GenerateTokenOptions(signinCredentials, claims);//token ayarları

            var refreshToken = GenerateRefreshToken();
            _user.RefreshToken = refreshToken;

            if (populateExp)
                _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            await _userManager.UpdateAsync(_user);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return new TokenDto()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signinCredentials,
           List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken(
                    issuer: jwtSettings["validIssuer"],
                    audience: jwtSettings["validAudience"], 
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                    signingCredentials: signinCredentials);

            return tokenOptions;
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, _user.UserName)
            };

            var roles = await _userManager
                .GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private SigningCredentials GetSiginCredentials()
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["secretKey"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            //maliyetli bir işlem olduğu için using kullanırız ki işlem bitince kaynakları serbes bıraksın.
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        //gelen token bize ait mi bakar ait ise kullanıcı bilgilerini döner
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["secretKey"];

            var tokenValidationParameters = new TokenValidationParameters
            {

                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["validIssuer"],
                ValidAudience = jwtSettings["validAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters,
                out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken is null ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token.");
            }
            return principal;
        }


       
    }
}
