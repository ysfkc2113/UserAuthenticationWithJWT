namespace Entities.Exceptions
{
    public sealed class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string userName) 
            : base($"The user with user name: {userName} could not found.")
        {
        }
    }


}
