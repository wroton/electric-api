using Microsoft.AspNetCore.Mvc;

using Service.Server.Infrastructure;

namespace Service.Server.Controllers
{
    /// <summary>
    /// Controller which all controllers must implement.
    /// </summary>
    [ValidateModel]
    [ProducesResponseType(typeof(string), 500)]
    [Consumes("application/json")]
    public abstract class BaseController : ControllerBase
    {
    }
}
