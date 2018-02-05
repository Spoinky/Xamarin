namespace ModeleXamarin.couches.api.patrouilleur
{
    using ModeleXamarin.couches.api.cg77;

    /// <summary>
    /// Couche de données pour le projet 
    /// </summary>
    public class PatrouilleurCoucheDonnees : CG77CoucheDonnees
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="PatrouilleurCoucheDonnees"/>.
        /// </summary>
        public PatrouilleurCoucheDonnees(string nom_fichier)
            : base(nom_fichier)
        {

            #region ART_AGENCE
            this.BaseDeDonnees.RequetesDeCreation.Add(
                "CREATE TABLE ART_AGENCE(" +
                    "ART_NOM NVARCHAR(50) NOT NULL," +
                    "CONSTRAINT PK_AGENCE PRIMARY KEY(ART_NOM)" +
                ");"
            );
            #endregion

            #region Création de la table CTR_CENTRE
            this.BaseDeDonnees.RequetesDeCreation.Add(
                "CREATE TABLE CTR_CENTRE(" +
                    "CTR_NOM NVARCHAR(50) NOT NULL, " +
                    "CTR_AGENCE NVARCHAR(50) NOT NULL, " +

                    "CONSTRAINT PK_CENTRE " +
                        "PRIMARY KEY(CTR_NOM), " +

                    "CONSTRAINT FK_CENTRE_AGENCE " +
                        "FOREIGN KEY(CTR_AGENCE) " +
                        "REFERENCES ART_AGENCE(ART_NOM)" +
                        "ON DELETE CASCADE ON UPDATE CASCADE" +
                ");"
            );
            #endregion

            #region Création de la table AGT_AGENT
            // Modification de la table AGT_AGENT.
            // Suppression de la table AFT_AGENT.
            this.BaseDeDonnees.RequetesDeCreation.Add("DROP TABLE AGT_AGENT;");
            // Re-création de la table AGT_AGENT avesc les propriétés nécessairess.
            this.BaseDeDonnees.RequetesDeCreation.Add(
                "CREATE TABLE AGT_AGENT(" +
                    "AGT_MATRICULE NVARCHAR(8) NOT NULL, " +
                    "AGT_NOM_PRENOM NVARCHAR(100) NOT NULL, " +
                    "AGT_CIVILITE NVARCHAR(4) NOT NULL, " +

                    "AGT_CENTRE NVARCHAR(50) NOT NULL, " +
                    "AGT_AGENCE NVARCHAR(50) NOT NULL, " +

                    "CONSTRAINT PK_AGENT PRIMARY KEY(AGT_MATRICULE), " +

                    "CONSTRAINT FK_AGENT_CENTRE " +
                        "FOREIGN KEY(AGT_CENTRE,AGT_AGENCE) " +
                        "REFERENCES CTR_CENTRE(CTR_NOM, CTR_AGENCE) " +
                        "ON DELETE CASCADE ON UPDATE CASCADE " +
                ");");
            #endregion

            #region Création de la table VIL_VILLE
            this.BaseDeDonnees.RequetesDeCreation.Add(
                "CREATE TABLE VIL_VILLE(" +
                    "VIL_NOM NVARCHAR(100) NOT NULL," +
                    "VIL_CODE NVARCHAR(3) NOT NULL," +

                    "CONSTRAINT PK_VILLE PRIMARY KEY(VIL_NOM)" +
                 ");"
            );
            #endregion

            #region Création de la table RTE_ROUTE
            this.BaseDeDonnees.RequetesDeCreation.Add(
                "CREATE TABLE RTE_ROUTE(" +
                    "RTE_NOM NVARCHAR(100) NOT NULL, " +
                    "RTE_CIRCUIT NVARCHAR(50) NOT NULL, " +

                    "CONSTRAINT PK_ROUTE " +
                        "PRIMARY KEY(RTE_NOM)" +
                 ");"
            );
            #endregion

            #region Création de la table IT_ITINERAIRE.
            this.BaseDeDonnees.RequetesDeCreation.Add(
                "CREATE TABLE IT_ITINERAIRE(" +

                "IT_PR_1 NVARCHAR(50) NOT NULL, " +
                "IT_PR_2 NVARCHAR(50) NOT NULL, " +

                "IT_ROUTE NVARCHAR(100) NOT NULL, " +

                "IT_VILLE NVARCHAR(100) NOT NULL, " +

                "IT_CENTRE NVARCHAR(50) NOT NULL, " +

                "CONSTRAINT PK_ITINERAIRE " +
                    "PRIMARY KEY(IT_ROUTE, IT_PR_1, IT_PR_2, IT_VILLE, IT_CENTRE), " +

                "CONSTRAINT FK_ITINERAIRE_ROUTE " +
                    "FOREIGN KEY(IT_ROUTE) " +
                    "REFERENCES RTE_ROUTE(RTE_NOM) " +
                        "ON DELETE CASCADE ON UPDATE CASCADE, " +

                "CONSTRAINT FK_ITINERAIRE_VILLE " +
                    "FOREIGN KEY(IT_VILLE) " +
                    "REFERENCES VIL_VILLE(VIL_NOM) " +
                        "ON DELETE CASCADE ON UPDATE CASCADE, " +

                "CONSTRAINT FK_ITINERAIRE_CENTRE " +
                    "FOREIGN KEY(IT_CENTRE) " +
                    "REFERENCES CTR_CENTRE(CTR_NOM) " +
                        "ON DELETE CASCADE ON UPDATE CASCADE " +

                ");");
            #endregion

            #region Création de la PAT_PATROUILLE.
            this.BaseDeDonnees.RequetesDeCreation.Add(
                "CREATE TABLE PAT_PATROUILLE(" +

                "PAT_DATE_DEBUT NVARCHAR(50) NOT NULL, " +
                "PAT_DATE_FIN NVARCHAR(50) NULL, " +

                "PAT_PR_1 NVARCHAR(50) NOT NULL, " +
                "PAT_PR_2 NVARCHAR(50) NOT NULL, " +

                "PAT_ROUTE NVARCHAR(100) NOT NULL, " +

                "PAT_VILLE NVARCHAR(100) NOT NULL, " +

                "PAT_CENTRE NVARCHAR(50) NOT NULL, " +

                "PAT_PATROUILLEUR_1 NVARCHAR(8) NOT NULL, " +
                "PAT_PATROUILLEUR_2 NVARCHAR(8) NOT NULL, " +

                "CONSTRAINT PK_PATROUILLE " +
                    "PRIMARY KEY(PAT_DATE_DEBUT, PAT_PATROUILLEUR_1, PAT_PATROUILLEUR_2, PAT_PR_1, PAT_PR_2, PAT_VILLE, PAT_CENTRE, PAT_ROUTE), " +

                "CONSTRAINT FK_PATROUILLE_AGENT_1 " +
                    "FOREIGN KEY(PAT_PATROUILLEUR_1) " +
                    "REFERENCES AGT_AGENT(AGT_MATRICULE) " +
                        "ON DELETE CASCADE ON UPDATE CASCADE," +

                "CONSTRAINT FK_PATROUILLE_AGENT_2 " +
                    "FOREIGN KEY(PAT_PATROUILLEUR_2) " +
                    "REFERENCES AGT_AGENT(AGT_MATRICULE) " +
                        "ON DELETE CASCADE ON UPDATE CASCADE, " +

                "CONSTRAINT FK_PATROUILLEUR_ITINERAIRE " +
                    "FOREIGN KEY(PAT_PR_1, PAT_PR_2, PAT_VILLE, PAT_CENTRE, PAT_ROUTE) " +
                    "REFERENCES IT_ITINERAIRE(IT_PR_1, IT_PR_2, IT_VILLE, IT_CENTRE, IT_ROUTE) " +
                        "ON DELETE CASCADE ON UPDATE CASCADE" +

                ");");
            #endregion

            #region Création de la table MET_METEO.
            this.BaseDeDonnees.RequetesDeCreation.Add(
                "CREATE TABLE MET_METEO(" +
                    "MET_TEMPS NVARCHAR(50) NOT NULL, " +
                    "CONSTRAINT PK_METEO PRIMARY KEY(MET_TEMPS));");
            #endregion

            #region Création de la table CON_CONDITION
            this.BaseDeDonnees.RequetesDeCreation.Add(
                "CREATE TABLE CON_CONDITION(" +
                    "CON_ETAT NVARCHAR(50) NOT NULL," +
                    "CONSTRAINT PK_CONDITION PRIMARY KEY(CON_ETAT)" +
                ");"
            );
            #endregion

            #region Création de la table EMP_EMPLACEMENT
            this.BaseDeDonnees.RequetesDeCreation.Add(
                "CREATE TABLE EMP_EMPLACEMENT(" +
                    "EMP_LIEU NVARCHAR(100) NOT NULL," +
                    "CONSTRAINT PK_EMPLACEMENT PRIMARY KEY(EMP_LIEU)" +
                ");"
            );
            #endregion

            #region Création de la table EVT_EVENEMENT
            this.BaseDeDonnees.RequetesDeCreation.Add(
                "CREATE TABLE EVT_EVENEMENT(" +
                    "EVT_TYPE NVARCHAR(100) NOT NULL," +
                    "EVT_DEGRADATION NVARCHAR(100) NOT NULL," +
                    "CONSTRAINT PK_EVENEMENT PRIMARY KEY(EVT_TYPE, EVT_DEGRADATION)" +
                ");"
            );
            #endregion

            #region Création de la table ACT_ACTION
            this.BaseDeDonnees.RequetesDeCreation.Add(
                "CREATE TABLE ACT_ACTION(" +
                    "ACT_NOM NVARCHAR(100) NOT NULL," +
                    "CONSTRAINT PK_ACTION PRIMARY KEY(ACT_NOM)" +
                ");"
            );
            #endregion

            #region Création de la table DES_DESORDRE
            this.BaseDeDonnees.RequetesDeCreation.Add(
                "CREATE TABLE DES_DESORDRE(" +
                    "DES_ID NVARCHAR(50)," +

                    "DES_DATE_DEBUT NVARCHAR(50) NOT NULL, " +
                    "DES_DATE_FIN NVARCHAR(50) NOT NULL, " +
                    "DES_X DECIMAL(10,5) NOT NULL, " +
                    "DES_Y DECIMAL(10,5) NOT NULL, " +
                    "DES_AGLO BIT NOT NULL," +

                    "DES_DATE_DEBUT_PATROUILLE NVARCHAR(50) NULL, " +
                    "DES_PR_1_PATROUILLE NVARCHAR(50) NULL, " +
                    "DES_PR_2_PATROUILLE NVARCHAR(50) NULL, " +
                    "DES_ROUTE_PATROUILLE NVARCHAR(100) NULL, " +
                    "DES_VILLE_PATROUILLE NVARCHAR(100) NULL, " +
                    "DES_CENTRE_PATROUILLE NVARCHAR(50) NULL, " +
                    "DES_PATROUILLEUR_1 NVARCHAR(8) NULL, " +
                    "DES_PATROUILLEUR_2 NVARCHAR(8) NULL, " +

                    "DES_PR_1_ITINERAIRE NVARCHAR(50) NULL, " +
                    "DES_PR_2_ITINERAIRE NVARCHAR(50) NULL, " +
                    "DES_ROUTE_ITINERAIRE NVARCHAR(100) NULL, " +
                    "DES_VILLE_ITINERAIRE NVARCHAR(100) NULL, " +
                    "DES_CENTRE_ITINERAIRE NVARCHAR(50) NULL, " +

                    "DES_METEO NVARCHAR(50) NOT NULL, " +

                    "DES_CONDITION NVARCHAR(50) NULL," +

                    "DES_EMPLACEMENT NVARCHAR(100) NOT NULL, " +

                    "DES_EVENEMENT_TYPE NVARCHAR(100) NOT NULL," +
                    "DES_EVENEMENT_DEGRADATION NVARCHAR(100) NOT NULL," +

                    "CONSTRAINT PK_DESORDRE PRIMARY KEY(DES_ID)" +

                    "CONSTRAINT FK_DESORDRE_PATROUILLEUR " +
                        "FOREIGN KEY(DES_PATROUILLEUR_1, DES_PATROUILLEUR_2, DES_PR_1_PATROUILLE, DES_PR_2_PATROUILLE, DES_ROUTE_PATROUILLE, DES_VILLE_PATROUILLE, DES_CENTRE_PATROUILLE) " +
                        "REFERENCES PAT_PATROUILLE(PAT_PATROUILLEUR_1, PAT_PATROUILLEUR_2, PAT_PR_1, PAT_PR_2, PAT_ROUTE, PAT_VILLE, PAT_CENTRE) " +
                            "ON DELETE CASCADE ON UPDATE CASCADE," +

                    "CONSTRAINT FK_DESORDRE_ITINERAIRE " +
                        "FOREIGN KEY(DES_PR_1_ITINERAIRE, DES_PR_2_ITINERAIRE, DES_ROUTE_ITINERAIRE, DES_VILLE_ITINERAIRE, DES_CENTRE_ITINERAIRE) " +
                        "REFERENCES PAT_PATROUILLE(IT_PR_1, IT_PR_2, IT_ROUTE, IT_VILLE, IT_CENTRE) " +
                            "ON DELETE CASCADE ON UPDATE CASCADE," +

                    "CONSTRAINT FK_DESORDRE_METEO " +
                        "FOREIGN KEY(DES_METEO) " +
                        "REFERENCES MET_METEO(MET_TEMPS) " +
                            "ON DELETE CASCADE ON UPDATE CASCADE," +

                    "CONSTRAINT FK_DESORDRE_CONDITION " +
                        "FOREIGN KEY(DES_CONDITION) " +
                        "REFERENCES CON_CONDITION(CON_ETAT) " +
                            "ON DELETE CASCADE ON UPDATE CASCADE," +

                    "CONSTRAINT FK_DESORDRE_EMPLACEMENT " +
                        "FOREIGN KEY(DES_EMPLACEMENT) " +
                        "REFERENCES EMP_EMPLACEMENT(EMP_LIEU) " +
                            "ON DELETE CASCADE ON UPDATE CASCADE," +

                    "CONSTRAINT FK_DESORDRE_EVENEMENT " +
                        "FOREIGN KEY(DES_EVENEMENT_TYPE, DES_EVENEMENT_DEGRADATION) " +
                        "REFERENCES EVT_EVENEMENT(EVT_TYPE, EVT_DEGRADATION) " +
                            "ON DELETE CASCADE ON UPDATE CASCADE" +

                ");"
            );
            #endregion

            #region Création de la table DCO_DEGRADATION_COMMENTAIRE
            this.BaseDeDonnees.RequetesDeCreation.Add(
                "CREATE TABLE DCO_DEGRADATION_COMMENTAIRE(" +
                    "DCO_COMENTAIRE NVARCHAR(100) NOT NULL," +
                    "DCO_DESORDRE_ID NVARCHAR(100) NOT NULL," +

                    "CONSTRAINT PK_EVENEMENT PRIMARY KEY(DCO_DESORDRE_ID), " +

                    "CONSTRAINT FK_DEGRADATION_COMMENTAIRE_DESORDRE " +
                        "FOREIGN KEY(DCO_DESORDRE_ID) " +
                        "REFERENCES DES_DESORDRE(DES_ID) " +
                            "ON DELETE CASCADE ON UPDATE CASCADE" +
                ");"
            );
            #endregion

            #region Création de la table INT_INTERVENTION
            this.BaseDeDonnees.RequetesDeCreation.Add(
                "CREATE TABLE INT_INTERVENTION(" +
                    "INT_NOM_ACTION NVARCHAR(100) NOT NULL, " +
                    "INT_DESORDRE NVARCHAR(50) NOT NULL, " +

                    "CONSTRAINT PK_INTERVENTION PRIMARY KEY(INT_NOM_ACTION, INT_DESORDRE)" +

                    "CONSTRAINT FK_INTERVENTION_DESORDRE " +
                        "FOREIGN KEY(INT_DESORDRE) REFERENCES DES_DESORDRE(DES_ID) " +
                        "ON DELETE CASCADE ON UPDATE CASCADE, " +

                    "CONSTRAINT FK_INTERVENTION_ACTION " +
                        "FOREIGN KEY(INT_NOM_ACTION) REFERENCES ACT_ACTION(ACT_NOM) " +
                        "ON DELETE CASCADE ON UPDATE CASCADE" +
                ");"
            );
            #endregion

            #region Création de la table COM_COMMENTAIRE
            this.BaseDeDonnees.RequetesDeCreation.Add(
                "CREATE TABLE COM_COMMENTAIRE(" +
                    "COM_COMMENTAIRE NVARCHAR(300) NOT NULL, " +
                    "COM_DESORDRE NVARCHAR(50) NOT NULL, " +

                    "CONSTRAINT PK_COMMENTAIRE_DESORDRE PRIMARY KEY(COM_DESORDRE), " +

                    "CONSTRAINT FK_COMMENTAIRE_DESORDRE " +
                        "FOREIGN KEY(COM_DESORDRE) REFERENCES DES_DESORDRE(DES_ID) " +
                        "ON DELETE CASCADE ON UPDATE CASCADE" +
                ");"
            );
            #endregion

            #region Création de la table SEC_SECTION
            this.BaseDeDonnees.RequetesDeCreation.Add(
                "CREATE TABLE SEC_SECTION(" +
                    "SEC_X DECIMAL(10,5) NOT NULL, " +
                    "SEC_Y DECIMAL(10,5) NOT NULL, " +
                    "SEC_DESORDRE NVARCHAR(50) NOT NULL, " +

                    "CONSTRAINT PK_SECTION PRIMARY KEY(SEC_DESORDRE), " +

                    "CONSTRAINT FK_SECTION_DESORDRE " +
                        "FOREIGN KEY(SEC_DESORDRE) REFERENCES DES_DESORDRE(DES_ID) " +
                        "ON DELETE CASCADE ON UPDATE CASCADE" +
                ");"
            );
            #endregion

            #region Création de la table PAU_PAUSE
            this.BaseDeDonnees.RequetesDeCreation.Add(
                "CREATE TABLE PAU_PAUSE(" +
                    "PAU_DEBUT NVARCHAR(50) NOT NULL, " +
                    "PAU_FIN NVARCHAR(50) NULL, " +

                    "PAU_DATE_DEBUT_PATROUILLE NVARCHAR(50) NOT NULL, " +
                    "PAU_PR_1_PATROUILLE NVARCHAR(50) NOT NULL, " +
                    "PAU_PR_2_PATROUILLE NVARCHAR(50) NOT NULL, " +
                    "PAU_ROUTE_PATROUILLE NVARCHAR(50) NOT NULL, " +
                    "PAU_VILLE_PATROUILLE NVARCHAR(50) NOT NULL, " +
                    "PAU_CENTRE_PATROUILLE NVARCHAR(50) NOT NULL, " +
                    "PAU_PATROUILLEUR_1_PATROUILLE NVARCHAR(50) NOT NULL, " +
                    "PAU_PATROUILLEUR_2_PATROUILLE NVARCHAR(50) NOT NULL, " +

                    "CONSTRAINT PK_SECTION PRIMARY KEY(PAU_DEBUT, PAU_DATE_DEBUT_PATROUILLE, PAU_PR_1_PATROUILLE, PAU_PR_2_PATROUILLE, PAU_ROUTE_PATROUILLE, PAU_VILLE_PATROUILLE, PAU_CENTRE_PATROUILLE, PAU_PATROUILLEUR_1_PATROUILLE, PAU_PATROUILLEUR_2_PATROUILLE), " +

                    "CONSTRAINT FK_PAUSE_PATROUILLE " +
                        "FOREIGN KEY(PAU_DATE_DEBUT_PATROUILLE, PAU_PR_1_PATROUILLE, PAU_PR_2_PATROUILLE, PAU_ROUTE_PATROUILLE, PAU_VILLE_PATROUILLE, PAU_CENTRE_PATROUILLE, PAU_PATROUILLEUR_1_PATROUILLE, PAU_PATROUILLEUR_2_PATROUILLE) " +
                        "REFERENCES PAT_PATROUILLE(PAT_DATE_DEBUT, PAT_PR_1, PAT_PR_2, PAT_ROUTE, PAT_VILLE, PAT_CENTRE, PAT_PATROUILLEUR_1, PAT_PATROUILLEUR_2) " +
                        "ON DELETE CASCADE ON UPDATE CASCADE" +
                ");"
            );
            #endregion

            #region LES ELEMENTS QUI SUIVENT SERONT A SUPPRIMER UNE FOIS LES TEST FINI.
            // LES ELEMENTS QUI SUIVENT SERONT A SUPPRIMER UNE FOIS LES TEST FINI.
            // Ajout de données pour les tests.			
            #region Insertion dans la table ART_AGENCE 
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO ART_AGENCE VALUES('Meaux-Villenoy');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO ART_AGENCE VALUES('Coulommiers');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO ART_AGENCE VALUES('Melun');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO ART_AGENCE VALUES('Provin');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO ART_AGENCE VALUES('Môret-Veneux');");
            #endregion

            #region Insertion dans la table CTR_CENTRE 
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO CTR_CENTRE VALUES('Paris', 'Meaux-Villenoy');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO CTR_CENTRE VALUES('Marseille', 'Meaux-Villenoy');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO CTR_CENTRE VALUES('Lyon', 'Meaux-Villenoy');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO CTR_CENTRE VALUES('Thorigny-sur-Marne', 'Coulommiers');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO CTR_CENTRE VALUES('Cesson', 'Coulommiers');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO CTR_CENTRE VALUES('Lésigny', 'Coulommiers');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO CTR_CENTRE VALUES('Nice', 'Melun');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO CTR_CENTRE VALUES('Nantes', 'Melun');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO CTR_CENTRE VALUES('Strasbourg', 'Melun');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO CTR_CENTRE VALUES('Montpellier', 'Provin');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO CTR_CENTRE VALUES('Bordeaux', 'Provin');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO CTR_CENTRE VALUES('Lille', 'Provin');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO CTR_CENTRE VALUES('Dijon', 'Môret-Veneux');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO CTR_CENTRE VALUES('Brest', 'Môret-Veneux');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO CTR_CENTRE VALUES('Angers', 'Môret-Veneux');");
            #endregion

            #region Insertion dans la table AGT_AGENT 
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M1234','Demis Roussos','Mr', 'Dijon', 'Môret-Veneux' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M2345','Michel Delpech','Mr', 'Dijon', 'Môret-Veneux' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M3456','Richard Anthony','Mr', 'Dijon', 'Môret-Veneux' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M4567','Louane Emera','Mlle', 'Dijon', 'Môret-Veneux' );");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M5678','Charlie Sheen','Mr', 'Brest', 'Môret-Veneux' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M6789','Bill Cosby','Mr', 'Brest', 'Môret-Veneux' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M7891','Taylor Kinney','Mr', 'Brest', 'Môret-Veneux' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M1011','Scott Eastwood','Mr', 'Brest', 'Môret-Veneux' );");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M1112','James Earl Jones','Mr', 'Angers', 'Môret-Veneux');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M1213','Tom Holland','Mr', 'Angers', 'Môret-Veneux' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M1314','Gary Busey','Mr', 'Angers', 'Môret-Veneux' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M1415','Randy Quaid','Mr', 'Angers', 'Môret-Veneux' );");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M1516','Tracy Morgan','Mme', 'Montpellier', 'Provin' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M1617','Jennifer Lawrence','Mlle', 'Montpellier', 'Provin' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M1718','Kim Kardashian','Mme', 'Montpellier', 'Provin');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M1819','Tony Stewart','Mr', 'Montpellier', 'Provin');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M2021','Iggy Azalea','Mme', 'Bordeaux', 'Provin');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M2122','Donald Sterling','Mr', 'Bordeaux', 'Provin');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M2223','Adrian Peterson','Mr', 'Bordeaux', 'Provin');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M2324','Rene Zellweger','Mr', 'Bordeaux', 'Provin' );");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M2526','Jared Leto','Mr', 'Lille', 'Provin' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M2627','Matthew McConaughey','Mr', 'Lille', 'Provin' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M2728','Macaulay Culkin','Mr', 'Lille', 'Provin' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M2829','Chris Pratt','Mr', 'Lille', 'Provin' );");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M2930','Theo James','Mr', 'Nice', 'Melun' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M3031','Ansel Elgort','Mr', 'Nice', 'Melun' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M3132','Jamie Dornan','Mr', 'Nice', 'Melun' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M3233','Alfonso Ribeiro','Mr', 'Nice', 'Melun' );");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M3334','James McAvoy','Mr', 'Nantes', 'Melun' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M3435','Laurence Fishburne','Mr', 'Nantes', 'Melun' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M3536','Ray Rice','Mr', 'Nantes', 'Melun' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M3637','Adrian Peterson','Mr', 'Nantes', 'Melun' );");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M3738','Richard Sherman','Mr', 'Strasbourg', 'Melun' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M3839','Paul George','Mr', 'Strasbourg', 'Melun' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M3940','Carmelo Anthony','Mr', 'Strasbourg', 'Melun' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M4041','Derek Jeter','Mr', 'Strasbourg', 'Melun' );");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M4142','Johnny Manziel','Mr', 'Thorigny-sur-Marne', 'Coulommiers' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M4243','Russell Wilson','Mr', 'Thorigny-sur-Marne', 'Coulommiers' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M4344','Tony Gwynn','Mr', 'Thorigny-sur-Marne', 'Coulommiers' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M4445','Tim Howard','Mr', 'Thorigny-sur-Marne', 'Coulommiers' );");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M4546','Myles Munroe','Mr', 'Cesson', 'Coulommiers' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M4647','Maya Angelou','Mme', 'Cesson', 'Coulommiers' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M4748','Eric Hill','Mr', 'Cesson', 'Coulommiers' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M4849','Casiodoro de Reina','Mr', 'Cesson', 'Coulommiers' );");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M4950','P. L. Travers','Mme', 'Lésigny', 'Coulommiers' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M5051','Gabriel García Márquez','Mr', 'Lésigny', 'Coulommiers' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M5152','Harold Pinter','Mr', 'Lésigny', 'Coulommiers' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M5253','Zora Neale Hurston','Mr', 'Lésigny', 'Coulommiers' );");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M5354','Isabel Briggs Myers','Mme', 'Lyon', 'Meaux-Villenoy' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M5455','Cory Monteith','Mr', 'Lyon', 'Meaux-Villenoy' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M5556','Aaron Hernandez','Mr', 'Lyon', 'Meaux-Villenoy' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M5657','Miley Cyrus','Mlle', 'Lyon', 'Meaux-Villenoy' );");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M5758','James Gandolfini','Mr', 'Marseille', 'Meaux-Villenoy' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M5859','Paula Deen','Mr', 'Marseille', 'Meaux-Villenoy' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M5960','Mindy McCready','Mme', 'Marseille', 'Meaux-Villenoy' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M6061','Trayvon Martin','Mr', 'Marseille', 'Meaux-Villenoy' );");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M6162','Amanda Bynes','Mme', 'Paris', 'Meaux-Villenoy' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M6364','Justin Bieber','Mr', 'Paris', 'Meaux-Villenoy' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M6465','Taylor Swift','Mme', 'Paris', 'Meaux-Villenoy' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO AGT_AGENT VALUES('M6667','Selena Gomez','Mme', 'Paris', 'Meaux-Villenoy' );");
            #endregion

            #region Insertion dans la table VIL_VILLE
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Maux','MAU');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Chelles','CHE');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Melun','MEL');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Pontault-Combault','PONT');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Champs-sur-Marne','CHA');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Savigny-le-Temple','SAV');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Torcy','TOR');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Nangis','NAN');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Villeparisis','VIL');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Le Mée-sur-Seine','MEE');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Combs-la-Ville','COM');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Ozoir-la-Ferrière','OZO');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Dammarie-les-Lys','DAM');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Roissy-en-Brie','ROI');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Lagny-sur-Marne','LAG');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Montereau-Fault-Yonne','MON');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Mitry-Mory','MIT');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Fontainebleau','FON');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Noisiel','NOI');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Moissy-Cramayel','MOI');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Lognes','LOG');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Avon','AVO');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Coulommiers','COU');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Brie-Comte-Robert','BRI');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Nemours','NEM');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Vaires-sur-Marne','MAU');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Provins','PRO');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Saint-Fargeau-Ponthierry','SAI');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Vaux-le-Pénil','VAU');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO VIL_VILLE VALUES('Claye-Souilly','CLA');");
            #endregion

            #region Insertion dans la table RTE_ROUTE
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO RTE_ROUTE VALUES('D1001','Structurant');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO RTE_ROUTE VALUES('D301', 'Structurant');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO RTE_ROUTE VALUES('D901','Structurant');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO RTE_ROUTE VALUES('D104g', 'Structurant');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO RTE_ROUTE VALUES('D604', 'Structurant');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO RTE_ROUTE VALUES('D1004', 'Secondaire');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO RTE_ROUTE VALUES('D1005', 'Secondaire');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO RTE_ROUTE VALUES('D606', 'Secondaire');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO RTE_ROUTE VALUES('D905', 'Secondaire');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO RTE_ROUTE VALUES('D415', 'Secondaire');");
            #endregion

            #region Insertion dans la table IT_ITINERAIRE			
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('1+123', '39+307', 'D1001', 'Chelles', 'Paris');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('2+123', '56+789', 'D1004', 'Chelles', 'Paris');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('3+123', '56+357', 'D301', 'Melun', 'Marseille');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('4+123', '75+687', 'D1005', 'Melun', 'Marseille');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('1+456', '1+456', 'D901', 'Pontault-Combault', 'Lyon');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('2+456', '41+123', 'D606', 'Pontault-Combault', 'Lyon');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('3+456', '42+123', 'D104g', 'Savigny-le-Temple', 'Thorigny-sur-Marne');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('1+789', '43+123', 'D415', 'Savigny-le-Temple', 'Thorigny-sur-Marne');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('2+789', '44+123', 'D604', 'Champs-sur-Marne', 'Cesson');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('3+789', '44+123', 'D905', 'Champs-sur-Marne', 'Cesson');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('4+123', '45+123', 'D1001', 'Torcy', 'Lésigny');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('5+123', '46+123', 'D1004', 'Torcy', 'Lésigny');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('6+123', '47+123', 'D301', 'Maux', 'Nice');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('7+123', '48+123', 'D1005', 'Maux', 'Nice');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('8+123', '49+123', 'D901', 'Villeparisis', 'Nantes');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('9+123', '50+123', 'D606', 'Villeparisis', 'Nantes');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('10+123', '51+123', 'D104g', 'Le Mée-sur-Seine', 'Strasbourg');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('11+123', '52+123', 'D415', 'Le Mée-sur-Seine', 'Strasbourg');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('12+123', '53+123', 'D604', 'Combs-la-Ville', 'Montpellier');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('13+123', '54+123', 'D905', 'Combs-la-Ville', 'Montpellier');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('14+123', '55+123', 'D1001', 'Ozoir-la-Ferrière', 'Bordeaux');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('15+123', '56+123', 'D1004', 'Ozoir-la-Ferrière', 'Bordeaux');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('16+123', '57+123', 'D301', 'Dammarie-les-Lys', 'Lille');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('17+123', '58+123', 'D1005', 'Dammarie-les-Lys', 'Lille');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('18+123', '59+123', 'D901', 'Roissy-en-Brie', 'Dijon');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('19+123', '60+123', 'D606', 'Roissy-en-Brie', 'Dijon');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('20+123', '61+123', 'D104g', 'Lagny-sur-Marne', 'Brest');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('21+123', '62+123', 'D415', 'Lagny-sur-Marne', 'Brest');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('12+123', '63+123', 'D604', 'Montereau-Fault-Yonne', 'Angers');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('64+123', '65+123', 'D905', 'Montereau-Fault-Yonne', 'Angers' );");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('13+123', '66+123', 'D1001', 'Mitry-Mory', 'Paris');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('14+123', '67+123', 'D1004', 'Mitry-Mory', 'Paris');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('15+123', '68+123', 'D301', 'Fontainebleau', 'Marseille');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('16+123', '69+123', 'D1005', 'Fontainebleau', 'Marseille');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('17+123', '70+123', 'D901', 'Noisiel', 'Lyon');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('18+123', '71+123', 'D606', 'Noisiel', 'Lyon');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('19+123', '72+123', 'D104g', 'Moissy-Cramayel', 'Thorigny-sur-Marne');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('20+123', '73+123', 'D415', 'Moissy-Cramayel', 'Thorigny-sur-Marne');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('21+123', '74+123', 'D604', 'Lognes', 'Cesson');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('22+123', '75+123', 'D905', 'Lognes', 'Cesson');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('23+123', '76+123', 'D1001', 'Avon', 'Lésigny');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('24+123', '77+123', 'D1004', 'Avon', 'Lésigny');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('25+123', '78+123', 'D301', 'Coulommiers', 'Nice' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('26+123', '79+123', 'D1005', 'Coulommiers', 'Nice' );");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('27+123', '80+123', 'D901', 'Claye-Souilly', 'Nantes');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('28+123', '81+123', 'D606', 'Claye-Souilly', 'Nantes');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('29+123', '82+123', 'D104g', 'Vaux-le-Pénil', 'Strasbourg');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('30+123', '83+123', 'D415', 'Vaux-le-Pénil', 'Strasbourg');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('31+123', '84+123', 'D604', 'Provins', 'Montpellier');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('32+123', '85+123', 'D905', 'Provins', 'Montpellier');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('33+123', '86+123', 'D1001', 'Saint-Fargeau-Ponthierry', 'Bordeaux');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('34+123', '87+123', 'D1004', 'Saint-Fargeau-Ponthierry', 'Bordeaux');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('35+123', '88+123', 'D301', 'Vaires-sur-Marne', 'Lille' );");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('36+123', '89+123', 'D1005', 'Vaires-sur-Marne', 'Lille' );");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('37+123', '90+123', 'D901', 'Nemours', 'Dijon');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('38+123', '91+123', 'D606', 'Nemours', 'Dijon');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('39+123', '92+123', 'D104g', 'Brie-Comte-Robert', 'Brest');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO IT_ITINERAIRE VALUES('40+123', '93+123', 'D415', 'Brie-Comte-Robert', 'Brest');");
            #endregion

            #region Insertion dans la table EMP_EMPLACEMENT
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EMP_EMPLACEMENT VALUES('Chaussée dans le sens croissant des PR');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EMP_EMPLACEMENT VALUES('Chaussée dans le sens décroissant des PR');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EMP_EMPLACEMENT VALUES('Chaussée dans les 2 sens');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EMP_EMPLACEMENT VALUES('Dépendance');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EMP_EMPLACEMENT VALUES('Accotement');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EMP_EMPLACEMENT VALUES('BAU');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EMP_EMPLACEMENT VALUES('TPC');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EMP_EMPLACEMENT VALUES('Ouvrage d''art');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EMP_EMPLACEMENT VALUES('Passage à niveau');");
            #endregion

            #region Insertion dans la table EVT_EVENEMENT
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Chaussées','Déformation');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Chaussées','Arrachement');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Chaussées','Etat de surface');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Chaussées','Présence de matériaux ou d''objet divers');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Chaussées','Présence d''eau sur la chaussée');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Chaussées','Animaux morts');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Chaussées','Plaque de fuel');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Chaussées','gravillon');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Chaussées','Véhicule en panne');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Chaussées','Autre');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Accotements','Dénivellation dangereuse');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Accotements','Dépôts de détritus');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Accotements','Dépôts de pierre');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Accotements','Trou localisé');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Accotements','Publicité sauvage sur domaine publique');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Accotements','Bordure déteriiorée ou pavée descellées');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Accotements','Problème sur ovrage d''un concessionnaire');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Accotements','Autre');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Ouvrage d''art et assainissement','Parapet endommagé');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Ouvrage d''art et assainissement','Rambarde endomagé');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Ouvrage d''art et assainissement','Garde-corps endomagé');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Ouvrage d''art et assainissement','Joint de chaussée dégradé');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Ouvrage d''art et assainissement','Inondataion - Saignée bouchée');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Ouvrage d''art et assainissement','Inondataion - Fossé encombré');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Ouvrage d''art et assainissement','Inondataion - Saignée busé obstrué');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Ouvrage d''art et assainissement','Autre');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Equipement','Panne de feu tricolore');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Equipement','Dégradation d''un candélabre');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Equipement','Béton endommagée');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Equipement','Glissière métallique endommagée');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Equipement','Autre');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Signalisation','Anomalie de signalisaation - Arraché');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Signalisation','Anomalie de signalisaation - Sale');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Signalisation','Signalisation horizontale effacée');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Signalisation','Marquage du point de repère effacé');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Signalisation','Autre');");

            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Espaces verts','Arbre ou branche qui masque la signalisation');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Espaces verts','Inclinaison de l''arbre');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Espaces verts','Branche tombée ou partiellement cassée');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO EVT_EVENEMENT VALUES('Espaces verts','Autre');");
            #endregion

            #region insertion dans la table ACT_ACTION
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO ACT_ACTION VALUES('Signalisation - Pose de AK4');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO ACT_ACTION VALUES('Signalisation - Pose de AK14');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO ACT_ACTION VALUES('Immédiate');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO ACT_ACTION VALUES('Différée');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO ACT_ACTION VALUES('Programmée');");
            #endregion

            #region insertion dans la table MET_METEO
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO MET_METEO VALUES('Temps claire ou soleil');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO MET_METEO VALUES('Couvert');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO MET_METEO VALUES('Pluit');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO MET_METEO VALUES('Brouillard');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO MET_METEO VALUES('Vent');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO MET_METEO VALUES('neige');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO MET_METEO VALUES('Gel');");
            this.BaseDeDonnees.RequetesDeCreation.Add("INSERT INTO MET_METEO VALUES('Canicule');");
            #endregion

            #endregion
        }
    }
}
