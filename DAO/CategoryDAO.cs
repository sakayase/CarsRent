using CarsRentEF;
using CarsRentEF.Objects;

namespace CLI.DAO
{
    public class CategoryDAO : IDAO
    {
        VehicleAppContext AppContext = new();

        public static Category? Get(int ID, VehicleAppContext appContext)
        {

            Category? foundCategory = appContext.Categories.Find(ID);
            if (foundCategory == null)
            {
                Console.WriteLine("La categorie n'a pas été trouvé");
            }
            return foundCategory;

        }

        public static void Create(VehicleAppContext appContext)
        {

            Category Category = CreateCategory();
            appContext.Categories.Add(Category);

        }

        public static void Update(VehicleAppContext appContext)
        {

            Category? selectedCategory = SelectCategory(appContext);

            if (selectedCategory == null)
            {
                return;
            }

            bool exit = false;

            while (!exit)
            {
                int choice = Helpers.CreateMenu(["ID", "Name", "Prix/KM", "Quitter"]);
                switch (choice)
                {
                    case 1:
                        selectedCategory.ID = Helpers.PromptInt("Nouvel ID :");
                        break;
                    case 2:
                        selectedCategory.Name = Helpers.PromptString("Nouveau Nom :");
                        break;
                    case 3:
                        selectedCategory.KmPrice = Helpers.PromptInt("Nouveau Prix/KM :");
                        break;
                    case 4:
                        exit = true;
                        break;
                }
                appContext.SaveChanges();
            }

        }

        public static void Delete(VehicleAppContext appContext)
        {

            Category? selectedCategory = SelectCategory(appContext);

            if (selectedCategory == null)
            {
                return;
            }
            if (selectedCategory != null)
            {
                appContext.Categories.Remove(selectedCategory);
                appContext.SaveChanges();
            }

        }

        public static Category CreateCategory()
        {
            Category Category = new()
            {
                Name = Helpers.PromptString("Nom de la marque :"),
                KmPrice = Helpers.PromptInt("Prix au km :"),
            };
            return Category;
        }

        public static Category? SelectOrCreateCategory(VehicleAppContext appContext)
        {
            bool exit = false;
            while (!exit)
            {
                int choice = Helpers.CreateMenu(["Selectionner une categorie", "Créer une categorie", "Retour"]);

                switch (choice)
                {
                    case 1:
                        List<Category> categories = [.. appContext.Categories];
                        Console.WriteLine("ID | Nom | Prix/KM");
                        categories.ForEach(category =>
                        {
                            Console.WriteLine(category.ID + " | " + category.Name + " | " + category.KmPrice);
                        });
                        int id = Helpers.PromptInt("Quelle categorie choisissez vous (id) ?");
                        return appContext.Categories.Where(category => category.ID == id).First();
                    case 2:
                        return CreateCategory();
                    case 3:
                        exit = true;
                        break;
                }
            }
            return null;
        }

        public static Category? SelectCategory(VehicleAppContext appContext)
        {

            List<Category> categories = [.. appContext.Categories];
            Console.WriteLine("ID | Nom | Prix/KM");
            categories.ForEach(category =>
            {
                Console.WriteLine(category.ID + " | " + category.Name + " | " + category.KmPrice);
            });
            int id = Helpers.PromptInt("Quelle categorie choisissez vous (id) ?");
            return appContext.Categories.Where(category => category.ID == id).First();


        }

        public void ListItems(int? limit, int? start)
        {
            using (VehicleAppContext appContext = new())
            {
                Console.WriteLine("ID | Nom | Prix/KM");
                AppContext.Categories.ToList().ForEach(category =>
                {
                    Console.WriteLine(category.ID + " | " + category.Name + " | " + category.KmPrice);
                });

            }
        }
    }
}
