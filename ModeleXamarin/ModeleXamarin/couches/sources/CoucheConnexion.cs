namespace ModeleXamarin.couches.sources
{
    using ModeleXamarin.couches.sources.definitions.connexion;
    using Xamarin.Forms;

    /// <summary>
    /// Couche permettant la gestion de la connexion du dispositif.
    /// </summary>
    public abstract class CoucheConnexion
    {
        /// <summary>
        /// Connexion permettant de savoir si de dispositif est connecté ou non.
        /// </summary>
        private IConnexion Connexion { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classes <see cref="CoucheConnexion"/>.
        /// </summary>
        public CoucheConnexion()
        {
            this.Connexion = DependencyService.Get<IConnexion>();
        }

        /// <summary>
        /// Méthode permettant de savoir si le dispositif est connecté comme il le devrait pour l'application données.
        /// </summary>
        public abstract bool EstConnecte { get; }
    }
}
