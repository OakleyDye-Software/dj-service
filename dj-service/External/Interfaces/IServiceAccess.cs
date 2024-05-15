namespace dj_service;

public interface IServiceAccess
{
    Task<IEnumerable<ServiceSummary>> GetServiceSummariesAsync();
    Task<int> CreateServiceAsync(ServiceSummary serviceSummary);
}
