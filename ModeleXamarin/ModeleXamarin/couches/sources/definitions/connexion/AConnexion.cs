namespace ModeleXamarin.couches.sources.definitions.connexion
{
    /// <summary>
    /// Classe permettant de vérifier la connexion d'un dispositif à un réseau.
    /// </summary>
    public abstract class AConnexion : IConnexion
    {
        /// <summary>
        /// Vérifie si le dispositif est connecté ou non à un réseau.
        /// </summary>
        public bool IsConnected
        {
            get
            {
                if (ETypeConnexion.NO_CONNECTION == this.TypeConnexion)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// Vérifie à quel type de réseau est connecté le dispositif.
        /// </summary>
        public abstract ETypeConnexion TypeConnexion { get; }
    }
}
