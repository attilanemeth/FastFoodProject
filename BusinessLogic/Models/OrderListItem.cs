// <copyright file="OrderListItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// An item contained by the order list.
    /// </summary>
    public class OrderListItem : Bindable
    {
        private int amount;
        private Termek termek;
        private int price;
        private Order order;

        /// <summary>
        /// Gets or sets the ID of an order list item.
        /// </summary>
        public int OrderListItemId { get; set; }

        /// <summary>
        /// Gets or sets the amount of a product in the order.
        /// </summary>
        public int Amount { get => this.amount; set => this.SetProperty(ref this.amount, value); }

        /// <summary>
        /// Gets or sets the subtotal of the products.
        /// </summary>
        public int SubTotal { get => this.price; set => this.SetProperty(ref this.price, value); }

        /// <summary>
        /// Gets or sets the product in the order.
        /// </summary>
        public Termek Termek { get => this.termek; set => this.SetProperty(ref this.termek, value); }

        /// <summary>
        /// Gets or sets the order that contains this item.
        /// </summary>
        public Order Order { get => this.order; set => this.SetProperty(ref this.order, value); }

        /// <inheritdoc/>
        public override string ToString()
        {
            return "x" + this.Amount.ToString() + " " + this.Termek.Name + this.SubTotal + " HUF";
        }
    }
}
