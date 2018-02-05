namespace ModeleXamarin.metiers.api.patrouilleur
{
    using ModeleXamarin.couches.api.patrouilleur;
    using ModeleXamarin.modeles.cg77.Modele;
    using ModeleXamarin.modeles.patrouilleur.Modele;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Couche métier de la page 'EtatPatrouillage'.
    /// </summary>
    public class EtatPatrouillageService : PatrouilleurCoucheMetier
    {
        /// <summary>
        /// Met en pause une patrouille identifier par ses patrouilleur, son itinéraire et sa date.
        /// </summary>
        /// <param name="patrouilleur_1">Le patrouilleur n°1 de la patrouille.</param>
        /// <param name="patrouilleur_2">Le patrouilleur n°2 de la patrouille.</param>
        /// <param name="itineraire">L'itinéraire de la patrouille.</param>
        /// <param name="date">La date de la patrouille.</param>
        internal void PausePatrouille(AgentModele patrouilleur_1, AgentModele patrouilleur_2, ItineraireModele itineraire, DateTime date)
        {
            // Mise en pause de la patrouille par insertion dans la table PAU_PAUSE.
            this.Donnees.BaseDeDonnees.Executer(
                string.Format(
                    "INSERT INTO PAU_PAUSE(PAU_DEBUT, PAU_DATE_DEBUT_PATROUILLE, PAU_PR_1_PATROUILLE, PAU_PR_2_PATROUILLE, PAU_ROUTE_PATROUILLE, PAU_VILLE_PATROUILLE, PAU_CENTRE_PATROUILLE, PAU_PATROUILLEUR_1_PATROUILLE, PAU_PATROUILLEUR_2_PATROUILLE) " +
                    "VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}');"
                    , DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    date.ToString("yyyy/MM/dd HH:mm:ss"),
                    itineraire.Abscisse1,
                    itineraire.Abscisse2,
                    itineraire.Nom,
                    itineraire.Ville,
                    itineraire.Centre,
                    patrouilleur_1.Matricule,
                    patrouilleur_2.Matricule
                )
            );
        }

        internal int ObtenirNbPause(AgentModele patrouilleur_1, AgentModele patrouilleur_2, ItineraireModele itineraire, DateTime date)
        {
            return this.Donnees.BaseDeDonnees.Executer<int>(
                string.Format(
                        "select count(*) from pau_pause " +
                        "WHERE PAU_DATE_DEBUT_PATROUILLE = '{0}' " +
                        "AND PAU_PR_1_PATROUILLE = '{1}' " +
                        "AND PAU_PR_2_PATROUILLE = '{2}' " +
                        "AND PAU_ROUTE_PATROUILLE = '{3}' " +
                        "AND PAU_VILLE_PATROUILLE = '{4}' " +
                        "AND PAU_CENTRE_PATROUILLE = '{5}' " +
                        "AND PAU_PATROUILLEUR_1_PATROUILLE = '{6}' " +
                        "AND PAU_PATROUILLEUR_2_PATROUILLE = '{7}' " +
                    ";",
                    date.ToString("yyyy/MM/dd HH:mm:ss"),
                    itineraire.Abscisse1,
                    itineraire.Abscisse2,
                    itineraire.Nom,
                    itineraire.Ville,
                    itineraire.Centre,
                    patrouilleur_1.Matricule,
                    patrouilleur_2.Matricule)).FirstOrDefault();
        }

        /// <summary>
        /// Permet de récupérer la liste des message disponible pour l'état d'un patrouillage.
        /// </summary>
        /// <param name="patrouilleur_1">Le patrouilleur numéro 1.</param>
        /// <param name="patrouilleur_2">Le patrouilleur numéro 2.</param>
        /// <param name="itineraire">L'itinéraire de la patrouille.</param>
        /// <param name="date">La date de début de la patrouille.</param>
        /// <returns>La listes des informations consultables pour un itinéraire.</returns>
        internal IEnumerable<IMessageVueModele> ObtenirMessages(AgentModele patrouilleur_1, AgentModele patrouilleur_2, ItineraireModele itineraire, DateTime date)
        {
            // Etape1 Récupérer tout les désordre lié à l'itinéraire.
            IEnumerable<DesordreMessageVueModele> desordres_itineraire = this.Donnees.BaseDeDonnees.Executer<DesordreMessageVueModele>(
                string.Format(
                    "SELECT " +
                        "DES_ID AS Id, " +
                        "DES_ROUTE_ITINERAIRE AS NomRoute, " +
                        "DES_X AS PositionX, " +
                        "DES_Y AS PositionY, " +
                        "DES_EVENEMENT_DEGRADATION AS Constat " +
                    "FROM DES_DESORDRE " +
                        "WHERE DES_PR_1_ITINERAIRE = '{0}' " +
                        "AND DES_PR_2_ITINERAIRE = '{1}' " +
                        "AND DES_ROUTE_ITINERAIRE = '{2}' " +
                        "AND DES_VILLE_ITINERAIRE = '{3}' " +
                        "AND DES_CENTRE_ITINERAIRE = '{4}'" +
                    ";",
                    itineraire.Abscisse1,
                    itineraire.Abscisse2,
                    itineraire.Nom,
                    itineraire.Ville,
                    itineraire.Centre
                )
            );

            // Etape 2: Récupération des désordre lié à la patrouille.
            IEnumerable<DesordreMessageVueModele> desordres_patrouille = this.Donnees.BaseDeDonnees.Executer<DesordreMessageVueModele>(
                string.Format(
                    "SELECT " +
                        "DES_ID AS Id, " +
                        "DES_ROUTE_ITINERAIRE AS NomRoute, " +
                        "DES_X AS PositionX, " +
                        "DES_Y AS PositionY, " +
                        "DES_EVENEMENT_DEGRADATION AS Constat " +
                    "FROM DES_DESORDRE " +
                        "WHERE DES_PR_1_PATROUILLE = '{0}' " +
                        "AND DES_PR_2_PATROUILLE = '{1}' " +
                        "AND DES_ROUTE_PATROUILLE = '{2}' " +
                        "AND DES_VILLE_PATROUILLE = '{3}' " +
                        "AND DES_CENTRE_PATROUILLE = '{4}' " +
                        "AND DES_PATROUILLEUR_1 = '{5}' " +
                        "AND DES_PATROUILLEUR_2 = '{6}';" +
                    ";",
                    itineraire.Abscisse1,
                    itineraire.Abscisse2,
                    itineraire.Nom,
                    itineraire.Ville,
                    itineraire.Centre,
                    patrouilleur_1.Matricule,
                    patrouilleur_2.Matricule
                )
            );

            // Etape 3: Récupération des pauses.
            IEnumerable<PauseMessageVueModele> pause_patrouille = this.Donnees.BaseDeDonnees.Executer<PauseMessageVueModele>(
                string.Format(
                    "SELECT " +
                        "PAU_ROUTE_PATROUILLE AS NomRoute, " +
                        "PAU_DEBUT AS DateDebut, " +
                        "PAU_FIN AS DateFin " +
                    "FROM PAU_PAUSE " +
                        "WHERE PAU_DATE_DEBUT_PATROUILLE = '{0}' " +
                        "AND PAU_PR_1_PATROUILLE = '{1}' " +
                        "AND PAU_PR_2_PATROUILLE = '{2}' " +
                        "AND PAU_ROUTE_PATROUILLE = '{3}' " +
                        "AND PAU_VILLE_PATROUILLE = '{4}' " +
                        "AND PAU_CENTRE_PATROUILLE = '{5}' " +
                        "AND PAU_PATROUILLEUR_1_PATROUILLE = '{6}' " +
                        "AND PAU_PATROUILLEUR_2_PATROUILLE = '{7}' " +
                    ";",
                    date.ToString("yyyy/MM/dd HH:mm:ss"),
                    itineraire.Abscisse1,
                    itineraire.Abscisse2,
                    itineraire.Nom,
                    itineraire.Ville,
                    itineraire.Centre,
                    patrouilleur_1.Matricule,
                    patrouilleur_2.Matricule
                )
            );

            List<IMessageVueModele> resultat = new List<IMessageVueModele>();
            resultat.AddRange(desordres_itineraire);
            resultat.AddRange(desordres_patrouille);
            resultat.AddRange(pause_patrouille);

            return resultat;
        }

        /// <summary>
        /// Fini la pause d'une patrouille identifier par ses patrouilleurs, son itinéraire et sa date.
        /// </summary>
        /// <param name="patrouilleur_1">Le patrouilleur n°1 de la patrouille.</param>
        /// <param name="patrouilleur_2">Le patrouilleur n°2 de la patrouille.</param>
        /// <param name="itineraire">L'itinéraire de la patrouille.</param>
        /// <param name="date">La date de la patrouille.</param>
        internal void ReprisePatrouille(AgentModele patrouilleur_1, AgentModele patrouilleur_2, ItineraireModele itineraire, DateTime date)
        {
            // Mise à jour de la données.
            this.Donnees.BaseDeDonnees.Executer(
                string.Format(
                    "UPDATE PAU_PAUSE SET PAU_FIN = '{0}' " +
                    "WHERE PAU_DATE_DEBUT_PATROUILLE = '{1}' " +
                    "AND PAU_PR_1_PATROUILLE = '{2}' " +
                    "AND PAU_PR_2_PATROUILLE = '{3}' " +
                    "AND PAU_ROUTE_PATROUILLE = '{4}' " +
                    "AND PAU_VILLE_PATROUILLE = '{5}' " +
                    "AND PAU_CENTRE_PATROUILLE = '{6}' " +
                    "AND PAU_PATROUILLEUR_1_PATROUILLE = '{7}' " +
                    "AND PAU_PATROUILLEUR_2_PATROUILLE = '{8}' " +
                    "AND PAU_FIN IS NULL;",
                    DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    date.ToString("yyyy/MM/dd HH:mm:ss"),
                    itineraire.Abscisse1,
                    itineraire.Abscisse2,
                    itineraire.Nom,
                    itineraire.Ville,
                    itineraire.Centre,
                    patrouilleur_1.Matricule,
                    patrouilleur_2.Matricule
                )
            );
        }

        /// <summary>
        /// Fini la patrouille en cours.
        /// </summary>
        /// <param name="patrouilleur_1">Le patrouilleur n°1 de la patrouille.</param>
        /// <param name="patrouilleur_2">Le patrouilleur n°2 de la patrouille.</param>
        /// <param name="itineraire">L'itinéraire de la patrouille.</param>
        /// <param name="date">La date de la patrouille.</param>
        internal void ItineraireFini(AgentModele patrouilleur_1, AgentModele patrouilleur_2, ItineraireModele itineraire, DateTime date)
        {
            this.Donnees.BaseDeDonnees.Executer(
                string.Format(
                    "UPDATE PAT_PATROUILLE " +
                    "SET PAT_DATE_FIN = '{0}' " +
                    "WHERE PAT_DATE_DEBUT = '{1}' " +
                    "AND PAT_PR_1 = '{2}' " +
                    "AND PAT_PR_2 = '{3}' " +
                    "AND PAT_ROUTE = '{4}' " +
                    "AND PAT_VILLE = '{5}' " +
                    "AND PAT_CENTRE = '{6}' " +
                    "AND PAT_PATROUILLEUR_1= '{7}' " +
                    "AND PAT_PATROUILLEUR_2 = '{8}' " +
                    "AND PAT_DATE_FIN IS NULL;",
                    DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    date.ToString("yyyy/MM/dd HH:mm:ss"),
                    itineraire.Abscisse1,
                    itineraire.Abscisse2,
                    itineraire.Nom,
                    itineraire.Ville,
                    itineraire.Centre,
                    patrouilleur_1.Matricule,
                    patrouilleur_2.Matricule
                )
            );
        }
    }
}
