namespace dj_service;

public class AboutLogic(IAboutAccess aboutAccess) : IAboutLogic
{
    private readonly IAboutAccess aboutAccess = aboutAccess;

    public async Task<About> GetAboutAsync() =>
        await aboutAccess.GetAboutAsync();

    public async Task UpdateAboutAsync(About about) =>
        await aboutAccess.UpdateAboutAsync(about);
}
