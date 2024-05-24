using System.ComponentModel.DataAnnotations.Schema;

namespace dj_service;

public class PricePackage
{
    [Column("id")]
    public int Id { get; set; }
    [Column("package_name")]
    public string Name { get; set; } = string.Empty;
    [Column("package_description")]
    public string Description { get; set; } = string.Empty;
    [Column("package_price")]
    public decimal Price { get; set; }
    public IEnumerable<PricePackageFeature> Features { get; set; } = new List<PricePackageFeature>();
}

public class PricePackageFeature
{
    [Column("id")]
    public int Id { get; set; }
    [Column("feature_name")]
    public string Name { get; set; } = string.Empty;
    [Column("feature_description")]
    public string Description { get; set; } = string.Empty;
}