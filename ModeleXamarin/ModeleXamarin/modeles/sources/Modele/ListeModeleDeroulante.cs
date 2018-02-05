namespace ModeleXamarin.modeles.sources.Modele
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// La liste des modèles pour une liste.
    /// </summary>
    /// <typeparam name="T">Le modele de la liste.</typeparam>
    public class ListeModeleDeroulante<T> : ListeModele<T>
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ListeModeleDeroulante"/>.
        /// </summary>
        /// <param name="hauteur_cellule">La hauteur d'une cellule de la liste.</param>
        /// <param name="hauteur_liste">La hauteur de la liste.</param>
        public ListeModeleDeroulante(IEnumerable<T> elements, int hauteur_cellule)
            :base(hauteur_cellule)
        {
            this.Elements = elements.ToList() ;
        }

    }
}
