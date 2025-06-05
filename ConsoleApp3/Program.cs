using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DoublyLinkedListLibrary;


namespace ConsoleApp3
{
    internal class Program
    {

        enum UserMenuOptions {ShowMenu, ShowList, AddElement, RemoveElement, ChangeElement, FindFirstEntry, FindSumOnOddPositions, 
            GetNewListWithHigherElements, RemoveElementsHigherThanAverageValue, Exit};

        const int MIN_ELEMENT_VALUE = -1000000;
        const int MAX_ELEMENT_VALUE = 1000000;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Console.WriteLine("Лабораторна робота 7 - Робота з двозв'язним списком.\n");
            DoublyLinkedList doublyLinkedList = new DoublyLinkedList();

            bool isTerminate = false;
            ShowMenu();

            do
            { 
                int option = OptionChoosing();
                switch (option)
                {
                    case (int)UserMenuOptions.ShowMenu:
                        ShowMenu();
                        break;
                    case (int)UserMenuOptions.ShowList:
                        Console.WriteLine("\n--- Виведення списку ---");
                        ShowList(doublyLinkedList);
                        break;
                    case (int)UserMenuOptions.AddElement:
                        Console.WriteLine("\n--- Додавання елементу в список ---");
                        AddElementToTheList(doublyLinkedList);
                        break;
                    case (int)UserMenuOptions.RemoveElement:
                        Console.WriteLine("\n--- Видалення елементу зі списку ---");
                        RemoveElement(doublyLinkedList);
                        break;
                    case (int)UserMenuOptions.ChangeElement:
                        Console.WriteLine("\n--- Зміна значення елементу списку ---");
                        ChangeElement(doublyLinkedList);
                        break;
                    case (int)UserMenuOptions.FindFirstEntry:
                        Console.WriteLine("\n--- Пошук першого входження елементу у список ---");
                        FindFirstEntry(doublyLinkedList);
                        break;
                    case (int)UserMenuOptions.FindSumOnOddPositions:
                        Console.WriteLine("\n--- Розрахунок суми елементів на непарних позиціях ---");
                        FindSumOnOddPositions(doublyLinkedList);
                        break;
                    case (int)UserMenuOptions.GetNewListWithHigherElements:
                        Console.WriteLine("\n--- Виведення списку елементів, значення яких вище зазначеного ---");
                        GetNewListWithHigherElements(doublyLinkedList);
                        break;
                    case (int)UserMenuOptions.RemoveElementsHigherThanAverageValue:
                        Console.WriteLine("\n--- Видалення елементів списку, які вище середнього значення ---");
                        RemoveElementsHigherThanAverageValue(doublyLinkedList);
                        break;
                    case (int)UserMenuOptions.Exit:
                        isTerminate = true;
                        break;
                }
                Console.WriteLine("\n");
            } while(!isTerminate);
        }

        static void ShowMenu()
        {
            Console.WriteLine($"Оберіть дію, яку бажаєте зробити:" +
                $"\n {(int)UserMenuOptions.ShowMenu} - Вивести меню;" +  
                $"\n {(int)UserMenuOptions.ShowList} - Вивести список в консоль;" +
                $"\n {(int)UserMenuOptions.AddElement} - Додати елемент вкінець списку;" +
                $"\n {(int)UserMenuOptions.RemoveElement} - Видалити елемент зі списку;" +
                $"\n {(int)UserMenuOptions.ChangeElement} - Змінити значення елемента;" +
                $"\n {(int)UserMenuOptions.FindFirstEntry} - Знайти перше входження елементу в список;" +
                $"\n {(int)UserMenuOptions.FindSumOnOddPositions} - Знайти суму елементів на непарних позиціях;" +
                $"\n {(int)UserMenuOptions.GetNewListWithHigherElements} - Вивести список з елементів, вище заданого значення;" +
                $"\n {(int)UserMenuOptions.RemoveElementsHigherThanAverageValue} - Видалити елементи, значення яких більше за середнє;" +
                $"\n {(int)UserMenuOptions.Exit} - Завершити виконання програми.");
        }

        static int OptionChoosing()
        {
            int optionNumber = -1;
            bool isError = false, res = false;
            do {
                Console.Write("Обріть дію, яку бажаєте виконати до списку(введіть числовий номер дії): ");
                string userInput = Console.ReadLine();
                res = int.TryParse(userInput, out optionNumber);

                if (!res)
                {
                    Console.WriteLine("Помилковий ввід. Очікувалось ціле число. Повторіть спробу.");
                    isError = true;
                }
                else if (optionNumber < (int)UserMenuOptions.ShowMenu || optionNumber > (int)UserMenuOptions.Exit)
                {
                    Console.WriteLine("Помилковий ввід. Немає даної дії. Повторіть спробу.");
                    isError = true;
                }
                else
                {
                    isError = false;
                }
            }
            while( isError );
            return optionNumber;
        }

