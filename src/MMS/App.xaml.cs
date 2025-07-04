using MMS.Helpers;
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
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Rufe die Helper-Methode zum Starten der Anwendung auf
            StartupHelper.RunAbgabeCompare();
        }
    }
}
