namespace ModeleXamarin.metiers.api.patrouilleur
{
    using ModeleXamarin.couches.api.patrouilleur;
    using ModeleXamarin.modeles.cg77.Modele;
    using System.Collections.Generic;

    /// <summary>
    /// Classe métier pour la page Identification
    /// </summary>
    public class IdentificationService : PatrouilleurCoucheMetier
    {
        /// <summary>
        /// Request de récupération de tout les ART (Agence) de la base de donnée
        /// </summary>
        /// <returns>Un objet IEnumerable<string> du résultat de la request</returns>
        public IEnumerable<string> ObtenirARTDisponible()
        {
            return this.Donnees.BaseDeDonnees.Executer<string>("SELECT ART_NOM AS Agence FROM ART_AGENCE;");
        }

        /// <summary>
        /// Request de récupération de tout les centres de la base de donnée pour une agence
        /// </summary>
        /// <param name="agence">L'agence dont on veux les centres</param>
        /// <returns>Un objet IEnumerable<string> du résultat de la request</returns>
        public IEnumerable<string> ObtenirNomCentreDisponible(string agence)
        {
            // Vérification des pré-condition.
            // Vérification que l'agence est été renseignée.
            if (string.IsNullOrEmpty(agence))
                return new List<string>();

            // Récupération des éléments de la requête.
            return this.Donnees.BaseDeDonnees.Executer<string>(string.Format("SELECT CTR_NOM FROM CTR_CENTRE WHERE CTR_AGENCE = '{0}';", agence));
        }

        /// <summary>
        /// Request de récupération de tout les agents de la base de donnée liée à un centre
        /// </summary>
        /// <param name="centre">Le centre dont on veux les agents</param>
        /// <returns><returns>Un objet IEnumerable<AgentModele> du résultat de la reques</returns>
        public IEnumerable<AgentModele> ObtenirAgentsDisponibleCentre(string centre)
        {
            // Vérification des pré-condition.
            // Vérification que l'agence est été renseignée.
            if (string.IsNullOrEmpty(centre))
                return new List<AgentModele>();

            // Récupération des résultats de la requête.
            return this.Donnees.BaseDeDonnees.Executer<AgentModele>(string.Format("SELECT AGT_MATRICULE AS Matricule, AGT_NOM_PRENOM as NomPrenom, AGT_CIVILITE as Civilite FROM AGT_AGENT WHERE AGT_CENTRE = '{0}';", centre));
        }
    }
}
