namespace ModeleXamarin.modeles.sources.VueModele.interfaces
{
    using ModeleXamarin.couches.sources;
    using ModeleXamarin.sources.couches;

    /// <summary>
    /// Interface définissant les opérations pour une page de la vue modèle en caroussel.
    /// </summary>
    /// <typeparam name="T">La vue modèle du carousel.</typeparam>
    /// <typeparam name="U">La couche métier de la page.</typeparam>
    /// <typeparam name="V">La couche données de l'application.</typeparam>
    /// <typeparam name="W">La couche de données de l'application.</typeparam>
    public interface IVueModeleCarouselPage<U, V, W> : IVueModele<U, V, W>
        where W : CoucheConnexion
        where V : CoucheDonnees
        where U : CoucheMetier<V, W>
    {
        /// <summary>
        /// Index de la page au sein de la page carousel.
        /// </summary>
        int Index { get; set; }

        /// <summary>
        /// Méthode permettant d'obtenir le modèle de la page précédente.
        /// </summary>
        /// <param name="index_modele_actuel">L'index du modèle actuel auquel on souhaite le model.</param>
        /// <returns>Une instance de la classe <see cref="IVueModeleCarouselPage{T, U, V}"/>.</returns>
        IVueModeleCarouselPage<U, V, W> ObtenirModelePagePrecedente();
    }
}
