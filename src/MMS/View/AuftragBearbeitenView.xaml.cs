using MMS.ViewModel;
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

namespace MMS.View
{
    /// <summary>
    /// Interaktionslogik für AuftragBearbeitenView.xaml
    /// </summary>
    public partial class AuftragBearbeitenView : UserControl
    {
        public AuftragBearbeitenView()
        {
            InitializeComponent();
            DataContext = new AuftragBearbeitenViewModel();
        }
    }
}
