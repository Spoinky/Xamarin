namespace ModeleXamarin.sources.couches
{
    using ModeleXamarin.couches.sources.definitions.bdd;
    using Xamarin.Forms;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Couche de données: Elle permet d'effectuer des opération vers la base de données locale.
    /// </summary>
    public abstract class CoucheDonnees
    {
        /// <summary>
        /// L'executeur de requêtes.
        /// </summary>
        public ISQLite BaseDeDonnees { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="CoucheDonnees"/>.
        /// <paramref name="nom_fichier"/>Le nom de fichier de base de données de l'application.
        /// </summary>
        public CoucheDonnees(string nom_fichier)
        {
            // Vérification des préconditions.
            // Vérification du nom de fichier.
            if (string.IsNullOrEmpty(nom_fichier))
                throw new ArgumentNullException(nameof(nom_fichier), "Le fichier de base de données doit porter un nom.");

            this.BaseDeDonnees = DependencyService.Get<ISQLite>();
            this.BaseDeDonnees.NomFichier = nom_fichier;
            this.BaseDeDonnees.RequetesDeCreation = new List<string>();
        }
    }
}
