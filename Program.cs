using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FieldOfWonders
{
    internal class Program
    {
        private static string Roll()
        /*
         * Вращение барабана
         */
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
            return rollResult;
        }

        public static void Main()
        {
            List<string> names = new List<string> {"Иван", "Алексей", "Кирилл"};
            List<int> points = new List<int>() {0, 0, 0};
            List<int> successWords = new List<int> {0, 0, 0};

            int turn = 0; // Чей сейчас ход

            List<string> tasksList = new List<string> {"КУКУРУЗА", "АНАНАС", "ГРИБ"};
            List<string> tasksDescription = new List<string> // Подсказки к заданиям
            {
                "В этих желтых пирамидках Сотни зерен аппетитных.",
                "Далеко на юге где-то. Он растет зимой и летом. Удивит собою нас ",
                "Стоял на крепкой ножке, теперь лежит в лукошке."
            };
            Random rnd = new Random();
            int numTask = rnd.Next(0, tasksList.Count); // Номер текущего задания
            string task = tasksList[numTask]; // Текущее задание
            char[] taskState = new char[task.Length]; // Состояние задания (сколько букв угадано)

            // Заполняем задание звездочками *****
            taskState = taskState.Select(i => '*').ToArray();
            Console.Clear();

            Console.WriteLine("Задание на игру: ");
            Console.WriteLine("\"{0}\"", tasksDescription[numTask]);
            // Main LOOP
            while (true)
            {
                Console.WriteLine("-------------------------------- \n" +
                                  "* Ходит игрок         <<{0}>>   \n" +
                                  "* СЛОВО: {1}                     \n" +
                                  "* Набрано              {2} очков \n" +
                                  "--------------------------------", names[turn], new string(taskState), points[turn]);
                Console.WriteLine("{0}, что будете делать?", names[turn]);
                Console.WriteLine("1.Крутить барабан!");
                Console.WriteLine("2.Напомнить задание");
                Console.WriteLine("3.Назвать слово");
                int choice = int.Parse(Console.ReadKey().KeyChar.ToString());
                Console.WriteLine();
                if (choice == 2)
                {
                    Console.WriteLine("Задание на игру: ");
                    Console.WriteLine("\"{0}\"", tasksDescription[numTask]);
                    Console.Write("А теперь вращайте барабан! ");
                    Console.Write("(Press EnyKey)");
                    Console.ReadKey();
                }
                else if (choice == 3)
                {
                    Console.Write("Назовите слово: ");
                    string word = Console.ReadLine();
                    if (word.ToUpper() == task)
                    {
                        Console.WriteLine("Поздравляем! Вы угадали слово и выиграли а-а-а-а-втомобиль!!!");
                        break;
                    }

                    Console.WriteLine("Увы. Вы неправильно назвали слово");
                    Console.Write("Переход хода");
                    turn = ++turn % names.Count;
                    Console.Write("(Press EnyKey)");
                    Console.ReadKey();
                    Console.Clear();
                }
                // Вращаем Барабан
                string rollResult = Roll();
                Console.WriteLine("RollResult {0}", rollResult);
                int currentPoint;
                if (int.TryParse(rollResult, out currentPoint)) // Если выпал цифровой сектор
                {
                    Console.WriteLine("У вас выпало {0} очков", currentPoint);
                }
                else // если выпал не цифровой сектор
                {
                    if (rollResult == " Б ")
                    {
                        Console.WriteLine("Увы. У вас Банкрот. Ваши очки сгорели.");
                        Console.Write("Переход хода");
                        turn = ++turn % names.Count;
                        Console.Write("(Press EnyKey)");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }
                    if (rollResult == " + ")
                    {
                        // Реализуйте самостоятельно
                        Console.WriteLine("Сектор + еще не реализован...");
                    }
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
                            points[turn] += currentPoint;
                        }
                    }
                    // Проверяем, остались ли неотгаданные буквы
                    bool checkEnd = true;
                    foreach (char letter in taskState)
                    {
                        if (letter == '*') checkEnd = false;
                    }
                    if (checkEnd)
                    {
                        Console.WriteLine("{0}, поздравляем! Вы угадали слово и выиграли а-а-а-а-втомобиль!!!", names[turn]);
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Сожалеем, такой буквы нет -( ");
                    Console.WriteLine("Переход хода");
                    turn = ++turn % names.Count;
                }
                Console.Write("(Press EnyKey)");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}