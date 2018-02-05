namespace ModeleXamarin.modeles.cg77.VueModele
{
    using ModeleXamarin.couches.api.cg77;
    using ModeleXamarin.modeles.sources.VueModele.validation;

    /// <summary>
    /// Modele validable pour les projets du conseil départementale de seine-et-marne.
    /// </summary>
    /// <typeparam name="T">Une couche métier du département.</typeparam>
    /// <typeparam name="U">Une couche de donnée du département.</typeparam>
    /// <typeparam name="V">La couche de connexion du département.</typeparam>
    public abstract class CG77VueModeleValidable<T,U,V> : VueModeleValidable<T, U, V>
        where V : CG77CoucheConnexion
        where U : CG77CoucheDonnees
        where T : CG77CoucheMetier<U, V>
    {
        /// <summary>
        /// Sous titre de la page.
        /// </summary>
        public string SousTitre { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="CG77VueModele"/>.
        /// </summary>
        /// <param name="titre"></param>
        /// <param name="sous_titre"></param>
        public CG77VueModeleValidable(string titre, string sous_titre)
            : base(string.Format("CG77 - {0}", titre))
        {
            this.SousTitre = sous_titre;
        }
    }
}
