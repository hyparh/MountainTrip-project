namespace MountainTrip.Services.Bookings
{
    public class BookingServiceModel
    {       
        public int Id { get; init; }
       
        public string Time { get; set; }
        
        public string UserId { get; set; }
        
        public byte PeopleCount { get; set; }
    }
}
