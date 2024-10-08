﻿
using Dapper;

namespace dj_service;

public class SongRequestAccess(IDbAccess dbAccess) : ISongRequestAccess
{
    private readonly IDbAccess dbAccess = dbAccess;
    public async Task<int> CreateSongRequestAsync(HitResult song) =>
        await dbAccess.GetAccessPolicy().Execute(async () => 
            await dbAccess.GetConnection().QueryFirstAsync<int>(
                sql: @"
                    INSERT INTO app.song_request
                    (
                        song_name,
                        artist_name,
                        song_url,
                        image_url
                    )
                    VALUES
                    (
                        @Title,
                        @ArtistNames,
                        @Url,
                        @ImageUrl
                    )
                    RETURNING id;",
                param: new { song.Title, song.ArtistNames, song.Url, song.ImageUrl }));

    public Task<IEnumerable<SongRequest>> GetSongRequestsAsync() =>
        dbAccess.GetAccessPolicy().Execute(async () =>
            await dbAccess.GetConnection().QueryAsync<SongRequest>(
                sql: @"
                    SELECT
                        id AS Id,
                        song_name AS Title,
                        artist_name AS ArtistNames,
                        song_url AS Url,
                        image_url AS ImageUrl,
                        request_date AS RequestDate,
                        is_archived AS IsArchived
                    FROM app.song_request
                    WHERE
                        is_archived = false;"));

    public async Task ArchiveAllSongRequestsAsync() =>
        await dbAccess.GetAccessPolicy().Execute(async () =>
            await dbAccess.GetConnection().ExecuteAsync(
                sql: @"
                    UPDATE app.song_request
                    SET
                        is_archived = true;"));

    public async Task ArchiveSongRequestAsync(int id) =>
        await dbAccess.GetAccessPolicy().Execute(async () =>
            await dbAccess.GetConnection().ExecuteAsync(
                sql: @"
                    UPDATE app.song_request
                    SET
                        is_archived = true
                    WHERE
                        id = @Id;",
                param: new { Id = id }));

    public async Task<bool> GetSongRequestSettingAsync() =>
        await dbAccess.GetAccessPolicy().Execute(async () =>
            await dbAccess.GetConnection().QueryFirstAsync<bool>(
                sql: @"
                    SELECT
                        setting_value
                    FROM app.settings
                    WHERE
                        setting_name = 'accepting_requests';"));

    public async Task<bool> ToggleSongRequestSettingAsync() =>
        await dbAccess.GetAccessPolicy().Execute(async () =>
            await dbAccess.GetConnection().QueryFirstAsync<bool>(
                sql: @"
                    UPDATE app.settings
                    SET
                        setting_value = NOT setting_value
                    WHERE
                        setting_name = 'accepting_requests'
                    RETURNING setting_value;"));
}
