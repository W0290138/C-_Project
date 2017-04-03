using WindowsCSharpFinal.Models;
using WindowsCSharpFinal.ViewModels.Commands;
using WindowsCSharpFinal.ViewModels.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;
using Windows.UI.Core;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace WindowsCSharpFinal.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private List<Partner> allPartners;
        public ObservableCollection<Partner> Partners { get; set; } = new ObservableCollection<Partner>();
        private Partner _SelectedPartner;
        public Partner SelectedPartner
        {
            get
            {
                return _SelectedPartner;
            }
            set
            {
                _SelectedPartner = value;
                //make a bitmap of the selected partner image for the ui
                if (SelectedPartner == null)
                    SelectedPartnerImage = null;
                else
                    SelectedPartnerImage = new BitmapImage(new Uri(Constants.BASE_IMAGE_PATH + value.ImagePath));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedPartner"));
            }
        }
        private string _Filter;
        //filter by name in search bar
        public string Filter
        {
            get
            {
                return _Filter;
            }
            set
            {
                if (value == _Filter)
                {
                    return;
                }
                _Filter = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(Filter)));
                PerformFiltering();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand PlayGameCommand { get; set; }
        private ImageSource _SelectedPartnerImage;
        public ImageSource SelectedPartnerImage
        {
            get
            {
                return _SelectedPartnerImage;
            }
            set
            {
                _SelectedPartnerImage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedPartnerImage"));
            }
        }

        public MainPageViewModel(INavigationService navigationService, Type navigationPage)
        {
            //when the forward button is pressed, navigate to the game page and send the selected partner over
            PlayGameCommand = new RelayCommand((object obj) => {
                Dictionary<string, object> navigationParameters = new Dictionary<string, object>();
                navigationParameters.Add("Partner", SelectedPartner);
                navigationService.Navigate(navigationPage, navigationParameters);
            }, CanExecutePlayGame);
            //wait while the partners are retrieved from the server
            Task.Run(() => {
                allPartners = JSONLinker.Get<List<Partner>>(Constants.PARTNERS_URI).Result;
                PerformFiltering();
            }).Wait();
            //default the selected partner to the first
            SelectedPartner = allPartners[0];            
        }

        //as long as there is a selected partner
        private bool CanExecutePlayGame(object obj)
        {
            return _SelectedPartner != null;
        }

        private void PerformFiltering()
        {
            //if the filter is null, initialize it
            if (_Filter == null)
            {
                _Filter = "";
            }
            //make sure to convert to lower case because search is case insensitive
            var lowerCaseFilter = Filter.ToLowerInvariant().Trim();
            var result = allPartners.Where(
                partner => partner.Name.ToLowerInvariant()
                        .Contains(lowerCaseFilter)).ToList();
            //remove from the public list anything that doesn't match the search
            var toRemove = Partners.Except(result).ToList();
            foreach (var note in toRemove)
            {
                Partners.Remove(note);
            }
            var resultcount = result.Count;
            for (int i = 0; i < resultcount; i++)
            {
                var resultItem = result[i];
                if (i + 1 > Partners.Count || !Partners[i].Equals(resultItem))
                {
                    Partners.Insert(i, resultItem);
                }
            }
        }

    }
}
