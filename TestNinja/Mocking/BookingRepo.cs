using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    public interface IBookingRepo
    {
        IQueryable<Booking> GetActiveBookings(int? execludedBookingId = null);
    }

    public class BookingRepo : IBookingRepo
    {
        public IQueryable<Booking> GetActiveBookings(int? execludedBookingId = null)
        {
            var unitOfWork = new UnitOfWork();
            var bookings =
                unitOfWork.Query<Booking>()
                    .Where(b =>
                        b.Status != "Cancelled");

            if (execludedBookingId.HasValue)
                bookings = bookings.Where(b => b.Id == execludedBookingId.Value);

            return bookings;
        }
    }
}
