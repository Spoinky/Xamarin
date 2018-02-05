namespace ModeleXamarin.modeles.sources.Modele
{
    using System.Collections.Generic;

    /// <summary>
    /// La liste des modèles pour une liste.
    /// </summary>
    /// <typeparam name="T">Le modele de la liste.</typeparam>
    public class ListeModeleErreur<T> : ListeModele<T>
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ListeModeleErreur"/>.
        /// </summary>
        /// <param name="hauteur_cellule">La hauteur d'une cellule de la liste.</param>
        public ListeModeleErreur(int hauteur_cellule)
            :base(hauteur_cellule)
        {
            this.Elements = new List<T>();
        }
    }
}
