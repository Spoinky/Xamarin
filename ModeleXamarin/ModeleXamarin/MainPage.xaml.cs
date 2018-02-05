namespace ModeleXamarin
{
    using ModeleXamarin.vues.patrouilleur;
    using System;
    using Xamarin.Forms;

    public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        public async void VaAPatrouilleur(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            await Navigation.PushAsync(new Identification());
        }
    }
}
