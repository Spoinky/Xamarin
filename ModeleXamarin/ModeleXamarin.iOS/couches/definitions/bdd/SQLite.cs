using ModeleXamarin.iOS.couches.definitions.bdd;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite))]
namespace ModeleXamarin.iOS.couches.definitions.bdd
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
        /// Execute an SQL query within the internal database.
        /// </summary>
        /// <typeparam name="T">Type of the object to get.</typeparam>
        /// <param name="request">The request to execute.</param>
        /// <returns>The result of the query on the base in <see cref="T"/> object.</returns>
        public override IEnumerable<T> Executer<T>(string request)
        {
            // Vérification des préconditions.
            // Vérification de la requête.
            if (string.IsNullOrEmpty(request))
                throw new ArgumentNullException(nameof(request));

            // Initialisation de la base su le fichier de base de données n'existe pas.
            if (!File.Exists(this.Chemin))
                this.InitialiserBase();

            // Initialisation de la connexion vers la base de données.
            SqliteConnection connection = new SqliteConnection(string.Format("Data Source={0}", this.Chemin));
            connection.Open();

            // Initialisation du résultat.
            IEnumerable<T> result = new List<T>();

            try
            {
                // Execution de la requête.
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = request;

                SqliteDataReader lecteur = command.ExecuteReader();

                // Récupération du résultat.
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
