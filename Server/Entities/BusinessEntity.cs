namespace Service.Server.Entities
{
    /// <summary>
    /// A registered business.
    /// </summary>
    public sealed class BusinessEntity
    {
        /// <summary>
        /// Unique id of the business.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Name of the business.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Address line 1 of the business' location.
        /// </summary>
        public string AddressLine1 { get; private set; }

        /// <summary>
        /// Address line 1 of the business' location.
        /// </summary>
        public string AddressLine2 { get; private set; }

        /// <summary>
        /// City of the business' location.
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// State of the business' location.
        /// </summary>
        public string State { get; private set; }

        /// <summary>
        /// Zip code of the business' location.
        /// </summary>
        public string ZipCode { get; private set; }
    }
}
