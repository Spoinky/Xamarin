namespace ModeleXamarin.modeles.sources.VueModele.validation
{
    using ModeleXamarin.couches.sources;
    using ModeleXamarin.modeles.sources.Modele;
    using ModeleXamarin.modeles.sources.VueModele.attributs.conditions;
    using ModeleXamarin.modeles.sources.VueModele.attributs.conditions.attributs;
    using ModeleXamarin.modeles.sources.VueModele.attributs.exceptions;
    using ModeleXamarin.modeles.sources.VueModele.interfaces;
    using ModeleXamarin.sources.couches;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows.Input;
    using Xamarin.Forms;

    /// <summary>
    /// Classe définissant les action de l'interface <see cref="IVueModeleValidable"/>.
    /// </summary>
    public abstract class VueModeleValidable<T, U, V> : VueModele<T, U, V>, IVueModeleValidable
        where V : CoucheConnexion
        where U : CoucheDonnees
        where T : CoucheMetier<U, V>
    {
        /// <summary>
        /// La liste des message d'erreurs du modèle.
        /// </summary>
        public ListeModele<string> MessageErreur { get; set; }

        /// <summary>
        /// Indique si le modèle est validable.
        /// </summary>
        protected abstract bool _EstValidable { get; }

        /// <summary>
        /// Indique si le modèle est validable.
        /// </summary>
        public string EstValidable
        {
            get
            {
                return _EstValidable.ToString();
            }
        }

        /// <summary>
        /// Indique si la vue moèle est en erreur.
        /// </summary>
        public string EstEnErreur
        {
            get
            {
                // Vérification si il y a des messages d'erreurs en mémoire.
                if (MessageErreur.Elements.Count == 0)
                    return false.ToString();
                // Si 'il n'y a aucun message alors le modèle n'est pas en erreur.
                return true.ToString();
            }
        }

        /// <summary>
        /// Valide une vue modèle.
        /// </summary>
        /// <returns><c>true</c> si la validation c'est déroulé avec succes; sinon <c>false</c>.</returns>
        public ICommand Valider { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="VueModeleValidable<T, U, V>"/>.
        /// </summary>
        /// <param name="titre">Le titre de l'application.</param>
        public VueModeleValidable(string titre)
            : base(titre)
        {
            this.MessageErreur = new ListeModeleErreur<string>(50);
            this.Valider = new Command(this.SurValidation);
        }

        /// <summary>
        /// Valide une vue modèle.
        /// </summary>
        /// <returns><c>true</c> si la validation c'est déroulé avec succes; sinon <c>false</c>.</returns>
        private void SurValidation()
        {
            // Mise à zéro de la liste des messages.
            this.MessageErreur.Elements = new List<string>();

            // Parcours de toutes les propriétés du model.
            foreach (PropertyInfo propriete in this.GetType().GetProperties())
            {
                // Récupération de tout les attribut de condition.
                foreach (object attribut in this.ObtenirConditionsPropriete(propriete))
                {
                    try
                    {
                        // Teste de la condition.
                        ((ICondition)attribut).Tester(this);
                    }
                    catch (ConditionException e)
                    {
                        // En cas d'erreur ajout du message.
                        this.MessageErreur.Elements.Add(e.Message);
                    }
                }
            }

            // 4. Si EstEnErreur est vrai lancé le changement de propriété sur 'MessageErreur' et 'EstEnErreur'.
            this.SurProprieteModifie("MessageErreur");
            this.SurProprieteModifie("EstEnErreur");

            // Vérification si il y a possibilité de validation.
            if (MessageErreur.Elements.Count == 0)
                this.Validation();
        }

        /// <summary>
        /// Permet d'obtenir les attribut d'une propriété de classe.
        /// </summary>
        /// <param name="propriete">La propriété dont on essaie de récupérer les attributs de type <see cref="ConditionPropriete"/>.</param>
        /// <returns>Un tableau d'objets.</returns>
        private object[] ObtenirConditionsPropriete(PropertyInfo propriete)
        {
            object[] comparaison = propriete.GetCustomAttributes(typeof(ConditionComparaison), false);
            object[] differente_autre_propriete = propriete.GetCustomAttributes(typeof(ConditionDifferenteAutrePropriete), false);
            object[] obligatoire = propriete.GetCustomAttributes(typeof(ConditionObligatoire), false);

            // Préparation du résultat.
            object[] resultat = new object[comparaison.Length + differente_autre_propriete.Length + obligatoire.Length];

            // Copie des attributs de type ProprieteComparaison
            comparaison.CopyTo(resultat, 0);
            // Copie des attributs de type ProprieteDifferenteAutrePropriete
            differente_autre_propriete.CopyTo(resultat, comparaison.Length);
            // Copie des attributs de type ProprieteObligatoire
            obligatoire.CopyTo(resultat, comparaison.Length + differente_autre_propriete.Length);

            return resultat;
        }

        /// <summary>
        /// Méthode permettant effectuer les opérations après validation du modèle.
        /// </summary>
        public abstract void Validation();
    }
}
