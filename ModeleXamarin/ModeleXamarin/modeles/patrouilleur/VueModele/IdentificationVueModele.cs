namespace ModeleXamarin.modeles.patrouilleur.VueModele
{
    using ModeleXamarin.couches.api.cg77;
    using ModeleXamarin.couches.api.patrouilleur;
    using ModeleXamarin.metiers.api.patrouilleur;
    using ModeleXamarin.modeles.cg77.Modele;
    using ModeleXamarin.modeles.cg77.VueModele;
    using ModeleXamarin.modeles.sources.VueModele.attributs.conditions.attributs;
    using ModeleXamarin.modeles.sources.VueModele.attributs.conditions.meta;
    using ModeleXamarin.vues.patrouilleur;
    using System.Collections.Generic;
    using Xamarin.Forms;

    /// <summary>
    /// Classe modèle pour la page Identification
    /// </summary>
    public class IdentificationVueModele : CG77VueModeleValidable<IdentificationService, PatrouilleurCoucheDonnees, CG77CoucheConnexion>
    {
        #region Variables
        /// <summary>
        /// Indique si l'utilisateur peux selectionner un centre
        /// </summary>
        public bool CentreEstDisponible
        {
            get
            {
                //Verifie si une agence est selectionner
                return !string.IsNullOrEmpty(AgenceSelectionne);
            }
        }

        /// <summary>
        /// Indique si l'utilisateur peux selectionner les agents
        /// </summary>
        public bool AgentEstDisponible
        {
            get
            {
                //Vérifie si un centre est selectionner
                return (!string.IsNullOrEmpty(CentreSelectionne));
            }
        }

        //Les variables locale et privé
        #region Locale
        /// <summary>
        /// L'Agence selectionner par l'utilisateur
        /// </summary>
        private string _AgenceSelectionne { get; set; }

        /// <summary>
        /// Le centre selectionner par l'utilisateur
        /// </summary>
        private string _CentreSelectionne { get; set; }

        /// <summary>
        /// Le premier patrouilleur selectionner par l'utilisateur
        /// </summary>
        private AgentModele _Patrouilleur1 { get; set; }

        /// <summary>
        /// Le second patrouilleur selectionner par l'utilisateur
        /// </summary>
        private AgentModele _Patrouilluer2 { get; set; }
        #endregion

        //Les variables récupérer depuis le fichier métier
        #region Recup Données
        /// <summary>
        /// Récupération de toutes les agences de la base de donnée
        /// </summary>
        public IEnumerable<string> ObtenirLesAgences
        {
            get
            {
                return Service.ObtenirARTDisponible();
            }
        }

        /// <summary>
        /// Récupération de les centres liés à l'agence selectionner par l'utilisateur 
        /// L'agence est envoyé en paramétre
        /// </summary>
        public IEnumerable<string> ObtenirLesCentres
        {
            get
            {
                return Service.ObtenirNomCentreDisponible(_AgenceSelectionne);
            }
        }

        /// <summary>
        /// Récupération des agents liés au centre selectionner par l'utilisateur
        /// Le centre est envoyé en paramétre
        /// </summary>
        public IEnumerable<AgentModele> ObtenirLesAgents
        {
            get
            {
                return Service.ObtenirAgentsDisponibleCentre(_CentreSelectionne);
            }
        }

        #endregion

        /// <summary>
        /// Séléction de l'agence 
        /// </summary>
        public string AgenceSelectionne
        {
            //Retourne l'agence selectionner
            get
            {
                return this._AgenceSelectionne;
            }
            //Modification de l'agence
            set
            {
                //Mise à jour de la valeur
                _AgenceSelectionne = value;
                this.CentreSelectionne = null;

                //Mise à jour de l'interface avec les modifications
                //Recharge les centres liés au changement
                SurProprieteModifie("ObtenirLesCentres");
                //Indique que le centre peut étre selctionner
                SurProprieteModifie("CentreEstDisponible");
            }
        }

        /// <summary>
        /// Séléction du centre
        /// </summary>
        public string CentreSelectionne
        {
            //Retourne le centre selectionner
            get
            {
                return this._CentreSelectionne;
            }
            //Modification du centre
            set
            {
                //Mise à jour de la valeur
                _CentreSelectionne = value;

                //Met les champs de selection des patrouilleurs à null (les vident) 
                PatrouilleurSelectionner1 = null;
                PatrouilleurSelectionner2 = null;

                //Mise à jour de l'interface avec les modifications
                //Recharge les agents avec le nouveu centre
                SurProprieteModifie("ObtenirLesAgents");
                //Indique les agents peuvent étre selectionner
                SurProprieteModifie("AgentEstDisponible");
            }
        }

        /// <summary>
        /// Séléction du patrouilleur 1
        /// </summary>
        [ConditionObligatoire("PatrouilleurSelectionner1", "Patrouilleur 1", "Radio")]
        [ConditionDifferenteAutrePropriete("PatrouilleurSelectionner1", "Patrouilleur 1", "PatrouilleurSelectionner2")]
        public AgentModele PatrouilleurSelectionner1
        {
            get
            {
                return this._Patrouilleur1;
            }
            set
            {
                //Mise à jour de la valuer
                this._Patrouilleur1 = value;

                //Mise à jour de la vue
                SurProprieteModifie("EstValidable");
            }
        }

        /// <summary>
        /// Selection patrouilleur 2
        /// </summary>
        [Comparable("Patrouilleur 2")]
        public AgentModele PatrouilleurSelectionner2
        {
            get
            {
                return this._Patrouilluer2;
            }
            set
            {
                //Mise à jour de la valeur
                this._Patrouilluer2 = value;

                //Mise à jour de la vue
                SurProprieteModifie("EstValidable");
            }
        }

        #endregion

        /// <summary>
        /// Constructeur
        /// </summary>
        public IdentificationVueModele()
                : base("Patrouilleur", "Identification")
        {
            //Met à null l'agence et le centre pour mettre le formulaire a zero
            AgenceSelectionne = null;
            CentreSelectionne = null;
            this.PatrouilleurSelectionner1 = null;
            this.PatrouilleurSelectionner2 = null;
        }

        /// <summary>
        /// Vérifie que l'utilisateur peux valider la selection
        /// </summary>
        protected override bool _EstValidable
        {
            //Retourne vrai seulement si un Patrouileur est selectonné
            get
            {
                if (PatrouilleurSelectionner1 != null || PatrouilleurSelectionner2 != null)
                    return true;
                return false;
            }
        }

        /// <summary>
        /// Si la selection de l'utilisateur est valider par la fonction _EstValidable
        /// </summary>
        public override void Validation()
        {
            SelectionRoutes page_suivante = new SelectionRoutes();
            page_suivante.BindingContext = new SelectionRoutesVueModele(CentreSelectionne, this.PatrouilleurSelectionner1, this.PatrouilleurSelectionner2);
            Application.Current.MainPage.Navigation.PushAsync(page_suivante);
        }
    }
}
