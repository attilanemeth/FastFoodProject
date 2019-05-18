// <copyright file="ViewModel.cs" company="PlaceholderCompany">
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
    /// ViewModel
    /// </summary>
    public class ViewModel : Bindable
    {
        private Order order;
        private ObservableCollection<Order> orderList;
        private ObservableCollection<Category> categoryList;
        private ObservableCollection<Termek> productList;
        private OrderListItem selectedProduct;
        private Termek actualTermek;
        private Termek prevTermek;
        private bool orderListEnabled;
        private PosDatabaseEntities ctx;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel"/> class.
        /// </summary>
        public ViewModel()
        {
            this.Order = new Order();
            this.OrderListEnabled = true;
            ConnectionBuilder cb = new ConnectionBuilder();
            this.Ctx = cb.DatabaseContext;

            // Reads categories from database;
            this.CategoryList = new ObservableCollection<Category>();
            this.CategoryList = DataConverter.CategoryListConverter(this.Ctx.CATEGORies.Select(x => x).ToObservableCollection());

            // Reads products from database
            this.ProductList = new ObservableCollection<Termek>();
            this.ProductList = DataConverter.ProductListConverter(this.Ctx.PRODUCTs.Select(x => x).ToObservableCollection());
            this.OrderList = new ObservableCollection<Order>();
        }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        public Order Order { get => this.order; set => this.SetProperty(ref this.order, value); }

        /// <summary>
        /// Gets or sets the collection of all categories.
        /// </summary>
        public ObservableCollection<Category> CategoryList { get => this.categoryList; set => this.SetProperty(ref this.categoryList, value); }

        /// <summary>
        /// Gets or sets the collection of all products.
        /// </summary>
        public ObservableCollection<Termek> ProductList { get => this.productList; set => this.SetProperty(ref this.productList, value); }

        /// <summary>
        /// Gets or sets the selected order list item.
        /// </summary>
        public OrderListItem SelectedProduct { get => this.selectedProduct; set => this.SetProperty(ref this.selectedProduct, value); }

        /// <summary>
        /// Gets or sets the collection of orders.
        /// </summary>
        public ObservableCollection<Order> OrderList { get => this.orderList; set => this.SetProperty(ref this.orderList, value); }

        /// <summary>
        /// Gets or sets a value indicating whether the order list is enabled to modify.
        /// </summary>
        public bool OrderListEnabled { get => this.orderListEnabled; set => this.SetProperty(ref this.orderListEnabled, value); }

        /// <summary>
        /// Gets or sets the database context.
        /// </summary>
        public PosDatabaseEntities Ctx { get => this.ctx; set => this.SetProperty(ref this.ctx, value); }

        /// <summary>
        /// Gets or sets the actually selected product.
        /// </summary>
        public Termek ActualTermek { get => this.actualTermek; set => this.SetProperty(ref this.actualTermek, value); }

        /// <summary>
        /// Gets or sets the previously selected product.
        /// </summary>
        public Termek PrevTermek { get => this.prevTermek; set => this.SetProperty(ref this.prevTermek, value); }

        /// <summary>
        /// Finds a category instance by its name.
        /// </summary>
        /// <param name="input">The name of the category.</param>
        /// <returns>The category.</returns>
        public Category StringToCategory(string input)
        {
            foreach (Category c in this.CategoryList)
            {
                if (c.Name.Equals(input))
                {
                    return c;
                }
            }

            return null;
        }

        /// <summary>
        /// Finds a product instance by its name.
        /// </summary>
        /// <param name="name">The name of the product.</param>
        /// <returns>The product.</returns>
        public Termek StringToProduct(string name)
        {
            Termek termek = new Termek();

            foreach (Termek c in this.ProductList)
            {
                if (c.Name.Equals(name))
                {
                    termek = c;
                    return termek;
                }
            }

            return null;
        }
    }
}
