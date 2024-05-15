namespace dj_service;

public interface IServiceLogic
{
    Task<IEnumerable<ServiceSummary>> GetServiceSummariesAsync();
    Task<int> CreateServiceAsync(ServiceSummary serviceSummary);
}
