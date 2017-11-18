using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FieldOfWonders
{
    internal class Program
    {
        private static string Roll()
        {
            List<string> rollValues = new List<string>() {"200", "400", " Б ", " + ", "100"};
            Random rnd = new Random();
            int rollNumber = rnd.Next(1, rollValues.Count);
            string rollResult = rollValues[rollNumber];
            for (int i = 0; i <= rollNumber + 20; i++)
            {
                int y = Console.CursorTop;
                Console.SetCursorPosition(0, y);
                Console.Write("| {0,3} |", rollValues[i % rollValues.Count]);
                Thread.Sleep(200);
            }
            Console.WriteLine(" --> Result = {0}", rollResult);
//            Console.ReadKey();
            return rollResult;
        }

        public static void Main()
        {
            List<string> tasksList = new List<string>() {"КУКУРУЗА", "АНАНАС", "ГРИБ"};
            int attempts = 5; // Попытки
            int points = 0;
            Random rnd = new Random();
            string task = tasksList[rnd.Next(0, tasksList.Count)];
            char[] taskState = new char[task.Length];
            taskState = taskState.Select(i => '*').ToArray();
            Console.Clear();
            while (true)
            {
                Console.WriteLine("-------------------------------- \n" +
                                  "* Ваше задание: {0}\n" +
                                  "* Набрано     : {1} очков \n" +
                                  "--------------------------------", new string(taskState), points);
                Console.Write("Крутите барабан!(Press EnyKey...)");
                Console.ReadKey(true);
                Console.WriteLine();
                string rollResult = Roll();
                if (int.TryParse(rollResult, out int currentPoint))
                {
                    Console.WriteLine("У вас выпало {0} очков", currentPoint);
                }
                else
                {
                    Console.WriteLine("Other");
                }
                Console.Write("Ваша буква: ");
                char choiceLetter = Char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();
                if (task.IndexOf(choiceLetter) != -1)
                {
                    Console.WriteLine("Есть такая буква!");
                    for (int i = 0; i < task.Length; i++)
                    {
                        if (task[i] == choiceLetter)
                        {
                            taskState[i] = choiceLetter;
                            points += currentPoint;
                        }
                    }
                    bool checkEnd = true;
                    foreach (char letter in taskState)
                    {
                        if (letter == '*') checkEnd = false;
                    }
                    if (checkEnd)
                    {
                        Console.WriteLine("Поздравляем! Вы угадали слово и выиграли а-а-а-а-в-томобиль!!!");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Сожалеем, такой буквы нет -( ");
                    Console.WriteLine("У вас осталось {0} попыток", --attempts);
                }
                if (attempts == 0)
                {
                    Console.WriteLine("Вам не удалось отгадать слово.");
                    break;
                }
                Console.Write("(Press EnyKey)");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}