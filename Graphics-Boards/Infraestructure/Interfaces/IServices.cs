

using Infraestructure.Helpers;


namespace Infraestructure.Interfaces
{
    public interface IServices<T> where T : class
    {
        Task DisposeAsync();
        Task<List<T>> GetTotalsByResponsableAsync(FiltersParams filters = null);
       
    }
}
