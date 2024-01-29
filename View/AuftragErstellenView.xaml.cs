using System;
using System.Windows;
using MMS.db_models; // Stellen Sie sicher, dass Sie den richtigen Namespace für Ihre Datenbankmodelle verwenden

namespace MMS.Views
{
    public partial class AuftragErstellenView : Window
    {
        public AuftragErstellenView()
        {
            InitializeComponent();
        }

        private void ErstellenButton_Click(object sender, RoutedEventArgs e)
        {
            // einlesen der vom benutzer eigegebenen werden
            string beschreibung = BeschreibungTextBox.Text; // Beispiel für ein Textfeld
            int maschinenId = Convert.ToInt32(MaschinenIdTextBox.Text); // Beispiel für eine Textbox, die eine Maschinen-ID enthält
            string material = MaterialTextBox.Text; // Beispiel für ein Textfeld
            DateTime abgabeDatum = DateTime.Now.AddDays(14); // Beispiel: Datum in 2 Wochen
            int dauer = Convert.ToInt32(DauerTextBox.Text); // Beispiel für ein Textfeld

            // erstellen der instanz ("datenpaar") und zugewiesen mit eintrag vom benutzer
            Auftraege newAuftrag = new Auftraege 
            {
                Beschreibung = beschreibung,
                Maschinen_ID = maschinenId,
                Material = material,
                Abgabe = abgabeDatum,
                Dauer = dauer
            };

            // hochladen auf die Datenbank
            using (var dbContext = new db_connect()) //datenbank standart connection
            {
                try
                {
                    dbContext.Auftraege.Add(newAuftrag);
                    dbContext.SaveChanges();
                    MessageBox.Show("Auftrag erfolgreich erstellt!");
                }
                catch (Exception ex) //exception für bessere kontrolle
                {
                    MessageBox.Show($"Fehler beim Erstellen des Auftrags:\n{ex.Message}");
                }
            }
        }
    }
}