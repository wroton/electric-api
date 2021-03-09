using Service.Server.Entities;
using System.Threading.Tasks;

namespace Service.Server.Services.Interfaces
{
    /// <summary>
    /// Contains context information for the request.
    /// </summary>
    public interface IRequestContext
    {
        /// <summary>
        /// Id of the user that submitted the request.
        /// Null if the calling user provided no authentication details.
        /// </summary>
        int? UserId { get; }

        /// <summary>
        /// Gets the user that submitted the request.
        /// Null if the calling user provided no authentication details.
        /// </summary>
        /// <returns>User that submitted the request.</returns>
        Task<UserEntity> User();
    }
}
