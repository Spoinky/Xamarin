namespace ModeleXamarin.modeles.sources.VueModele.attributs.conditions.attributs
{
    using ModeleXamarin.modeles.sources.VueModele.attributs.conditions.proprietes;
    using System;

    /// <summary>
    /// Attribut permettant la comparaison de deux propriété du même objet.
    /// </summary>
    public class ConditionDifferenteAutrePropriete : Attribute, ICondition
    {
        /// <summary>
        /// La condition de l'attribut.
        /// </summary>
        private ICondition Condition { get; set; }

        /// <summary>
        /// Intialise une nouvelle instance de la classe <see cref="ConditionAttribut"/>.
        /// </summary>
        /// <param name="condition">La condition de l'attribut.</param>
        public ConditionDifferenteAutrePropriete(string nom_propriete, string nom_client, string nom_propriete_compare)
        {
            // Initialisation
            this.Condition = new ProprieteDifferenteAutrePropriete(nom_propriete, nom_client, nom_propriete_compare);
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
