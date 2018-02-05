namespace ModeleXamarin.couches.api.patrouilleur
{
    using ModeleXamarin.couches.api.cg77;

    /// <summary>
    /// Couche métier de l'application Test.
    /// </summary>
    public class PatrouilleurCoucheMetier : CG77CoucheMetier<PatrouilleurCoucheDonnees, CG77CoucheConnexion>
    {
        /// <summary>
        /// Le nom de fichier de la base de données.
        /// </summary>
        protected override string NomFichier
        {
            get
            {
                return "Patrouilleur";
            }
        }

        /// <summary>
        /// Initialise une nouvelle instance de la clase <see cref="TestCoucheMetier"/>.
        /// </summary>
        public PatrouilleurCoucheMetier()
            : base()
        {
        }
    }
}
