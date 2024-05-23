
namespace dj_service;

public class PricePackageLogic(IPricePackageAccess pricePackageAccess) : IPricePackageLogic
{
    private readonly IPricePackageAccess _pricePackageAccess = pricePackageAccess;

    public async Task<PricePackage> CreatePricePackageAsync(PricePackage pricePackage)
    {
        var result = await _pricePackageAccess.CreatePricePackageAsync(pricePackage);
        if (result.Id != 0)
        {
            await Task.WhenAll(pricePackage.Features.Select(async feature =>
            {
                var f = await _pricePackageAccess.CreatePricePackageFeatureAsync(result.Id, feature);
                if (f.Id != 0)
                {
                    lock(result.Features)
                    {
                        result.Features = result.Features.Append(f);
                    }
                }
            }));

            if (result.Features.Count() == pricePackage.Features.Count())
            {
                return result;
            }
            else
            {
                await _pricePackageAccess.DeletePricePackageAsync(result.Id);
                return null;
            }
        }
        return null;
    }

    public async Task DeletePricePackageAsync(int id)
    {
        await Task.WhenAll((await _pricePackageAccess.GetPricePackageFeaturesAsync(id)).Select(async feature =>
        {
            await _pricePackageAccess.DeletePricePackageFeatureAsync(feature.Id);
        }));
        await _pricePackageAccess.DeletePricePackageAsync(id);
    }

    public Task<PricePackage> GetPricePackageAsync(int id) => _pricePackageAccess.GetPricePackageAsync(id);

    public async Task<IEnumerable<PricePackage>> GetPricePackagesAsync()
    {
        var pricePackages = await _pricePackageAccess.GetPricePackagesAsync();
        await Task.WhenAll(pricePackages.Select(async pricePackage =>
        {
            pricePackage.Features = await _pricePackageAccess.GetPricePackageFeaturesAsync(pricePackage.Id);
        }));
        return pricePackages;
    }

    public async Task<PricePackage> UpdatePricePackageAsync(PricePackage pricePackage)
    {
        var result = await _pricePackageAccess.UpdatePricePackageAsync(pricePackage);
        if (result.Id != 0)
        {
            await Task.WhenAll(pricePackage.Features.Select(async feature =>
            {
                if (feature.Id == 0)
                {
                    var f = await _pricePackageAccess.CreatePricePackageFeatureAsync(result.Id, feature);
                    if (f.Id != 0)
                    {
                        lock(result.Features)
                        {
                            result.Features = result.Features.Append(f);
                        }
                    }
                }
                else
                {
                    var f = await _pricePackageAccess.UpdatePricePackageFeatureAsync(feature);
                    if (f.Id != 0)
                    {
                        lock(result.Features)
                        {
                            result.Features = result.Features.Select(f => f.Id == feature.Id ? f : feature);
                        }
                    }
                }
            }));

            if (result.Features.Count() == pricePackage.Features.Count())
            {
                return result;
            }
            else
            {
                await _pricePackageAccess.DeletePricePackageAsync(result.Id);
                return null;
            }
        }
        return null;
    }
}
