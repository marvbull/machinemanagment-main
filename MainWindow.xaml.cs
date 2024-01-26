using MMS.db_models;
using System;
using System.Collections.Generic;
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

namespace MMS
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TestDatabaseConnection();
        }

        private void TestDatabaseConnection()
        {
            using (var dbContext = new db_connect()) // Verwenden Sie den richtigen Namen Ihrer DbContext-Klasse
            {
                try
                {
                    // Versuchen Sie, eine einfache Abfrage auf der Datenbank auszuführen
                    var facharbeiterCount = dbContext.Facharbeiter.Count();
                    MessageBox.Show($"Verbindung zur Datenbank erfolgreich!\nAnzahl der Facharbeiter: {facharbeiterCount}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler bei der Datenbankverbindung:\n{ex.Message}");
                }
            }
        }
    }
}