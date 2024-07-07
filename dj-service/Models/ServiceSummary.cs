using System.ComponentModel.DataAnnotations.Schema;

namespace dj_service;

public class ServiceSummary
{
    [Column("id")]
    public int Id { get; set; }
    [Column("service_name")]
    public string Name { get; set; } = string.Empty;
    [Column("service_description")]
    public string Description { get; set; } = string.Empty;
    [Column("service_image_path")]
    public string ImageUrl { get; set; } = string.Empty;
    [Column("page_id")]
    public int PageId { get; set; }
    [Column("url_slug")]
    public string UrlSlug { get; set; } = string.Empty;
}
