namespace ModeleXamarin.modeles.sources.VueModele.interfaces
{
    using System.Collections.Generic;
    using System.Windows.Input;

    /// <summary>
    /// Interface pour les modèle validables.
    /// </summary>
    public interface IVueModeleValidable
    {
        /// <summary>
        /// Indique si le modèle est validable.
        /// </summary>
        string EstValidable { get; }

        /// <summary>
        /// Indique si le modèle est en erreur ou nom.
        /// </summary>
        string EstEnErreur { get; }

        /// <summary>
        /// Valide une vue modèle.
        /// </summary>
        /// <returns><c>true</c> si la validation c'est déroulé avec succes; sinon <c>false</c>.</returns>
        ICommand Valider { get; set; }
    }
}
