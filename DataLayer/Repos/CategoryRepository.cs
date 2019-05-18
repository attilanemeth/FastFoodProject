// <copyright file="CategoryRepository.cs" company="PlaceholderCompany">
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
    /// Repository for categories.
    /// </summary>
    public class CategoryRepository : IRepository<CATEGORY>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryRepository"/> class.
        /// </summary>
        /// <param name="ctx">The database context.</param>
        public CategoryRepository(DbContext ctx)
        {
            this.Ctx = ctx;
        }

        /// <summary>
        /// Gets or sets the database context.
        /// </summary>
        public DbContext Ctx { get; set; }

        /// <summary>
        /// Deletes a category.
        /// </summary>
        /// <param name="entityToDelete">Category to delete.</param>
        public void Delete(CATEGORY entityToDelete)
        {
            this.Ctx.Set<CATEGORY>().Remove(this.Ctx.Set<CATEGORY>().SingleOrDefault(x => x.CATEGORYID == entityToDelete.CATEGORYID));
            this.Ctx.SaveChanges();
        }

        /// <summary>
        /// Inserts a category.
        /// </summary>
        /// <param name="newentity">Category to insert.</param>
        /// <returns>The ID of the inserted entity.</returns>
        public int Insert(CATEGORY newentity)
        {
            this.Ctx.Set<CATEGORY>().Add(newentity);
            this.Ctx.SaveChanges();

            return (int)this.Ctx.Set<CATEGORY>().Max(x => x.CATEGORYID);
        }

        /// <summary>
        /// Updates the category.
        /// </summary>
        /// <param name="entityToUpdate">Category to update.</param>
        public void Update(CATEGORY entityToUpdate)
        {
            this.Ctx.Set<CATEGORY>().Where(x => x.CATEGORYID == entityToUpdate.CATEGORYID).Single<CATEGORY>().CNAME = entityToUpdate.CNAME;
            this.Ctx.Set<CATEGORY>().Where(x => x.CATEGORYID == entityToUpdate.CATEGORYID).Single<CATEGORY>().PICTURE = entityToUpdate.PICTURE;
            this.Ctx.SaveChanges();
        }
    }
}
