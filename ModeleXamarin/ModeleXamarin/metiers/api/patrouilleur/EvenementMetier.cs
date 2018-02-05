namespace ModeleXamarin.metiers.api.patrouilleur
{
    using ModeleXamarin.couches.api.patrouilleur;
    using ModeleXamarin.modeles.patrouilleur.Modele;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class EvenementMetier : PatrouilleurCoucheMetier
    {
        public IEnumerable<string> ObtenirEmplacement()
        {
            return this.Donnees.BaseDeDonnees.Executer<string>(
                "SELECT EMP_LIEU AS Lieu " +
                "FROM EMP_EMPLACEMENT");
        }

        public IEnumerable<string> ObtenirTypeEvenement()
        {
            return this.Donnees.BaseDeDonnees.Executer<string>(
                "SELECT DISTINCT(EVT_TYPE) " +
                "FROM EVT_EVENEMENT");
        }

        public IEnumerable<string> ObtenirDegradationEvenement(string TypeEvenement)
        {
            return this.Donnees.BaseDeDonnees.Executer<string>(string.Format(
                "SELECT EVT_DEGRADATION " +
                "FROM EVT_EVENEMENT " +
                "WHERE EVT_TYPE = '{0}'",
                TypeEvenement.Replace("'", "''")));
        }

        public IEnumerable<string> ObtenirMeteo()
        {
            return this.Donnees.BaseDeDonnees.Executer<string>(
                "SELECT MET_TEMPS " +
                "FROM MET_METEO "
                );
        }

        public IEnumerable<ActionModele> ObtenirAction()
        {
            return this.Donnees.BaseDeDonnees.Executer<ActionModele>(
                "SELECT ACT_NOM AS NomAction, 0 AS Active " +
                "FROM ACT_ACTION"
                );
        }

        public int ObtenirNombreDegradation()
        {
            return this.Donnees.BaseDeDonnees.Executer<int>(
                "SELECT COUNT(DES_ID) " +
                "FROM DES_DESORDRE").FirstOrDefault();
        }

        public string ObtenirCodeVille()
        {
            return this.Donnees.BaseDeDonnees.Executer<string>(
                "SELECT VIL_CODE " +
                "FROM VIL_VILLE, IT_ITINERAIRE, PAT_PATROUILLE " +
                "WHERE VIL_NOM = IT_VILLE " +

                "AND IT_PR_1 = PAT_PR_1 " +
                "AND IT_PR_2 = PAT_PR_2 " +
                "AND IT_ROUTE = PAT_ROUTE " +
                "AND IT_VILLE = PAT_VILLE " +
                "AND IT_CENTRE = PAT_CENTRE " +

                "AND PAT_DATE_FIN IS NULL"
                ).FirstOrDefault();
        }

        public string ObtenirNomRoute()
        {
            return this.Donnees.BaseDeDonnees.Executer<string>(
                "SELECT PAT_ROUTE " +
                "FROM PAT_PATROUILLE " +
                "WHERE PAT_DATE_FIN IS NULL"
                ).FirstOrDefault();
        }

        public string ObtenirNomVille()
        {
            return this.Donnees.BaseDeDonnees.Executer<string>(
                "SELECT PAT_VILLE " +
                "FROM PAT_PATROUILLE " +
                "WHERE PAT_DATE_FIN IS NULL"
                ).FirstOrDefault();
        }

        public string ObtenirNomCentre()
        {
            return this.Donnees.BaseDeDonnees.Executer<string>(
                "SELECT PAT_CENTRE " +
                "FROM PAT_PATROUILLE " +
                "WHERE PAT_DATE_FIN IS NULL"
                ).FirstOrDefault();
        }

        public string ObtenirPR1()
        {
            return this.Donnees.BaseDeDonnees.Executer<string>(
                "SELECT PAT_PR_1 " +
                "FROM PAT_PATROUILLE " +
                "WHERE PAT_DATE_FIN IS NULL"
                ).FirstOrDefault();
        }

        public string ObtenirPR2()
        {
            return this.Donnees.BaseDeDonnees.Executer<string>(
                "SELECT PAT_PR_1 " +
                "FROM PAT_PATROUILLE " +
                "WHERE PAT_DATE_FIN IS NULL"
                ).FirstOrDefault();
        }

        public DateTime ObtenirDateDebutPatrouille()
        {
            return this.Donnees.BaseDeDonnees.Executer<DateTime>(
                "SELECT PAT_DATE_DEBUT " +
                "FROM PAT_PATROUILLE " +
                "WHERE PAT_DATE_FIN IS NULL"
                ).FirstOrDefault();
        }

        public string ObtenirNomPatrouilleur1()
        {
            return this.Donnees.BaseDeDonnees.Executer<string>(
                "SELECT AGT_NOM_PRENOM " +
                "FROM PAT_PATROUILLE, AGT_AGENT " +
                "WHERE PAT_PATROUILLEUR_1 = AGT_MATRICULE " +
                "AND PAT_DATE_FIN IS NULL"
                ).FirstOrDefault();
        }

        public string ObtenirNomPatrouilleur2()
        {
            return this.Donnees.BaseDeDonnees.Executer<string>(
                "SELECT AGT_NOM_PRENOM " +
                "FROM PAT_PATROUILLE, AGT_AGENT " +
                "WHERE PAT_PATROUILLEUR_2 = AGT_MATRICULE " +
                "AND PAT_DATE_FIN IS NULL"
                ).FirstOrDefault();
        }

        public string ObtenirMatriculePatrouilleur1()
        {
            return this.Donnees.BaseDeDonnees.Executer<string>(
                "SELECT PAT_PATROUILLEUR_1 " +
                "FROM PAT_PATROUILLE " +
                "WHERE PAT_DATE_FIN IS NULL"
                ).FirstOrDefault();
        }

        public string ObtenirMatriculePatrouilleur2()
        {
            return this.Donnees.BaseDeDonnees.Executer<string>(
                "SELECT PAT_PATROUILLEUR_2 " +
                "FROM PAT_PATROUILLE " +
                "WHERE PAT_DATE_FIN IS NULL"
                ).FirstOrDefault();
        }

        public IEnumerable<string> ObtenirTest()
        {
            return this.Donnees.BaseDeDonnees.Executer<string>(
                "SELECT PAT_DATE_DEBUT " +
                "FROM PAT_PATROUILLE " +
                "WHERE PAT_DATE_FIN IS NULL"
                );
        }

        public void AjouterEvenement(
            string id, string dateDebut, string dateFin, string positionX, string positionY, string agglomeration,
            string patrouilleur1, string patrouilleur2, string abscisse1, string abscisse2, string nomRoute, string nomVille, string nomCentre, string meteo,
            //string condition,
            string emplacement, string typeEvenement, string DegradationEvenement)
        {
            this.Donnees.BaseDeDonnees.Executer(string.Format("select count(*) from des_desordre"));

            this.Donnees.BaseDeDonnees.Executer(string.Format(
                "INSERT INTO DES_DESORDRE (" +
                    "DES_ID, " +
                    "DES_DATE_DEBUT, " +
                    "DES_DATE_FIN, " +
                    "DES_X, " +
                    "DES_Y, " +
                    "DES_AGLO, " +

                    "DES_PATROUILLEUR_1, " +
                    "DES_PATROUILLEUR_2, " +
                    "DES_PR_1_PATROUILLE, " +
                    "DES_PR_2_PATROUILLE, " +
                    "DES_ROUTE_PATROUILLE, " +
                    "DES_VILLE_PATROUILLE, " +
                    "DES_CENTRE_PATROUILLE, " +

                    "DES_METEO, " +
                    //"DES_CONDITION, " +
                    "DES_EMPLACEMENT, " +

                    "DES_EVENEMENT_TYPE, " +
                    "DES_EVENEMENT_DEGRADATION) " +

                "VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}')",

                id, dateDebut, dateFin, positionX, positionY, agglomeration,

                patrouilleur1, patrouilleur2, abscisse1, abscisse2, nomRoute, nomVille, nomCentre,

                meteo,
                //condition,
                emplacement.Replace("'", "''"),

                typeEvenement.Replace("'", "''"), DegradationEvenement.Replace("'", "''")));

            this.Donnees.BaseDeDonnees.Executer(string.Format("select count(*) from des_desordre"));
        }

        public void AjouterSection(string sectionX, string sectionY, string IDDegradation)
        {
            this.Donnees.BaseDeDonnees.Executer(string.Format(
                "INSERT INTO SEC_SECTION " +
                "VALUES('{0}', '{1}', '{2}')",
                sectionX,
                sectionY,
                IDDegradation
                ));
        }

        public void AjouterCommentaireDegradation(string commentaire, string IDDesordre)
        {
            this.Donnees.BaseDeDonnees.Executer(string.Format(
                "INSERT INTO DCO_DEGRADATION_COMMENTAIRE " +
                "VALUES('{0}', '{1}')",
                commentaire, IDDesordre
                ));
        }

        public void AjouterIntervention(string action, string IDDesordre)
        {
            this.Donnees.BaseDeDonnees.Executer(string.Format(
                "INSERT INTO INT_INTERVENTION " +
                "VALUES('{0}', '{1}')",
                action.Replace("'", "''"), IDDesordre
                ));
        }

        #region PATRICK

        /// <summary>
        /// Récupère le code de la ville grâce à son nom.
        /// </summary>
        /// <param name="nom">Le nom de la ville.</param>
        /// <returns>Le code de la ville.</returns>
        internal string ObtenirCodeVille(string nom)
        {
            // Récupération du code de la ville par son nom.
            return this.Donnees.BaseDeDonnees.Executer<string>(string.Format("SELECT VIL_CODE FROM VIL_VILLE WHERE VIL_NOM = '{0}';", nom)).FirstOrDefault();
        }

        /// <summary>
        ///  Permet d'obtenir un numéro unique pour une fiche.
        /// </summary>
        /// <returns>Un numéro d'identifiant pour une fiche.</returns>
        public int ObtenirNumeroFiche()
        {
            return this.Donnees.BaseDeDonnees.Executer<int>("SELECT COUNT(*) FROM DES_DESORDRE;").FirstOrDefault() + 1;
        }
        #endregion
    }
}
