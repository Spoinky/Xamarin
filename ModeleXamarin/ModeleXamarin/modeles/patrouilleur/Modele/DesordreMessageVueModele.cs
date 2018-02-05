namespace ModeleXamarin.modeles.patrouilleur.Modele
{
    using System;

    /// <summary>
    /// Modele de classe pour afficher un évènement dans le tableau de constat des fiches d'évènement.
    /// </summary>
    public class DesordreMessageVueModele : IMessageVueModele
    {
        /// <summary>
        /// L'identifiant du désordre.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Le nom de la route.
        /// </summary>
        public string NomRoute { get; set; }

        /// <summary>
        /// La position X du désordre.
        /// </summary>
        public decimal PositionX { get; set; }

        /// <summary>
        /// La position Y du désordre.
        /// </summary>
        public decimal PositionY { get; set; }

        /// <summary>
        /// Le constat du désordre.
        /// </summary>
        public string Constat { get; set; }

        /// <summary>
        /// Le message à affichier.
        /// </summary>
        public string Message
        {
            get
            {
                return string.Format("{0} {1} - {2} à {3} - {4}", this.NomRoute, this.Id, this.PositionX, PositionY, this.Constat);
            }
        }
    }
}
