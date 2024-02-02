using System.Collections.ObjectModel;
using MMSLib.db_models;
using System.Windows.Input;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace MMS.ViewModel
{
    // Abschnitt: Properties für die Benutzeroberfläche
    public class AuftragAnlegenViewModel : ViewModelBase
    {
        // Abschnitt: Auswahl von Facharbeitern
        private Facharbeiter _selectedFacharbeiter;

        public Facharbeiter SelectedFacharbeiter
        {
            get { return _selectedFacharbeiter; }
            set
            {
                _selectedFacharbeiter = value;
                OnPropertyChanged(nameof(SelectedFacharbeiter));

                // Aktualisieren Sie die Eigenschaften FacharbeiterVorname und SelectedFacharbeiterId
                FacharbeiterVorname = value?.Facharbeiter_Vorname;
                SelectedFacharbeiterId = value?.Facharbeiter_ID ?? 0;
                LoadFacharbeiterDetails();
            }
        }

        private int _dauerInMinuten;

        // Abschnitt: Dauer des Auftrags
        public int DauerInMinuten
        {
            get { return _dauerInMinuten; }
            set
            {
                _dauerInMinuten = value;
                OnPropertyChanged(nameof(DauerInMinuten));
            }
        }

        private string _beschreibung;

        // Abschnitt: Beschreibung des Auftrags
        public string Beschreibung
        {
            get { return _beschreibung; }
            set
            {
                _beschreibung = value;
                OnPropertyChanged(nameof(Beschreibung));
            }
        }

        private string _material;

        // Abschnitt: Material für den Auftrag
        public string Material
        {
            get { return _material; }
            set
            {
                _material = value;
                OnPropertyChanged(nameof(Material));
            }
        }

        private string _facharbeiterNachname;

        // Abschnitt: Nachname des ausgewählten Facharbeiters
        public string FacharbeiterNachname
        {
            get { return _facharbeiterNachname; }
            set
            {
                _facharbeiterNachname = value;
                OnPropertyChanged(nameof(FacharbeiterNachname));
            }
        }

        private string _facharbeiterVorname;

        // Abschnitt: Vorname des ausgewählten Facharbeiters
        public string FacharbeiterVorname
        {
            get { return _facharbeiterVorname; }
            set
            {
                _facharbeiterVorname = value;
                OnPropertyChanged(nameof(FacharbeiterVorname));
            }
        }

        private int _selectedFacharbeiterId;

        // Abschnitt: ID des ausgewählten Facharbeiters
        public int SelectedFacharbeiterId
        {
            get { return _selectedFacharbeiterId; }
            set
            {
                // Weisen Sie die ID direkt dem ausgewählten Facharbeiter zu (falls vorhanden)
                if (SelectedFacharbeiter != null)
                {
                    SelectedFacharbeiter.Facharbeiter_ID = value;
                    _selectedFacharbeiterId = value;
                    OnPropertyChanged(nameof(SelectedFacharbeiterId));

                    // Laden Sie die Details für den ausgewählten Facharbeiter mit der neuen ID
                    LoadFacharbeiterDetails();
                }
            }
        }

        private Maschine _selectedMaschine;

        // Abschnitt: Auswahl von Maschinen
        public Maschine SelectedMaschine
        {
            get { return _selectedMaschine; }
            set
            {
                _selectedMaschine = value;
                OnPropertyChanged(nameof(SelectedMaschine));

                // Aktualisieren Sie die Eigenschaften MaschinenName und SelectedMaschinenId
                MaschinenName = value?.Maschinen_Name;
                SelectedMaschinenId = value?.Maschinen_ID ?? 0;
                LoadMaschinenDetails();
            }
        }

        private string _maschinenName;

        // Abschnitt: Name der ausgewählten Maschine
        public string MaschinenName
        {
            get { return _maschinenName; }
            set
            {
                _maschinenName = value;
                OnPropertyChanged(nameof(MaschinenName));
            }
        }

        private int _selectedMaschinenId;

        // Abschnitt: ID der ausgewählten Maschine
        public int SelectedMaschinenId
        {
            get { return _selectedMaschinenId; }
            set
            {
                // Weisen Sie die ID direkt der ausgewählten Maschine zu (falls vorhanden)
                if (SelectedMaschine != null)
                {
                    SelectedMaschine.Maschinen_ID = value;
                    _selectedMaschinenId = value;
                    OnPropertyChanged(nameof(SelectedMaschinenId));

                    // Laden Sie die Details für die ausgewählte Maschine mit der neuen ID
                    LoadMaschinenDetails();
                }
            }
        }

        public ObservableCollection<Facharbeiter> FacharbeiterList { get; private set; }

        // Abschnitt: Liste von Facharbeitern
        public ObservableCollection<Maschine> MaschinenList { get; private set; }

        // Abschnitt: Konstruktor und Initialisierungsmethoden
        public AuftragAnlegenViewModel()
        {
            FacharbeiterList = new ObservableCollection<Facharbeiter>();
            MaschinenList = new ObservableCollection<Maschine>();

            LoadFacharbeiter();
            LoadMaschinen();
        }

        // Abschnitt: Methoden zum Laden von Facharbeitern und Maschinen
        private async void LoadFacharbeiter()
        {
            using (var context = new db_connect())
            {
                var facharbeiter = await context.Facharbeiter.ToListAsync();
                foreach (var fach in facharbeiter)
                {
                    FacharbeiterList.Add(fach);
                }
            }
        }

        private async void LoadMaschinen()
        {
            using (var context = new db_connect())
            {
                var maschinen = await context.Maschine.ToListAsync();
                foreach (var maschine in maschinen)
                {
                    MaschinenList.Add(maschine);
                }
            }
        }

        // Abschnitt: Methoden zum Laden von Details zu Facharbeitern und Maschinen
        private async void LoadFacharbeiterDetails()
        {
            using (var context = new db_connect())
            {
                var selectedFacharbeiter = await context.Facharbeiter
                    .FirstOrDefaultAsync(f => f.Facharbeiter_ID == SelectedFacharbeiterId);

                if (selectedFacharbeiter != null)
                {
                    FacharbeiterNachname = selectedFacharbeiter.Facharbeiter_Name;
                    FacharbeiterVorname = selectedFacharbeiter.Facharbeiter_Vorname;
                    // Weitere Eigenschaften aktualisieren, wenn erforderlich
                }
            }
        }

        private async void LoadMaschinenDetails()
        {
            using (var context = new db_connect())
            {
                var selectedMaschine = await context.Maschine
                    .FirstOrDefaultAsync(m => m.Maschinen_ID == SelectedMaschinenId);

                if (selectedMaschine != null)
                {
                    MaschinenName = selectedMaschine.Maschinen_Name;
                    // Weitere Eigenschaften aktualisieren, wenn erforderlich
                }
            }
        }

        private string _statusMessage;

        // Abschnitt: Statusmeldung
        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }

        // Abschnitt: Command für den Auftrag anlegen
        private ViewModelCommand _auftragAnlegenCommand;

        public ICommand AuftragAnlegenCommand
        {
            get
            {
                if (_auftragAnlegenCommand == null)
                {
                    _auftragAnlegenCommand = new ViewModelCommand(
                        async param => await AuftragAnlegenAsync(),
                        param => CanAuftragAnlegen()
                    );
                }
                return _auftragAnlegenCommand;
            }
        }

        // Abschnitt: Überprüfung, ob ein Auftrag angelegt werden kann
        private bool CanAuftragAnlegen()
        {
            // Hier kannst du überprüfen, ob alle notwendigen Daten vorhanden sind, um einen Auftrag anzulegen
            return SelectedFacharbeiter != null && SelectedMaschine != null;
        }

        // Abschnitt: Asynchrone Methode zum Anlegen eines Auftrags
        private async Task AuftragAnlegenAsync()
        {
            try
            {
                using (var context = new db_connect())
                {
                    // Erstelle eine neue Instanz von Auftraege und setze die Eigenschaften
                    var auftrag = new Auftraege
                    {
                        Beschreibung = Beschreibung,
                        Material = Material,
                        Abgabe = DateTime.Now, // Hier musst du das tatsächliche Abgabedatum setzen
                        Dauer = DauerInMinuten,
                        Maschinen_ID = SelectedMaschine.Maschinen_ID
                        // Andere Eigenschaften, die du setzen möchtest
                    };

                    // Füge den Auftrag zur Tabelle Auftraege hinzu
                    context.Auftraege.Add(auftrag);
                    await context.SaveChangesAsync();

                    // Erstelle eine neue Instanz von Aufgabe_Zuweisung und setze die Eigenschaften
                    var aufgabeZuweisung = new Aufgabe_Zuweisung
                    {
                        Auftrags_ID = auftrag.Auftrags_ID,
                        Facharbeiter_ID = SelectedFacharbeiter.Facharbeiter_ID,
                        // Andere Eigenschaften, die du setzen möchtest
                    };

                    // Füge die Zuweisung zur Tabelle Aufgabe_Zuweisung hinzu
                    context.Aufgabe_Zuweisung.Add(aufgabeZuweisung);
                    await context.SaveChangesAsync();

                    // Setze die Statusmeldung für die Bestätigung
                    StatusMessage = "Auftrag erfolgreich angelegt!";
                }
            }
            catch (Exception ex)
            {
                // Hier kannst du die Ausnahme behandeln oder Debug-Informationen ausgeben
                Console.WriteLine($"Fehler beim Anlegen des Auftrags: {ex.Message}");

                // Setze die Statusmeldung für Fehler
                StatusMessage = "Fehler beim Anlegen des Auftrags!";
            }
        }
    }
}
