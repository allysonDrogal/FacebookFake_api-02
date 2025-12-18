using Facebook_Fake.Communication.Requests;
using Facebook_Fake.Communication.Responses;
using Facebook_Fake.Application.useCase.Users.Register;
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
}

