namespace dj_service;

public interface IAboutAccess
{
    Task<About> GetAboutAsync();
    Task UpdateAboutAsync(About about);
}
