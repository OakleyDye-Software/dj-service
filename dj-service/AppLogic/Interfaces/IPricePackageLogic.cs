namespace dj_service;

public interface IPricePackageLogic
{
    Task<IEnumerable<PricePackage>> GetPricePackagesAsync();
    Task<PricePackage> GetPricePackageAsync(int id);
    Task<PricePackage> CreatePricePackageAsync(PricePackage pricePackage);
    Task<PricePackage> UpdatePricePackageAsync(PricePackage pricePackage);
    Task DeletePricePackageAsync(int id);
}
