using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Parks.API.Controllers
{
    public class ControllerSuper : ControllerBase
    {
        protected readonly Serilog.ILogger Logger;
        protected readonly IMapper Mapper;

        public ControllerSuper(Serilog.ILogger logger, IMapper mapper)
        {
            Logger = logger;
            Mapper = mapper;
        }

        protected ObjectResult WriteExceptionMessage(Exception ex)
        {
            Logger.Error(ex, ex.Message);

            return Problem(ex.Message, StatusCodes.Status500InternalServerError.ToString());
        }
    }
}
