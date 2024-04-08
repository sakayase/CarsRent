namespace CLI
{
    internal class Helpers
    {
        /// <summary>
        /// Demande à l'utilisateur de rentrer un string dans la console avec possibilité de valider la valeur
        /// </summary>
        /// <param name="label"></param>
        /// <param name="validator">Fonction de validation</param>
        /// <returns></returns>
        public static string PromptString(string label = "String ?", Func<string, bool>? validator = null)
        {
            Console.WriteLine(label);
            string input = "";
            bool isValid = false;
            while (!isValid)
            {
                input = Console.ReadLine() ?? "";
                if (validator != null)
                {
                    isValid = validator(input);
                }
                else
                {
                    isValid = true;
                }
            }
            return input;
        }

        /// <summary>
        /// Demande à l'utilisateur de rentrer un int dans la console avec possibilité de valider la valeur
        /// </summary>
        /// <param name="label"></param>
        /// <param name="validator">Fonction de validation</param>
        /// <returns></returns>
        public static int PromptInt(string label = "Entier ?", Func<int, bool>? validator = null)
        {
            bool hasParsed = false;
            int num = 0;
            string input;
            while (!hasParsed)
            {
                input = PromptString(label);
                hasParsed = int.TryParse(input, out num);
                if (validator != null)
                {
                    hasParsed = validator(num);
                }
            }
            return num;
        }

        /// <summary>
        /// Demande à l'utilisateur de rentrer un int dans la console avec possibilité de valider la valeur (avec le nb d'elements du menu)
        /// </summary>
        /// <param name="label"></param>
        /// <param name="validator">Fonction de validation</param>
        /// <param name="menus">Elements du menu</param>
        /// <returns></returns>
        public static int PromptIntMenu(List<string> menus, string label = "Valeur ?", Func<int, List<string>, bool>? validator = null)
        {
            bool hasParsed = false;
            int num = 0;
            string input;
            while (!hasParsed)
            {
                input = PromptString(label);
                hasParsed = int.TryParse(input, out num);
                if (validator != null)
                {
                    hasParsed = validator(num, menus);
                }
            }
            return num;
        }

        /// <summary>
        /// Demande à l'utilisateur de rentrer un bool
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public static bool PromptBool(string label = "bool ?")
        {
            while (true)
            {
                string input = PromptString(label).ToLower();
                if (input == "y" || input == "oui" || input == "o")
                {
                    return true;
                }
                else if (input == "n" || input == "non" || input == "no")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("La valeur doit etre \"y\" ou \"oui\" ou \"o\" ou \"n\" ou \"non\" ou \"no\"");
                }
            }
        }

        /// <summary>
        /// Crée un menu avec la liste de string, chaque element est une valeur du menu, retourne le numéro (affiché donc n+1 pour un array) du menu séléctionné.
        /// </summary>
        /// <param name="menu">Liste des elements du menu</param>
        /// <param name="label"></param>
        /// <returns>Numéro (int) du menu séléctionné</returns>
        public static int CreateMenu(List<string> menus, string label = "Quelle action ?")
        {
            Console.WriteLine("------------------------");
            Console.WriteLine(label);
            Console.WriteLine("");
            for (int i = 0; i < menus.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {menus[i]}");
            }
            Console.WriteLine("");
            return PromptIntMenu(menus, "Entrez la valeur", Validators.ValidateMenu);

        }
        /// <summary>
        /// Demande une date et optionnelement un temps à l'utilisateur
        /// </summary>
        /// <param name="time">si true demande un temps</param>
        /// <param name="label">label à afficher</param>
        /// <returns></returns>
        public static DateTime PromptDateTime(bool time = false, string? label = null)
        {
            DateTime date;
            if (label != null)
            {
                Console.WriteLine(label!);
            }
            int year = PromptInt(label: "Année :", Validators.ValidateYear);
            int month = PromptInt(label: "Mois :", Validators.ValidateMonth);
            int day = PromptInt(label: "Jour :", Validators.ValidateDay);
            if (time)
            {
                int hour = PromptInt(label: "Heure :");
                int minute = PromptInt(label: "Minute :");
                int second = PromptInt(label: "Seconde :");

                date = new DateTime(year: year, month: month, day: day, hour: hour, minute: minute, second: second);
                return date;
            }
            else
            {
                date = new DateTime(year: year, month: month, day: day);
                return date;
            }
        }


    }
}
