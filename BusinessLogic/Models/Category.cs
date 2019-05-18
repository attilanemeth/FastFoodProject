// <copyright file="Category.cs" company="PlaceholderCompany">
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
    /// The category of a product.
    /// </summary>
    public class Category : Bindable, IMenuItem
    {
        private string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// </summary>
        public Category()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// </summary>
        /// <param name="name">The name of the category.</param>
        public Category(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets the ID of the category.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        public string Name { get => this.name; set => this.SetProperty(ref this.name, value); }

        /// <summary>
        /// Gets or sets the picture of the category to be displayed.
        /// </summary>
        public string Picture { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
