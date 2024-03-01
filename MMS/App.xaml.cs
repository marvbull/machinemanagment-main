using MMSLib.Klassen;
using MMSLib.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MMS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var context = new DBConnect(); // Erstelle eine Instanz eines DB-Kontexts
            var abgabeCompare = new AbgabeCompare(context); // instanz von abagbecompare

            Task.Run(() => abgabeCompare.LoescheAbgelaufeneAuftraegeAsync()).ConfigureAwait(false); //ausführen von abgabecompare
        }
    }
}
