using Microsoft.AspNetCore.Mvc;

namespace Parks.API.Controllers
{
    public class ControllerSuper:ControllerBase
    {
        protected readonly Serilog.ILogger Logger;

        public ControllerSuper(Serilog.ILogger logger)
        {
            Logger = logger;
        }

        protected ObjectResult WriteExceptionMessage(Exception ex)
        {
            Logger.Error(ex, ex.Message);

            return Problem(ex.Message, StatusCodes.Status500InternalServerError.ToString());
        }
    }
}
