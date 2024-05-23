namespace dj_service;

public class PricePackage
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public IEnumerable<PricePackageFeature> Features { get; set; } = new List<PricePackageFeature>();
}

public class PricePackageFeature
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}