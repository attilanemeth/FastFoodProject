// <copyright file="Order.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// The order of the customer.
    /// </summary>
    public class Order : Bindable
    {
        private ObservableCollection<OrderListItem> termekList;
        private int total;
        private DateTime date;
        private int rating;

        /// <summary>
        /// Initializes a new instance of the <see cref="Order"/> class.
        /// </summary>
        public Order()
        {
            this.TermekList = new ObservableCollection<OrderListItem>();
        }

        /// <summary>
        /// Gets or sets the ID of the order.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the collection of order list items.
        /// </summary>
        public ObservableCollection<OrderListItem> TermekList { get => this.termekList; set => this.SetProperty(ref this.termekList, value); }

        /// <summary>
        /// Gets or sets the total price of the order.
        /// </summary>
        public int Total { get => this.total; set => this.SetProperty(ref this.total, value); }

        /// <summary>
        /// Gets or sets the date when the order is payed.
        /// </summary>
        public DateTime Date { get => this.date; set => this.SetProperty(ref this.date, value); }

        /// <summary>
        /// Gets or sets the rating of the order.
        /// </summary>
        public int Rating { get => this.rating; set => this.SetProperty(ref this.rating, value); }

        /// <summary>
        /// Adds a product to the order's product list.
        /// </summary>
        /// <param name="product">Product to be added.</param>
        public void AddToTermekList(Termek product)
        {
            int i = 0;
            while (i < this.TermekList.Count && !this.TermekList[i].Termek.Equals(product))
            {
                i++;
            }

            if (i >= this.TermekList.Count)
            {
                this.TermekList.Add(new OrderListItem() { Termek = product, Order = this });
            }

            this.TermekList[i].Amount++;
            this.TermekList[i].SubTotal = this.TermekList[i].Amount * product.Price;
        }

        /// <summary>
        /// Adds a specific number of products to the order's product list.
        /// </summary>
        /// <param name="product">Product to be added.</param>
        /// <param name="db">The number of the products.</param>
        public void AddToTermekList(Termek product, int db)
        {
            int i = 0;
            while (i < this.TermekList.Count && !this.TermekList[i].Termek.Equals(product))
            {
                i++;
            }

            if (i >= this.TermekList.Count)
            {
                this.TermekList.Add(new OrderListItem() { Amount = 1, Termek = product, Order = this });
            }

            this.TermekList[i].Amount = db;
            this.TermekList[i].SubTotal = this.TermekList[i].Amount * product.Price;

            if (db == 0)
            {
                this.TermekList.Remove(this.TermekList[i]);
            }
        }

        /// <summary>
        /// Calculates the total price of the order.
        /// </summary>
        /// <param name="termekList">Collection of order list items.</param>
        /// <returns>The total price.</returns>
        public int TotalPrice(ObservableCollection<OrderListItem> termekList)
        {
            int sum = 0;
            if (termekList.Count > 0)
            {
                foreach (OrderListItem t in termekList)
                {
                    sum += t.Amount * t.Termek.Price;
                }
            }

            return sum;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            string output = this.Date + "\t"
                + this.Total + "\t";

            foreach (OrderListItem t in this.TermekList)
            {
                output += t.Amount + "\t" + t.Termek + "\t";
            }

            return output;
        }
    }
}
