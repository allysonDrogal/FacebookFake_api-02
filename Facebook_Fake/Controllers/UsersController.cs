using Facebook_Fake.Communication.Requests;
using Facebook_Fake.Communication.Responses;
using Facebook_Fake.Application.useCase.Users.Register;
using Microsoft.AspNetCore.Mvc;
using Facebook_Fake.Application.useCase.Users.Login;

namespace FacebookFake.Api.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UsersController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseUsersJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUsersUseCase registerUsersUseCase,
        [FromBody] RequestUsersJson request)
    {

        var response = await registerUsersUseCase.Execute(request);
        return Created(string.Empty, response);

    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ResponseLoginJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(
        [FromServices] ILoginUseCase loginUseCase,
        [FromBody] RequestLoginJson request)
    {
        try
        {
            var response = await loginUseCase.Execute(request);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);

        }
    }
}

