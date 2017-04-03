using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WindowsCSharpFinal.ViewModels.Navigation;

namespace WindowsCSharpFinal.Views
{
    public class NavigationService : INavigationService
    {
        public void Navigate(Type page)
        {
            Frame frame = (Frame) Window.Current.Content;
            frame.Navigate(page);
        }

        public void Navigate(Type page, object obj)
        {
            Frame frame = (Frame)Window.Current.Content;
            frame.Navigate(page, obj);
        }
    }
}
