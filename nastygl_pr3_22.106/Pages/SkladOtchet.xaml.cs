using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace nastygl_pr3_22._106.Pages
{
    /// <summary>
    /// Логика взаимодействия для SkladOtchet.xaml
    /// </summary>
    public partial class SkladOtchet : Page
    {
        public SkladOtchet()
        {
            InitializeComponent();
            PopulateListView();
        }

        public class SkladInfo
        {
            public int ID_Sklada { get; set; }
            public int ID_Modeli { get; set; }
            public int Kolvo_Na_Sklade { get; set; }
            public DateTime Data_Postupleniya { get; set; }
            public string Nazvanie_Modeni { get; set; }
        }

        public List<SkladInfo> GetSkladInfo()
        {
            List<SkladInfo> skladInfoList = new List<SkladInfo>();

            string connectionString = "Data Source=PSVAGR;Initial Catalog=atelie;Integrated Security=True";
            string query = "SELECT s.ID_Sklada, s.ID_Modeli, s.Kolvo_Na_Sklade, s.Data_Postupleniya, m.Nazvanie_Modeni " +
                          "FROM Sklad s " +
                          "INNER JOIN Modeli m ON s.ID_Modeli = m.ID_Modeli";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SkladInfo skladInfo = new SkladInfo
                    {
                        ID_Sklada = reader.GetInt32(0),
                        ID_Modeli = reader.GetInt32(1),
                        Kolvo_Na_Sklade = reader.GetInt32(2),
                        Data_Postupleniya = reader.GetDateTime(3),
                        Nazvanie_Modeni = reader.GetString(4)
                    };
                    skladInfoList.Add(skladInfo);
                }

                reader.Close();
            }

            return skladInfoList;
        }

        private void PopulateListView()
        {
            List<SkladInfo> skladInfoList = GetSkladInfo();

            skladListView.ItemsSource = skladInfoList;
        }
        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == true)
            {
                IDocumentPaginatorSource idp = flowDoc;
                pd.PrintDocument(idp.DocumentPaginator, Title);
            }
        }
    }

}
