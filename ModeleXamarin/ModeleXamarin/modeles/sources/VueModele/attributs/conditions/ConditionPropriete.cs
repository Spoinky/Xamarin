namespace ModeleXamarin.modeles.sources.VueModele.attributs.conditions
{
    using ModeleXamarin.modeles.sources.VueModele.attributs.exceptions;
    using System;

    /// <summary>
    /// Classe preprésentant une condition sur une propriété de classe.
    /// </summary>
    public abstract class ConditionPropriete : ICondition
    {
        /// <summary>
        /// Le nom de la propriété sur laquelle l'attribut est posé.
        /// </summary>
        private string _NomPropriete { get; set; }

        /// <summary>
        /// Le nom de la propriété pour le client.
        /// </summary>
        private string _NomClient { get; set; }

        /// <summary>
        /// Le nom de la propriété sur laquelle l'attribut est posé.
        /// </summary>
        public string NomPropriete { get { return _NomPropriete; } }

        /// <summary>
        /// Le nom de la propriété pour le client.
        /// </summary>
        public string NomClient { get { return _NomClient; } }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ConditionPropriete"/>.
        /// </summary>
        /// <param name="nom_propriete">Le nom de la propriété sur laquelle l'attribut est posé.</param>
        /// <param name="nom_client">Le nom client de la propriété sur laquelle l'attribut est posé.</param>
        public ConditionPropriete(string nom_propriete, string nom_client)
            : base()
        {
            // Vérificationd des préconditions.
            // Vérification que le nom de la propriété est valable (non null)
            if (string.IsNullOrEmpty(nom_propriete))
                throw new ArgumentNullException(nameof(nom_propriete), "Le nom de la propriété doit être renseigné.");

            // Vérification que le nom client de la propriété est valable (non null)
            if (string.IsNullOrEmpty(nom_client))
                throw new ArgumentNullException(nameof(nom_client), "Le nom client de la propriété doit être renseigné.");

            // Intialisation des champs.
            this._NomPropriete = nom_propriete;
            this._NomClient = nom_client;
        }

        /// <summary>
        /// Définit si la condition est vrai.
        /// </summary>
        /// <param name="objet"></param>
        /// <returns><c>vrai</c> si la condition est vrai; <c>faux</c> sinon.</returns>
        protected abstract bool ConditionEstVrai(object objet);

        /// <summary>
        /// Permet d'obtenir le message d'erreur au cas ou la condition est vrai.
        /// </summary>
        /// <returns></returns>
        protected abstract string MessageErreurDefini(object objet);

        /// <summary>
        /// Teste la condition de la propriété.
        /// </summary>
        /// <param name="objet">L'objet contenant la propriété.</param>
        public void Tester(object objet)
        {
            // Si la condition est fausse on retourne le message d'erreur.
            if (!this.ConditionEstVrai(objet))
                throw new ConditionException(this.MessageErreurDefini(objet));
        }
    }
}
