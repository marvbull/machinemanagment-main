using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MMS.View;
using MMSLib.Klassen;

namespace MMS.ViewModel
{
    public class AuftragBearbeitenViewModel : ViewModelBase
    {
        public ObservableCollection<dynamic> AuftraegeMitFacharbeitern { get; private set; } = new ObservableCollection<dynamic>();

        public ICommand LoeschenCommand { get; private set; }

        public AuftragBearbeitenViewModel()
        {
            LoadData();
            LoeschenCommand = new ViewModelCommand(param => this.OeffneAuftragLoeschenPopup());
        }
        private void LoadData()
        {
            var auftragBearbeiten = new AuftragBearbeiten(); // Stellen Sie sicher, dass AuftragBearbeiten Zugriff auf die DB hat
            var data = auftragBearbeiten.GetAuftraegeMitFacharbeitern(); // Diese Methode muss angepasst werden

            foreach (var item in data)
            {
                AuftraegeMitFacharbeitern.Add(item);
            }
        }


        private void OeffneAuftragLoeschenPopup()
        {
            var popupViewModel = new AuftragLoeschenViewModel();
            var popupView = new AuftragLoeschenPopupView()
            {
                DataContext = popupViewModel
            };

            popupView.ShowDialog();
        }
    }

}
