using Domain.Entities;
using Domain.Enums;
using Action = Domain.Enums.Action;

namespace DomainTests.Bookings
{
    public class StateMachineTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldAlwaysStartWithCreatedStatus()
        {
            var booking = new Booking();

            Assert.AreEqual(booking.CurrentStatus, Status.Created);
        }
        
        [Test]
        public void ShouldSetStatusToPaidWhenPayingForABookingWithCreatedStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Action.pay);

            Assert.AreEqual(booking.CurrentStatus, Status.Paid);
        }
        
        [Test]
        public void ShouldSetStatusToCanceledWhenCancelingForABookingCreatedStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Action.Cancel);

            Assert.AreEqual(booking.CurrentStatus, Status.Canceled);
        }
        
        [Test]
        public void ShouldSetStatusToFinishedWhenFinishinAPaidBooking()
        {
            var booking = new Booking();
            booking.ChangeState(Action.pay);
            booking.ChangeState(Action.Finish);

            Assert.AreEqual(booking.CurrentStatus, Status.Finished);
        }
        
        [Test]
        public void ShouldSetStatusToRefoundedWhenPaidABooking()
        {
            var booking = new Booking();
            booking.ChangeState(Action.pay);
            booking.ChangeState(Action.Refound);

            Assert.AreEqual(booking.CurrentStatus, Status.Refounded);
        }
        
        [Test]
        public void ShouldSetStatusToCreatedWhenReopeningACanceledBooking()
        {
            var booking = new Booking();
            booking.ChangeState(Action.Cancel);
            booking.ChangeState(Action.Reopen);

            Assert.AreEqual(booking.CurrentStatus, Status.Created);
        }
        
        
        [Test]
        public void ShouldNotChangeStatusWhenRefoundingABookingWithCreatedStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Action.pay);
            booking.ChangeState(Action.Finish);
            booking.ChangeState(Action.Refound);

            Assert.AreEqual(booking.CurrentStatus, Status.Finished);
        }
    }
}