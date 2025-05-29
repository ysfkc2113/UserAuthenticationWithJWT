namespace Entities.DataTransferObjects
{
    public record MemberClubUserDtoRelations
    {
        public int Id { get; set; }
        //public Club Club { get; set; }//fk club
        public string ClubClubName { get; set; }
        public string ClubDescription { get; set; }
        public int ClubId { get; set; }
        //public User User { get; set; }//fk user 
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserUserName { get; set; }
        public string UserId { get; set; }
        public bool Approved { get; set; } = false;
        public string? role_in_club { get; set; }
        public DateTime? ApprovedTime { get; set; }//onaylanma tarihi
        public DateTime CreatedTime { get; set; }//istek atma tarihi
    }
    
}
