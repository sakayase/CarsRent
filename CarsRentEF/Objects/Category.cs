using System.ComponentModel.DataAnnotations;

namespace CarsRentEF.Objects
{
    public class Category : IDataObject
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public double KmPrice { get; set; }
        public ICollection<Car> Cars { get; set; } = new List<Car>();

    }
}
