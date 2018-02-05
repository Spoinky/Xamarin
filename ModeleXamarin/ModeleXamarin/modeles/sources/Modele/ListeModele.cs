namespace ModeleXamarin.modeles.sources.Modele
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// La liste des modèles pour une liste.
    /// </summary>
    /// <typeparam name="T">Le modele de la liste.</typeparam>
    public abstract class ListeModele<T>
    {
        /// <summary>
        /// La liste des éléments du modèle.
        /// </summary>
        public List<T> Elements { get; set; }

        /// <summary>
        /// La hauteur d'une cellule.
        /// </summary>
        protected int HauteurCellule { get; set; }

        /// <summary>
        /// La hauteur de la liste sur la vue.
        /// </summary>
        protected int HauteurListe {
            get
            {
                return (Elements.Count * HauteurCellule);
            }
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ListeModele"/>.
        /// </summary>
        /// <param name="hauteur_cellule">La hauteur d'une cellule de la liste.</param>
        public ListeModele(int hauteur_cellule)
        {
            this.Elements = new List<T>();
            this.HauteurCellule = hauteur_cellule;
        }
    }
}
