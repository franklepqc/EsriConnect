namespace ESRI.NetCore.Interfaces
{
    /// <summary>
    /// Paramèetres pour la requête 'Obtenir'.
    /// </summary>
    public interface IParametresRequete
    {
        /// <summary>
        /// Clause Where.
        /// </summary>
        string Where { get; set; }

        /// <summary>
        /// Point pour l'intersection spatiale.
        /// </summary>
        IPoint Point { get; set; }

        /// <summary>
        /// Indicateur pour retourner le polygone associé à la requête.
        /// </summary>
        bool RetournerGeometrie { get; set; }

        /// <summary>
        /// Indicateur à savoir si on veut avoir tous les champs en sortie.
        /// </summary>
        bool AfficherTousLesChamps { get; set; }
    }
}
