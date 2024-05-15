
using Dapper;

namespace dj_service;

public class ServiceAccess(IDbAccess dbAccess) : IServiceAccess
{
    private readonly IDbAccess dbAccess = dbAccess;

    public async Task<IEnumerable<ServiceSummary>> GetServiceSummariesAsync() =>
        await dbAccess.GetAccessPolicy().Execute(async () => 
            await dbAccess.GetConnection().QueryAsync<ServiceSummary>(
                sql: @"
                    SELECT
                        id AS Id,
                        service_name AS Name,
                        service_description AS Description,
                        service_image_path AS ImageUrl,
                        page_id AS PageId
                    FROM
                        app.service_summary;"
            ));

    public async Task<int> CreateServiceAsync(ServiceSummary serviceSummary) =>
        await dbAccess.GetAccessPolicy().Execute(async () =>
            await dbAccess.GetConnection().ExecuteAsync(
                sql: @"
                    INSERT INTO app.service_summary (
                        service_name,
                        service_description,
                        service_image_path,
                        page_id
                    )
                    VALUES (
                        @Name,
                        @Description,
                        @ImageUrl,
                        @PageId
                    )
                    RETURNING id;",
                param: serviceSummary
            ));
}
