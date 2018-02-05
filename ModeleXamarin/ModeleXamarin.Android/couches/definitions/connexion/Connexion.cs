using ModeleXamarin.Droid.couches.definitions.connexion;
using Xamarin.Forms;

[assembly: Dependency(typeof(Connexion))]
namespace ModeleXamarin.Droid.couches.definitions.connexion
{
    using Android.Net;
    using Android.Content;
    using ModeleXamarin.couches.sources.definitions.connexion;

    /// <summary>
    /// Classe définisant la connexion pour un dispositif Android.
    /// </summary>
    public class Connexion : AConnexion
    {
        /// <summary>
        /// Vérifie à quel type de réseau est connecté le dispositif.
        /// </summary>
        //public override ETypeConnexion TypeConnexion
        //{
        //    get
        //    {
        //        return ETypeConnexion.NO_CONNECTION;
        //    }
        //}

        public override ETypeConnexion TypeConnexion
        {
            get
            {
                ConnectivityManager manager = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
                NetworkInfo networkinfo = manager.ActiveNetworkInfo;

                if (networkinfo == null)
                    return ETypeConnexion.NO_CONNECTION;

                // Check if it's a wifi connection.
                if (networkinfo.Type == ConnectivityType.Wifi)
                    return ETypeConnexion.WIFI_CONNECTION;

                //Check if it's a device connection.
                else if (networkinfo.Type == ConnectivityType.Mobile)
                    return ETypeConnexion.MOBILE_CONNECTION;

                // No connection.
                return ETypeConnexion.NO_CONNECTION;
            }
        }
    }
}