using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WindowsCSharpFinal.ViewModels;
using WindowsCSharpFinal.ViewModels.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsCSharpFinal.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameView : Page
    {
        public GameView()
        {
            //add an action to the suspending event to save our stuff before a close
            Application.Current.Suspending += OnSuspension;
            this.InitializeComponent();
        }

        private void OnSuspension(object sender, SuspendingEventArgs e)
        {
            //execute the back/save command in our view model
            GameViewModel gameViewModel = (GameViewModel)this.DataContext;
            gameViewModel.BackCommand.Execute(this);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //make our view model in the navigation method so we can get our selected partner from the main menu
            this.DataContext = new GameViewModel(new NavigationService(), typeof(MainPageView), (Dictionary<string, object>) e.Parameter);
            base.OnNavigatedTo(e);
        }

    }
}
