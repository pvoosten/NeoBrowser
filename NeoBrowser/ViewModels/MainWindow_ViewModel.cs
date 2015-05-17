using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBrowser.ViewModels
{
    public class MainWindow_ViewModel : ViewModelBase
    {
        public MainWindow_ViewModel()
        {
            Browser = new Browser_ViewModel();
        }


        public Browser_ViewModel Browser { get; private set; }

    }
}
