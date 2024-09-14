namespace dj_service;

public interface ISearchAccess
{
    Task<GeniusSearchResponse> GeniusSearchAsync(string query);
}
