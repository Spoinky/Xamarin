using ModeleXamarin.iOS.couches.definitions.connexion;
using Xamarin.Forms;

[assembly: Dependency(typeof(Connexion))]
namespace ModeleXamarin.iOS.couches.definitions.connexion
{
    using ModeleXamarin.couches.sources.definitions.connexion;

    /// <summary>
    /// Classe définisant la connexion pour un dispositif IOs.
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
                NetworkStatus status = Reachability.InternetConnectionStatus();

                // Vérificatrion si c'est une connexion internet.
                if (status == NetworkStatus.ReachableViaCarrierDataNetwork)
                    return ETypeConnexion.MOBILE_CONNECTION;

                // Vérificatrion si c'est une connexion wifi.
                if (status == NetworkStatus.ReachableViaWiFiNetwork)
                    return ETypeConnexion.WIFI_CONNECTION;

                // No connection.
                return ETypeConnexion.NO_CONNECTION;
            }
        }
    }
}