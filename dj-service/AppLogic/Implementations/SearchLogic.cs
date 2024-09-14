
namespace dj_service;

public class SearchLogic(ISearchAccess searchAccess) : ISearchLogic
{
    private readonly ISearchAccess _searchAccess = searchAccess;
    public Task<GeniusSearchResponse> PerformSearchAsync(string query)
    {
        return _searchAccess.GeniusSearchAsync(query);
    }
}
