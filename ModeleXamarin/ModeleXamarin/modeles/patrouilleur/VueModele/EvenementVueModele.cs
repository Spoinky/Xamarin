namespace ModeleXamarin.modeles.patrouilleur.VueModele
{
    using ModeleXamarin.couches.api.cg77;
    using ModeleXamarin.couches.api.patrouilleur;
    using ModeleXamarin.metiers.api.patrouilleur;
    using ModeleXamarin.modeles.cg77.Modele;
    using ModeleXamarin.modeles.cg77.VueModele;
    using ModeleXamarin.modeles.patrouilleur.Modele;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class EvenementVueModele : CG77VueModeleValidable<EvenementMetier, PatrouilleurCoucheDonnees, CG77CoucheConnexion>
    {

        #region variable privé
        private string _NumeroFiche { get; set; }

        private string _NomRoute { get; set; }

        private DateTime _DateEvenement { get; set; }

        private DateTime _HeureDebut { get; set; }

        private DateTime _HeureFin { get; set; }

        private string _NomPatrouilleur1 { get; set; }

        private string _NomPatrouilleur2 { get; set; }

        private string _MatriculePatrouilleur1 { get; set; }

        private string _MatriculePatrouilleur2 { get; set; }

        private string _Meteo { get; set; }

        private string _Abscisse1 { get; set; }

        private string _Abscisse2 { get; set; }

        private string _Section1 { get; set; }

        private string _Section2 { get; set; }

        private bool _EstUneSection { get; set; }

        private string _NomVille { get; set; }

        private string _NomCentre { get; set; }

        private bool _Agglomeration { get; set; }

        private string _Condition { get; set; }

        private string _EmplacementSelectionne { get; set; }

        private string _EvenementTypeSelectionne { get; set; }

        private string _EvenementDegradationSelectionne { get; set; }

        private string _EvenementDegradationAutre { get; set; }
        #endregion

        #region variable publique
        public string NumeroFiche
        {
            get
            {
                return _NumeroFiche;
            }
            set
            {
                _NumeroFiche = value;
            }
        }

        public string NomRoute
        {
            get
            {
                return _NomRoute;
            }
            set
            {
                _NomRoute = value;
            }
        }

        public DateTime DateEvenement
        {
            get
            {
                return _DateEvenement;
            }
            set
            {
                _DateEvenement = value;
            }
        }

        public DateTime HeureDebut
        {
            get
            {
                return _HeureDebut;
            }
            set
            {
                _HeureDebut = value;
            }
        }

        public DateTime HeureFin
        {
            get
            {
                return _HeureFin;
            }
            set
            {
                _HeureFin = value;
            }
        }

        public string NomPatrouilleur1
        {
            get
            {
                return _NomPatrouilleur1;
            }
            set
            {
                _NomPatrouilleur1 = value;
            }
        }

        public string NomPatrouilleur2
        {
            get
            {
                return _NomPatrouilleur2;
            }
            set
            {
                _NomPatrouilleur2 = value;
            }
        }

        public string MatriculePatrouilleur1
        {
            get
            {
                return _MatriculePatrouilleur1;
            }
            set
            {
                _MatriculePatrouilleur1 = value;
            }
        }

        public string MatriculePatrouilleur2
        {
            get
            {
                return _MatriculePatrouilleur2;
            }
            set
            {
                _MatriculePatrouilleur2 = value;
            }
        }

        public string MeteoSelectionne
        {
            get
            {
                return _Meteo;
            }
            set
            {
                _Meteo = value;
            }
        }

        public string Abscisse_1_Selectionne
        {
            get
            {
                return _Abscisse1;
            }
            set
            {
                _Abscisse1 = value;
                SurProprieteModifie("EstValidable");
            }
        }

        public string Abscisse_2_Selectionne
        {
            get
            {
                return _Abscisse2;
            }
            set
            {
                _Abscisse2 = value;
                SurProprieteModifie("EstValidable");
            }
        }

        public string Section_1_Selectionne
        {
            get
            {
                return _Section1;
            }
            set
            {
                _Section1 = value;
                SurProprieteModifie("EstValidable");
            }
        }

        public string Section_2_Selectionne
        {
            get
            {
                return _Section2;
            }
            set
            {
                _Section2 = value;
                SurProprieteModifie("EstValidable");
            }
        }

        public bool EstUneSectionSelectionne
        {
            get
            {
                return _EstUneSection;
            }
            set
            {
                _EstUneSection = value;
                SurProprieteModifie("EstUneSection");
                SurProprieteModifie("EstValidable");
            }
        }

        public string NomVille
        {
            get
            {
                return _NomVille;
            }
            set
            {
                _NomVille = value;
            }
        }

        public string NomCentre
        {
            get
            {
                return _NomCentre;
            }
            set
            {
                _NomCentre = value;
            }
        }

        public bool AgglomerationSelectionne
        {
            get
            {
                return _Agglomeration;
            }
            set
            {
                _Agglomeration = value;
            }
        }

        public string ConditionSelectionne
        {
            get
            {
                return _Condition;
            }
            set
            {
                _Condition = value;
            }
        }

        public string EmplacementSelectionne
        {
            get
            {
                return _EmplacementSelectionne;
            }
            set
            {
                _EmplacementSelectionne = value;
                SurProprieteModifie("EstValidable");
            }
        }

        public string EvenementTypeSelectionne
        {
            get
            {
                return _EvenementTypeSelectionne;
            }
            set
            {
                _EvenementTypeSelectionne = value;
                EvenementDegradationSelectionne = null;
                SurProprieteModifie("DegradationEvenementSelectionnable");
                SurProprieteModifie("ObtenirDegradationEvenement");
            }
        }

        public string EvenementDegradationSelectionne
        {
            get
            {
                return _EvenementDegradationSelectionne;
            }
            set
            {
                _EvenementDegradationSelectionne = value;
                EvenementDegradationAutre = null;
                SurProprieteModifie("AutreDegradationDisponible");
                SurProprieteModifie("EstValidable");
            }
        }

        public string EvenementDegradationAutre
        {
            get
            {
                return _EvenementDegradationAutre;
            }
            set
            {
                _EvenementDegradationAutre = value;
                SurProprieteModifie("EvenementDegradationAutre");
                SurProprieteModifie("EstValidable");
            }
        }
        #endregion

        #region METHODE QUENTIN
        public IEnumerable<string> ObtenirEmplacement
        {
            get
            {
                return this.Service.ObtenirEmplacement();
            }
        }

        public IEnumerable<string> ObtenirTypeEvenement
        {
            get
            {
                return this.Service.ObtenirTypeEvenement();
            }
        }

        public IEnumerable<string> ObtenirDegradationEvenement
        {
            get
            {
                if (EvenementTypeSelectionne != null)
                    return this.Service.ObtenirDegradationEvenement(EvenementTypeSelectionne);
                return new List<string>();
            }
        }

        public IEnumerable<string> ObtenirMeteo
        {
            get
            {
                return this.Service.ObtenirMeteo();
            }
        }

        private IEnumerable<ActionModele> _ObtenirLesActions
        {
            get
            {
                return this.Service.ObtenirAction();
            }
        }

        public IEnumerable<ActionModele> ObtenirLesActions
        {
            get; set;
        }

        public int ObtenirNombreDegradation
        {
            get
            {
                return this.Service.ObtenirNombreDegradation();
            }
        }

        public string ObtenirLeCodeDeLaVille
        {
            get
            {
                return this.Service.ObtenirCodeVille();
            }
        }

        public string ObtenirLeNomDeLaRoute
        {
            get
            {
                return this.Service.ObtenirNomRoute();
            }
        }

        public string ObtenirLeNomDeLaVille
        {
            get
            {
                return this.Service.ObtenirNomVille();
            }
        }

        public string ObtenirLeNomDuCentre
        {
            get
            {
                return this.Service.ObtenirNomCentre();
            }
        }

        public DateTime ObtenirDateDebutDeLaPatrouille
        {
            get
            {
                return this.Service.ObtenirDateDebutPatrouille();
            }
        }

        public string ObtenirLeNomDuPatroulleur1
        {
            get
            {
                return this.Service.ObtenirNomPatrouilleur1();
            }
        }

        public string ObtenirLeNomDuPatroulleur2
        {
            get
            {
                return this.Service.ObtenirNomPatrouilleur2();
            }
        }

        public string ObtenirLeMatriculeDuPatroulleur1
        {
            get
            {
                return this.Service.ObtenirMatriculePatrouilleur1();
            }
        }

        public string ObtenirLeMatriculeDuPatroulleur2
        {
            get
            {
                return this.Service.ObtenirMatriculePatrouilleur2();
            }
        }

        private string ObtenirIDFiche()
        {
            int nbFiche = ObtenirNombreDegradation + 1;

            return string.Format("{0}_{1}_{2}_{3}",
                DateTime.Now.ToString("yyyyMMdd"),
                ObtenirLeCodeDeLaVille,
                NomRoute,
                nbFiche.ToString("000")
                );
        }

        //public IEnumerable<string> Test
        //{
        //    get
        //    {
        //        return Service.ObtenirTest();
        //    }
        //}

        public string DegradationEvenementSelectionnable
        {
            get
            {
                return (!string.IsNullOrEmpty(EvenementTypeSelectionne)).ToString();
            }
        }

        public string EstUneSection
        {
            get
            {
                return EstUneSectionSelectionne.ToString();
            }
        }

        public string AutreDegradationDisponible
        {
            get
            {
                return (EvenementDegradationSelectionne == "Autre").ToString();
            }
        }

        #endregion

        public EvenementVueModele()
            : base("Patrouillage", "Fiche d'evenement")
        {
            NomRoute = ObtenirLeNomDeLaRoute;
            NomVille = ObtenirLeNomDeLaVille;
            DateEvenement = ObtenirDateDebutDeLaPatrouille;
            HeureDebut = DateTime.Now;
            NomPatrouilleur1 = ObtenirLeNomDuPatroulleur1;
            NomPatrouilleur2 = ObtenirLeNomDuPatroulleur2;
            MatriculePatrouilleur1 = ObtenirLeMatriculeDuPatroulleur1;
            MatriculePatrouilleur2 = ObtenirLeMatriculeDuPatroulleur2;
            NomCentre = ObtenirLeNomDuCentre;
            EstUneSectionSelectionne = false;
            AgglomerationSelectionne = false;

            NumeroFiche = ObtenirIDFiche();
        }

        public ICommand PagePrecedente { get; set; }

        #region PROPRIETE PATRICK

        /// <summary>
        /// L'identifiant de la fiche.
        /// </summary>
        private string _Identifiant { get; set; }

        /// <summary>
        /// La date de démarrage de l'incident.
        /// </summary>
        private DateTime _DateDebut { get; set; }

        /// <summary>
        /// La date de démarrage de l'incident.
        /// </summary>
        private DateTime DateDebut
        {
            set
            {
                this._DateDebut = value;
                this.SurProprieteModifie("DateDebutFiche");
                this.SurProprieteModifie("HeureDebutFiche");
            }
        }

        public IEnumerable<string> ObtenirAglomeration
        {
            get { return new List<string>() { "En Aglomeration", "Hors Aglomeration" }; }
        }

        /// <summary>
        /// Le premier patrouilleur de la patrouille.
        /// </summary>
        public AgentModele Patrouilleur1 { get; set; }

        /// <summary>
        /// Le second patrouilleur de la patrouille.
        /// </summary>
        public AgentModele Patrouilleur2 { get; set; }

        /// <summary>
        /// L'itinéraire de la patrouille.
        /// </summary>
        public ItineraireModele Itineraire { get; set; }

        /// <summary>
        /// La date de commencement de la patrouille.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// La date de démarrage de l'incident.
        /// </summary>
        public string DateDebutFiche
        {
            get
            {
                return this._DateDebut.ToString("dd-MM-yyyy");
            }
        }

        /// <summary>
        /// L'heure de démarrage de l'incident.
        /// </summary>
        public string HeureDebutFiche
        {
            get
            {
                return this._DateDebut.ToString("HH:mm");
            }
        }

        /// <summary>
        /// L'identifiant de la fiche.
        /// </summary>
        public string Identifiant
        {
            get
            {
                return this._Identifiant;
            }
            set
            {
                this._Identifiant = value;
                this.SurProprieteModifie("Identifiant");
            }
        }

        /// <summary>
        /// Permier point de localisation de l'incident.
        /// </summary>
        public decimal? Abscisse11 { get; set; }

        /// <summary>
        /// Second point de localisation de l'incident.
        /// </summary>
        public decimal? Abscisse12 { get; set; }

        /// <summary>
        /// Permier point de localisation de l'incident.
        /// </summary>
        public decimal? Abscisse21 { get; set; }

        /// <summary>
        /// Second point de localisation de l'incident.
        /// </summary>
        public decimal? Abscisse22 { get; set; }

        #endregion


        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="EvenementVueModele"/>.
        /// </summary>
        /// <param name="patrouilleur_1">Le premier patrouilleur de la patrouille.</param>
        /// <param name="patrouilleur_2">Le second patrouilleur de la patrouille.</param>
        /// <param name="itineraire">L'itinéraire de la patrouille.</param>
        /// <param name="date">La date de commencement de la patrouille.</param>
        public EvenementVueModele(AgentModele patrouilleur_1, AgentModele patrouilleur_2, ItineraireModele itineraire, DateTime date)
            : base("Patrouilleur", "Fiche Evènement")
        {
            // La patrouille.
            this.Patrouilleur1 = patrouilleur_1;
            this.Patrouilleur2 = patrouilleur_2;
            this.Itineraire = itineraire;
            this.Date = date;

            NomRoute = itineraire.Nom;
            NomVille = itineraire.Ville;
            DateEvenement = ObtenirDateDebutDeLaPatrouille;
            HeureDebut = DateTime.Now;
            NomPatrouilleur1 = Patrouilleur1.NomPrenom;
            NomPatrouilleur2 = Patrouilleur2.NomPrenom;
            MatriculePatrouilleur1 = Patrouilleur1.Matricule;
            MatriculePatrouilleur2 = Patrouilleur2.Matricule;
            NomCentre = itineraire.Centre;
            EstUneSectionSelectionne = false;
            AgglomerationSelectionne = false;
            ObtenirLesActions = _ObtenirLesActions;

            PagePrecedente = new Command(this.CommandPagePrecedente);

            NumeroFiche = string.Format("{0}-{1}-{2}-{3}", DateTime.Now.ToString("yyyyMMdd"), this.Service.ObtenirCodeVille(itineraire.Ville), itineraire.Nom, this.Service.ObtenirNumeroFiche());

            // La fiche.
            //this.DateDebut = DateTime.Now;
            //this.Identifiant = string.Format("{0}-{1}-{2}", DateTime.Now.ToString("yyyyMMdd"), this.Service.ObtenirCodeVille(itineraire.Ville), this.Service.ObtenirNumeroFiche());
        }

        private void CommandPagePrecedente()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        #region format d'affichage
        public string AffichageDate
        {
            get
            {
                return DateEvenement.ToString("dd/MM/yy");
            }
        }

        public string AffichageHeureDebut
        {
            get
            {
                return string.Format("{0}H{1}", HeureDebut.ToString("HH"), HeureDebut.ToString("mm"));
            }
        }

        public string AffichageHeureFin
        {
            get
            {
                if (HeureFin != null)
                {
                    return string.Format("{0}H{1}", HeureFin.ToString("HH"), HeureFin.ToString("mm"));
                }
                else
                {
                    return "";
                }
            }
        }
        #endregion

        public bool PositionDesordreValide
        {
            get
            {
                if (EstUneSectionSelectionne)
                {
                    return (!string.IsNullOrEmpty(Abscisse_1_Selectionne) && !string.IsNullOrEmpty(Abscisse_2_Selectionne) && !string.IsNullOrEmpty(Section_1_Selectionne) && !string.IsNullOrEmpty(Section_2_Selectionne));
                }
                else
                {
                    return (!string.IsNullOrEmpty(Abscisse_1_Selectionne) && !string.IsNullOrEmpty(Abscisse_2_Selectionne));
                }
            }
        }

        public bool DegradationValide
        {
            get
            {
                if (EvenementDegradationSelectionne == "Autre")
                {
                    return !string.IsNullOrEmpty(EvenementDegradationAutre);
                }
                else
                {
                    return !string.IsNullOrEmpty(EvenementDegradationSelectionne);
                }
            }
        }

        protected override bool _EstValidable
        {
            get
            {
                return (
                    PositionDesordreValide &&
                    !string.IsNullOrEmpty(AgglomerationSelectionne.ToString()) &&
                    !string.IsNullOrEmpty(MeteoSelectionne) &&
                    !string.IsNullOrEmpty(EmplacementSelectionne) &&
                    DegradationValide
                    );
            }
        }

        public override void Validation()
        {
            this.Service.AjouterEvenement(
                NumeroFiche, HeureDebut.ToString("yyyy/MM/dd HH:mm:ss"), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), Abscisse_1_Selectionne, Abscisse_2_Selectionne, AgglomerationSelectionne.ToString(),
                MatriculePatrouilleur1, MatriculePatrouilleur2, Itineraire.Abscisse1, Itineraire.Abscisse2, NomRoute, NomVille, NomCentre,
                MeteoSelectionne, EmplacementSelectionne,
                EvenementTypeSelectionne, EvenementDegradationSelectionne);

            if (EstUneSectionSelectionne)
            {
                this.Service.AjouterSection(Section_1_Selectionne, Section_2_Selectionne, NumeroFiche);
            }

            if (EvenementDegradationSelectionne == "Autre")
            {
                this.Service.AjouterCommentaireDegradation(EvenementDegradationAutre, NumeroFiche);
            }

            foreach (ActionModele Act in this.ObtenirLesActions)
            {
                if (Act.EstActif)
                    this.Service.AjouterIntervention(Act.NomAction, NumeroFiche);
            }

            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
