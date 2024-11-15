using System.ComponentModel.DataAnnotations.Schema;
using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Modules.MovieManagement.Domain.Entities
{
    public class Seat : BaseAuditableEntity
    {
        public string SeatPosition { get; set; } = default!;
        public byte SeatRow { get; set; }
        public byte SeatColumn { get; set; }

        [ForeignKey("SeatType")]
        public Guid SeatTypeId { get; set; }
        public string SeatTypeName { get; set; } = default!;
        public double SeatTypePrice { get; set; }

        [ForeignKey("Hall")]
        public Guid HallId { get; set; }
        public Hall Hall { get; set; } = default!;

        /// <summary>
        ///   Cột sẽ bắt đầu với 1, nên cần chuyển đổi nó sang bảng chữ cái
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns>GetSeatName(1, 3) sẽ trả về "A03"</returns>
        public static string GetSeatPosition(byte row, byte col)
        {
            char rowLetter = (char)('A' + row - 1);
            return $"{rowLetter}{col:D2}";
        }
    }
}
