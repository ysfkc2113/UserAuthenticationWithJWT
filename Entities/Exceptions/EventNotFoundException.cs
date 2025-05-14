namespace Entities.Exceptions
{
    public sealed class EventNotFoundException : NotFoundException
    {
        public EventNotFoundException(int id) 
            : base($"The event with id : {id} could not found.")
        {
        }
    }


}
