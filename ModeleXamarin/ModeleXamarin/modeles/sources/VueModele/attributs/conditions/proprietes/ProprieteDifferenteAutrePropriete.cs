namespace ModeleXamarin.modeles.sources.VueModele.attributs.conditions.proprietes
{
    using ModeleXamarin.modeles.sources.VueModele.attributs.conditions.meta;
    using System;
    using System.Reflection;

    /// <summary>
    /// Condition permettant de signifier que deux propriété du même objet ne doivent pas être égale à la validation.
    /// </summary>
    public class ProprieteDifferenteAutrePropriete : ConditionPropriete
    {
        /// <summary>
        /// La propriété avec laquelle on compare la propriété source.
        /// </summary>
        private string ProprieteCompare { get; set; }

        /// <summary>
        /// Initialise ine nouvelle instance de la classe <see cref="ProprieteDifferenteAutrePropriete"/>.
        /// </summary>
        /// <param name="nom_propriete">Le nom de la propriété sur laquelle l'attribut repose.</param>
        /// <param name="nom_client">Le nom client de la propriété sur laquelle l'attribut repose.</param>
        /// <param name="nom_propriete_compare">Le nom de la propriété sur avec lequel il faut le comparer.</param>
        public ProprieteDifferenteAutrePropriete(string nom_propriete, string nom_client, string nom_propriete_compare)
            : base(nom_propriete, nom_client)
        {
            // Vérification des pré-conditions.
            if (string.IsNullOrEmpty(nom_client))
                throw new ArgumentNullException(nameof(nom_client), "Le nom client de la propriété doit être renseigné.");
            if (string.IsNullOrEmpty(nom_propriete_compare))
                throw new ArgumentNullException(nameof(nom_propriete_compare), "Le nom client de la propriété à comparer doit être renseigné.");

            // Intialisation des propriétés.
            this.ProprieteCompare = nom_propriete_compare;
        }

        /// <summary>
        /// Vérifie que la valeur de la propriété est différentes d'une autre propriété.
        /// </summary>
        /// <param name="objet">L'objet contenant les propriétés à comparer.</param>
        /// <returns><c>true</c> si la propriété est différente de la seconde propriété.</returns>
        protected override bool ConditionEstVrai(object objet)
        {
            if (objet == null)
                throw new ArgumentNullException(nameof(objet), "La valeur de l'objet source est null.");

            // Récupération de la valeur de la propriété source.
            object valeur_source = objet.GetType().GetProperty(this.NomPropriete).GetValue(objet);
            object valeur_compare = objet.GetType().GetProperty(this.ProprieteCompare).GetValue(objet);

            // Comparaison.
            if (valeur_source == null)
            {
                if (valeur_compare == null)
                    return false;
                return true;
            }
            return !valeur_source.Equals(valeur_compare);
        }

        /// <summary>
        /// Le message d'erreur au cas ou les deux propriétés n'ont pas de valeurs différentes.
        /// </summary>
        /// <param name="objet">L'objet contenant les propriétés à comparer.</param>
        /// <returns><c>true</c> si les propriétés sont différentes; <c>false</c> sinon.</returns>
        protected override string MessageErreurDefini(object objet)
        {
            //((ConditionPropriete)objet.GetType().GetProperty(this.ProprieteSource).GetCustomAttribute(typeof(NomPropriete), false)).Nom;
            return string.Format("Le champs {0} doit être différent du champs {1}.", this.NomClient, ((Comparable)objet.GetType().GetProperty(this.ProprieteCompare).GetCustomAttribute(typeof(Comparable), false)).NomClient);
        }
    }
}
