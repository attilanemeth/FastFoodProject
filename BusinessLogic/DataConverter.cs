// <copyright file="DataConverter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DataLayer;

    /// <summary>
    /// Data converter.
    /// </summary>
    public class DataConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataConverter"/> class.
        /// </summary>
        /// <param name="entities">The database context.</param>
        public DataConverter(PosDatabaseEntities entities)
        {
            this.Entities = entities;
        }

        /// <summary>
        /// Gets the database context.
        /// </summary>
        public PosDatabaseEntities Entities { get; }

        /// <summary>
        /// Converts a category list to be able to used by the database.
        /// </summary>
        /// <param name="dbcollection">The category list.</param>
        /// <returns>The converted category list.</returns>
        public static ObservableCollection<Category> CategoryListConverter(ObservableCollection<CATEGORY> dbcollection)
        {
            ObservableCollection<Category> output = new ObservableCollection<Category>();

            foreach (var item in dbcollection)
            {
                output.Add(new Category
                {
                    CategoryId = (int)item.CATEGORYID,
                    Name = item.CNAME,
                    Picture = item.PICTURE
                });
            }

            return output;
        }

        /// <summary>
        /// Converts a product list to be able to used by the database.
        /// </summary>
        /// <param name="dbcollection">The product list.</param>
        /// <returns>The converted product list.</returns>
        public static ObservableCollection<Termek> ProductListConverter(ObservableCollection<PRODUCT> dbcollection)
        {
            ObservableCollection<Termek> output = new ObservableCollection<Termek>();

            foreach (var item in dbcollection)
            {
                output.Add(new Termek
                {
                    TermekId = (int)item.PRODUCTID,
                    Name = item.PNAME,
                    Picture = item.PICTURE,
                    Category = CategoryConverter(item.CATEGORY),
                    Price = (int)item.UNITPRICE
                });
            }

            return output;
        }

        /// <summary>
        /// Converts a category to be able to used by the database.
        /// </summary>
        /// <param name="cat">The category.</param>
        /// <returns>The converted category.</returns>
        public static Category CategoryConverter(CATEGORY cat)
        {
            Category output = new Category()
            {
                CategoryId = (int)cat.CATEGORYID,
                Name = cat.CNAME,
                Picture = cat.PICTURE
            };

            return output;
        }

        /// <summary>
        /// Converts a category to be able to used by the database.
        /// </summary>
        /// <param name="cat">The category.</param>
        /// <returns>The converted category.</returns>
        public static CATEGORY CategoryConverter(Category cat)
        {
            CATEGORY output = new CATEGORY()
            {
                CATEGORYID = cat.CategoryId,
                CNAME = cat.Name,
                PICTURE = cat.Picture
            };

            return output;
        }

        /// <summary>
        /// Converts a product to be able to use by the database.
        /// </summary>
        /// <param name="prod">The product to convert.</param>
        /// <returns>The converted product.</returns>
        public PRODUCT ProductConverter(Termek prod)
        {
            PRODUCT output = new PRODUCT()
            {
                PRODUCTID = prod.TermekId,
                CATEGORY = this.Entities.CATEGORies.SingleOrDefault(x => x.CATEGORYID == prod.Category.CategoryId),
                PNAME = prod.Name,
                PICTURE = prod.Picture,
                UNITPRICE = prod.Price
            };

            return output;
        }

        /*public static ORDERLISTITEM OrderListItemConverter(OrderListItem item)
        {
            ORDERLISTITEM output = new ORDERLISTITEM()
            {
                ORDERLISTITEMID = (int)item.OrderListItemId,
                SUBTOTAL = (int)item.SubTotal,
                AMOUNT = (int)item.Amount,
                ORDER = OrderConverter(item.Order),
                PRODUCT = item.Termek.TermekId
            };

            return output;
        }*/

        /*public ICollection<ORDERLISTITEM> OrderListItemCollectionConverter(ObservableCollection<OrderListItem> list)
        {
            ObservableCollection<ORDERLISTITEM> output = new ObservableCollection<ORDERLISTITEM>();

            foreach (var item in list)
            {
                output.Add(new ORDERLISTITEM()
                {
                    ORDERLISTITEMID = item.OrderListItemId,
                    AMOUNT = item.Amount,
                    ORDER = OrderConverter(item.Order),
                    SUBTOTAL = item.SubTotal,
                    PRODUCT1 = this.Entities.PRODUCTs.SingleOrDefault(x => x.PRODUCTID == item.Termek.TermekId)
                });
            }

            return output;
        }*/

        /// <summary>
        /// Converts an order to be able to use by the database.
        /// </summary>
        /// <param name="order">The order to convert.</param>
        /// <returns>The converted order.</returns>
        public ORDER OrderConverter(Order order)
        {
            ORDER output = new ORDER()
            {
                ORDERDATE = order.Date,
                ORDERID = order.OrderId,
                RATING = order.Rating,
                TOTALPRICE = order.Total
            };

            foreach (var item in order.TermekList)
            {
                output.ORDERLISTITEMs.Add(new ORDERLISTITEM()
                {
                    AMOUNT = item.Amount,
                    SUBTOTAL = item.SubTotal,
                    ORDERID = item.Order.OrderId,
                    ORDERLISTITEMID = item.OrderListItemId,
                    PRODUCT = item.Termek.TermekId,
                    PRODUCT1 = this.Entities.PRODUCTs.SingleOrDefault(x => x.PRODUCTID == item.Termek.TermekId)
                });
            }

            return output;
        }
    }
}
