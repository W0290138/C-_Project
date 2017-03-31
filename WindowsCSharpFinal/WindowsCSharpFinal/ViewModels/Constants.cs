namespace WindowsCSharpFinal.ViewModels
{
    internal static class Constants
    {
        //resources
        internal const string BASE_IMAGE_PATH = "ms-appx:///Images/";
        internal const string USER_URI = "http://localhost:3000/User";
        internal const string PARTNERS_URI = "http://localhost:3000/Partners";
        //text responses
        internal const string DEFAULT_TEXT = "Hey! I'm so happy to see you <3";
        internal const string GIFT_TEXT = "Thanks for the gift!";
        internal const string JOB_TEXT = "Oh yeah! Work it for me (;";
        internal const string UNHAPPY_TEXT = "I don't know how I'm feeling about us lately...";
        internal const string HOPELESS_TEXT = "I really don't think things have been working out between us.";
        internal const string CANNOT_AFFORD_TEXT = "You Are Too Broke To Afford That For Me ):< Better Work Some More!";
        //settings
        internal const int JOB_WAGE = 150;
        internal const int HAPPINESS_DECREASE_SPEED = 600;
        internal const int HAPPINESS_DESCREASE_AMOUNT = 1;
        internal const int GIFT_COST = 100;
        internal const int JOB_DISABLED_DELAY = 5000;
        internal const int GIFT_HAPPINESS_INCREASE = 10;
        internal const int UNHAPPINESS_TRIGGER_LEVEL = 50;
        internal const int HOPELESSNESS_TRIGGER_LVEL = 20;
    }
}