using api.Filters;
using api.TransferModels;
using Microsoft.AspNetCore.Mvc;
using service;

namespace api.Controllers;

public class UserController : ControllerBase
{
    private readonly UserService _service;

    public UserController(UserService service)
    {
        _service = service;
    }

    [RequireAuthentication]
    [HttpGet("/api/users")]
    public ResponseDto Get()
    {
        return new ResponseDto
        {
            MessageToClient = "Successfully fetched",
            ResponseData = _service.GetAll()
        };
    }
}