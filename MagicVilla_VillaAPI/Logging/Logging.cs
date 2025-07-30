namespace MagicVilla_VillaAPI.Logging
{
    public class Logging : Ilogging
    {
        public void Log(string message, string type)
        {

            if(type == "error")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: " + message);
            }
            else if (type == "info")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Info: " + message);
            }
            else if (type == "warning")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Warning: " + message);
            }
            else
            {
                Console.WriteLine("Log: " + message);
            }
        }
    }
}
