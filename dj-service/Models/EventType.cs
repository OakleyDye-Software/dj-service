using System.ComponentModel.DataAnnotations.Schema;

namespace dj_service;

public class EventType
{
    [Column("id")]
    public int Id { get; set; }
    [Column("event_type_name")]
    public string Name { get; set; } = string.Empty;
}
