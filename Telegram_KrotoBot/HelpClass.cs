using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram_KrotoBot
{
    public class HelpClass
    {
        public static void CaseHelpMethod()
        {
            Console.WriteLine("Команды для бота:");
            Console.WriteLine("\t/help - вывод списка команд");
            Console.WriteLine("\t/command - запустить какую-то команду");
            Console.WriteLine("\t/adduser - добавить пользователя в чёрный список");
            Console.WriteLine("\t/addword - добавить слово в бан-список");
            Console.WriteLine("\t/deleteword - удалить слово из бан-списка");
            Console.WriteLine("\t/deleteuser - удалить пользователя из чёрного списка"); 
            Console.WriteLine("\t/continue - продолжить очистку сообщений");
            Console.WriteLine("\t/stop - остановить работу бота без выхода из консоли");
            Console.WriteLine("\t/clearconsole - очистить консоль");
            Console.WriteLine("\t/exit - выйти из консоли");
            Console.WriteLine("  Ваши действия?");
        }

        public static void CaseAddUserMethod()
        {

        }
        public static void CaseAddWordMethod()
        {

        }

        public static void CaseDeleteWordMethod()
        {

        }

        public static void CaseDeleteUserMethod()
        {

        }

        public static void CaseContinueMethod()
        {

        }

        public static void CaseStopMethod()
        {
           
        }

        public static void CaseClearConsoleMethod()
        {
            Console.Clear();
        }
        public static void CaseExitMethod()
        {
            Environment.Exit(20);
        }

    }
}
