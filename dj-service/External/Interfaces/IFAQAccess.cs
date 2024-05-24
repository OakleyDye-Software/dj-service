namespace dj_service;

public interface IFAQAccess
{
    Task<IEnumerable<FAQ>> GetFAQs();
    Task<FAQ> GetFAQ(int id);
    Task<FAQ> AddFAQ(FAQ faq);
    Task<FAQ> UpdateFAQ(FAQ faq);
    Task<FAQ> DeleteFAQ(int id);
}
