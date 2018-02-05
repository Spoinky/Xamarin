namespace ModeleXamarin.modeles.patrouilleur.Modele
{
    /// <summary>
    /// Le modèle de classe pour une route.
    /// </summary>
    public class ItineraireModele
    {
        /// <summary>
        /// Le nom du centre.
        /// </summary>
        public string Centre { get; set; }

        /// <summary>
        /// Le nom de la ville.
        /// </summary>
        public string Ville { get; set; }

        /// <summary>
        /// Le nom de la route.
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Sa position de départ sur l'abscisse.
        /// </summary>
        public string Abscisse1 { get; set; }

        /// <summary>
        /// Sa position d'arriver sur l'abcisse.
        /// </summary>
        public string Abscisse2 { get; set; }

        /// <summary>
        /// L'affichage du modèle sur la combo-box.
        /// </summary>
        public string Affichage
        {
            get { return string.Format("{0} de {1} à {2}", this.Nom, this.Abscisse1, this.Abscisse2); }
        }

        public string AffichageAbcsisse1
        {
            get { return string.Format("{0}", this.Abscisse1); }
        }

        public string AffichageAbcsisse2
        {
            get { return string.Format("{0}", this.Abscisse1); }
        }
    }
}
