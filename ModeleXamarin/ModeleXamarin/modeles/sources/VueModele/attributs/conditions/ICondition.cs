namespace ModeleXamarin.modeles.sources.VueModele.attributs.conditions
{
    /// <summary>
    /// Interface définissant les opérations nécessaire d'un attribut de propriété pour une condition.
    /// </summary>
    public interface ICondition
    {
        /// <summary>
        /// Teste la condition de la propriété.
        /// </summary>
        /// <param name="objet">La classe de la propriété.</param>
        void Tester(object objet);
    }
}
