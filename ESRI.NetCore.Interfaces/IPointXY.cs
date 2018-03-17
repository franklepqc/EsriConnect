namespace ESRI.NetCore.Interfaces
{
    /// <summary>
    /// Point de la carte.
    /// </summary>
    public interface IPointXY : IPoint
    {
        /// <summary>
        /// Well-known ID. Projection.
        /// </summary>
        new int WKID { get; }
    }
}
