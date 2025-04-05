using System.Linq;

namespace dj_service;

public class FAQLogic(IFAQAccess fAQAccess) : IFAQLogic
{
    private readonly IFAQAccess _fAQAccess = fAQAccess;
    public async Task<IEnumerable<FAQ>> GetFAQs() => await _fAQAccess.GetFAQs().OrderBy(x => x.Id);
    public async Task<FAQ> GetFAQ(int id) => await _fAQAccess.GetFAQ(id);
    public async Task<FAQ> AddFAQ(FAQ faq) => await _fAQAccess.AddFAQ(faq);
    public async Task<FAQ> UpdateFAQ(FAQ faq) => await _fAQAccess.UpdateFAQ(faq);
    public async Task<FAQ> DeleteFAQ(int id) => await _fAQAccess.DeleteFAQ(id);
}
