using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MMS.db_models;
using System.Data.Entity;

namespace MMS.ViewModel
{
    public class AuftragAnlegenViewModel : ViewModelBase
    {
        private ObservableCollection<Facharbeiter> _facharbeiterList;

        public ObservableCollection<Facharbeiter> FacharbeiterList
        {
            get => _facharbeiterList;
            set
            {
                _facharbeiterList = value;
                OnPropertyChanged(nameof(FacharbeiterList));
            }
        }

        public AuftragAnlegenViewModel()
        {
            FacharbeiterList = new ObservableCollection<Facharbeiter>();
            LoadFacharbeiterAsync();
        }

        private async Task LoadFacharbeiterAsync()
        {
            using (var context = new db_connect())
            {
                var facharbeiterList = await context.Facharbeiter.ToListAsync();
                foreach (var fach in facharbeiterList)
                {
                    FacharbeiterList.Add(fach);
                }
            }
        }
    }
}