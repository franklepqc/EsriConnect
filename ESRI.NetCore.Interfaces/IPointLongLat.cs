namespace ESRI.NetCore.Interfaces
{
    /// <summary>
    /// Point de la carte.
    /// </summary>
    public interface IPointLongLat : IPoint
    {
        /// <summary>
        /// Well-known ID. Projection.
        /// </summary>
        new int WKID { get; }
    }
}
