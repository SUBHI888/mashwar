using mashwar.DTOS;
using mashwar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mashwar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingApiController(AppDbContext context)
        {
            _context = context;
        }

        // -------------------- GET All --------------------
        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _context.Bookings
                .AsNoTracking()
                .Select(b => new BookingDto
                {
                    Id = b.Id,
                    BookingDate = b.BookingDate,
                    BookingTime = b.BookingTime.ToString(),
                    NumberOfPeople = b.NumberOfPeople,
                    TableLocation = (int)b.TableLocation,
                    Status = (int)b.Status,
                    UserId = b.UserId,
                    PlaceId = b.PlaceId
                })
                .ToListAsync();

            return Ok(bookings);
        }

        // -------------------- GET By ID --------------------
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var booking = await _context.Bookings
                .AsNoTracking()
                .Where(b => b.Id == id)
                .Select(b => new BookingDto
                {
                    Id = b.Id,
                    BookingDate = b.BookingDate,
                    BookingTime = b.BookingTime.ToString(),
                    NumberOfPeople = b.NumberOfPeople,
                    TableLocation = (int)b.TableLocation,
                    Status = (int)b.Status,
                    UserId = b.UserId,
                    PlaceId = b.PlaceId
                })
                .FirstOrDefaultAsync();

            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        // -------------------- POST Create --------------------
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingDto dto)
        {
            var booking = new Booking
            {
                BookingDate = dto.BookingDate,
                BookingTime = TimeSpan.Parse(dto.BookingTime),
                NumberOfPeople = dto.NumberOfPeople,
                TableLocation = (TableLocation)dto.TableLocation,
                Status = BookingStatus.Confirmed,
                UserId = dto.UserId,
                PlaceId = dto.PlaceId
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            dto.Id = booking.Id;
            dto.Status = (int)booking.Status;

            return CreatedAtAction(nameof(GetBookingById), new { id = booking.Id }, dto);
        }


        // -------------------- PUT Update --------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] Booking booking)
        {
            if (id != booking.Id)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Bookings.Any(b => b.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // -------------------- DELETE --------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return NotFound();

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // -------------------- PATCH Cancel --------------------
        [HttpPatch("cancel/{id}")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return NotFound();

            booking.Status = BookingStatus.Cancelled;
            await _context.SaveChangesAsync();

            return Ok(new BookingDto
            {
                Id = booking.Id,
                BookingDate = booking.BookingDate,
                BookingTime = booking.BookingTime.ToString(),
                NumberOfPeople = booking.NumberOfPeople,
                TableLocation = (int)booking.TableLocation,
                Status = (int)booking.Status,
                UserId = booking.UserId,
                PlaceId = booking.PlaceId
            });
        }
    }
}