        static void ShowList(DoublyLinkedList doublyLinkedList)
        {
            if(doublyLinkedList.Length != 0)
            {
                foreach (var listElement in doublyLinkedList)
                {
                    Console.Write($"{listElement}    ");
                }
            }
            else
            {
                Console.WriteLine("Наразі список порожній");
            }
        }

        static void AddElementToTheList(DoublyLinkedList doublyLinkedList)
        {
            int number = ElementValueInput();
            doublyLinkedList.AddNode(number);
            Console.WriteLine("Елемент був вдало доданий до списку.");
        }

        static void RemoveElement(DoublyLinkedList doublyLinkedList)
        {
            if( doublyLinkedList.Length == 0)
            {
                Console.WriteLine("Список порожній.");
                return;
            }
            int index = -1;
            bool isError = false, res = false;
            do
            {
                Console.Write("Введіть індекс елемента, який потрібно видалити: ");
                string userInput = Console.ReadLine();
                res = int.TryParse(userInput, out index);
                if (!res || index < 0)
                {
                    Console.WriteLine("Помидковий ввід. Очікувалось ціле додатнє число. Повторіть спробу.");
                    isError = true;
                }

                try
                {
                    doublyLinkedList.RemoveNode(index);
                    Console.WriteLine("Елемент було успішно видалено.");
                }
                catch (IndexOutOfRangeException e) 
                {
                    Console.WriteLine("Помилковий ввід. Введений індекс не входить до коректного діапазону. Повторіть спробу.");
                    isError = true;
                }

            }
            while(isError);
        }

        static void ChangeElement(DoublyLinkedList doublyLinkedList)
        {
            if (doublyLinkedList.Length == 0)
            {
                Console.WriteLine("Список порожній.");
                return;
            }
            int index = -1;
            bool isError = false, res = false;
            do
            {
                Console.Write("Введіть індекс елемента, який потрібно змінити: ");
                string userInput = Console.ReadLine();
                res = int.TryParse(userInput, out index);
                if (!res || index < 0)
                {
                    Console.WriteLine("Помидковий ввід. Очікувалось ціле додатнє число. Повторіть спробу.");
                    isError = true;
                }
                try
                {
                    int newValue  = ElementValueInput();
                    doublyLinkedList[index] = newValue;
                    Console.WriteLine($"Значення елементу {index} було успішно змінено.");
                    isError = false;
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine("Помилковий ввід. Введений індекс не входить до коректного діапазону. Повторіть спробу.");
                    isError = true;
                }

            }
            while (isError);
        }

        static void FindFirstEntry(DoublyLinkedList doublyLinkedList)
        {
            int nuber = ElementValueInput();
            int elementIndex = doublyLinkedList.FirstEntry(nuber);
            if(elementIndex != -1)
            {
                Console.WriteLine($"Індекс першого входження: {elementIndex}");
            }
            else
            {
                Console.WriteLine($"Елементу не має у списку");
            }
        }

        static void FindSumOnOddPositions(DoublyLinkedList doublyLinkedList) 
        {
            if(doublyLinkedList.Length != 0)
            {
                Console.WriteLine($"Сума елементів, які розташовані на непарних позиціях у списку: {doublyLinkedList.FindSumOnOddPositions()}");
            }
            else
            {
                Console.WriteLine("Список порожній.");
            }
        }

        static void GetNewListWithHigherElements(DoublyLinkedList doublyLinkedList)
        {
            int number = ElementValueInput();
            DoublyLinkedList resultList = doublyLinkedList.GetNewListWithHigherElements(number);
            ShowList(resultList);
        }

        static void RemoveElementsHigherThanAverageValue(DoublyLinkedList doublyLinkedList)
        {
            doublyLinkedList.RemoveElementsHigherAverageValue();
            ShowList(doublyLinkedList);
        }

        static int ElementValueInput()
        {
            int number;
            bool isError = false, res = false;
            do
            {
                Console.Write("Введіть числове значення: ");
                string userInput = Console.ReadLine();
                res = int.TryParse(userInput, out number);
                if (!res)
                {
                    Console.WriteLine("Помидковий ввід. Очікувалось ціле число. Повторіть спробу.");
                    isError = true;
                }
                else if (number < MIN_ELEMENT_VALUE || number > MAX_ELEMENT_VALUE)
                {
                    Console.WriteLine($"Помилковий ввід. Число повинно бути в діаіпазоні [{MIN_ELEMENT_VALUE}, {MAX_ELEMENT_VALUE}]");
                    isError = true;
                }
                else
                {
                    isError = false;
                }
            }
            while (isError);
            return number;
        }
    }
}
