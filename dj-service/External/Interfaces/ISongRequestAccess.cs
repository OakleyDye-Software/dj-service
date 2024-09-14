namespace dj_service;

public interface ISongRequestAccess
{
    Task<int> CreateSongRequestAsync(HitResult song);
    Task<IEnumerable<SongRequest>> GetSongRequestsAsync();
    Task ArchiveAllSongRequestsAsync();
    Task ArchiveSongRequestAsync(int id);
    Task<bool> GetSongRequestSettingAsync();
    Task<bool> ToggleSongRequestSettingAsync();
}
