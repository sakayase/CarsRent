using CarsRentEF;
using CarsRentEF.Objects;

namespace CLI.DAO
{
    public class BrandDAO : IDAO
    {

        public static Brand? Get(int ID, VehicleAppContext appContext)
        {
            Brand? foundBrand = appContext.Brands.Find(ID);
            if (foundBrand == null)
            {
                Console.WriteLine("La marque n'a pas été trouvé");
            }
            return foundBrand;
        }

        public static void Create(VehicleAppContext appContext)
        {

            Brand Brand = CreateBrand();
            appContext.Brands.Add(Brand);
            appContext.SaveChanges();

        }

        public static void Update(VehicleAppContext appContext)
        {

            Brand? selectedBrand = SelectBrand(appContext);

            if (selectedBrand == null)
            {
                return;
            }

            bool exit = false;

            while (!exit)
            {
                int choice = Helpers.CreateMenu(["ID", "Name", "LastName", "BirthDate", "Address", "City", "Zipcode", "Quitter"]);
                switch (choice)
                {
                    case 1:
                        selectedBrand.ID = Helpers.PromptInt("Nouvel ID :");
                        break;
                    case 2:
                        selectedBrand.Name = Helpers.PromptString("Nouveau Nom :");
                        break;
                    case 3:
                        exit = true;
                        break;
                }
                appContext.SaveChanges();
            }

        }

        public static void Delete(VehicleAppContext appContext)
        {

            //TODO
            Brand? selectedBrand = SelectBrand(appContext);

            if (selectedBrand == null)
            {
                return;
            }
            if (selectedBrand != null)
            {
                appContext.Brands.Remove(selectedBrand);
                appContext.SaveChanges();
            }


        }

        public static Brand CreateBrand()
        {
            Brand Brand = new()
            {
                Name = Helpers.PromptString("Nom de la marque :"),
            };
            return Brand;
        }

        public static Brand? SelectOrCreateBrand(VehicleAppContext appContext)
        {

            bool exit = false;
            while (!exit)
            {
                int choice = Helpers.CreateMenu(["Selectionner une marque", "Créer une marque", "Retour"]);

                switch (choice)
                {
                    case 1:
                        List<Brand> brands = [.. appContext.Brands];
                        Console.WriteLine("ID | Nom");
                        brands.ForEach(brand =>
                        {
                            Console.WriteLine(brand.ID + " | " + brand.Name);
                        });
                        int id = Helpers.PromptInt("Quelle marque choisissez vous (id) ?");
                        return appContext.Brands.Where(brand => brand.ID == id).First();
                    case 2:
                        return CreateBrand();
                    case 3:
                        exit = true;
                        break;
                }
            }
            return null;


        }

        public static Brand? SelectBrand(VehicleAppContext appContext)
        {
            List<Brand> brands = [.. appContext.Brands];
            Console.WriteLine("ID | Nom");
            brands.ForEach(brand =>
            {
                Console.WriteLine(brand.ID + " | " + brand.Name);
            });
            int id = Helpers.PromptInt("Quelle marque choisissez vous (id) ?");
            return appContext.Brands.Where(brand => brand.ID == id).First();

        }

        public static void ListItems(int? limit, int? start)
        {
            using (VehicleAppContext appContext = new())
            {
                Console.WriteLine("ID | Nom");
                appContext.Brands.ToList().ForEach(brand =>
                {
                    Console.WriteLine(brand.ID + " | " + brand.Name);
                });

            }
        }
    }
}
