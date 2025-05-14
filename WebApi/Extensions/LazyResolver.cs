namespace WebApi.Extensions
{
   
    public class LazyResolver<T> : Lazy<T> where T : class
    {
        public LazyResolver(IServiceProvider serviceProvider)
            : base(() => serviceProvider.GetRequiredService<T>())
        {
        }
    }
}
