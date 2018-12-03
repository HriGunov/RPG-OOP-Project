using Models.Interfaces;
using Console = System.Console;

namespace Models
{
    public class ConsoleLogger : Ilogger
    {
        public void Log(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
