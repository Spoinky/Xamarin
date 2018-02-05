namespace ModeleXamarin.modeles.patrouilleur.VueModele
{
    using ModeleXamarin.couches.api.cg77;
    using ModeleXamarin.couches.api.patrouilleur;
    using ModeleXamarin.metiers.api.patrouilleur;
    using ModeleXamarin.modeles.cg77.Modele;
    using ModeleXamarin.modeles.cg77.VueModele;
    using ModeleXamarin.modeles.patrouilleur.Modele;
    using ModeleXamarin.modeles.sources.Modele;
    using ModeleXamarin.vues.patrouilleur;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    /// <summary>
    /// Classe modèle pour la la vue 'EtatPatrouillage.xaml'.
    /// </summary>
    public class EtatPatrouillageVueModele : CG77VueModele<EtatPatrouillageService, PatrouilleurCoucheDonnees, CG77CoucheConnexion>
    {
        /// <summary>
        /// Permet de signaler si la patrouille est en pause ou non.
        /// </summary>
        private bool _EstEnPause { get; set; }

        /// <summary>
        /// La date de la patrouille.
        /// </summary>
        private DateTime _Date { get; set; }

        /// <summary>
        /// La date de la patrouille.
        /// </summary>
        public string Date
        {
            get
            {
                return _Date.ToString("HH:mm");
            }
        }

        /// <summary>
        /// La liste des notification disponible pour un un etat de patrouille.
        /// </summary>
        public ListeModeleDeroulante<IMessageVueModele> Messages
        {
            get
            {
                return new ListeModeleDeroulante<IMessageVueModele>(this.Service.ObtenirMessages(Patrouilleur1, Patrouilleur2, Itineraire, _Date), 50);
            }
        }

        /// <summary>
        /// Le premier patrouilleur selectionné pour la patrouille.
        /// </summary>
        public AgentModele Patrouilleur1 { get; internal set; }

        /// <summary>
        /// Le second patrouilleur selectionné pour la patrouille.
        /// </summary>
        public AgentModele Patrouilleur2 { get; internal set; }

        /// <summary>
        /// L'itinéraire de la patrouille.
        /// </summary>
        public ItineraireModele Itineraire { get; internal set; }

        /// <summary>
        /// Permet de signaler si la patrouille est en pause ou non.
        /// </summary>
        public bool EstEnPause
        {
            get
            {
                return !this._EstEnPause;
            }
            internal set
            {
                if (this._EstEnPause != value)
                {
                    this._EstEnPause = value;
                    this.SurProprieteModifie("EstEnPause");
                    this.SurProprieteModifie("PeutReprendre");
                }

            }
        }

        /// <summary>
        /// Indique si la patrouille en pause peut reprendre.
        /// </summary>
        public bool PeutReprendre
        {
            get { return this._EstEnPause; }
        }

        /// <summary>
        /// Commande permettant d'aller à l'écran de création d'évènement.
        /// </summary>
        public ICommand CreerEvenement { get; set; }

        /// <summary>
        /// Commande permettant de mettre en pause la patrouille.
        /// </summary>
        public ICommand ArretPatrouille { get; set; }

        /// <summary>
        /// Commande permettant de reprendre la patrouille arreter.
        /// </summary>
        public ICommand ReprisePatrouille { get; set; }

        /// <summary>
        /// Commande permettant de mettre din à la patrouille.
        /// </summary>
        public ICommand FinItineraire { get; set; }

        /// <summary>
        /// Commande permettant de passer à la page précédente.
        /// </summary>
        public ICommand PagePrecedente { get; set; }

        /// <summary>
        /// L'heure de la page.
        /// </summary>
        public string Heure { get; set; }

        /// <summary>
        /// Intialise une nouvelle instance de la classe <see cref="EtatPatrouillageVueModele"/>.
        /// </summary>
        /// <param name="patrouilleur_1">Le patrouilleur n°1 de la patrouille</param>
        /// <param name="patrouilleur_2">Le patrouilleur n°2 de la patrouille</param>
        /// <param name="itineraire">L'itinéraire de la patrouille.</param>
        /// <param name="date">La date de la patrouille.</param>
        public EtatPatrouillageVueModele(AgentModele patrouilleur_1, AgentModele patrouilleur_2, ItineraireModele itineraire, DateTime date)
            : base("Patrouilleur", "EtatPatrouillage")
        {
            // Initialisation des propriété du modèle.
            this.Patrouilleur1 = patrouilleur_1;
            this.Patrouilleur2 = patrouilleur_2;
            this.Itineraire = itineraire;
            this.EstEnPause = false;
            this._Date = date;

            // Initialisation des commandes.
            this.FinItineraire = new Command(this.CommandFinItineraire);
            this.PagePrecedente = new Command(this.CommandPagePrecedente);
            this.CreerEvenement = new Command(this.CommandCreerEvenement);
            this.ArretPatrouille = new Command(this.CommandArreterPatrouille);
            this.ReprisePatrouille = new Command(this.CommandReprisePatrouille);

            // Initialisation de l'heure.
            // Modification de l'heure toutes les minutes.
            Device.StartTimer(TimeSpan.FromMinutes(1), this.SurTimer);
            // Intialisation de l'heure.
            this.SurTimer();
        }

        /// <summary>
        /// Permet de retourner sur la page de déclaration d'évènement de patrouillage.
        /// </summary>
        private void CommandCreerEvenement()
        {
            // Création et initialisation de la page.
            EvenementPatrouillage page_suivante = new EvenementPatrouillage();
            page_suivante.BindingContext = new EvenementVueModele(this.Patrouilleur1, this.Patrouilleur2, this.Itineraire, this._Date);

            Application.Current.MainPage.Navigation.PushAsync(page_suivante);
        }

        /// <summary>
        /// Commande permettant de mettre en pause une patrouille.
        /// </summary>
        private void CommandArreterPatrouille()
        {
            // Mise en pause de la patrouille.
            this.Service.PausePatrouille(this.Patrouilleur1, this.Patrouilleur2, this.Itineraire, this._Date);
            // Mise en 'pause' de la vue.
            this.EstEnPause = true;
            this.SurProprieteModifie("Messages");
        }

        /// <summary>
        /// Commande permettant de reprendre un patrouille mise en pause.
        /// </summary>
        private void CommandReprisePatrouille()
        {
            this.Service.ReprisePatrouille(this.Patrouilleur1, this.Patrouilleur2, this.Itineraire, this._Date);
            // Retrait de la mise en pause de la vue.
            this.EstEnPause = false;
            this.SurProprieteModifie("Messages");
        }

        /// <summary>
        /// Commande permettant de mettre fin à l'itinéraire.
        /// </summary>
        private void CommandFinItineraire()
        {
            // Mis en fin de l'itinréraire.
            this.Service.ItineraireFini(this.Patrouilleur1, this.Patrouilleur2, this.Itineraire, this._Date);
            Application.Current.MainPage.Navigation.PopAsync();
        }

        /// <summary>
        /// Methiode nécessaire pour le timer afficher l'heure sur la page.
        /// </summary>
        /// <returns>Un <see cref="bool"/> égale à <c>true</c></returns>
        private bool SurTimer()
        {
            DateTime maintenant = DateTime.Now;

            this.Heure = string.Format("{0}:{1}", maintenant.Hour.ToString("00"), maintenant.Minute.ToString("00"));
            this.SurProprieteModifie("Heure");

            return true;
        }

        /// <summary>
        /// Méthode permettant de passe sur la page précédente.
        /// </summary>
        private void CommandPagePrecedente()
        {
            // Mis en fin de l'itinréraire.
            this.Service.ItineraireFini(this.Patrouilleur1, this.Patrouilleur2, this.Itineraire, this._Date);
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
