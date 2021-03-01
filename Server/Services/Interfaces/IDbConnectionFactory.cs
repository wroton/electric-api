using System.Data;

namespace Service.Server.Services.Interfaces
{
    /// <summary>
    /// Builds database connections.
    /// </summary>
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// Builds a database connection that is connected.
        /// </summary>
        /// <returns>Connected database connection.</returns>
        IDbConnection Build();
    }
}
