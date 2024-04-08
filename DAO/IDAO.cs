using CarsRentEF;

namespace CLI.DAO
{
    internal interface IDAO
    {

        //TODO virtual => abstract 
        virtual static void Create(VehicleAppContext appContext) { }
        virtual static void Delete(VehicleAppContext appContext) { }
        virtual static void Update(VehicleAppContext appContext) { }
        virtual static void Get(VehicleAppContext appContext) { }
        virtual static void ListItems(int? limit = null, int? start = null) { }
    }
}
