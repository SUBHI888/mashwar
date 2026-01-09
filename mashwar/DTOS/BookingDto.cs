namespace mashwar.DTOS
{


    // DTO لعرض البيانات المختصرة
    public class BookingDto
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public string BookingTime { get; set; }
        public int NumberOfPeople { get; set; }
        public int TableLocation { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }
        public int PlaceId { get; set; }
    }

}


