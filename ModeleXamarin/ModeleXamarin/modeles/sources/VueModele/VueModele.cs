namespace ModeleXamarin.modeles.sources.VueModele
{
    using ModeleXamarin.couches.sources;
    using ModeleXamarin.modeles.sources.VueModele.interfaces;
    using ModeleXamarin.sources.couches;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Classe général pour un modele de page.
    /// </summary>
    public class VueModele<T,U,V> : IVueModele<T, U, V>
        where V: CoucheConnexion
        where U : CoucheDonnees 
        where T : CoucheMetier<U,V> 
    {
        /// <summary>
        /// Le titre de la page.
        /// </summary>
        public string Titre { get; set; }

        /// <summary>
        /// Le controleur de la vue modele/.
        /// </summary>
        public T Service { get; set; }

        /// <summary>
        /// Evenement lorsqu'une propriété change de valeur.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        
        /// <summary>
        /// Intialise une nouvelle instance de la classe <see cref="VueModele"/>.
        /// </summary>
        /// <param name="titre">Le titre de la page.</param>
        public VueModele(string titre)
        {
            // Vérification des pré-conditions.
            // Vérification que le titre est rempli.
            if (string.IsNullOrEmpty(titre))
                throw new ArgumentNullException(nameof(titre), "Vous devez choisir un titre de page!");

            this.Titre = titre;
            this.Service = (T)Activator.CreateInstance(typeof(T));
        }

        /// <summary>
        /// Enclenche l'évènement permettant de mettre à jour la vue.
        /// </summary>
        /// <param name="nom_propriete">Le nom de la propriété.</param>
        protected virtual void SurProprieteModifie(string nom_propriete)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nom_propriete));
        }
    }
}
