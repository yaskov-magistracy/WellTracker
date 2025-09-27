using System.Security.Claims;
using API.Configuration.Auth;
using API.Modules.AccountsModule.DTO;
using Domain.Accounts;
using Domain.Accounts.Admins;
using Domain.Accounts.Admins.DTO;
using Domain.Accounts.DTO;
using Domain.Accounts.Users;
using Domain.Accounts.Users.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.AccountsModule;

[Route("api/accounts")]
[ApiController]
public class AccountsController(
    IAccountsService accountsService,
    IAdminsService adminsService,
    IUsersService usersService
) : ControllerBase
{
    /// <summary>
    /// Инфа о сессии
    /// </summary>
    [Authorize]
    [HttpGet("session")]
    public ActionResult<SessionInfoApiResponse> GetCurrentSession()
    {
        return Ok(new SessionInfoApiResponse(
            User.GetId(),
            User.GetRole()));
    }

    /// <summary>
    /// Регистрация Админа
    /// </summary>
    [HttpPost("register/admin")]
    [AuthorizeRoles(AccountRole.Admin)]
    public async Task<ActionResult> RegisterAdmin(AdminCreateRequest request)
    {
        var result = await adminsService.Register(request);
        return result.ActionResult; // TODO: remove hashedPass from response
    }

    /// <summary>
    /// Регистрация Пользователя
    /// </summary>
    [HttpPost("register/user")]
    public async Task<ActionResult> RegisterPartner(UserCreateRequest request)
    {
        var result = await usersService.Register(request);
        return result.ActionResult;
    }

    /// <summary>
    /// Смена пароля. Нужно быть авторизованным
    /// </summary>
    /// <remarks>После смены пароля разлогинивает</remarks>
    [Authorize]
    [HttpPost("change-password")]
    public async Task<ActionResult> ChangePassword(AccountChangePasswordApiRequest apiRequest)
    {
        var userId = User.GetId();
        var userRole = User.GetRole();
        var request = AccountsApiMapper.ToDomain(userId, apiRequest);
        var result = userRole == AccountRole.Admin
            ? await adminsService.ChangePassword(request)
            : await usersService.ChangePassword(request);
        if (result.IsSuccess)
            await Logout();
        
        return result.ActionResult;
    }

    /// <summary>
    /// Вход в аккаунт
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<SessionInfoApiResponse>> Login(AccountLoginRequest apiRequest)
    {
        var result = await accountsService.Login(apiRequest);
        if (!result.IsSuccess)
            return result.ActionResult;

        var loginResult = result.Value;
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, BuildClaims(loginResult));
        return Ok(new SessionInfoApiResponse(
            loginResult.Id,
            loginResult.Role)
        );
    }

    /// <summary>
    /// Выход из аккаунта
    /// </summary>
    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return NoContent();
    }

    private ClaimsPrincipal BuildClaims(Account account)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
            new Claim(ClaimTypes.Role, account.Role.ToString())
        };
        var credentials = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        return new ClaimsPrincipal(credentials);
    }
}