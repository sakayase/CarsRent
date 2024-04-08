using CarsRentEF;
using CarsRentEF.Objects;
using CLI;
using CLI.DAO;

VehicleAppContext vehicleAppContext = new VehicleAppContext();

void main()
{
    bool exit = false;

    while (!exit)
    {
        int choice = Helpers.CreateMenu(["Vehicules", "Clients", "Locations", "Categories", "Marques", "Quitter"]);
        switch (choice)
        {
            case 1:
                Console.WriteLine("Vehicules");
                SelectType<Car>();
                break;
            case 2:
                Console.WriteLine("Clients");
                SelectType<Client>();
                break;
            case 3:
                Console.WriteLine("Locations");
                SelectType<Rent>();
                break;
            case 4:
                Console.WriteLine("Categories");
                SelectType<Category>();
                break;
            case 5:
                Console.WriteLine("Marques");
                SelectType<Brand>();
                break;
            case 6:
                exit = true;
                break;
        }
    }
}

void SelectType<T>() where T : IDataObject
{
    Console.WriteLine(typeof(T));
    if (typeof(T) == typeof(Car))
    {
        CarDAO DAO = new();
        CRUDMenu<CarDAO>(DAO);
    }
    else if (typeof(T) == typeof(Client))
    {
        ClientDAO DAO = new();
        CRUDMenu<ClientDAO>(DAO);
    }
    else if (typeof(T) == typeof(Rent))
    {
        RentDAO DAO = new();
        CRUDMenu<RentDAO>(DAO);
    }
    else if (typeof(T) == typeof(Category))
    {
        CategoryDAO DAO = new();
        CRUDMenu<CategoryDAO>(DAO);
    }
    else if (typeof(T) == typeof(Brand))
    {
        BrandDAO DAO = new();
        CRUDMenu<BrandDAO>(DAO);
    }
    else
    {
        Console.WriteLine("Erreur selection type");
    }
}

void CRUDMenu<T>(T DAO)
    where T : IDAO
{
    bool exit = false;

    while (!exit)
    {
        VehicleAppContext appContext = new();
        int choice = Helpers.CreateMenu(["Lister", "Créer", "Modifier", "Supprimer", "Retour"]);
        switch (choice)
        {
            case 1:
                T.ListItems();
                break;
            case 2:
                T.Create(appContext);
                break;
            case 3:
                T.Update(appContext);
                break;
            case 4:
                T.Delete(appContext);
                break;
            case 5:
                exit = true;
                break;
        }
    }
}


main();