using System.ComponentModel.DataAnnotations;

namespace CarsRentEF.Objects
{
    public class Brand : IDataObject
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
