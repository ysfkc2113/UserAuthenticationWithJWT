namespace Entities.RequestFeatures
{
    public class UsersParametersClubManager : RequestParameters
    {
        public string? Role { get; set; }
        public string? SearchTerm { get; set; }

        //çalışmıyor boş olduğunda düzgün çalışıyor
        public UsersParametersClubManager()
        {
            OrderBy = "Id";
        }
    }

}
