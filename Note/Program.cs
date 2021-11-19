using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Note
{
    class Program
    {

        static readonly (string, ValidFlags)[] fields = new (string, ValidFlags)[] { 
            ("фамилию", ValidFlags.Required | ValidFlags.Leters),
            ("имя", ValidFlags.Required | ValidFlags.Leters),
            ("отчество", ValidFlags.Leters),
            ("номер телефона", ValidFlags.Required | ValidFlags.Digits),
            ("страну", ValidFlags.Required | ValidFlags.Leters),
            ("дату рождения", ValidFlags.Digits | ValidFlags.Symbols),
            ("организацию", ValidFlags.Leters),
            ("должность", ValidFlags.Leters),
            ("иные заметки", ValidFlags.Leters | ValidFlags.Digits | ValidFlags.Symbols)};

        static Dictionary<int, Note> allNotes = new Dictionary<int, Note>();

        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в записную книжку!");
            do
            {
                Console.WriteLine("Что вы хотите сделать?");
                Console.WriteLine("Доступные команды: \"Создать\", \"Редактировать\", \"Удалить\", \"Просмотреть запись\", \"Просмотреть все записи\"");
                Console.WriteLine("Чтобы выйти нажмите крестик в правом верхнем углу окна");
                Controller(Console.ReadLine());
                Console.Clear();
            } while (true);
        }

        static void Controller(string comand)
        {
            switch(comand)
            {
                case "Создать": { Create(); break;}
                case "Редактировать": { Edit(); break;}
                case "Удалить": { Delete(); break;}
                case "Просмотреть запись":{ Show(); break;}
                case "Просмотреть все записи":{ AllShow(); break;}
                default :
                    {
                        Console.WriteLine("Такой команды не существует, попробуйте ещё раз");
                        break;
                    }
            }
        }



        static void Create()
        {
            string[] input = new string[fields.Length];
            for(int i = 0; i < fields.Length; i++)
            {
                string s = Read(i);
                if (s == null) return;
                else input[i] = s;
            }
            try
            {
                allNotes.Add(Note.count, Note.Factory(input));
            }
            catch (ArgumentException) { Console.WriteLine("Ошибка создания заметки, попробуйте ещё раз"); }
        }

        static void Edit()
        {
            do
            {
                Console.Write("Ввведите ID записи: ");
                int index;
                if(int.TryParse(Console.ReadLine(), out index))
                {
                    if(allNotes.Keys.Contains(index))
                    {
                        do
                        {
                            Console.WriteLine("Выбирите параметр для редактирования");
                            Console.WriteLine("Доступные команды: \"Фамилия\", \"Имя\", \"Отчество\", \"Номер телефона\",\r\n\"Страна\", \"Дата рождения\", \"Организация\", \"Должность\", \"Иные заметки\", \"Выход\"");
                            switch (Console.ReadLine())
                            {
                                case "Фамилия": { string s = Read(0); if(s != null) allNotes[index].Surname = s; break; }
                                case "Имя": { string s = Read(1); if (s != null) allNotes[index].FirstName = s; break; }
                                case "Отчество": { string s = Read(2); if (s != null) allNotes[index].SecondName = s; break; }
                                case "Номер телефона": { string s = Read(3); if (s != null) allNotes[index].Phone = String.Format("{0:+# (###) ###-##-##}", long.Parse(s)); break; }
                                case "Страна": { string s = Read(4); if (s != null) allNotes[index].Country = s; break; }
                                case "Дата рождения": { string s = Read(5); if (s != null) allNotes[index].Birth = s; break; }
                                case "Организация": { string s = Read(6); if (s != null) allNotes[index].Organithation = s; break; }
                                case "Должность": { string s = Read(7); if (s != null) allNotes[index].Position = s; break; }
                                case "Иные заметки": { string s = Read(8); if (s != null) allNotes[index].AnotherNote = s; break; }
                                case "Выход": { return; }
                                default : { Console.WriteLine("Такой команды нет, попробуйте ещё раз"); break; }
                            }
                        } while (true);
                    }
                    else
                    {
                        Console.WriteLine("Такого ID не существует, попробуйте ещё раз");
                    }
                }
                else
                {
                    Console.WriteLine("ID - это число, попробуйте ещё раз");
                }
            } while (true);
        }

        static void Delete()
        {
            do
            {
                Console.Write("Ввведите ID записи или Выход: ");
                int index;
                string s = Console.ReadLine();
                if (int.TryParse(s, out index))
                {
                    if (allNotes.Keys.Contains(index))
                    {
                        allNotes.Remove(index);
                        Console.WriteLine("Запись удалена");
                    }
                    else
                    {
                        Console.WriteLine("Такого ID не существует, попробуйте ещё раз");
                    }
                }
                else
                {
                    if (s == "Выход") return;
                    Console.WriteLine("ID - это число, попробуйте ещё раз");
                }
            } while (true);
        }

        static void Show()
        {
            do
            {
                Console.Write("Ввведите ID записи или Выход: ");
                int index;
                string s = Console.ReadLine();
                if (int.TryParse(s, out index))
                {
                    if (allNotes.Keys.Contains(index))
                    {
                        Console.WriteLine(allNotes[index]);
                    }
                    else
                    {
                        Console.WriteLine("Такого ID не существует, попробуйте ещё раз");
                    }
                }
                else
                {
                    if (s == "Выход") return;
                    Console.WriteLine("ID - это число, попробуйте ещё раз");
                }
            } while (true);
        }

        static void AllShow()
        {
            foreach(var value in allNotes)
            {
                Console.WriteLine($"ID - {value.Key}\r\nФамилия - {value.Value.Surname}\r\nИмя - {value.Value.FirstName}\r\nНомер телефона - {value.Value.Phone}");
            }
            Console.WriteLine("Нажмите Enter, чтобы продолжить");
            Console.ReadLine();
        }

        static string Read(int i)
        {
            bool valid;
            string s;
            do
            {
                Console.Write($"Введите {fields[i].Item1}{((fields[i].Item2 & ValidFlags.Required) != 0 ? "" : ", поставьте \"-\", чтобы пропустить")} или напишите Выход: ");
                s = Console.ReadLine();
                if (s == "Выход") return null;
                valid = !Validation(s, fields[i].Item2);
                if (valid) Console.WriteLine("Введено некоректное значение");
                else break;
            } while (true);
            return s;
        }

        static bool Validation(string input, ValidFlags rules)
        {
            if (((rules & ValidFlags.Required) != 0 && input == "-") || input.Length == 0) return false;
            else
            {
                if (!((rules & ValidFlags.Leters) != 0))
                {
                    foreach (var value in input) if (char.IsLetter(value)) return false;
                }
                if (!((rules & ValidFlags.Digits) != 0))
                {
                    foreach (var value in input) if (char.IsDigit(value)) return false;
                }
                if (!((rules & ValidFlags.Symbols) != 0))
                {
                    foreach (var value in input) if (char.IsSymbol(value)) return false;
                }
                return true;
            }

        }
    }
}
