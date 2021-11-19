using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note
{
    class Note
    {

        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string Birth { get; set; }
        public string Organithation { get; set; }
        public string Position { get; set; }
        public string AnotherNote { get; set; }

        public static int count = 1;

        private Note()
        {
            count++;
        }

        public static Note Factory(string[] array)
        {
            if (array.Length < 9) throw new ArgumentException();
            Note note = new Note();
            try
            {
                note.Surname = array[0];
                note.FirstName = array[1];
                note.SecondName = array[2] == "-" ? null : array[2];
                note.Phone = String.Format("{0:+# (###) ###-##-##}", long.Parse(array[3]));
                note.Country = array[4];
                note.Birth = array[5] == "-" ? null : array[5];
                note.Organithation = array[6] == "-" ? null : array[6];
                note.Position = array[7] == "-" ? null : array[7];
                note.AnotherNote = array[8] == "-" ? null : array[8];
            }
            catch { throw new ArgumentException(); }
            return note;
        }

        public override string ToString()
        {
            return $"Фамилия: {Surname}\r\n" +
                $"Имя: {FirstName}\r\n" +
                $"Отчество: {SecondName}\r\n" +
                $"Телефон: {Phone}\r\n" +
                $"Страна: {Country}\r\n" +
                $"День рождения: {Birth}\r\n" +
                $"Организация: {Organithation}\r\n" +
                $"Дожность: {Position}\r\n" +
                $"Коментарий: {AnotherNote}";
        }
    }
}
