namespace Service.Server.Entities
{
    /// <summary>
    /// A client served by a business.
    /// </summary>
    public sealed class ClientEntity
    {
        /// <summary>
        /// Unique id of the client.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Name of the client.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Address line 1 of the client's location.
        /// </summary>
        public string AddressLine1 { get; private set; }

        /// <summary>
        /// Address line 1 of the client's location.
        /// </summary>
        public string AddressLine2 { get; private set; }

        /// <summary>
        /// City of the client's location.
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// State of the client's location.
        /// </summary>
        public string State { get; private set; }

        /// <summary>
        /// Zip code of the client's location.
        /// </summary>
        public string ZipCode { get; private set; }
    }
}