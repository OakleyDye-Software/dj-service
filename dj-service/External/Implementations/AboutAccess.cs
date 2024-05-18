
using Dapper;

namespace dj_service;

public class AboutAccess(IDbAccess dbAccess) : IAboutAccess
{
    private readonly IDbAccess dbAccess = dbAccess;

    public async Task<About> GetAboutAsync() =>
        await dbAccess.GetAccessPolicy().Execute(async () => 
            await dbAccess.GetConnection().QuerySingleAsync<About>(
                sql: @"
                    SELECT
                        id AS Id,
                        about_us_description AS Description,
                        about_us_image_path AS ImageUrl
                    FROM app.about_us;"
            ));

    public async Task UpdateAboutAsync(About about) =>
        await dbAccess.GetAccessPolicy().Execute(async () =>
            await dbAccess.GetConnection().ExecuteAsync(
                sql: @"
                    UPDATE app.about_us
                    SET
                        about_us_description = @Description,
                        about_us_image_path = @ImageUrl
                    WHERE id = 1;",
                param: about
            ));
}
