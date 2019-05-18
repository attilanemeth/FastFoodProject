// <copyright file="OrderListItemRepository.cs" company="PlaceholderCompany">
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
    /// Repository for order list items.
    /// </summary>
    public class OrderListItemRepository : IRepository<ORDERLISTITEM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderListItemRepository"/> class.
        /// </summary>
        /// <param name="ctx">The database context.</param>
        public OrderListItemRepository(DbContext ctx)
        {
            this.Ctx = ctx;
        }

        /// <summary>
        /// Gets or sets the database context.
        /// </summary>
        public DbContext Ctx { get; set; }

        /// <summary>
        /// Deletes an order list item.
        /// </summary>
        /// <param name="entityToDelete">Order list item to delete.</param>
        public void Delete(ORDERLISTITEM entityToDelete)
        {
            this.Ctx.Set<ORDERLISTITEM>().Remove(this.Ctx.Set<ORDERLISTITEM>().SingleOrDefault(x => x.ORDERLISTITEMID == entityToDelete.ORDERLISTITEMID));
            this.Ctx.SaveChanges();
        }

        /// <summary>
        /// Inserts an order list item.
        /// </summary>
        /// <param name="newentity">Order list item to insert.</param>
        /// <returns>The ID of the inserted entity.</returns>
        public int Insert(ORDERLISTITEM newentity)
        {
            this.Ctx.Set<ORDERLISTITEM>().Add(newentity);
            this.Ctx.SaveChanges();

            return (int)this.Ctx.Set<ORDERLISTITEM>().Max(x => x.ORDERLISTITEMID);
        }

        /// <summary>
        /// Updates an order list item.
        /// </summary>
        /// <param name="entityToUpdate">Order list item to update.</param>
        public void Update(ORDERLISTITEM entityToUpdate)
        {
            this.Ctx.Set<ORDERLISTITEM>().Where(x => x.ORDERLISTITEMID == entityToUpdate.ORDERLISTITEMID).Single<ORDERLISTITEM>().AMOUNT = entityToUpdate.AMOUNT;
            this.Ctx.Set<ORDERLISTITEM>().Where(x => x.ORDERLISTITEMID == entityToUpdate.ORDERLISTITEMID).Single<ORDERLISTITEM>().ORDER = entityToUpdate.ORDER;
            this.Ctx.Set<ORDERLISTITEM>().Where(x => x.ORDERLISTITEMID == entityToUpdate.ORDERLISTITEMID).Single<ORDERLISTITEM>().ORDERID = entityToUpdate.ORDERID;
            this.Ctx.Set<ORDERLISTITEM>().Where(x => x.ORDERLISTITEMID == entityToUpdate.ORDERLISTITEMID).Single<ORDERLISTITEM>().PRODUCT = entityToUpdate.PRODUCT;
            this.Ctx.Set<ORDERLISTITEM>().Where(x => x.ORDERLISTITEMID == entityToUpdate.ORDERLISTITEMID).Single<ORDERLISTITEM>().PRODUCT1 = entityToUpdate.PRODUCT1;
            this.Ctx.Set<ORDERLISTITEM>().Where(x => x.ORDERLISTITEMID == entityToUpdate.ORDERLISTITEMID).Single<ORDERLISTITEM>().SUBTOTAL = entityToUpdate.SUBTOTAL;
            this.Ctx.SaveChanges();
        }
    }
}
