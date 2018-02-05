namespace ModeleXamarin.couches.sources.definitions.bdd
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Méthode implemetant les opérations pour gérer une base de données.
    /// </summary>
    public abstract class ASQLite : ISQLite
    {
        /// <summary>
        /// Le nom de fichier de la base de données.
        /// </summary>
        public string NomFichier { get; set; }

        /// <summary>
        /// Le chemin vers le fichier de base de données.
        /// </summary>
        public abstract string Chemin { get; }

        /// <summary>
        /// Requêtes permettant de créer la base de données si celle-ci n'est pas initialisé.
        /// </summary>
        public List<string> RequetesDeCreation { get; set; }

        /// <summary>
        /// Méthode permettant d'initialiser la base de données locale.
        /// </summary>
        public void InitialiserBase()
        {   
            // Vérification des pré-conditions.
            if (RequetesDeCreation == null || RequetesDeCreation.Count == 0)
                throw new ArgumentNullException(nameof(this.RequetesDeCreation), "Il n'y a aucune requête permettant d'initalisez votre base de données.");
            
            // Execution des requête d'initialisation.
            foreach (string requete in this.RequetesDeCreation)
                this.ExecuterSansVerification(requete);
        }

        /// <summary>
        /// Execute une requete sql au sein de la base de données SQLite.
        /// </summary>
        /// <param name="requete">La requête à exécuter.</param>
        /// <returns>Les résultats de la requpete au format <see cref="dynamic"/>.</returns>
        public abstract IEnumerable<dynamic> Executer(string requete);

        /// <summary>
        /// Execute une requete sql au sein de la base de données SQLite.
        /// </summary>
        /// <param name="requete">La requête à exécuter.</param>
        /// <typeparam name="T">Le type d'objet retourner par la réponse.</typeparam>
        /// <returns>Les résultats de la requpete au format <see cref="dynamic"/>.</returns>
        public abstract IEnumerable<T> Executer<T>(string requete);

        /// <summary>
        /// Execute une requete sans vérifier au préalable si le fichier existe.
        /// </summary>
        /// <param name="requete">La requete à executer.</param>
        protected abstract void ExecuterSansVerification(string requete);

        /// <summary>
        /// Execute une liste de requete au sein d'une base de données SQLite.
        /// </summary>
        /// <typeparam name="T">Le model de résultat de la base de données.</typeparam>
        /// <param name="request">La liste de requête à executer.</param>
        /// <returns>La liste des résultats des requêtes.</returns>
        public IEnumerable<IEnumerable<T>> Executer<T>(IEnumerable<string> requetes)
        {
            if (requetes == null || requetes.Count() == 0)
                throw new ArgumentNullException(nameof(requetes), "Le tableau de requête doit au moins avoir une requête.");

            // Initialisation des resultats.
            List<IEnumerable<T>> resultats = new List<IEnumerable<T>>();

            // Execution des requêtes.
            foreach (string requete in requetes)
                resultats.Add(this.Executer<T>(requete));

            // Retourne la liste des resultats.
            return resultats;
        }

        /// <summary>
        /// Execute une liste de requete au sein d'une base de données SQLite.
        /// </summary>
        /// <param name="request">La liste de requête à executer.</param>
        /// <returns>La liste des résultats des requêtes.</returns>
        public IEnumerable<IEnumerable<dynamic>> Executer(IEnumerable<string> requetes)
        {
            if (requetes == null || requetes.Count() == 0)
                throw new ArgumentNullException(nameof(requetes), "Le tableau de requête doit au moins avoir une requête.");

            // Initialisation des resultats.
            List<IEnumerable<dynamic>> resultats = new List<IEnumerable<dynamic>>();

            // Execution des requêtes.
            foreach (string requete in requetes)
                resultats.Add(this.Executer(requete));

            // Retourne la liste des resultats.
            return resultats;
        }
    }
}
