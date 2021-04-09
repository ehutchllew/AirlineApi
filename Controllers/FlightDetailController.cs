
using AirlineApi.Data;
using AirlineApi.Dtos;
using AirlineApi.MessageBus;
using AirlineApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AirlineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightDetailController : ControllerBase
    {
        private readonly AirlineDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMessageBusClient _messageBus;

        public FlightDetailController(AirlineDbContext context, IMapper mapper, IMessageBusClient messageBusClient)
        {
            this._context = context;
            this._mapper = mapper;
            this._messageBus = messageBusClient;
        }

        [HttpGet("{flightCode}", Name = "GetFlightDetailsByCode")]
        public ActionResult<FlightDetailReadDto> GetFlightDetailsByCode([FromRoute] string flightCode)
        {
            FlightDetail flight = this._context.FlightDetails.FirstOrDefault(f => f.FlightCode == flightCode);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(this._mapper.Map<FlightDetailReadDto>(flight));
        }

        [HttpPost]
        public ActionResult<FlightDetailReadDto> CreateFlight([FromBody] FlightDetailCreateDto flightDetailCreateDto)
        {
            FlightDetail flight = this._context.FlightDetails.FirstOrDefault(f => f.FlightCode == flightDetailCreateDto.FlightCode);

            if (flight == null)
            {
                FlightDetail flightDetailModel = this._mapper.Map<FlightDetail>(flightDetailCreateDto);

                try
                {
                    this._context.FlightDetails.Add(flightDetailModel);
                    this._context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

                FlightDetailReadDto flightReadDto = this._mapper.Map<FlightDetailReadDto>(flightDetailModel);

                return CreatedAtRoute(nameof(this.GetFlightDetailsByCode), new { flightCode = flightReadDto.FlightCode }, flightReadDto);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateFlightDetail([FromRoute] int id, FlightDetailUpdateDto flightDetailUpdateDto)
        {
            FlightDetail flight = this._context.FlightDetails.FirstOrDefault(f => f.Id == id);

            if (flight == null)
            {
                return NotFound();
            }

            decimal oldPrice = flight.Price;

            this._mapper.Map(flightDetailUpdateDto, flight);

            try
            {
                this._context.SaveChanges();
                if (oldPrice != flight.Price)
                {
                    Console.WriteLine("Price Changed -- Place Message on Bus");
                    var message = new NotificationMessageDto
                    {
                        WebhookType = "priceChange",
                        OldPrice = oldPrice,
                        NewPrice = flight.Price,
                        FlightCode = flight.FlightCode
                    };

                    this._messageBus.SendMessage(message);
                }
                else
                {
                    Console.WriteLine("No Price Change");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}