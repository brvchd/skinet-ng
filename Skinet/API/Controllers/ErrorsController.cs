using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("errors/{code:int}")]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController
{
    public IActionResult Error(int code)
    {
        return new ObjectResult(new ApiResponse(code));
    }
}