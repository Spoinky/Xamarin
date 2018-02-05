namespace ModeleXamarin.couches.api.cg77
{
    using ModeleXamarin.couches.sources;

    /// <summary>
    /// Couche de connexion pour les applications du Conseil Départemental de Seine et marne.
    /// </summary>
    public class CG77CoucheConnexion : CoucheConnexion
    {
        /// <summary>
        /// Méthode permettant de savoir si le dispositif est connecté comme il le devrait pour l'application données.
        /// </summary>
        public override bool EstConnecte
        {
            get
            {
                // TO DO: Il faut définir quel sont les paramètre de connexion des API CG77.
                return false;
            }
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="CG77CoucheConnexion"/>.
        /// </summary>
        public CG77CoucheConnexion()
            : base()
        {

        }
    }
}
