#nullable disable
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using Restaurant.Database;
using Restaurant.Models;
using Restaurant.ViewModels.Manager;

namespace Restaurant.Controllers.Manager
{
    [Route("api/manager/config")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public ConfigController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        // GET: api/manager/config
        [HttpGet]
        public async Task<ActionResult<RestaurantConfigViewModel>> GetConfig()
        {
            var config = await _repository.GetConfigAsync();

            if (config == null)
            {
                return NotFound();
            }

            return _mapper.Map<RestaurantConfigViewModel>(config);
        }

        // HttpPut: api/manager/config
        [HttpPut]
        public async Task<ActionResult<RestaurantConfigViewModel>> PutConfig(RestaurantConfigViewModel postConfig)
        {
            try
            {
                var oldConfig = await _repository.GetConfigAsync();
                if (oldConfig == null)
                {
                    return NotFound();
                }

                _mapper.Map(postConfig, oldConfig);
                
                // Json serialization cannot process TimeOnly type automatically. https://github.com/dotnet/runtime/issues/53539
                oldConfig.CloseHour = TimeOnly.Parse(postConfig.CloseHour);
                oldConfig.OpenHour = TimeOnly.Parse(postConfig.OpenHour);

                if (await _repository.SaveChangesAsync())
                {
                    return postConfig;
                }
                return BadRequest("Failed to update database");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to put Config");
            }
        }
    }
}
