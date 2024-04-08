using CarsRentEF;
using CarsRentEF.Objects;
using Microsoft.EntityFrameworkCore;

namespace CLI.DAO
{
    public class RentDAO : IDAO
    {
        public static Rent? Get(int ID, VehicleAppContext appContext)
        {

            Rent? foundRent = appContext.Rents.Find(ID);
            if (foundRent == null)
            {
                Console.WriteLine("Aucune location trouvée");
            }
            return foundRent;

        }

        public static void Create(VehicleAppContext appContext)
        {

            Rent? rent = CreateRent(appContext);
            if (rent != null)
            {
                appContext.Rents.Add(rent);
                appContext.SaveChanges();
                Console.WriteLine("Location crée");
            }

        }

        public static void Update(VehicleAppContext appContext)
        {

            Rent? selectedRent = SelectRent(appContext);

            if (selectedRent == null)
            {
                return;
            }

            bool exit = false;

            while (!exit)
            {

                int choice = Helpers.CreateMenu(["ID", "NbKm", "StartDate", "EndDate", "Car", "Client", "Quitter"]);
                switch (choice)
                {
                    case 1:
                        selectedRent.ID = Helpers.PromptInt("Nouvel ID :");
                        break;
                    case 2:
                        selectedRent.NbKm = Helpers.PromptInt("Nouveau Nb KM :");
                        break;
                    case 3:
                        selectedRent.StartDate = DateOnly.FromDateTime(Helpers.PromptDateTime(label: "Nouvelle date de départ : "));
                        break;
                    case 4:
                        selectedRent.EndDate = DateOnly.FromDateTime(Helpers.PromptDateTime(label: "Nouvelle date de fin : "));
                        break;
                    case 5:
                        Car? car = CarDAO.SelectOrCreateCar(appContext);
                        if (car != null)
                        {
                            selectedRent.Car = car;
                        }
                        else
                        {
                            Console.WriteLine("Aucune voiture séléctionnée");
                        }
                        break;
                    case 6:
                        Client? client = ClientDAO.SelectOrCreateClient(appContext);
                        if (client != null)
                        {
                            selectedRent.Client = client;
                        }
                        else
                        {
                            Console.WriteLine("Aucun client séléctionné");
                        }
                        break;
                    case 7:
                        exit = true;
                        break;
                }
                appContext.SaveChanges();

            }
        }

        public static void Delete(VehicleAppContext appContext)
        {


            Rent? selectedRent = SelectRent(appContext);

            if (selectedRent == null)
            {
                Console.WriteLine("Pas de location selectionnée");
                return;
            }

            if (selectedRent != null)
            {
                appContext.Rents.Remove(selectedRent);
                appContext.SaveChanges();
                Console.WriteLine("Location supprimée");
            }


        }

        //TODO valider les dates
        public static Rent? CreateRent(VehicleAppContext appContext)
        {

            Car? car = CarDAO.SelectOrCreateCar(appContext);
            if (car == null)
            {
                return null;
            }
            Client? client = ClientDAO.SelectOrCreateClient(appContext);
            if (client == null)
            {
                return null;
            }

            Rent Rent = new()
            {
                Car = car,
                Client = client,
                StartDate = DateOnly.FromDateTime(Helpers.PromptDateTime(label: "Date de début :")),
                EndDate = DateOnly.FromDateTime(Helpers.PromptDateTime(label: "Date de fin :")),
            };
            return Rent;

        }

        public static Rent? SelectRent(VehicleAppContext appContext)
        {

            List<Rent> rents = [.. appContext.Rents.Include(rent => rent.Client).Include(rent => rent.Car)];
            Console.WriteLine("ID | NB KM | Date Debut | Date Fin | Id Client | Prenom Client | Nom Client | Id Voiture | Modele | Immat ");
            rents.ForEach(rent =>
            {
                Console.WriteLine(rent.ID + " | " + rent.NbKm + " | " + rent.StartDate + " | " + rent.EndDate + " | " + rent.Client.ID + " | " + rent.Client.FirstName + " | " + rent.Client.LastName + " | " + rent.Car.ID + " | " + rent.Car.Model + " | " + rent.Car.Immat);
            });
            int id = Helpers.PromptInt("Quelle location choisissez vous (id) ?");
            return appContext.Rents.Where(category => category.ID == id).First();

        }

        public static void ListItems(int? limit, int? start)
        {
            using (VehicleAppContext appContext = new())
            {
                Console.WriteLine("ID | NB KM | Date Debut | Date Fin | Id Client | Prenom Client | Nom Client | Id Voiture | Modele | Immat ");
                appContext.Rents.Include(rent => rent.Client).Include(rent => rent.Car).ToList().ForEach(rent =>
                {
                    Console.WriteLine(rent.ID + " | " + rent.NbKm + " | " + rent.StartDate + " | " + rent.EndDate + " | " + rent.Client.ID + " | " + rent.Client.FirstName + " | " + rent.Client.LastName + " | " + rent.Car.ID + " | " + rent.Car.Model + " | " + rent.Car.Immat);
                });
            }
        }
    }
}
