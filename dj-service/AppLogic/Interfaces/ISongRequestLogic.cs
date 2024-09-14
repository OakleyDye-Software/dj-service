namespace dj_service;

public interface ISongRequestLogic
{
    Task<int> CreateSongRequestAsync(HitResult song);
    Task<IEnumerable<SongRequest>> GetSongRequestsAsync();
    Task ArchiveAllSongRequestsAsync();
    Task ArchiveSongRequestsAsync(IEnumerable<int> ids);
    Task<bool> GetSongRequestSettingAsync();
    Task<bool> ToggleSongRequestSettingAsync();
}
