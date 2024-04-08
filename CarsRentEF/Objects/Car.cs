using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsRentEF.Objects
{
    public class Car : IDataObject
    {
        [Key]
        public int ID { get; set; }
        public string Immat { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int BrandID { get; set; }
        [ForeignKey("BrandID")]
        public Brand Brand { get; set; } = new Brand();
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public Category Category { get; set; } = new Category();
    }
}
