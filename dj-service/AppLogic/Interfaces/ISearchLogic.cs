namespace dj_service;

public interface ISearchLogic
{
    Task<GeniusSearchResponse> PerformSearchAsync(string query);
}
