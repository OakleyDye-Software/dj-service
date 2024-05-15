namespace dj_service;

public class ServiceLogic(IServiceAccess serviceAccess) : IServiceLogic
{
    private readonly IServiceAccess serviceAccess = serviceAccess;

    public async Task<IEnumerable<ServiceSummary>> GetServiceSummariesAsync() =>
        await serviceAccess.GetServiceSummariesAsync();

    public async Task<int> CreateServiceAsync(ServiceSummary serviceSummary) =>
        await serviceAccess.CreateServiceAsync(serviceSummary);
}
