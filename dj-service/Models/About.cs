using System.ComponentModel.DataAnnotations.Schema;

namespace dj_service;

public class About
{
    [Column("id")]
    public int Id { get; set; }
    [Column("about_us_description")]
    public string Description { get; set; } = string.Empty;
    [Column("about_us_image_path")]
    public string ImageUrl { get; set; } = string.Empty;
}
