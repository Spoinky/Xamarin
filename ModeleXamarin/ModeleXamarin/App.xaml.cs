namespace ModeleXamarin
{
    using ModeleXamarin.vues.patrouilleur;
    using Xamarin.Forms;

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new ModeleXamarin.MainPage();
            NavigationPage navigation = new NavigationPage(new Identification());

            navigation.BarTextColor = new Color(255, 255, 255);
            navigation.BarBackgroundColor = Color.FromHex("#04B431");
            navigation.Icon = "./images/cg77/logo_header.jpg";

            this.MainPage = navigation;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
