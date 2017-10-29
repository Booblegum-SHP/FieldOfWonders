using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication
{
    internal class Program
    {
        public static void Main()
        {
            List<string> tasksList = new List<string>() {"КУКУРУЗА", "АНАНАС", "ГРИБ"};
            int attempts = 5;
            Random rnd = new Random();
            string task = tasksList[rnd.Next(0, tasksList.Count)];
            char[] taskState = new char[task.Length];
            taskState = taskState.Select(i => '*').ToArray();
            Console.Clear();
            while (true)
            {
                Console.WriteLine("-------------------------------- \n" +
                                  "* Ваше задание: {0}\n" +
                                  "--------------------------------", new string(taskState));
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
            }
        }
    }
}