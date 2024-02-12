using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Helper
{
    public class DialogResultChangedEventArgs : EventArgs
    {
        public bool DialogResult { get; }

        public DialogResultChangedEventArgs(bool dialogResult)
        {
            DialogResult = dialogResult;
        }
    }
}
