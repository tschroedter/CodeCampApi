using System ;
using System.Threading.Tasks ;
using CodeCampApi.Data ;
using Microsoft.AspNetCore.Http ;
using Microsoft.AspNetCore.Mvc ;
using Microsoft.Extensions.Logging ;

namespace CodeCampApi.Controllers
{
    [ Route ( "api/[controller]" ) ]
    public class CampsController
        : ControllerBase
    {
        private readonly ILogger < CampsController > _logger ;
        private readonly ICampsControllerProcessor   _processor ;

        public CampsController ( ILogger < CampsController > logger ,
                                 ICampsControllerProcessor processor )
        {
            _logger    = logger ;
            _processor = processor ;
        }

        [HttpGet]
        public async Task < IActionResult > Get ( )
        {
            _logger.LogInformation ( $"{nameof ( Get )} Received Get Request..." ) ;

            try
            {
                return await _processor.Get ( this ) ;
            }
            catch ( Exception e )
            {
                _logger.LogError ( e.Message ) ;

                return StatusCode ( StatusCodes.Status500InternalServerError ,
                                    "Database Error" ) ;
            }
        }
    }

    public class CampsControllerProcessor
        : ICampsControllerProcessor
    {
        private readonly ICampRepository _repository ;

        public CampsControllerProcessor ( ICampRepository repository )
        {
            _repository = repository ;
        }

        public async Task < IActionResult > Get ( CampsController controller )
        {
            var result = await _repository.GetAllCampsAsync ( ) ;

            return controller.Ok ( result ) ;
        }
    }

    public interface ICampsControllerProcessor
    {
        Task < IActionResult > Get ( CampsController controller ) ;
    }
}