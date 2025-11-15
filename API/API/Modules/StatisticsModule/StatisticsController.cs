using API.Configuration.Auth;
using API.Modules.StatisticsModule.DTO;
using Domain.Accounts;
using Domain.Accounts.Users;
using Domain.Statistics.Weight;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.StatisticsModule;

[Route("api/[controller]")]
[ApiController]
public class StatisticsController(
    IWeightRecordsService weightRecordsService,
    IUsersService usersService) : ControllerBase
{
    [AuthorizeRoles(AccountRole.User)]
    [HttpGet("Weight")]
    public async Task<ActionResult<WeightStatisticsApiResponse>> GetWeight(
        [FromQuery] DateOnly from, 
        [FromQuery] DateOnly to)
    {
        var statistics = await weightRecordsService.GetByPeriod(User.GetId(), from, to);
        var userInfo = await usersService.GetById(User.GetId());
        var user = userInfo.Value;
        return new WeightStatisticsApiResponse(user.Weight, user.TargetWeight, statistics);
    } 
}