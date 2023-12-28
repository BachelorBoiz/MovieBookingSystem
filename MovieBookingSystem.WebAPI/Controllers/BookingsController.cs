using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieBookingSystem.Core.Models;
using MovieBookingSystem.Domain.IRepositories;

namespace MovieBookingSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        
        public BookingsController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [HttpGet]
        public IEnumerable<Booking> GetAll()
        {
            return _bookingRepository.GetBookings();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var booking = _bookingRepository.GetBookingById(id);
            if (booking == null)
            {
                return NotFound();
            }

            return new ObjectResult(booking);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Booking booking)
        {
            if (booking == null)
            {
                return BadRequest();
            }

            return new ObjectResult(_bookingRepository.CreateBooking(booking));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Booking booking)
        {
            if (booking == null || booking.Id != id)
            {
                return BadRequest();
            }
            return new ObjectResult(_bookingRepository.UpdateBooking(booking));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_bookingRepository.GetBookingById(id) == null)
            {
                return NotFound();
            }
            _bookingRepository.DeleteBooking(id);
            return NoContent();
        }
    }
}
