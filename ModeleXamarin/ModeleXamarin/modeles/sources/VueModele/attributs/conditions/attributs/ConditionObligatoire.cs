namespace ModeleXamarin.modeles.sources.VueModele.attributs.conditions.attributs
{
    using ModeleXamarin.modeles.sources.VueModele.attributs.conditions.proprietes;
    using System;

    /// <summary>
    /// Condition indiquant que la propriété est non null à la validation de l'objet.
    /// </summary>
    public class ConditionObligatoire : Attribute, ICondition
    {
        /// <summary>
        /// La condition de l'attribut.
        /// </summary>
        private ICondition Condition { get; set; }

        /// <summary>
        /// Intialise une nouvelle instance de la classe <see cref="ConditionAttribut"/>.
        /// </summary>
        /// <param name="condition">La condition de l'attribut.</param>
        public ConditionObligatoire(string nom_propriete, string nom_client, string type)
        {
            // Initialisation
            this.Condition = new ProprieteObligatoire(nom_propriete, nom_client, type);
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
