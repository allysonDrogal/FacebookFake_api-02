using Facebook_Fake.Application.useCase.Users.ForgotPassword;
using Facebook_Fake.Application.useCase.Users.Login;
using Facebook_Fake.Application.useCase.Users.Register;
using Facebook_Fake.Application.useCase.Users.ResetPassword;
using Facebook_Fake.Communication.Requests;
using Facebook_Fake.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost("forgot-password")]
    [ProducesResponseType(typeof(ResponseForgotPasswordJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> ForgotPassword(
       [FromServices] IForgotPasswordUseCase forgotPasswordUseCase,
       [FromBody] RequestForgotPasswordJson request)
    {
        var response = await forgotPasswordUseCase.Execute(request);
        return Ok(response);
    }

    [HttpPost("reset-password")]
    [ProducesResponseType(typeof(ResponseForgotPasswordJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ResetPassword(
        [FromServices] IResetPasswordUseCase resetPasswordUseCase,
        [FromBody] RequestResetPasswordJson request)
    {
        try
        {
            var response = await resetPasswordUseCase.Execute(request);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);

        }
    }
}

