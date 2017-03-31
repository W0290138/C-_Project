using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsCSharpFinal.ViewModels.Navigation
{
    //http://stackoverflow.com/questions/26816264/page-navigation-using-mvvm-in-store-app
    interface INavigationService
    {
        void Navigate(Type page);
        void Navigate(Type page, object obj);
    }
}
