using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Status;

public class StatusController : ControllerBase

{
    [HttpGet("/status")]
    public ActionResult GetTheStatus()
    {
        return Ok();
    }
}
