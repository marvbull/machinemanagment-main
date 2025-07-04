using MMSLib.Klassen; //AbgabeCompare
using MMSLib.Model; //DBConnect
using System.Threading.Tasks;

namespace MMS.Helpers
{
    public static class StartupHelper
    {
        public static void RunAbgabeCompare()
        {
            var context = new DBConnect(); // Erstelle eine Instanz eines DB-Kontexts
            var abgabeCompare = new AbgabeCompare(context); // Instanz von AbgabeCompare

            // Ausführen von LoescheAbgelaufeneAuftraegeAsync ohne den UI-Thread zu blockieren
            Task.Run(() => abgabeCompare.LoescheAbgelaufeneAuftraegeAsync()).ConfigureAwait(false);
        }
    }
}