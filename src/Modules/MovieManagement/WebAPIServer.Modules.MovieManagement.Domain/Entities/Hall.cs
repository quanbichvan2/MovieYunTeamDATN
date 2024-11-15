using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Modules.MovieManagement.Domain.Entities
{
    public class Hall : BaseAuditableEntity
    {
        public string CinemaName { get; private set; } = "7 Anh Em Cinema";
        public string Name { get; set; } = default!;
        public byte TotalSeat { get; private set; }

        public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();

        public void InitializeSeats(byte seatColumn, byte seatRow, string seatTypeName, double seatTypePrice)
        {
            for (byte col = 1; col <= seatColumn; col++)
            {
                for (byte row = 1; row <= seatRow; row++)
                {
                    Seats.Add(new Seat
                    {
                        SeatRow = col,  
                        SeatColumn = row,
                        SeatTypeId = SeatTypeConstants.Regular,
                        SeatPosition = Seat.GetSeatPosition(row, col),
                        SeatTypeName = seatTypeName,
                        SeatTypePrice = seatTypePrice
					});
                }
            }
            TotalSeat = (byte)(seatColumn * seatRow);
        }
    }
}