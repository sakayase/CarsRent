
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsRentEF.Objects
{
    public class Rent : IDataObject
    {
        [Key]
        public int ID { get; set; }
        public int NbKm { get; set; }
        public DateOnly StartDate { get; set; } = new DateOnly();
        public DateOnly EndDate { get; set; } = new DateOnly();
        public int CarID { get; set; }
        [ForeignKey("CarID")]
        public Car Car { get; set; } = new Car();
        public int ClientID { get; set; }
        [ForeignKey("ClientID")]
        public Client Client { get; set; } = new Client();
    }
}
