using Newtonsoft.Json;

namespace dj_service;

public class GeniusSearchResponse
{
    public GeniusResponse Response { get; set; }
}

public class GeniusResponse
{
    public List<Hit> Hits { get; set; }
}

public class Hit
{
    public HitResult Result { get; set; }
}

public class HitResult
{
    public string Title { get; set; }
    [JsonProperty("artist_names")]
    public string ArtistNames { get; set; }
    public string Url { get; set; }
    [JsonProperty("song_art_image_thumbnail_url")]
    public string ImageUrl { get; set; }
}

public class SongRequest : HitResult
{
    public int Id { get; set; }
    public DateTime RequestDate { get; set; }
    public bool IsArchived { get; set; }
}