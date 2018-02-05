namespace ModeleXamarin.modeles.patrouilleur.Modele
{
    using System.Text;

    /// <summary>
    /// Vue modele pour la liste des évènement à afficher sur la page 'Etat patrouillage'.
    /// </summary>
    public class PauseMessageVueModele : IMessageVueModele
    {
        /// <summary>
        /// Le nom de la route.
        /// </summary>
        public string  NomRoute { get; set; }

        /// <summary>
        /// Heure de début de la pause.
        /// </summary>
        public string DateDebut { get; set; }

        /// <summary>
        /// Heure de fin de la pause.
        /// </summary>
        public string DateFin { get; set; }

        /// <summary>
        /// Le message à affichier.
        /// </summary>
        public string Message{
            get
            {
                StringBuilder sub_resultat = new StringBuilder("Arrêt de la patrouille - ");
                sub_resultat.Append(string.Format("{0} - {1} ", this.NomRoute, this.DateDebut.Substring(11)));

                // Récupération d'une heure de fin si elle existe.
                if (!string.IsNullOrEmpty(DateFin))
                    sub_resultat.Append(string.Format("_ Reprise patrouille {0} - {1}", this.NomRoute, this.DateFin.Substring(11)));

                // Retour du résultat.
                return sub_resultat.ToString();
            }
        }
    }
}
