namespace StubGPT.Core;
public class UserConfigurationOptionsProvider : IConfigureOptions<UserConfiguration>
{
    #region Fields..
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserService _userService;
    #endregion Fields..

    #region Constructors..
    public UserConfigurationOptionsProvider(IHttpContextAccessor httpContextAccessor, IUserService userService)
    {
        _httpContextAccessor = httpContextAccessor;
        _userService = userService;
    }
    #endregion Constructors..

    #region Methods..
    public void Configure(UserConfiguration options)
    {
        string? sessionToken = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (sessionToken != null)
        {
            var user = _userService.GetUserBySessionToken(sessionToken);
            var userConfiguration = _userService.GetUserConfiguration(user.UserId);

            if (userConfiguration == null)
                options = _userService.AddUserConfiguration(user.UserId);
            else
                options = userConfiguration;
        }
    }
    #endregion Methods..
}
