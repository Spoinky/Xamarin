namespace ModeleXamarin.modeles.cg77.VueModele
{
    using ModeleXamarin.couches.api.cg77;
    using ModeleXamarin.modeles.sources.VueModele;

    /// <summary>
    /// Classe général pour un modele de page pour une application du conseil départemental de seine-et-marne.
    /// </summary>
    public class CG77VueModele<T,U,V> : VueModele<T,U,V>
        where V: CG77CoucheConnexion
        where U: CG77CoucheDonnees 
        where T : CG77CoucheMetier<U,V>
        
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
        public CG77VueModele(string titre, string sous_titre)
            : base(string.Format("CG77 - {0}", titre))
        {
            this.SousTitre = sous_titre;
        }
    }
}
