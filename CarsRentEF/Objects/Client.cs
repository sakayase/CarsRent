using System.ComponentModel.DataAnnotations;

namespace CarsRentEF.Objects
{
    public class Client : IDataObject
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
        public string Address { get; set; } = string.Empty;
        public int Zipcode { get; set; } 
        public string City { get; set; } = string.Empty;
        public ICollection<Rent> Rents { get; set; } = new List<Rent>();
    }
}
