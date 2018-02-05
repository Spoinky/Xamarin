namespace ModeleXamarin.modeles.patrouilleur.Modele
{
    using System;

    /// <summary>
    /// Modèle pour les actions effectués par les patrouilleurs.
    /// </summary>
    public class ActionModele
    {
        /// <summary>
        /// Le nom de l'action.
        /// </summary>
        public string NomAction { get; set; }

        /// <summary>
        /// Indique si l'action a été seletionné par l'agent ou non.
        /// </summary>
        public Int64 Active { get; set; }

        public bool EstActif {
            get
            {
                if (Active == 1) return true;
                return false;
            }
        }

    }
}
