using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace Altre.AppData
{
    internal class Reports
    {
        public static void Employees()
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application()
            {
                Visible = true,
                SheetsInNewWorkbook = 1
            };
            Microsoft.Office.Interop.Excel.Workbook work = app.Workbooks.Add(Type.Missing);
            app.DisplayAlerts = false;
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)app.Worksheets.get_Item(1);
            sheet.Name = "pomogite";

            // Заголовки столбцов
            sheet.Cells[1, 1] = "Фамилия";
            sheet.Cells[1, 2] = "Имя";
            sheet.Cells[1, 3] = "Отчество";
            sheet.Cells[1, 4] = "Должность";
            sheet.Cells[1, 5] = "Отдел";
            sheet.Cells[1, 6] = "Год рождения";
            sheet.Cells[1, 7] = "Пол";
            sheet.Cells[1, 8] = "ИНН";
            sheet.Cells[1, 9] = "СНИЛС";
            sheet.Cells[1, 10] = "Номер телефона";
            sheet.Cells[1, 11] = "Почта";
            sheet.Cells[1, 12] = "Дата найма";

            // Заполнение данных
            var currentRow = 2;
            foreach (var employee in ConnectionDB.GetCont().Employee)
            {
                var position = ConnectionDB.GetCont().Positions.FirstOrDefault(y => y.position_id == employee.employee_id);
                var gndr = ConnectionDB.GetCont().Gndr.FirstOrDefault(z => z.gndr_id == employee.gndr_id);
                var divison = ConnectionDB.GetCont().Departments.FirstOrDefault(x => x.department_id == position.department_id);

                sheet.Cells[currentRow, 1] = employee.last_name;
                sheet.Cells[currentRow, 2] = employee.first_name;
                sheet.Cells[currentRow, 3] = employee.last_name;
                sheet.Cells[currentRow, 4] = position.position_name;
                sheet.Cells[currentRow, 5] = divison.department_name;
                sheet.Cells[currentRow, 6] = employee.birthday;
                sheet.Cells[currentRow, 7] = gndr.gndr_name;
                sheet.Cells[currentRow, 8] = employee.inn;
                sheet.Cells[currentRow, 9] = employee.snils;
                sheet.Cells[currentRow, 10] = employee.phone_number;
                sheet.Cells[currentRow, 11] = employee.email;
                sheet.Cells[currentRow, 12] = employee.employment_date;

                currentRow++;
            }

            // Форматирование
            Microsoft.Office.Interop.Excel.Range rang = sheet.get_Range("A1", "H" + (currentRow - 1).ToString()); // Изменили "F12" на динамическое значение
            rang.Cells.Font.Name = "Times New Roman";
            rang.Font.Size = 14;
            rang.Font.Bold = true;
        }
    }
}
