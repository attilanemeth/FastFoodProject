// <copyright file="RatingWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Szt2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;
    using BusinessLogic;
    using DataLayer;
    using Szt2.ViewModels;

    /// <summary>
    /// Interaction logic for RatingWindow.xaml
    /// </summary>
    public partial class RatingWindow : Window
    {
        private Order order;
        private OrderRepository repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="RatingWindow"/> class.
        /// </summary>
        public RatingWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RatingWindow"/> class.
        /// </summary>
        /// <param name="order">The order to rate.</param>
        public RatingWindow(Order order)
            : this()
        {
            this.order = order;
        }

        private void RateClick(object sender, RoutedEventArgs e)
        {
            this.repo = new OrderRepository((this.DataContext as ViewModel).Ctx);
            DataConverter converter = new DataConverter((this.DataContext as ViewModel).Ctx);
            this.order.Rating = int.Parse((sender as Button).Tag.ToString());
            this.repo.Update(converter.OrderConverter(this.order));
            this.Close();
        }
    }
}
