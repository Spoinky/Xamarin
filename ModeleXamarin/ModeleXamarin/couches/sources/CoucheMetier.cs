namespace ModeleXamarin.sources.couches
{
    using ModeleXamarin.couches.sources;
    using System;

    /// <summary>
    /// Couche métier de l'application.
    /// </summary>
    /// <typeparam name="T">Le type <see cref="CoucheDonnees"/>.</typeparam>
    public abstract class CoucheMetier<T, U> 
        where T: CoucheDonnees
        where U: CoucheConnexion
    {
        /// <summary>
        /// La couche de données permettant de récupérer des données.
        /// </summary>
        protected T Donnees { get; set; }

        /// <summary>
        /// Couche permettant de gérer la connexion du dispositif.
        /// </summary>
        protected U Connexion { get; set; }

        /// <summary>
        /// Le nom de fichier de la base de données.
        /// </summary>
        protected abstract string NomFichier { get; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="CoucheMetier"/>.
        /// </summary>
        /// <param name="nom_fichier">Le nom de fichier de la base de données.</param>
        /// <param name="requetes_de_creation"></param>
        public CoucheMetier()
        {
            this.Donnees = (T)Activator.CreateInstance(typeof(T), this.NomFichier);
            this.Connexion = (U)Activator.CreateInstance(typeof(U));
        }

        /// <summary>
        /// Indique si le dispositif est connecté.
        /// </summary>
        /// <returns><c>true</c> si le dispositif est connecté selon le mode voulut; <c>false</c> sinon.</returns>
        public bool EstConnecte()
        {
            return this.Connexion.EstConnecte;
        }
    }
}
