// <copyright file="OrderRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// Repository for orders.
    /// </summary>
    public class OrderRepository : IRepository<ORDER>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderRepository"/> class.
        /// </summary>
        /// <param name="ctx">The database context.</param>
        public OrderRepository(DbContext ctx)
        {
            this.Ctx = ctx;
        }

        /// <summary>
        /// Gets or sets the database context.
        /// </summary>
        public DbContext Ctx { get; set; }

        /// <summary>
        /// Deletes an order.
        /// </summary>
        /// <param name="entityToDelete">Order to delete.</param>
        public void Delete(ORDER entityToDelete)
        {
            this.Ctx.Set<ORDER>().Remove(this.Ctx.Set<ORDER>().SingleOrDefault(x => x.ORDERID == entityToDelete.ORDERID));
            this.Ctx.SaveChanges();
        }

        /// <summary>
        /// Inserts an order.
        /// </summary>
        /// <param name="newentity">Order to insert.</param>
        /// <returns>The ID of the inserted entity.</returns>
        public int Insert(ORDER newentity)
        {
            try
            {
                this.Ctx.Set<ORDER>().Add(newentity);
                this.Ctx.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                MessageBox.Show("Az adatbázis művelet nem hajtható végre!");
            }

            return (int)this.Ctx.Set<ORDER>().Max(x => x.ORDERID);
        }

        /// <summary>
        /// Updates an order.
        /// </summary>
        /// <param name="entityToUpdate">Order to update.</param>
        public void Update(ORDER entityToUpdate)
        {
            this.Ctx.Set<ORDER>().Where(x => x.ORDERID == entityToUpdate.ORDERID).Single<ORDER>().ORDERDATE = entityToUpdate.ORDERDATE;
            this.Ctx.Set<ORDER>().Where(x => x.ORDERID == entityToUpdate.ORDERID).Single<ORDER>().ORDERLISTITEMs = entityToUpdate.ORDERLISTITEMs;
            this.Ctx.Set<ORDER>().Where(x => x.ORDERID == entityToUpdate.ORDERID).Single<ORDER>().RATING = entityToUpdate.RATING;
            this.Ctx.Set<ORDER>().Where(x => x.ORDERID == entityToUpdate.ORDERID).Single<ORDER>().TOTALPRICE = entityToUpdate.TOTALPRICE;
            this.Ctx.SaveChanges();
        }
    }
}
