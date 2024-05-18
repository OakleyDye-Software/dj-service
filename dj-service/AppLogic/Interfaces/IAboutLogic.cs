namespace dj_service;

public interface IAboutLogic
{
    Task<About> GetAboutAsync();
    Task UpdateAboutAsync(About about);
}
