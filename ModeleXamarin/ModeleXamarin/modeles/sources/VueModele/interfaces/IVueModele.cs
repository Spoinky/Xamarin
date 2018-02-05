namespace ModeleXamarin.modeles.sources.VueModele.interfaces
{
    using ModeleXamarin.couches.sources;
    using ModeleXamarin.sources.couches;
    using System.ComponentModel;

    /// <summary>
    /// Interface définissant les éléments de base pour une vue modèle simple.
    /// </summary>
    public interface IVueModele<T, U, V> : INotifyPropertyChanged
        where V : CoucheConnexion
        where U : CoucheDonnees
        where T : CoucheMetier<U, V>
    {
        /// <summary>
        /// Le titre de la page.
        /// </summary>
        string Titre { get; set; }

        /// <summary>
        /// Le controleur de la vue modele/.
        /// </summary>
        T Service { get; set; }
    }
}
