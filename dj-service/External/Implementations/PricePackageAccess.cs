
using Dapper;

namespace dj_service;

public class PricePackageAccess(IDbAccess dbAccess) : IPricePackageAccess
{
    private readonly IDbAccess _dbAccess = dbAccess;

    public async Task<PricePackage> CreatePricePackageAsync(PricePackage pricePackage) =>
        await _dbAccess.GetAccessPolicy().Execute(async () =>
        {
            var id = await _dbAccess.GetConnection().QuerySingleAsync<int>(
                sql: @"
                    INSERT INTO app.price_package (package_name, package_description, package_price)
                    VALUES (@PackageName, @PackageDescription, @PackagePrice)
                    RETURNING id;",
                param: new { PackageName = pricePackage.Name, PackageDescription = pricePackage.Description, PackagePrice = pricePackage.Price }
            );

            return await GetPricePackageAsync(id);
        });

    public async Task<PricePackageFeature> CreatePricePackageFeatureAsync(int pricePackageId, PricePackageFeature pricePackageFeature) =>
        await _dbAccess.GetAccessPolicy().Execute(async () =>
        {
            var id = await _dbAccess.GetConnection().QuerySingleAsync<int>(
                sql: @"
                    INSERT INTO app.price_package_feature (package_id, feature_name, feature_description)
                    VALUES (@PricePackageId, @Name, @Description)
                    RETURNING id;",
                param: new { PricePackageId = pricePackageId, pricePackageFeature.Name, pricePackageFeature.Description }
            );

            return await GetPricePackageFeatureAsync(id);
        });

    public async Task DeletePricePackageAsync(int id) => 
        await _dbAccess.GetAccessPolicy().Execute(async () =>
            await _dbAccess.GetConnection().ExecuteAsync(
                sql: @"
                    DELETE FROM app.price_package
                    WHERE id = @Id;",
                param: new { Id = id }
            ));

    public async Task DeletePricePackageFeatureAsync(int id) =>
        await _dbAccess.GetAccessPolicy().Execute(async () =>
            await _dbAccess.GetConnection().ExecuteAsync(
                sql: @"
                    DELETE FROM app.price_package_feature
                    WHERE id = @Id;",
                param: new { Id = id }
            ));

    public async Task<PricePackage> GetPricePackageAsync(int id) =>
        await _dbAccess.GetAccessPolicy().Execute(async () =>
            await _dbAccess.GetConnection().QueryFirstAsync<PricePackage>(
                sql: @"
                    SELECT
                        id AS Id,
                        package_name AS Name,
                        package_description AS Description,
                        package_price AS Price
                    FROM app.price_package
                    WHERE id = @Id;",
                param: new { Id = id }
            ));

    public async Task<PricePackageFeature> GetPricePackageFeatureAsync(int id) =>
        await _dbAccess.GetAccessPolicy().Execute(async () =>
            await _dbAccess.GetConnection().QueryFirstAsync<PricePackageFeature>(
                sql: @"
                    SELECT
                        id AS Id,
                        feature_name AS Name,
                        feature_description AS Description
                    FROM app.price_package_feature
                    WHERE id = @Id;",
                param: new { Id = id }
            ));

    public async Task<IEnumerable<PricePackage>> GetPricePackagesAsync() =>
        await _dbAccess.GetAccessPolicy().Execute(async () =>
            await _dbAccess.GetConnection().QueryAsync<PricePackage>(
                sql: @"
                    SELECT
                        id AS Id,
                        package_name AS Name,
                        package_description AS Description,
                        package_price AS Price
                    FROM app.price_package;"
            ));

    public async Task<IEnumerable<PricePackageFeature>> GetPricePackageFeaturesAsync(int pricePackageId) =>
        await _dbAccess.GetAccessPolicy().Execute(async () =>
            await _dbAccess.GetConnection().QueryAsync<PricePackageFeature>(
                sql: @"
                    SELECT
                        id AS Id,
                        feature_name AS Name,
                        feature_description AS Description
                    FROM app.price_package_feature
                    WHERE package_id = @PricePackageId;",
                param: new { PricePackageId = pricePackageId }
            ));

    public async Task<PricePackage> UpdatePricePackageAsync(PricePackage pricePackage) =>
        await _dbAccess.GetAccessPolicy().Execute(async () =>
        {
            await _dbAccess.GetConnection().ExecuteAsync(
                sql: @"
                    UPDATE app.price_package
                    SET
                        package_name = @Name,
                        package_description = @Description,
                        package_price = @Price
                    WHERE id = @Id;",
                param: pricePackage
            );

            return await GetPricePackageAsync(pricePackage.Id);
        });

    public async Task<PricePackageFeature> UpdatePricePackageFeatureAsync(PricePackageFeature pricePackageFeature) =>
        await _dbAccess.GetAccessPolicy().Execute(async () =>
        {
            await _dbAccess.GetConnection().ExecuteAsync(
                sql: @"
                    UPDATE app.price_package_feature
                    SET
                        feature_name = @Name,
                        feature_description = @Description
                    WHERE id = @Id;",
                param: pricePackageFeature
            );

            return await GetPricePackageFeatureAsync(pricePackageFeature.Id);
        });
}
