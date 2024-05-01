using nastygl_pr3_22._106.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Packaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;

namespace nastygl_pr3_22._106.Pages
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        List<Sotrudniki> sotrud;
        
        public Admin()
        {
            InitializeComponent();
            sotrud = Model1.GetContext().Sotrudniki.ToList();
            listSotrudniki.ItemsSource = sotrud;
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                int ind = listSotrudniki.SelectedIndex;

                if (ind >= 0 && ind < sotrud.Count)
                {
                    int id = sotrud[ind].ID_Sotrudnika;
                    NavigationService.Navigate(new Edit(id));
                }
            }
        }
        private string GetDoljnost(int idDoljnosti)
        {
            string doljnost = "";
            switch (idDoljnosti)
            {
                case 1:
                    doljnost = "Администратор";
                    break;
                case 2:
                    doljnost = "Портной";
                    break;
                case 3:
                    doljnost = "Дизайнер";
                    break;
                case 4:
                    doljnost = "Работник склада";
                    break;
                case 5:
                    doljnost = "Консультант";
                    break;
            }
            return doljnost;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddUser());
        }

        private void btnPoisk_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = txtbPoisk.Text.ToLower();
            var sotrudnik = sotrud.Where(s => s.Familiya.ToLower().Contains(searchQuery) || s.Imya.ToLower().Contains(searchQuery)).ToList();
            listSotrudniki.ItemsSource = sotrudnik;
        }
      

        private void btnPechat_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == true)
            {
                IDocumentPaginatorSource idp = flowDoc;
                pd.PrintDocument(idp.DocumentPaginator, Title);
            }
        }

        private void btnSklad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SkladOtchet());
        }

        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            var spisokSotrudnikov = sotrud.OrderBy(p => p.Familiya).ToList();
            var excelApp = new Excel.Application();
            excelApp.SheetsInNewWorkbook = spisokSotrudnikov.Count();
            Excel.Workbook wb = excelApp.Workbooks.Add(Type.Missing);
            Excel.Worksheet ws = excelApp.Worksheets.Add(Type.Missing);
            ws.Name = "Сотрудники";
            int startIndex = 1;
            ws.Cells[1][startIndex] = "Фамилия";
            ws.Cells[2][startIndex] = "Имя";
            ws.Cells[3][startIndex] = "Отчество";
            ws.Cells[4][startIndex] = "Возраст";
            ws.Cells[5][startIndex] = "Должность";
            ws.Cells[6][startIndex] = "Зарплата";
            ws.Cells[7][startIndex] = "Стаж";
            ws.Cells[8][startIndex] = "Почта";
            ws.Cells[9][startIndex] = "Серия паспорта";
            ws.Cells[10][startIndex] = "Номер паспорта";

            for (int i = 0; i < spisokSotrudnikov.Count; i++)
            {
                startIndex++;
                ws.Cells[1][startIndex] = spisokSotrudnikov[i].Familiya;
                ws.Cells[2][startIndex] = spisokSotrudnikov[i].Imya;
                ws.Cells[3][startIndex] = spisokSotrudnikov[i].Otchestvo;
                ws.Cells[4][startIndex] = spisokSotrudnikov[i].Vozrast;
                ws.Cells[5][startIndex] = GetDoljnost(spisokSotrudnikov[i].Doljnost);
                ws.Cells[6][startIndex] = spisokSotrudnikov[i].Zarplata;
                ws.Cells[7][startIndex] = spisokSotrudnikov[i].Stag;
                ws.Cells[8][startIndex] = spisokSotrudnikov[i].Email;
                ws.Cells[9][startIndex] = spisokSotrudnikov[i].Pasport.Seriya;
                ws.Cells[10][startIndex] = spisokSotrudnikov[i].Pasport.Nomer;
                ws.Columns.AutoFit();
            }
            excelApp.Visible = true;
        }
    }
}
