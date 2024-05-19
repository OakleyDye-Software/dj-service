using System.ComponentModel.DataAnnotations.Schema;

namespace dj_service;

public class Counter
{
    [Column("id")]
    public int Id { get; set; }
    [Column("counter_name")]
    public string Name { get; set; } = string.Empty;
    [Column("counter_value")]
    public int Value { get; set; }
    [Column("show_plus")]
    public bool ShowPlus { get; set; }
}
