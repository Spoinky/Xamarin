using ModeleXamarin.UWP.couches.definitions.connexion;
using Xamarin.Forms;

[assembly: Dependency(typeof(Connexion))]
namespace ModeleXamarin.UWP.couches.definitions.connexion
{
    using ModeleXamarin.couches.sources.definitions.connexion;
    using Windows.Networking.Connectivity;

    /// <summary>
    /// Classe définisant la connexion pour un dispositif windows.
    /// </summary>
    public class Connexion : AConnexion
    {
        /// <summary>
        /// Vérifie à quel type de réseau est connecté le dispositif.
        /// </summary>
        public override ETypeConnexion TypeConnexion
        {
            get
            {
                ConnectionProfile status = NetworkInformation.GetInternetConnectionProfile();

                // Checking the connection level.
                if (status == null)
                    return ETypeConnexion.NO_CONNECTION;

                NetworkConnectivityLevel level = status.GetNetworkConnectivityLevel();

                if (level == NetworkConnectivityLevel.None)
                    return ETypeConnexion.NO_CONNECTION;

                // Check if it's a wifi connection.
                if (level == NetworkConnectivityLevel.LocalAccess && !string.IsNullOrEmpty(status.WlanConnectionProfileDetails.GetConnectedSsid()))
                    return ETypeConnexion.WIFI_CONNECTION;

                // Check if it's a device connection.
                if (level == NetworkConnectivityLevel.InternetAccess)
                    return ETypeConnexion.MOBILE_CONNECTION;

                // No connection.
                return ETypeConnexion.NO_CONNECTION;
            }
        }
    }
}
