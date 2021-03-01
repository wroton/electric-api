using Microsoft.AspNetCore.Mvc;

namespace Service.Server.Controllers
{
    /// <summary>
    /// Controller which all controllers must implement.
    /// </summary>
    [ProducesResponseType(typeof(string), 500)]
    [Consumes("application/json")]
    public abstract class BaseController : ControllerBase
    {
    }
}
