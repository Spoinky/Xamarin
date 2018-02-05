namespace ModeleXamarin.couches.api.cg77
{
    using ModeleXamarin.sources.couches;

    /// <summary>
    /// Couche de données pour les application du Conseil départemental de seine et marne.
    /// </summary>
    public abstract class CG77CoucheDonnees : CoucheDonnees
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="CG77CoucheDonnees"/>.
        /// </summary>
        /// <param name="nom_fichier">Le nom de fichier de base de données de l'application.</param>
        public CG77CoucheDonnees(string nom_fichier)
            : base(nom_fichier)
        {
            // Modification du tableau pour y ajouter les éléments utilsiateurs.
            this.BaseDeDonnees.RequetesDeCreation.Add("CREATE TABLE AGT_AGENT(AGT_MATRICULE NVARCHAR(8) NOT NULL, AGT_CIVILITE NVARCHAR(4) NOT NULL, AGT_NOM_PRENOM NVARCHAR(100) NOT NULL, CONSTRAINT PK_UTILISATEUR PRIMARY KEY(AGT_MATRICULE));");
        }
    }
}
