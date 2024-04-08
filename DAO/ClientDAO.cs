using CarsRentEF;
using CarsRentEF.Objects;

namespace CLI.DAO
{
    public class ClientDAO : IDAO
    {
        public static Client? Get(int ID, VehicleAppContext appContext)
        {

            Client? foundClient = appContext.Clients.Find(ID);
            if (foundClient == null)
            {
                Console.WriteLine("Le client n'a pas été trouvé");
            }
            return foundClient;

        }

        public static void Create(VehicleAppContext appContext)
        {

            Client client = CreateClient();
            appContext.Clients.Add(client);
            appContext.SaveChanges();

        }

        public static void Update(VehicleAppContext appContext)
        {

            Client? selectedClient = SelectClient(appContext);

            if (selectedClient == null)
            {
                return;
            }

            bool exit = false;

            while (!exit)
            {
                int choice = Helpers.CreateMenu(["ID", "FirstName", "LastName", "BirthDate", "Address", "City", "Zipcode", "Quitter"]);
                switch (choice)
                {
                    case 1:
                        selectedClient.ID = Helpers.PromptInt("Nouvel ID :");
                        break;
                    case 2:
                        selectedClient.FirstName = Helpers.PromptString("Nouveau Prenom :");
                        break;
                    case 3:
                        selectedClient.LastName = Helpers.PromptString("Nouveau Nom :");
                        break;
                    case 4:
                        selectedClient.BirthDate = DateOnly.FromDateTime(Helpers.PromptDateTime(label: "Nouvelle date de naissance"));
                        break;
                    case 5:
                        selectedClient.Address = Helpers.PromptString("Nouvelle adresse (rue et numero) :");
                        break;
                    case 6:
                        selectedClient.City = Helpers.PromptString("Nouvelle ville :");
                        break;
                    case 7:
                        selectedClient.Zipcode = Helpers.PromptInt("Nouveau Nom :");
                        break;
                    case 8:
                        exit = true;
                        break;
                }
                appContext.SaveChanges();
            }

        }

        public static void Delete(VehicleAppContext appContext)
        {

            Client? selectedClient = SelectClient(appContext);

            if (selectedClient == null)
            {
                return;
            }
            if (selectedClient != null)
            {
                appContext.Clients.Remove(selectedClient);
                appContext.SaveChanges();
            }


        }
        public static Client CreateClient()
        {
            Client Client = new()
            {
                FirstName = Helpers.PromptString("Prenom : "),
                LastName = Helpers.PromptString("Nom : "),
                Address = Helpers.PromptString("Numero et rue : "),
                City = Helpers.PromptString("Ville : "),
                Zipcode = Helpers.PromptInt("Code postal : "),
                BirthDate = DateOnly.FromDateTime(Helpers.PromptDateTime(label: "Date de naissance")),
            };
            return Client;
        }

        public static Client? SelectOrCreateClient(VehicleAppContext appContext)
        {

            bool exit = false;
            while (!exit)
            {
                int choice = Helpers.CreateMenu(["Selectionner un client", "Créer un client", "Retour"]);

                switch (choice)
                {
                    case 1:
                        List<Client> clients = [.. appContext.Clients];
                        Console.WriteLine("ID | Prenom | Nom | Date de naissance | Adresse | Ville | Code Postal");
                        clients.ForEach(client =>
                        {
                            Console.WriteLine(client.ID + " | " + client.FirstName + " | " + client.LastName + " | " + client.BirthDate + " | " + client.Address + " | " + client.City + " | " + client.Zipcode);
                        });
                        int id = Helpers.PromptInt("Quel client choisissez vous (id) ?");
                        return appContext.Clients.Where(client => client.ID == id).First();
                    case 2:
                        return CreateClient();
                    case 3:
                        exit = true;
                        break;
                }
            }
            return null;

        }

        public static Client SelectClient(VehicleAppContext appContext)
        {

            List<Client> clients = [.. appContext.Clients];
            Console.WriteLine("ID | Prenom | Nom | Date de naissance | Adresse | Ville | Code Postal");
            clients.ForEach(client =>
            {
                Console.WriteLine(client.ID + " | " + client.FirstName + " | " + client.LastName + " | " + client.BirthDate + " | " + client.Address + " | " + client.City + " | " + client.Zipcode);
            });
            int id = Helpers.PromptInt("Quel client choisissez vous (id) ?");
            return appContext.Clients.Where(client => client.ID == id).First();
        }

        public static void ListItems(int? limit, int? start)
        {
            using (VehicleAppContext appContext = new())
            {
                Console.WriteLine("ID | Prenom | Nom | Date de naissance | Adresse | Ville | Code Postal");
                appContext.Clients.ToList().ForEach(client =>
                {
                    Console.WriteLine(client.ID + " | " + client.FirstName + " | " + client.LastName + " | " + client.BirthDate + " | " + client.Address + " | " + client.City + " | " + client.Zipcode);
                });
            }
        }
    }
}
