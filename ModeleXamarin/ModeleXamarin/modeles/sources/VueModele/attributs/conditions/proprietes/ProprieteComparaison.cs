namespace ModeleXamarin.modeles.sources.VueModele.attributs.conditions.proprietes
{
    using System;

    public class ProprieteComparaison : ConditionPropriete
    {
        /// <summary>
        /// Le comparateur entre les deux valeurs.
        /// </summary>
        private string Comparateur { get; set; }

        /// <summary>
        /// La valeur de comparaison avec la propriété.
        /// </summary>
        private object Valeur { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ProprieteComparaison"/>.
        /// </summary>
        /// <param name="nom_propriete">Le nom de la propriété.</param>
        /// <param name="valeur">La valeur de comparaison.</param>
        /// <param name="comparateur">Le comparateur des prochaines valeur.</param>
        public ProprieteComparaison(string nom_propriete, string nom_client, object valeur, string comparateur)
            : base(nom_propriete, nom_client)
        {
            // Vérification des pré-conditions.
            // Vérification que le comparateur ne soit pas null ou vide.
            if (string.IsNullOrEmpty(comparateur))
                throw new ArgumentNullException(nameof(comparateur));

            // Vérification que le comparateur ne soit que sur l'une de ces trois valeurs '>', '<', '=', '!='
            if (!comparateur.Equals(">") && !comparateur.Equals("<") && !comparateur.Equals("="))
                throw new ArgumentException("La valeur du comparateur ne peut être que: '>', '<', '!=', ou '='");

            // Intialisation des valeurs
            this.Comparateur = comparateur;
            this.Valeur = valeur;
        }

        /// <summary>
        /// Vérifie que la comparaison des deux valeurs soit vrai.
        /// </summary>
        /// <param name="objet">L'objet contenant la propriété.</param>
        /// <returns><c>true</c> si la comparaison est un succes; <c>faux</c> sinon.</returns>
        protected override bool ConditionEstVrai(object objet)
        {
            // Vérification si les deux valeur sont égales.
            if (this.Comparateur.Equals("="))
                return objet.GetType().GetProperty(this.NomPropriete).GetValue(objet).Equals(this.Valeur);

            // Vérification si la valeur de la propriété est plus grande que celle de comparaison.
            if (this.Comparateur.Equals(">"))
                return ((int)objet.GetType().GetProperty(this.NomPropriete).GetValue(objet)) > ((int)this.Valeur);

            // Vérification si la valeur de la propriété est différentes.
            if (this.Comparateur.Equals("!="))
                return objet.GetType().GetProperty(this.NomPropriete).GetValue(objet) != this.Valeur;

            // Vérification si les deux valeur sont égales.
            return ((int)objet.GetType().GetProperty(this.NomPropriete).GetValue(objet)) > ((int)this.Valeur);
        }

        /// <summary>
        /// Retourne le message adéquat en cas d'erreur de comparaison.
        /// </summary>
        /// <param name="objet">L'objet contenant la propriété de comparaison.</param>
        /// <returns>Une chaîne de caractère définissant l'erreur.</returns>
        protected override string MessageErreurDefini(object objet)
        {
            // Le message doit être adapté selon le comparateur choisit.
            if (this.Comparateur.Equals("="))
                return string.Format("{0} doit être égale à {1}.", this.NomClient, this.Valeur);

            if (this.Comparateur.Equals(">"))
                return string.Format("{0} doit être supérieur à {1}.", this.NomClient, this.Valeur);

            if (this.Comparateur.Equals("!="))
                return string.Format("{0} doit être différente à {1}.", this.NomClient, this.Valeur);

            // Vérification si les deux valeur sont égales.
            return string.Format("{0} doit être inférieur à {1}.", this.NomClient, this.Valeur);
        }
    }
}
