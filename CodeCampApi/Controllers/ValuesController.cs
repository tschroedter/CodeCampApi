using Microsoft.AspNetCore.Mvc ;
using Microsoft.Extensions.Logging ;

namespace CodeCampApi.Controllers
{
    [ ApiController ]
    [Route( "api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger < WeatherForecastController > _logger ;

        public ValuesController ( ILogger < WeatherForecastController > logger )
        {
            _logger = logger ;
        }

        [ HttpGet ]
        public string [ ] Get ( )
        {
            return new [ ] { "Hello" , "From" , "Pluralsight" } ;
        }
    }
}