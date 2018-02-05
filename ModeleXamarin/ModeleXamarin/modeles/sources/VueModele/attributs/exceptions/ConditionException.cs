namespace ModeleXamarin.modeles.sources.VueModele.attributs.exceptions
{
    using System;

    /// <summary>
    /// Exception pour la classe <see cref="ConditionPropriete"/>. 
    /// Elle définit les <see cref="Exception"/> lorsque les conditions d'une propriété ne sont pas respecté.
    /// </summary>
    public class ConditionException : Exception
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ConditionException"/>.
        /// </summary>
        /// <param name="message">Le message d'erreur de la condition.</param>
        public ConditionException(string message)
            : base(message)
        {
        }
    }
}
