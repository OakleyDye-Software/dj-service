using System.ComponentModel.DataAnnotations.Schema;

namespace dj_service;

public class ServiceSummary
{
    [Column("id")]
    public int Id { get; set; }
    [Column("service_name")]
    public string Name { get; set; }
    [Column("service_description")]
    public string Description { get; set; }
    [Column("service_image_path")]
    public string ImageUrl { get; set; }
    [Column("page_id")]
    public int PageId { get; set; }
}
