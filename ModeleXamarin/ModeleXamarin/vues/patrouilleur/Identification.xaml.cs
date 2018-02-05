namespace ModeleXamarin.vues.patrouilleur
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Page d'identification de l'application Patrouilleur du Conseil départemental de Seine-et-Marne.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Identification : ContentPage
	{
        /// <summary>
        /// Intialise une instance de la classe <see cref="Identification"/>.
        /// </summary>
        public Identification ()
		{
			InitializeComponent ();
        }
    }
}