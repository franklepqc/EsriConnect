namespace ESRI.NetCore.Interfaces
{
    /// <summary>
    /// Point de la carte.
    /// </summary>
    public interface IPoint
    {
        /// <summary>
        /// Well-known ID. Projection.
        /// </summary>
        int WKID { get; }
    }
}
