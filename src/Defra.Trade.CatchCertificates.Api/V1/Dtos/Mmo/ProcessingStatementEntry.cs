// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;

namespace Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo
{
    public class ProcessingStatementEntry
    {
        /// <summary>
        /// Processing Statement internal identifier.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Document Number.
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// MMO schema version under which this Statement was saved.
        /// </summary>
        public int SchemaVersion { get; set; }

        /// <summary>
        /// Date/time when the Statement was published to the Trade Store.
        /// </summary>
        public DateTimeOffset CreatedOn { get; set; }

        /// <summary>
        /// User Id who created the Statement.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// System responsible for creating the Statement.
        /// </summary>
        public string CreatedSystem { get; set; }

        /// <summary>
        /// Date/time when the Statement was modified.
        /// </summary>
        /// <remarks>Defaults to the current UTC date/time if not specified.</remarks>
        public DateTimeOffset? LastUpdated { get; set; }

        /// <summary>
        /// User Id who made the change.
        /// </summary>
        public string LastUpdatedBy { get; set; }

        /// <summary>
        /// System responsible for making the change.
        /// </summary>
        public string LastUpdatedSystem { get; set; }
    }
}
