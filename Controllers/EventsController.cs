using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.WebAPI.Dtos;
using ProAgil.WebAPI.Models;
using ProAgil.WebAPI.Services;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        public ProAgilService _proAgilService { get; }
        public IMapper _mapper { get; }
        public EventsController(ProAgilService proAgilService, IMapper mapper)
        {
            _mapper = mapper;
            _proAgilService = proAgilService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var events = await _proAgilService.GetAllEventAsync(true);
                var eventsDto = _mapper.Map<EventDto[]>(events);
                return Ok(eventsDto);
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
                var objectEvent = await _proAgilService.GetEventAsyncById(EventId, true);
                var eventDto = _mapper.Map<EventDto>(objectEvent);
                return Ok(eventDto);
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
                var events = await _proAgilService.GetAllEventAsyncByTheme(Theme, true);
                var eventsDto = _mapper.Map<EventDto[]>(events);
                return Ok(eventsDto);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventDto EventDto)
        {
            try
            {
                var Event = _mapper.Map<Event>(EventDto);
                _proAgilService.Add(Event);
                if (await _proAgilService.SaveChangesAsync())
                {
                    return Created($"/api/event/{Event.Id}", _mapper.Map<EventDto>(Event));
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int EventId, EventDto eventDto)
        {
            try
            {
                var Event = await _proAgilService.GetEventAsyncById(EventId, false);
                if (Event == null) return NotFound();

                _mapper.Map(eventDto, Event);

                _proAgilService.Update(Event);
                if (await _proAgilService.SaveChangesAsync())
                {
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
                if (await _proAgilService.SaveChangesAsync())
                {
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