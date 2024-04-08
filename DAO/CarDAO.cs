using CarsRentEF;
using CarsRentEF.Objects;
using Microsoft.EntityFrameworkCore;

namespace CLI.DAO
{
    public class CarDAO : IDAO
    {

        public static Car? Get(int ID, VehicleAppContext appContext)
        {
            Car? foundCar = appContext.Cars.Find(ID);
            if (foundCar == null)
            {
                Console.WriteLine("Le vehicule n'a pas été trouvé");
            }
            return foundCar;
        }

        public static void Create(VehicleAppContext appContext)
        {
            Car? car = CreateCar(appContext);
            if (car != null)
            {
                appContext.Cars.Add(car);
                appContext.SaveChanges();
                Console.WriteLine("Car created");
            }
        }

        //TODO
        public static void Update(VehicleAppContext appContext)
        {

            Car? selectedCar = SelectCar(appContext);

            if (selectedCar == null)
            {
                return;
            }

            bool exit = false;

            while (!exit)
            {
                int choice = Helpers.CreateMenu(["ID", "Immat", "Model", "Color", "Brand", "Category", "Quitter"]);
                switch (choice)
                {
                    case 1:
                        selectedCar.ID = Helpers.PromptInt("Nouvel ID :");
                        break;
                    case 2:
                        selectedCar.Immat = Helpers.PromptString("Nouvelle immatriculation :");
                        break;
                    case 3:
                        selectedCar.Model = Helpers.PromptString("Nouveau Modele :");
                        break;
                    case 4:
                        selectedCar.Color = Helpers.PromptString("Nouvelle Couleur :");
                        break;
                    case 5:
                        Brand? brand = BrandDAO.SelectOrCreateBrand(appContext);
                        if (brand != null)
                        {
                            selectedCar.Brand = brand;
                        }
                        else
                        {
                            Console.WriteLine("Aucune marque séléctionnée");
                        }
                        break;
                    case 6:
                        Category? category = CategoryDAO.SelectOrCreateCategory(appContext);
                        if (category != null)
                        {
                            selectedCar.Category = category;
                        }
                        else
                        {
                            Console.WriteLine("Aucune categorie séléctionné");
                        }
                        break;
                    case 7:
                        exit = true;
                        break;
                }
                appContext.SaveChanges();
            }
        }

        public static void Remove(VehicleAppContext appContext)
        {
            Car? selectedCar = SelectCar(appContext);

            if (selectedCar == null)
            {
                return;
            }

            if (selectedCar != null)
            {
                appContext.Cars.Remove(selectedCar);
                appContext.SaveChanges();
            }
        }

        public static Car? CreateCar(VehicleAppContext appContext)
        {

            Brand? brand = BrandDAO.SelectOrCreateBrand(appContext);
            if (brand == null)
            {
                return null;
            }
            Category? category = CategoryDAO.SelectOrCreateCategory(appContext);
            if (category == null)
            {
                return null;
            }

            Car Car = new()
            {
                Immat = Helpers.PromptString("Immatriculation du vehicule : "),
                Model = Helpers.PromptString("Modele du vehicule : "),
                Color = Helpers.PromptString("Couleur du vehicule : "),
                Brand = brand,
                Category = category,
            };
            return Car;

        }

        public static Car? SelectOrCreateCar(VehicleAppContext appContext)
        {
            bool exit = false;
            while (!exit)
            {
                int choice = Helpers.CreateMenu(["Selectionner une voiture", "Créer une voiture", "Retour"]);

                switch (choice)
                {
                    case 1:
                        List<Car> cars = [.. appContext.Cars.Include(car => car.Brand).Include(car => car.Category)];
                        Console.WriteLine("ID | Marque | Modele | Couleur | Categorie | Immatriculation");
                        cars.ForEach(car =>
                        {
                            Console.WriteLine(car.ID + " | " + car.Brand.Name + " | " + car.Model + " | " + car.Color + " | " + car.Category.Name + " | " + car.Immat);
                        });
                        int id = Helpers.PromptInt("Quelle voiture choisissez vous (id) ?");
                        return appContext.Cars.Where(car => car.ID == id).First();
                    case 2:
                        return CreateCar(appContext);
                    case 3:
                        exit = true;
                        break;
                }
            }
            return null;
        }

        public static Car SelectCar(VehicleAppContext appContext)
        {
            List<Car> cars = [.. appContext.Cars.Include(car => car.Brand).Include(car => car.Category)];
            Console.WriteLine("ID | Marque | Modele | Couleur | Categorie | Immatriculation");
            cars.ForEach(car =>
            {
                Console.WriteLine(car.ID + " | " + car.Brand.Name + " | " + car.Model + " | " + car.Color + " | " + car.Category.Name + " | " + car.Immat);
            });
            int id = Helpers.PromptInt("Quelle voiture choisissez vous (id) ?");
            return appContext.Cars.Where(car => car.ID == id).First();
        }

        public static void ListItems(int? limit, int? start)
        {
            using (VehicleAppContext appContext = new())
            {
                Console.WriteLine("ID | Marque | Modele | Couleur | Categorie | Immatriculation");
                appContext.Cars.Include(car => car.Brand).Include(car => car.Category).ToList().ForEach(car =>
                {
                    Console.WriteLine(car.ID + " | " + car.Brand.Name + " | " + car.Model + " | " + car.Color + " | " + car.Category.Name + " | " + car.Immat);
                });
            }
        }
    }
}
