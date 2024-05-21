

using Infraestructure.Helpers;


namespace Infraestructure.Interfaces
{
    public interface IServices<T> where T : class
    {
        Task DisposeAsync();
        Task<List<T>> GetTotalsByResponsableAsync(FiltersParams filters = null);

        Task<List<T>> GetTotalsByRangeDaysAsync(FiltersParams filters = null);

        Task<List<T>> GetTotalsByStatuseAsync(FiltersParams filters = null);

    }
}
