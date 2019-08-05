using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.WebAPI.Models;
using ProAgil.WebAPI.Services;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        public ProAgilService _proAgilService { get; }
        public EventController(ProAgilService proAgilService)
        {
            _proAgilService = proAgilService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var list = await _proAgilService.GetAllEventAsync(true);
                return Ok(list);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        [HttpGet("{EventId}")]
        public async Task<IActionResult> Get(int EventId)
        {
            try
            {
                var list = await _proAgilService.GetEventAsyncById(EventId, true);
                return Ok(list);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        [HttpGet("getByTheme/{Theme}")]
        public async Task<IActionResult> Get(string Theme)
        {
            try
            {
                var list = await _proAgilService.GetAllEventAsyncByTheme(Theme, true);
                return Ok(list);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Event Event)
        {
            try
            {
                _proAgilService.Add(Event);
                if (await _proAgilService.SaveChangesAsync()) {
                    return Created($"/api/event/{Event.Id}", Event);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int EventId, Event Event)
        {
            try
            {
                var objectEvent = await _proAgilService.GetEventAsyncById(EventId, false);
                if (objectEvent == null) return NotFound();
                _proAgilService.Add(Event);
                if (await _proAgilService.SaveChangesAsync()) {
                    return NoContent();
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Delete(int EventId, Event Event)
        {
            try
            {
                var objectEvent = await _proAgilService.GetEventAsyncById(EventId, false);
                if (objectEvent == null) return NotFound();
                _proAgilService.Delete(Event);
                if (await _proAgilService.SaveChangesAsync()) {
                    return NoContent();
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
            return BadRequest();
        }
    }
}