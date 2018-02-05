namespace ModeleXamarin.modeles.sources.VueModele.attributs.conditions.proprietes
{
    using System;

    /// <summary>
    /// Condition d'attribut signifiant que la propriété est obligatoire et donc avoir une valeur.
    /// </summary>
    public class ProprieteObligatoire : ConditionPropriete
    {
        /// <summary>
        /// Le type de la propriété sur la vue (radio, combo, text...)
        /// </summary>
        private string Type { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ProprieteObligatoire"/>.
        /// </summary>
        /// <param name="nom_propriete">Le nom de la propriété sur laquelle est posée l'attribut.</param>
        public ProprieteObligatoire(string nom_propriete, string nom_client, string type)
            : base(nom_propriete, nom_client)
        {
            // Vérification du champs type
            // Est-il null ou vide?
            if (string.IsNullOrEmpty(type))
                throw new ArgumentNullException(nameof(nom_propriete), "Le type du champs obligatoire doit être renseigné.");

            // A-t-t-il la valeur adéquate?
            if (!type.Equals("Checkbox") && !type.Equals("Radio") && !type.Equals("Text"))
                throw new ArgumentException(string.Format("Le type de la propriété {0} ne peu avoir pour valeur 'Checkbox', 'Radio' ou 'Text'.", this.NomPropriete));

            // Vérification du nom client de la propriété.
            if (string.IsNullOrEmpty(nom_client))
                throw new ArgumentNullException(nameof(nom_client), "Le nom client de la propriété doit être renseigné.");

            this.Type = type;
        }

        /// <summary>
        /// Vérifie la propriété est null ou vide.
        /// </summary>
        /// <param name="objet">L'objet contenant la propriété.</param>
        /// <returns><c>true</c> si la propriété est dument remplie; <c>false</c> sinon.</returns>
        protected override bool ConditionEstVrai(object objet)
        {
            Type type = objet.GetType();
            object valeur = type.GetProperty(this.NomPropriete).GetValue(objet);

            // Si la valeur est null.
            if (valeur != null) return true;

            // Vérifie si la valeur est un string null ou vide.
            if (type == typeof(string)) return !string.IsNullOrEmpty((string)valeur);

            return false;
        }

        /// <summary>
        /// Definit le message d'erreur de la propriété en cas de condition fausse.
        /// </summary>
        /// <param name="objet">L'objet contenant la propriété.</param>
        /// <returns>Le message d'erreur indiquant l'obligation de donner une valeur à la propriété.</returns>
        protected override string MessageErreurDefini(object objet)
        {
            return string.Format("Vous devez {0} pour le champs {1}.", this.ObtenirVerbeType(), this.NomClient);
        }

        /// <summary>
        /// Permet d'obtenir le verbe du type de la propriété.
        /// </summary>
        /// <returns>Le verbe du type de la propriété.</returns>
        private string ObtenirVerbeType()
        {
            // Si le type à remplir est un input de type texte.
            if (this.Type.Equals("Text")) return "remplir";

            // Si le type à remplir est un input de type checkbox.
            else if (this.Type.Equals("Checkbox")) return "choisir une valeur";

            // Si le type à remplir est un input de type radio.
            return "selectionner une valeur";
        }
    }
}
