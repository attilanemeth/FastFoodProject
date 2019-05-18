// <copyright file="IRepository.cs" company="PlaceholderCompany">
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
    /// Interface for General Repository.
    /// </summary>
    /// <typeparam name="TEntity">Specify the objects stored in Repository</typeparam>
    public interface IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Gets or sets the database context.
        /// </summary>
        DbContext Ctx { get; set; }

        /// <summary>
        /// Interface for inserting general element
        /// </summary>
        /// <param name="newentity">General object to insert</param>
        /// <returns>The ID of the inserted entity.</returns>
        int Insert(TEntity newentity);

        /// <summary>
        /// Interface for deleting general element.
        /// </summary>
        /// <param name="entityToDelete">General object to delete</param>
        void Delete(TEntity entityToDelete);

        /// <summary>
        /// Interface for updating general element.
        /// </summary>
        /// <param name="entityToUpdate">Entity to update.</param>
        void Update(TEntity entityToUpdate);
    }
}
