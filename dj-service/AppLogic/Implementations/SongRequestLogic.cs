﻿namespace dj_service;

public class SongRequestLogic(ISongRequestAccess songRequestAccess) : ISongRequestLogic
{
    private readonly ISongRequestAccess songRequestAccess = songRequestAccess;
    public Task<int> CreateSongRequestAsync(HitResult song) => songRequestAccess.CreateSongRequestAsync(song);
    public async Task<IEnumerable<SongRequest>> GetSongRequestsAsync() => (await songRequestAccess.GetSongRequestsAsync()).Where(song => !song.IsArchived).OrderBy(song => song.RequestDate);
    public Task ArchiveAllSongRequestsAsync() => songRequestAccess.ArchiveAllSongRequestsAsync();
    public Task ArchiveSongRequestsAsync(IEnumerable<int> ids) => Task.WhenAll(ids.Select(songRequestAccess.ArchiveSongRequestAsync));
    public Task<bool> GetSongRequestSettingAsync() => songRequestAccess.GetSongRequestSettingAsync();
    public Task<bool> ToggleSongRequestSettingAsync() => songRequestAccess.ToggleSongRequestSettingAsync();
}
