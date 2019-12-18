using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class BookingHelperTests_OverlappingBookingsExists
    {
        private Booking _existingBooking;
        private Mock<IBookingRepo> _repo;
        [SetUp]
        public void Setup()
        {
            _existingBooking = new Booking
            {
                Id = 1,
                ArrivalDate = ArriveOn(2017, 1, 15),
                DepartureDate = DeparteOn(2017, 1, 20),
                Reference = "a"
            };

            _repo = new Mock<IBookingRepo>();

            _repo.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>
            {
                _existingBooking
            }.AsQueryable()); ;
        }

        [Test]
        public void BookingStartsAndEndsBeforeExistingBooking_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                DepartureDate = Before(_existingBooking.ArrivalDate),
            }, _repo.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingStartsBeforeExistingBookingAndEndsInMiddleOfExistingBooking_ReturnExistingBookingsRefernce()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                DepartureDate = After(_existingBooking.ArrivalDate),
            }, _repo.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsBeforeExistingBookingAndEndsAfterExistingBooking_ReturnExistingBookingsRefernce()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),
            }, _repo.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsAndEndsInMiddleOfExistingBooking_ReturnExistingBookingsRefernce()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = Before(_existingBooking.DepartureDate),
            }, _repo.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsAndEndsAfterExistingBooking_ReturnExistingBookingsRefernce()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.DepartureDate, days: 1),
                DepartureDate = After(_existingBooking.DepartureDate, days: 3),
            }, _repo.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingsOverlapsButExistingBookingCancelled_ReturnExistingBookingsRefernce()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = Before(_existingBooking.DepartureDate),
                Status = "Cancelled",
            }, _repo.Object);

            Assert.That(result, Is.Empty);
        }

        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }

        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime DeparteOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }
    }
}