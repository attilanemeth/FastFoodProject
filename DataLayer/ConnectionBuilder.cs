// <copyright file="ConnectionBuilder.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Builds a connection to a database.
    /// </summary>
    public class ConnectionBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionBuilder"/> class.
        /// </summary>
        public ConnectionBuilder()
        {
            PosDatabaseEntities entities = new PosDatabaseEntities();
            this.DatabaseContext = entities;
        }

        /// <summary>
        /// Gets the database context.
        /// </summary>
        public PosDatabaseEntities DatabaseContext { get; }
    }
}
