namespace Entities.RequestFeatures
{
    public class UsersParametersAcademician : RequestParameters
    {
        public string? Role { get; set; }
        public bool? IsActive { get; set; }
        public string? SearchTerm { get; set; }

        //çalışmıyor boş olduğunda düzgün çalışıyor
        public UsersParametersAcademician()
        {
            OrderBy = "Id";
        }
    }

}
