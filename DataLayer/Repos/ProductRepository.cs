// <copyright file="ProductRepository.cs" company="PlaceholderCompany">
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
    /// Repository for products.
    /// </summary>
    public class ProductRepository : IRepository<PRODUCT>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="ctx">The database context.</param>
        public ProductRepository(DbContext ctx)
        {
            this.Ctx = ctx;
        }

        /// <summary>
        /// Gets or sets the database context.
        /// </summary>
        public DbContext Ctx { get; set; }

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="entityToDelete">Product to delete.</param>
        public void Delete(PRODUCT entityToDelete)
        {
            this.Ctx.Set<PRODUCT>().Remove(this.Ctx.Set<PRODUCT>().SingleOrDefault(x => x.PRODUCTID == entityToDelete.PRODUCTID));
            this.Ctx.SaveChanges();
        }

        /// <summary>
        /// Inserts a product.
        /// </summary>
        /// <param name="newentity">Product to insert.</param>
        /// <returns>The ID of the inserted entity.</returns>
        public int Insert(PRODUCT newentity)
        {
            this.Ctx.Set<PRODUCT>().Add(newentity);
            this.Ctx.SaveChanges();

            return (int)this.Ctx.Set<PRODUCT>().Max(x => x.PRODUCTID);
        }

        /// <summary>
        /// Updates a product.
        /// </summary>
        /// <param name="entityToUpdate">Product to update.</param>
        public void Update(PRODUCT entityToUpdate)
        {
            this.Ctx.Set<PRODUCT>().Where(x => x.PRODUCTID == entityToUpdate.PRODUCTID).Single<PRODUCT>().PNAME = entityToUpdate.PNAME;
            this.Ctx.Set<PRODUCT>().Where(x => x.PRODUCTID == entityToUpdate.PRODUCTID).Single<PRODUCT>().PICTURE = entityToUpdate.PICTURE;
            this.Ctx.Set<PRODUCT>().Where(x => x.PRODUCTID == entityToUpdate.PRODUCTID).Single<PRODUCT>().UNITPRICE = entityToUpdate.UNITPRICE;
            this.Ctx.SaveChanges();
        }
    }
}
