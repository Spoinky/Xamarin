using ModeleXamarin.Droid.couches.definitions.bdd;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite))]
namespace ModeleXamarin.Droid.couches.definitions.bdd
{
    using ModeleXamarin.couches.sources.definitions.bdd;
    using Mono.Data.Sqlite;
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Classe permettant d'effectuer des opérations vers une base de données local.
    /// </summary>
    public class SQLite : ASQLite
    {
        /// <summary>
        /// Le chemin vers le fichier de la base de données.
        /// </summary>
        public override string Chemin {
            get
            {
                return  Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), string.Format("{0}.sqlite", this.NomFichier));
            }
        }

        /// <summary>
        /// Execute une requête sql au sein de la base de données local.
        /// </summary>
        /// <param name="requete">La requête SQL.</param>
        /// <returns>Une liste d'instance <see cref="dynamic"/>.</returns>
        public override IEnumerable<dynamic> Executer(string requete)
        {
            // Vérification des précondition.
            // Vérification de la requête.
            if (string.IsNullOrEmpty(requete))
                throw new ArgumentNullException(nameof(requete));

            // Initialisation de la base su le fichier de base de données n'existe pas.
            if (!File.Exists(this.Chemin))
                this.InitialiserBase();

            // Initialisation de la connexion vers la base de données.
            SqliteConnection connection = new SqliteConnection("Data Source=" + this.Chemin);
            connection.Open();

            // Initialisation du résultat.
            IEnumerable<dynamic> result = new List<dynamic>();

            try
            {
                // Execution de la requête.
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = requete;

                SqliteDataReader lecteur = command.ExecuteReader();

                // Récupération du résultat.
                if (lecteur.HasRows)
                    result = SQLiteTraducteur.Traduire(lecteur);
            }
            catch (Exception e)
            {
                connection.Close();
                throw e;
            }

            connection.Close();
            return result;
        }

        /// <summary>
        /// Execute une requête SQL au sein de la base de données local.
        /// </summary>
        /// <typeparam name="T">Le type d'objet attendu par la requête.</typeparam>
        /// <param name="request">La requête à executer.</param>
        /// <returns>Une liste d'instance de classe <see cref="T"/>.</returns>
        public override IEnumerable<T> Executer<T>(string request)
        {
            // Check the pre-condition
            // Check the request argument.
            if (string.IsNullOrEmpty(request))
                throw new ArgumentNullException(nameof(request));

            // Initialisation de la base su le fichier de base de données n'existe pas.
            if (!File.Exists(this.Chemin))
                this.InitialiserBase();

            // Initialise connection.
            SqliteConnection connection = new SqliteConnection(string.Format("Data Source={0}", this.Chemin));
            connection.Open();

            // Initialize the result.
            IEnumerable<T> result = new List<T>();

            try
            {
                // Execute the request.
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = request;

                SqliteDataReader lecteur = command.ExecuteReader();

                // Set the result.
                if (lecteur.HasRows)
                    result = SQLiteTraducteur.Traduire<T>(lecteur);
            }
            catch (Exception e)
            {
                connection.Close();
                throw e;
            }

            connection.Close();
            return result;
        }

        /// <summary>
        /// Execute une requete sans vérifier au préalable si le fichier existe.
        /// </summary>
        /// <param name="requete">La requete à executer.</param>
        protected override void ExecuterSansVerification(string requete)
        {
            // Vérification des pré-conditions
            // Vérification de la requête SQL.
            if (string.IsNullOrEmpty(requete))
                throw new ArgumentNullException(nameof(requete));

            // Initialisation de la connexion vers la base de données.
            SqliteConnection connection = new SqliteConnection(string.Format("Data Source={0}", this.Chemin));
            connection.Open();

            try
            {
                // Execution de la requete.
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = requete;

                command.ExecuteReader();
            }
            catch (Exception e)
            {
                connection.Close();
                throw e;
            }

            connection.Close();
        }
    }
}
