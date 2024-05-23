namespace dj_service;

public interface IPricePackageAccess
{
    Task<IEnumerable<PricePackage>> GetPricePackagesAsync();
    Task<PricePackage> GetPricePackageAsync(int id);
    Task<PricePackage> CreatePricePackageAsync(PricePackage pricePackage);
    Task<PricePackage> UpdatePricePackageAsync(PricePackage pricePackage);
    Task DeletePricePackageAsync(int id);
    Task<IEnumerable<PricePackageFeature>> GetPricePackageFeaturesAsync(int pricePackageId);
    Task<PricePackageFeature> GetPricePackageFeatureAsync(int id);
    Task<PricePackageFeature> CreatePricePackageFeatureAsync(int pricePackageId, PricePackageFeature pricePackageFeature);
    Task<PricePackageFeature> UpdatePricePackageFeatureAsync(PricePackageFeature pricePackageFeature);
    Task DeletePricePackageFeatureAsync(int id);
}
