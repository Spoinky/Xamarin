namespace ModeleXamarin.UWP.couches.definitions.bdd
{
    using Mono.Data.Sqlite;
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Reflection;

    /// <summary>
    /// Classe permettant de traduire le résultat d'une requete d'une instance <see cref="SqliteDataReader"/> en objet.
    /// </summary>
    public static class SQLiteTraducteur
    {
        /// <summary>
        /// Vérifie si l'instance de classe <see cref="Type"/> est une primitice ou non.
        /// </summary>
        /// <param name="type">Une instance de classe <see cref="Type"/>.</param>
        /// <returns>Vrai si le type est une primitive, faux sinon.</returns>
        private static bool EstUnePrimitive(Type type)
        {
            if (type.IsPrimitive || type == typeof(string) || type == typeof(String) || type == typeof(DateTime))
                return true;

            return false;
        }

        /// <summary>
        /// Traduit une ligne de donnée d'une instance de classe <see cref="SqliteDataReader"/> en instance <see cref="dynamic"/>.
        /// </summary>
        /// <param name="reader">Une ligne de données du résultat d'une requête SQL.</param>
        /// <returns>L'objet <see cref="dynamic"/> correspondant à la ligne de donnée.</returns>
        private static dynamic ObtenirLigneDeDonnees(SqliteDataReader reader)
        {
            // Preparation du réusltat à retourner.
            IDictionary<string, object> resultat = new ExpandoObject() as IDictionary<string, object>;

            // Récupération des champs et de leur valeurs
            for (int i = 0; i < reader.FieldCount; i++)
            {
                try
                {
                    if (!(reader[i] is System.DBNull))
                        resultat[reader.GetName(i)] = Convert.ChangeType(reader[i], reader.GetFieldType(i));
                    else
                        resultat[reader.GetName(i)] = null;
                }
                catch (Exception e)
                {
                    resultat[reader.GetName(i)] = null;
                }
            }

            return resultat;
        }

        /// <summary>
        /// Traduit les résultats d'une requete en liste de d'objet dynamique.
        /// </summary>
        /// <param name="lecteur">Une instance de la classe <see cref="DbDataReader"/> contenant les résultats de la requête.</param>
        /// <returns>Une liste d'objets <see cref="dynamic"/>.</returns>
        public static IEnumerable<dynamic> Traduire(SqliteDataReader lecteur)
        {
            List<dynamic> result = new List<dynamic>();

            if (lecteur == null || !lecteur.HasRows)
                return result;

            while (lecteur.Read())
                result.Add(ObtenirLigneDeDonnees(lecteur));
            
            return result;
        }

        /// <summary>
        /// Traduit une ligne de donnée d'une instance de classe <see cref="SqliteDataReader"/> en instance <see cref="dynamic"/>.
        /// </summary>
        /// <param name="lecteur">Une ligne de données du résultat d'une requête SQL.</param>
        /// <param name="est_primitive">Indique si le resltat est une liste de primitive.</param>
        /// <typeparam name="T">Le type de classe modèle que l'on souhaite comme résultat.</typeparam>
        /// <returns>L'objet <see cref="T"/> correspondant à la ligne de donnée.</returns>
        private static T ObtenirLigneDeDonnees<T>(SqliteDataReader lecteur, bool est_primitive)
        {
            if (est_primitive)
                return (T)Convert.ChangeType(lecteur[0], typeof(T));

            Type type = typeof(T);
            // Preparation of the result to be returned.
            T result = (T)Activator.CreateInstance(type);

            // Recovery of property objects value.
            foreach (PropertyInfo propriete in type.GetProperties())
            {
                if (propriete.CanWrite)
                {
                    if (!(lecteur[propriete.Name] is System.DBNull))
                        propriete.SetValue(result, lecteur[propriete.Name]);
                    else
                        propriete.SetValue(result, null);
                }
            }

            return result;
        }

        /// <summary>
        /// Traduit les résultats d'une requete en liste de d'objet dynamique.
        /// </summary>
        /// <param name="lecteur">Une instance de la classe <see cref="DbDataReader"/> contenant les résultats de la requête.</param>
        /// <typeparam name="T">Le type d'objet que l'on souhaite comme résultat.</typeparam>
        /// <returns>Une liste d'objets <see cref="T"/>.</returns>
        public static IEnumerable<T> Traduire<T>(SqliteDataReader lecteur)
        {
            bool isPrimitive = EstUnePrimitive(typeof(T));

            List<T> result = new List<T>();
            if (lecteur == null || !lecteur.HasRows)
                return result;

            while (lecteur.Read())
                result.Add(ObtenirLigneDeDonnees<T>(lecteur, isPrimitive));
            
            return result;
        }   
    }
}
