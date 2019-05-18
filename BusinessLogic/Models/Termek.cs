// <copyright file="Termek.cs" company="PlaceholderCompany">
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
    /// A product to order.
    /// </summary>
    public class Termek : Bindable, IMenuItem
    {
        private int price;
        private Category category;
        private string name;
        private string picture;

        /// <summary>
        /// Initializes a new instance of the <see cref="Termek"/> class.
        /// </summary>
        public Termek()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Termek"/> class.
        /// </summary>
        /// <param name="name">The name of the product.</param>
        /// <param name="price">The price of the product.</param>
        /// <param name="category">The category of the product.</param>
        public Termek(string name, int price, Category category)
        {
            this.Name = name;
            this.Price = price;
            this.Category = category;
        }

        /// <summary>
        /// Gets or sets the ID of the product.
        /// </summary>
        public int TermekId { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public int Price { get => this.price; set => this.SetProperty(ref this.price, value); }

        /// <summary>
        /// Gets or sets the category of the product.
        /// </summary>
        public Category Category { get => this.category; set => this.category = value; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get => this.name; set => this.SetProperty(ref this.name, value); }

        /// <summary>
        /// Gets or sets the picture of the product to be displayed.
        /// </summary>
        public string Picture { get => this.picture; set => this.SetProperty(ref this.picture, value); }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Name + "\t" + this.Price;
        }
    }
}
