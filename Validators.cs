namespace CLI
{
    internal class Validators
    {
        public static bool ValidateMenu(int menu, List<string> listMenu)
        {
            if (menu <= 0 || menu >= listMenu.Count + 1)
            {
                Console.WriteLine($"Le numéro choisi doit etre entre 1 et {listMenu.Count}");
                return false;
            }
            return true;
        }

        public static bool ValidateYear(int num)
        {
            if ((num > 0) && (num < 9000))
            {
                return true;
            }
            return false;
        }

        public static bool ValidateMonth(int num)
        {
            if ((num > 0) && (num < 13))
            {
                return true;
            }
            return false;
        }

        public static bool ValidateDay(int num)
        {
            if ((num > 0) && (num < 32))
            {
                return true;
            }
            return false;
        }
    }
}
