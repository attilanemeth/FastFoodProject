// <copyright file="AdminViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Szt2.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessLogic;
    using DataLayer;

    /// <summary>
    /// The view model of the admin window.
    /// </summary>
    public class AdminViewModel : Bindable
    {
        private ObservableCollection<Termek> productList;
        private ObservableCollection<Category> categoryList;
        private ObservableCollection<Termek> filteredProducts;
        private Order order;
        private Category selectedCategory;
        private Termek selectedProduct;
        private PosDatabaseEntities ctx;
        private Statistics stats;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminViewModel"/> class.
        /// </summary>
        public AdminViewModel()
        {
            this.ProductList = new ObservableCollection<Termek>();
            this.CategoryList = new ObservableCollection<Category>();
            this.FilteredProducts = new ObservableCollection<Termek>();

            // this.Ctx = new PosContext();
            this.Stats = new Statistics
            {
                DateFrom = DateTime.Now - TimeSpan.FromDays(1),
                DateTo = DateTime.Now
            };
        }

        /// <summary>
        /// Gets or sets the collection of products.
        /// </summary>
        public ObservableCollection<Termek> ProductList { get => this.productList; set => this.SetProperty(ref this.productList, value); }

        /// <summary>
        /// Gets or sets the collection of categories.
        /// </summary>
        public ObservableCollection<Category> CategoryList { get => this.categoryList; set => this.SetProperty(ref this.categoryList, value); }

        /// <summary>
        /// Gets or sets a filtered list of products.
        /// </summary>
        public ObservableCollection<Termek> FilteredProducts { get => this.filteredProducts; set => this.SetProperty(ref this.filteredProducts, value); }

        /// <summary>
        /// Gets or sets the currently selected category.
        /// </summary>
        public Category SelectedCategory { get => this.selectedCategory; set => this.SetProperty(ref this.selectedCategory, value); }

        /// <summary>
        /// Gets or sets the currently selected product.
        /// </summary>
        public Termek SelectedProduct { get => this.selectedProduct; set => this.SetProperty(ref this.selectedProduct, value); }

        /// <summary>
        /// Gets or sets the database context.
        /// </summary>
        public PosDatabaseEntities Ctx { get => this.ctx; set => this.SetProperty(ref this.ctx, value); }

        /// <summary>
        /// Gets or sets the statistics.
        /// </summary>
        public Statistics Stats { get => this.stats; set => this.SetProperty(ref this.stats, value); }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        public Order Order { get => this.order; set => this.SetProperty(ref this.order, value); }
    }
}
