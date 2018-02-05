namespace ModeleXamarin.metiers.api.patrouilleur
{
    using ModeleXamarin.couches.api.patrouilleur;
    using ModeleXamarin.modeles.cg77.Modele;
    using ModeleXamarin.modeles.patrouilleur.Modele;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SelectionRoutesMetier : PatrouilleurCoucheMetier
    {
        /// <summary>
        /// permet d'obtenir la liste des circuit disponible
        /// </summary>
        public IEnumerable<string> ObtenirCircuit()
        {
            return this.Donnees.BaseDeDonnees.Executer<string>("SELECT DISTINCT(RTE_CIRCUIT) FROM RTE_ROUTE");
        }

        /// <summary>
        /// permet d'obtenir la liste des itineraires disponibles
        /// </summary>
        public IEnumerable<ItineraireModele> ObtenirItineraire(string circuit, string centre)
        {
            return this.Donnees.BaseDeDonnees.Executer<ItineraireModele>(string.Format(
                "SELECT IT_PR_1 AS Abscisse1, IT_PR_2 AS Abscisse2, IT_VILLE AS Ville, IT_ROUTE AS Nom, IT_CENTRE AS Centre " +
                "FROM IT_ITINERAIRE, RTE_ROUTE " +
                "WHERE RTE_NOM = IT_ROUTE " +
                "AND RTE_CIRCUIT = '{0}'" +
                "AND IT_CENTRE = '{1}';", circuit, centre));
        }

        /// <summary>
        /// Permet l'ajout d'une nouvelle patrouille en base locale.
        /// </summary>
        /// <param name="patouilleur_1">Le premier patrouilleur de la patrouille.</param>
        /// <param name="patrouilleur_2">Le second patrouilleur de la patrouille.</param>
        /// <param name="itineraire">L'itinéraire sélectionné.</param>
        public void AjouterPatrouille(AgentModele patrouilleur_1, AgentModele patrouilleur_2, ItineraireModele itineraire, DateTime date)
        {
            // Insertion de la patrouille.
            this.Donnees.BaseDeDonnees.Executer(string.Format(
                "INSERT INTO PAT_PATROUILLE (PAT_DATE_DEBUT, PAT_PATROUILLEUR_1, PAT_PATROUILLEUR_2, PAT_PR_1, PAT_PR_2, PAT_VILLE, PAT_CENTRE, PAT_ROUTE) " +
                "VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')",
                date.ToString("yyyy/MM/dd HH:mm:ss"), patrouilleur_1.Matricule, patrouilleur_2.Matricule, itineraire.Abscisse1 , itineraire.Abscisse2, itineraire.Ville, itineraire.Centre, itineraire.Nom));
        }
    }
}
