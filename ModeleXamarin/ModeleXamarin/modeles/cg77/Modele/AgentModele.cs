namespace ModeleXamarin.modeles.cg77.Modele
{
    /// <summary>
    /// Classe modèle représentant un agent.
    /// </summary>
    public class AgentModele
    {
        /// <summary>
        /// Le matricule de l'agent.
        /// </summary>
        public string Matricule { get; set; }

        /// <summary>
        /// La civilité de l'agent.
        /// </summary>
        public string Civilite { get; set; }

        /// <summary>
        /// Le nom et prénom de l'agent.
        /// </summary>
        public string NomPrenom { get; set; }

        public override bool Equals(object objet)
        {
            if (objet == null)
                return false;

            if (this.Matricule == (objet as AgentModele).Matricule)
                return true;

            return false;
        }
    }
}
