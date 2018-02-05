namespace ModeleXamarin.couches.api.cg77
{
    using ModeleXamarin.sources.couches;
    using System;

    /// <summary>
    /// Couche métier pour les applications du conseil départemetal de seine-et-marne.
    /// </summary>
    /// <typeparam name="T">Une instance de classe <see cref="CG77CoucheDonnees"/>.</typeparam>
    public abstract class CG77CoucheMetier<T,U> : CoucheMetier<T,U> 
        where T: CG77CoucheDonnees
        where U: CG77CoucheConnexion
    {
        /// <summary>
        /// Intialise une nouvelle instance de la classe <see cref="CG77CoucheMetier"/>.
        /// </summary>
        public CG77CoucheMetier()
            : base()
        {
        }
    }
}
