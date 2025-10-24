using API.Configuration.Auth;
using API.Modules.UsersModule.DTO;
using Domain.Accounts;
using Domain.Accounts.Users;
using Infrastructure.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.UsersModule;

[Route("api/[controller]")]
[ApiController]
public class UsersController(
    IUsersService usersService) : ControllerBase
{
    [Authorize]
    [HttpGet("{userId:Guid}/info")]
    public async Task<ActionResult<UserInfoApiResponse>> GetUserInfo([FromRoute] Guid userId)
    {
        var accessError = EnsureAccess(userId);
        if (accessError != null)
            return Forbid(accessError);

        var user = await usersService.GetById(userId);
        return user
            .Map(UsersMapper.ToApiResponse)
            .ActionResult;
    }
    
    [Authorize]
    [HttpPost("{userId:Guid}/info")]
    public async Task<ActionResult<UserInfoApiResponse>> PatchUserInfo(
        [FromRoute] Guid userId,
        [FromBody] UserInfoPatchRequest updateReq)
    {
        var accessError = EnsureAccess(userId);
        if (accessError != null)
            return Forbid(accessError);

        var user = await usersService.Update(userId, UsersMapper.ToDomain(updateReq));
        return user
            .Map(UsersMapper.ToApiResponse)
            .ActionResult;
    }

    private string? EnsureAccess(Guid userId)
    {
        var role = User.GetRole();
        return role switch
        {
            AccountRole.Admin => null,
            AccountRole.User => userId == User.GetId()
                ? null
                : $"Нет доступа до пользователя {userId}",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}