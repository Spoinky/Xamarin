namespace ModeleXamarin.couches.sources.definitions.connexion
{
    /// <summary>
    /// Interface définissant les opérations nécessaires à la récupération des types de connexions.
    /// </summary>
    public interface IConnexion
    {
        /// <summary>
        /// Vérifie si le dispositif est connecté ou non à un réseau.
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Vérifie à quel type de réseau est connecté le dispositif.
        /// </summary>
        ETypeConnexion TypeConnexion { get; }
    }
}
