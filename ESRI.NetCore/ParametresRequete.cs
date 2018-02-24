using ESRI.NetCore.Interfaces;

namespace ESRI.NetCore
{
    /// <summary>
    /// Objet représentant les paramètres pour une requête.
    /// </summary>
    public class ParametresRequete : IParametresRequete
    {
        /// <summary>
        /// Retourne un objet par défaut.
        /// </summary>
        public static IParametresRequete Defaut => new ParametresRequete();

        /// <summary>
        /// Clause Where. Sert pour filtrer certains champs.
        /// </summary>
        public string Where { get; set; } = @"1 = 1";

        /// <summary>
        /// Point. Intersection spatiale.
        /// </summary>
        public IPoint Point { get; set; }

        /// <summary>
        /// Retourne la géométrie.
        /// </summary>
        public bool RetournerGeometrie { get; set; } = false;

        /// <summary>
        /// Indicateur à savoir si on veut avoir tous les champs en sortie.
        /// </summary>
        public bool AfficherTousLesChamps { get; set; }
    }
}
