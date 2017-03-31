using WindowsCSharpFinal.Models;
using WindowsCSharpFinal.ViewModels.Commands;
using WindowsCSharpFinal.ViewModels.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading;
using Windows.UI.Xaml;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace WindowsCSharpFinal.ViewModels
{
    class GameViewModel : INotifyPropertyChanged
    {
        private User _User;
        public User User
        {
            get
            {
                return _User;
            }
            set
            {
                _User = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("User"));
            }
        }
        private Partner _Partner;
        public Partner Partner
        {
            get
            {
                return _Partner;
            }
            set
            {
                _Partner = value;
                PartnerImage = new BitmapImage(new Uri(Constants.BASE_IMAGE_PATH + value.ImagePath));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Partner"));
            }
        }
        private DispatcherTimer jobTimer;
        private DispatcherTimer happinessTimer;
        public bool _JobButtonIsEnabled = true;
        public bool JobButtonIsEnabled
        {
            get
            {
                return _JobButtonIsEnabled;
            }
            set
            {
                _JobButtonIsEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("JobButtonIsEnabled"));
            }
        }
        public ICommand JobCommand { get; set; }
        public ICommand GiftCommand { get; set; }
        public ICommand BackCommand { get; set; }
        private string _PartnerText;
        public string PartnerText
        {
            get
            {
                return _PartnerText;
            }
            set
            {
                _PartnerText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PartnerText"));
            }
        }
        private ImageSource _PartnerImage;
        public ImageSource PartnerImage
        {
            get
            {
                return _PartnerImage;
            }
            set
            {
                _PartnerImage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PartnerImage"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        //the game view model needs navigation info for it's back button and parameters to receive the selected partner
        public GameViewModel(INavigationService navigationService, Type navigationPage, Dictionary<string, object> parameters)
        {
            //get the selected partner
            Partner = (Partner) parameters["Partner"];
            //synchronously wait on the GET request for the user
            Task.Run(() => {
                User = JSONLinker.Get<User>(Constants.USER_URI).Result;
            }).Wait();
            //the back command writes the updated user and partner to the server asynchronously
            BackCommand = new RelayCommand(async (object obj) => {
                await JSONLinker.Put(User, Constants.USER_URI);
                await JSONLinker.Put(Partner, Constants.PARTNERS_URI + "/" + Partner.Id);
                //call the navigation service to take you back to the main menu
                navigationService.Navigate(navigationPage);
            });
            //assign the other commands to their actions
            JobCommand = new RelayCommand(ExecuteJobCommand);
            GiftCommand = new RelayCommand(ExecuteGiftCommand);
            //make a couple timers for the happiness depletion and job disabled delay
            happinessTimer = new DispatcherTimer();
            happinessTimer.Tick += AdjustHappiness;
            happinessTimer.Interval = TimeSpan.FromMilliseconds(Constants.HAPPINESS_DECREASE_SPEED);
            happinessTimer.Start();
            jobTimer = new DispatcherTimer();
            jobTimer.Tick += JobButtonReset;
            jobTimer.Interval = TimeSpan.FromMilliseconds(Constants.JOB_DISABLED_DELAY);
            jobTimer.Start();
            //set your partner's default text
            PartnerText = Constants.DEFAULT_TEXT;
        }

        //reset the job button after the specified interval elapses
        private void JobButtonReset(object sender, object e)
        {
            if (!JobButtonIsEnabled)
                JobButtonIsEnabled = true;
            jobTimer.Stop();
        }

        //deplete some happiness from your partner at specified intervals
        private void AdjustHappiness(object sender, object e)
        {
            Partner.Happiness -= Constants.HAPPINESS_DESCREASE_AMOUNT;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Partner"));
            //check how unhappy your partner is and respond
            if (Partner.Happiness < Constants.HOPELESSNESS_TRIGGER_LVEL)
                PartnerText = Constants.HOPELESS_TEXT;
            else if (Partner.Happiness < Constants.UNHAPPINESS_TRIGGER_LEVEL)
                PartnerText = Constants.UNHAPPY_TEXT;
        }

        //take money when a gift is bought
        private void ExecuteGiftCommand(object obj)
        {
            //if you have enough money, buy it
            if (User.Money - Constants.GIFT_COST >= 0)
            {
                User.Money -= Constants.GIFT_COST;
                Partner.Happiness += Constants.GIFT_HAPPINESS_INCREASE;
                PartnerText = Constants.GIFT_TEXT;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Partner"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("User"));
            }
            //else your partner complains about your broke ass
            else
            {
                PartnerText = Constants.CANNOT_AFFORD_TEXT;
            }
        }

        //do your job, freeloader
        private void ExecuteJobCommand(object obj)
        {
            User.Money += Constants.JOB_WAGE;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("User"));
            JobButtonIsEnabled = false;
            jobTimer.Start();
            PartnerText = Constants.JOB_TEXT;
        }
    }
}
