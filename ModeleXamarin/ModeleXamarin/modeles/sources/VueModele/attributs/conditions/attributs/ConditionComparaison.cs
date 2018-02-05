namespace ModeleXamarin.modeles.sources.VueModele.attributs.conditions.attributs
{
    using ModeleXamarin.modeles.sources.VueModele.attributs.conditions.proprietes;
    using System;

    /// <summary>
    /// Attribut permettant la comparaison d'une propriété avec une valeur imposé.
    /// </summary>
    public class ConditionComparaison : Attribute, ICondition
    {
        /// <summary>
        /// La condition de l'attribut.
        /// </summary>
        private ICondition Condition { get; set; }

        /// <summary>
        /// Intialise une nouvelle instance de la classe <see cref="ConditionAttribut"/>.
        /// </summary>
        /// <param name="condition">La condition de l'attribut.</param>
        public ConditionComparaison(string nom_propriete, string nom_client, object valeur, string comparateur)
        {
            // Initialisation
            this.Condition = new ProprieteComparaison(nom_propriete, nom_client, valeur, comparateur);
        }

        /// <summary>
        /// Teste la condition de la propriété.
        /// </summary>
        /// <param name="objet">L'objet contenant la propriété.</param>
        public void Tester(object objet = null)
        {
            this.Condition.Tester(objet);
        }
    }
}
