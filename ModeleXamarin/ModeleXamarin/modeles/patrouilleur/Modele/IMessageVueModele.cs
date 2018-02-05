using System;

namespace ModeleXamarin.modeles.patrouilleur.Modele
{
    /// <summary>
    /// Interface signifier un message d'évènement sur fiche.
    /// </summary>
    public interface IMessageVueModele
    {
       /// <summary>
        /// Le message à affichier.
        /// </summary>
        string Message { get; }
    }
}
