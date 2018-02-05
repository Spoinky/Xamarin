namespace ModeleXamarin.modeles.sources.VueModele.attributs.conditions.meta
{
    using System;

    /// <summary>
    /// Attribut signifiant que la propriété peut être comparé à une autre propriété.
    /// </summary>
    public class Comparable : Attribute
    {
        /// <summary>
        /// Le nom de la proprieté pour le client.
        /// </summary>
        private string _NomClient { get; set; }

        /// <summary>
        /// Le nom de la propriété pour le client
        /// </summary>
        public string NomClient { get { return _NomClient; } }

        public Comparable(string nom_client)
            : base()
        {
            // Vérification des pré-conditions.
            if (string.IsNullOrEmpty(nom_client))
                throw new ArgumentNullException(nameof(nom_client), "Le nom de la propriété pour le client.");

            this._NomClient = nom_client;
        }
    }
}
