namespace ModeleXamarin.modeles.patrouilleur.VueModele
{
    using ModeleXamarin.couches.api.cg77;
    using ModeleXamarin.couches.api.patrouilleur;
    using ModeleXamarin.metiers.api.patrouilleur;
    using ModeleXamarin.modeles.cg77.Modele;
    using ModeleXamarin.modeles.cg77.VueModele;
    using ModeleXamarin.modeles.patrouilleur.Modele;
    using ModeleXamarin.modeles.sources.VueModele.attributs.conditions.attributs;
    using ModeleXamarin.vues.patrouilleur;
    using System;
    using System.Collections.Generic;
    using Xamarin.Forms;

    /// <summary>
    /// Classe modèle pour la page SelectionRoutes.
    /// </summary>
    public class SelectionRoutesVueModele : CG77VueModeleValidable<SelectionRoutesMetier, PatrouilleurCoucheDonnees, CG77CoucheConnexion>
    {
        /// <summary>
        /// le centre selectionné par le client
        /// </summary>
        private string _CentreSelectionne { get; set; }

        /// <summary>
        /// le circuit selectionné par le client
        /// </summary>
        private string _CircuitSelectionne { get; set; }

        /// <summary>
        /// le patrouilleur n°1 selectionné par le client
        /// </summary>
        private AgentModele _Patrouilleur1 { get; set; }

        /// <summary>
        /// le patrouilleur n°2 selectionné par le client
        /// </summary>
        private AgentModele _Patrouilleur2 { get; set; }

        /// <summary>
        /// l'itineraire selectionné par le client
        /// </summary>
        private ItineraireModele _ItineraireSelectionnee { get; set; }

        /// <summary>
        /// le patrouilleur n°1 selectionné par le client
        /// </summary>
        public AgentModele Patrouilleur1
        {
            get
            {
                return _Patrouilleur1;
            }
            set
            {
                _Patrouilleur1 = value;
            }
        }

        /// <summary>
        /// le patrouilleur n°2 selectionné par le client
        /// </summary>
        public AgentModele Patrouilleur2
        {
            get
            {
                return _Patrouilleur2;
            }
            set
            {
                _Patrouilleur2 = value;
            }
        }

        /// <summary>
        /// le centre selectionné par le client
        /// </summary>
        public string CentreSelectionne
        {
            get
            {
                return _CentreSelectionne;
            }
            set
            {
                _CentreSelectionne = value;
                SurProprieteModifie("CircuitsDisponibles");
            }
        }

        /// <summary>
        /// le circuit selectionné par le client
        /// </summary>
        [ConditionObligatoire("CircuitSelectionne", "Circuit", "Radio")]
        public string CircuitSelectionne
        {
            get
            {
                return _CircuitSelectionne;
            }
            set
            {
                if (value == null || _CircuitSelectionne != value)
                {
                    // Modificaion des valeurs.
                    _CircuitSelectionne = value;
                    ItineraireSelectionnee = null;

                    if (CircuitSelectionne != null)
                        SurProprieteModifie("ItineraireDisponibles");
                }
            }
        }

        /// <summary>
        /// l'itineraire selectionné par le client
        /// </summary>
        //Condition qui indique que ce champs est obligatoir
        [ConditionObligatoire("ItineraireSelectionnee", "Patrouille", "Radio")]
        public ItineraireModele ItineraireSelectionnee
        {
            get
            {
                return _ItineraireSelectionnee;
            }
            set
            {
                _ItineraireSelectionnee = value;
                SurProprieteModifie("ItineraireSelectionable");
                SurProprieteModifie("EstValidable");
            }
        }

        /// <summary>
        /// définit si la selection de l'itinéraire est possible sur la vue
        /// </summary>
        public string ItineraireSelectionable
        {
            get
            {
                return (!string.IsNullOrEmpty(CircuitSelectionne)).ToString();
            }
        }

        /// <summary>
        /// indique si le modèle est validable.
        /// </summary>
        protected override bool _EstValidable
        {
            get
            {
                return (ItineraireSelectionnee != null && CircuitSelectionne != null);
            }
        }

        /// <summary>
        /// donne la liste des circuits disponibles
        /// </summary>
        public IEnumerable<string> CircuitsDisponibles
        {
            get
            {
                return Service.ObtenirCircuit();
            }
        }

        /// <summary>
        /// donne la liste des itinéraires disponibles
        /// </summary>
        public IEnumerable<ItineraireModele> ItineraireDisponibles
        {
            get
            {
                return Service.ObtenirItineraire(CircuitSelectionne, CentreSelectionne);
            }
        }

        /// <summary>
        /// Intialise une nouvelle instance de la classe <see cref="SelectionRoutesVueModele"/>.
        /// </summary>
        /// <param name="centre">Le centre de l'agent.</param>
        /// <param name="Patrouilleur_1">Le premier patrouilleur de la patrouille en instance de classe <see cref="AgentModele"/>.</param>
        /// <param name="Patrouilleur_2">Le second patrouilleur de la patrouille en instance de classe <see cref="AgentModele"/>.</param>
        public SelectionRoutesVueModele(string centre, AgentModele Patrouilleur_1, AgentModele Patrouilleur_2)
            : base("Patrouillage", "Seletion des routes")
        {
            CentreSelectionne = centre;
            Patrouilleur1 = Patrouilleur_1;
            Patrouilleur2 = Patrouilleur_2;

            CircuitSelectionne = null;
        }

        /// <summary>
        /// methode appelée lors de la validation du formulaire
        /// </summary>
        public override void Validation()
        {
            // Récuperation de la date de validations.
            DateTime date = DateTime.Now;

            // Enregistrement de la patrouille.
            Service.AjouterPatrouille(Patrouilleur1,Patrouilleur2, ItineraireSelectionnee, date);

            // Préparation du modèle à fournir à la page suivante.
            EtatPatrouillageVueModele modele = new EtatPatrouillageVueModele(Patrouilleur1, Patrouilleur2, ItineraireSelectionnee,   date);
            Page page_suivante = new EtatPatrouillage();
            page_suivante.BindingContext = modele;

            // Passage à la page 'Etat de patrouille'
            Application.Current.MainPage.Navigation.PushAsync(page_suivante);
        }
    }
}
