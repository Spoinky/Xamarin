namespace ModeleXamarin.couches.sources.definitions.bdd
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface définissant les opérations pour gérer une base de données SQLite.
    /// </summary>
    public interface ISQLite
    {
        /// <summary>
        /// Le nom de fichier de la base de données.
        /// </summary>
        string NomFichier { get; set; }

        /// <summary>
        /// Execute une requete sql au sein de la base de données SQLite.
        /// </summary>
        /// <param name="requete">La requête à exécuter.</param>
        /// <returns>Les résultats de la requpete au format <see cref="dynamic"/>.</returns>
        IEnumerable<dynamic> Executer(string requete);

        /// <summary>
        /// Execute une requete sql au sein de la base de données SQLite.
        /// </summary>
        /// <param name="requete">La requête à exécuter.</param>
        /// <typeparam name="T">Le type d'objet retourner par la réponse.</typeparam>
        /// <returns>Les résultats de la requpete au format <see cref="dynamic"/>.</returns>
        IEnumerable<T> Executer<T>(string requete);

        /// <summary>
        /// Requêtes permettant de créer la base de données si celle-ci n'est pas initialisé.
        /// </summary>
        List<string> RequetesDeCreation { get; set; }

        /// <summary>
        /// Execute une liste de requete au sein d'une base de données SQLite.
        /// </summary>
        /// <typeparam name="T">Le model de résultat de la base de données.</typeparam>
        /// <param name="requetes">La liste de requête à executer.</param>
        /// <returns>La liste des résultats des requêtes.</returns>
        IEnumerable<IEnumerable<T>> Executer<T>(IEnumerable<string> requetes);

        /// <summary>
        /// Execute une liste de requete au sein d'une base de données SQLite.
        /// </summary>
        /// <param name="requetes">La liste de requête à executer.</param>
        /// <returns>La liste des résultats des requêtes.</returns>
        IEnumerable<IEnumerable<dynamic>> Executer(IEnumerable<string> requetes);

        /// <summary>
        /// Méthode permettant d'initialiser la base de données locale.
        /// </summary>
        void InitialiserBase();
    }
}
