﻿using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
   
    //[ApiVersion("1.0")] //bu kısmı extensionda da yaptık.
    [ServiceFilter(typeof(LogFilterAttribute))]
    [ApiController]
    [Route("api/books")]
    [Authorize]
    [ApiExplorerSettings(GroupName ="v1")]
    //[ResponseCache(CacheProfileName="5min")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;
        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }
        [Authorize]
        [HttpHead]//respons body siz sadece header ile oluşturulur
        [HttpGet(Name = "GetAllBooksAsync")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        //[HttpCacheExpiration(CacheLocation =CacheLocation.Private,MaxAge = 40)]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetAllBooksAsync([FromQuery] BookParameters bookParameters)
        {
            var linkParameters = new LinkParameters()
            {
                BookParameters = bookParameters,
                HttpContext = HttpContext
            };

            var result = await _manager
                .BookService
                .GetAllBooksAsync(linkParameters, false);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(result.metaData));

            return result.linkResponse.HasLinks ?
                Ok(result.linkResponse.LinkedEntities) :
                Ok(result.linkResponse.ShapedEntities);
        }
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneBookAsync([FromRoute(Name = "id")] int id)
        {
            var book = await _manager
            .BookService
            .GetOneBookByIdAsync(id, false);

            return Ok(book);
        }
        [Authorize(Roles ="Admin, Editor")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost(Name = "CreateOneBookAsync")]
        public async Task<IActionResult> CreateOneBookAsync([FromBody] BookDtoForInsertion bookDto)
        {
            var book = await _manager.BookService.CreateOneBookAsync(bookDto);
            return StatusCode(201, book); // CreatedAtRoute()
        }

        [Authorize(Roles ="Admin, Editor")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneBookAsync([FromRoute(Name = "id")] int id,[FromBody] BookDtoForUpdate bookDto)
        {
            await _manager.BookService.UpdateOneBookAsync(id, bookDto, false);
            return NoContent(); // 204
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneBookAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.BookService.DeleteOneBookAsync(id, false);
            return NoContent();
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartiallyUpdateOneBookAsync([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<BookDtoForUpdate> bookPatch)
        {

            if (bookPatch is null)
                return BadRequest(); // 400

            var result = await _manager.BookService.GetOneBookForPatchAsync(id, false);

            bookPatch.ApplyTo(result.bookDtoForUpdate, ModelState);

            TryValidateModel(result.bookDtoForUpdate);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _manager.BookService.SaveChangesForPatchAsync(result.bookDtoForUpdate, result.book);

            return NoContent(); // 204
        }

        [Authorize]
        [HttpOptions]
        public IActionResult OptionsBooks()
        {
            Response.Headers.Add("Allow", "Get,Put,Post,Patch,Delete,Options");
            return Ok();
        }

        [Authorize]
        [HttpGet("details")]
        public async Task <IActionResult> GetAllBooksWithDetailsAsync()
        {
            return Ok(await _manager.BookService.GetAllBooksWithDetailsAsync(false));
        }

    }
}
