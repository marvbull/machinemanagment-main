﻿using System;
using System.Collections.ObjectModel;
using MMSLib.Model;
using System.Windows.Input;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using MMSLib.Klassen;

namespace MMS.ViewModel
{
    public class AuftragAnlegenViewModel : ViewModelBase
    {
        private Facharbeiter _selectedFacharbeiter;
        private VorgesetzterIDEingabeViewModel _vorgesetzterIDEingabeViewModel;
        private string _vorgesetztenID;



        public string VorgesetztenID
        {
            get { return _vorgesetztenID; }
            set
            {
                _vorgesetztenID = value;
                OnPropertyChanged(nameof(VorgesetztenID));
            }
        }

        public Facharbeiter SelectedFacharbeiter
        {
            get { return _selectedFacharbeiter; }
            set
            {
                _selectedFacharbeiter = value;
                OnPropertyChanged(nameof(SelectedFacharbeiter));
                FacharbeiterVorname = value?.FacharbeiterVorname;
                SelectedFacharbeiterId = value?.FacharbeiterID ?? 0;
                LoadFacharbeiterDetails();
            }
        }

        private int _selectedFacharbeiterId;
        public int SelectedFacharbeiterId
        {
            get { return _selectedFacharbeiterId; }
            set
            {
                if (SelectedFacharbeiter != null)
                {
                    SelectedFacharbeiter.FacharbeiterID = value;
                    _selectedFacharbeiterId = value;
                    OnPropertyChanged(nameof(SelectedFacharbeiterId));
                    LoadFacharbeiterDetails();
                }
            }
        }

        private int _dauerInMinuten;
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
        public string FacharbeiterVorname
        {
            get { return _facharbeiterVorname; }
            set
            {
                _facharbeiterVorname = value;
                OnPropertyChanged(nameof(FacharbeiterVorname));
            }
        }

        private Maschine _selectedMaschine;
        public Maschine SelectedMaschine
        {
            get { return _selectedMaschine; }
            set
            {
                _selectedMaschine = value;
                OnPropertyChanged(nameof(SelectedMaschine));
                MaschinenName = value?.MaschinenName;
                SelectedMaschinenId = value?.MaschinenID ?? 0;
                LoadMaschinenDetails();
            }
        }

        private string _maschinenName;
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
        public int SelectedMaschinenId
        {
            get { return _selectedMaschinenId; }
            set
            {
                if (SelectedMaschine != null)
                {
                    SelectedMaschine.MaschinenID = value;
                    _selectedMaschinenId = value;
                    OnPropertyChanged(nameof(SelectedMaschinenId));
                    LoadMaschinenDetails();
                }
            }
        }

        private DateTime _selectedStart;
        public DateTime SelectedStart
        {
            get { return _selectedStart; }
            set
            {
                if (_selectedStart != value)
                {
                    _selectedStart = value;
                    OnPropertyChanged(nameof(SelectedStart));
                }
            }
        }


        public ObservableCollection<Facharbeiter> FacharbeiterList { get; private set; }
        public ObservableCollection<Maschine> MaschinenList { get; private set; }

        public AuftragAnlegenViewModel()
        {
            FacharbeiterList = new ObservableCollection<Facharbeiter>();
            MaschinenList = new ObservableCollection<Maschine>();

            LoadFacharbeiter();
            LoadMaschinen();

            var context = new DBConnect(); // Achte darauf, den richtigen DB-Kontext zu verwenden
            _maschinenÜberschneidung = new MaschinenÜberschneidung(context);
            _naechstesDatum = new NaechstesDatum(context);
        }

        private async void LoadFacharbeiter()
        {
            using (var context = new DBConnect())
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
            using (var context = new DBConnect())
            {
                var maschinen = await context.Maschine.ToListAsync();
                foreach (var maschine in maschinen)
                {
                    MaschinenList.Add(maschine);
                }
            }
        }

        private async void LoadFacharbeiterDetails()
        {
            using (var context = new DBConnect())
            {
                var selectedFacharbeiter = await context.Facharbeiter
                    .FirstOrDefaultAsync(f => f.FacharbeiterID == SelectedFacharbeiterId);

                if (selectedFacharbeiter != null)
                {
                    FacharbeiterNachname = selectedFacharbeiter.FacharbeiterName;
                    FacharbeiterVorname = selectedFacharbeiter.FacharbeiterVorname;
                }
            }
        }

        private async void LoadMaschinenDetails()
        {
            using (var context = new DBConnect())
            {
                var selectedMaschine = await context.Maschine
                    .FirstOrDefaultAsync(m => m.MaschinenID == SelectedMaschinenId);

                if (selectedMaschine != null)
                {
                    MaschinenName = selectedMaschine.MaschinenName;
                }
            }
        }

        private string _successMessage;
        public string SuccessMessage
        {
            get { return _successMessage; }
            set
            {
                if (_successMessage != value)
                {
                    _successMessage = value;
                    OnPropertyChanged(nameof(SuccessMessage));
                }
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }


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

        private bool CanAuftragAnlegen()
        {
            return SelectedFacharbeiter != null && SelectedMaschine != null;
        }


        private MaschinenÜberschneidung _maschinenÜberschneidung;
        private NaechstesDatum _naechstesDatum;

        private async Task AuftragAnlegenAsync()
        {
            //Das nur eine Message angezeigt wird
            SuccessMessage = string.Empty;
            ErrorMessage = string.Empty;


            // Überprüfe zunächst, ob die Maschine zum ausgewählten Startzeitpunkt verfügbar ist.
            bool istVerfügbar = await _maschinenÜberschneidung.IstMaschineVerfuegbar(SelectedStart, DauerInMinuten, SelectedMaschine.MaschinenID);
            if (!istVerfügbar)
            {
                // Wenn nicht verfügbar, finde das nächste verfügbare Datum.
                var nächstesVerfügbaresDatum = await _naechstesDatum.FindeNächstesVerfügbaresZeitfenster(SelectedMaschine.MaschinenID, TimeSpan.FromMinutes(DauerInMinuten));

                // Aktualisiere die Statusmeldung, um den Nutzer über das nächste verfügbare Datum zu informieren.
                ErrorMessage = $"Die ausgewählte Maschine ist im angegebenen Zeitraum bereits belegt. Das nächstmögliche Startdatum wäre: {nächstesVerfügbaresDatum.Value.ToString("g")}";
                return; // Beende die Methode, ohne den Auftrag anzulegen.
            }

            try
            {
                using (var context = new DBConnect())
                {
                    var auftrag = new Auftraege
                    {
                        Beschreibung = Beschreibung,
                        Material = Material,
                        Abgabe = SelectedStart.AddDays(Math.Ceiling(DauerInMinuten / 480.0)),
                        Dauer = DauerInMinuten,
                        MaschinenID = SelectedMaschine.MaschinenID,
                        Beginn = SelectedStart
                    };

                    context.Auftraege.Add(auftrag);
                    await context.SaveChangesAsync();

                    var aufgabeZuweisung = new AufgabenZuweisen
                    {
                        AuftragsID = auftrag.AuftragsID,
                        FacharbeiterID = SelectedFacharbeiter.FacharbeiterID,
                    };

                    context.AufgabenZuweisen.Add(aufgabeZuweisung);
                    await context.SaveChangesAsync();

                    SuccessMessage = "Auftrag erfolgreich angelegt!";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Anlegen des Auftrags: {ex.Message}");
                ErrorMessage = "Fehler beim Anlegen des Auftrags!";
            }
        }
    }
}