// <copyright file="AmountSelector.xaml.cs" company="PlaceholderCompany">
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
    using Szt2.ViewModels;

    /// <summary>
    /// Interaction logic for AmountSelector.xaml
    /// </summary>
    public partial class AmountSelector : Window
    {
        private AmountSelectorViewModel avm;
        private ViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="AmountSelector"/> class.
        /// </summary>
        public AmountSelector()
        {
            this.InitializeComponent();
            this.avm = this.FindResource("VM") as AmountSelectorViewModel;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AmountSelector"/> class.
        /// </summary>
        /// <param name="vm">The ViewModel to use.</param>
        public AmountSelector(ViewModel vm)
            : this()
        {
            this.vm = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var number = (sender as Button).Content;

            if (number.Equals("OK"))
            {
                try
                {
                    if (!this.vm.OrderListEnabled)
                    {
                        var temp = this.vm.SelectedProduct;
                        this.vm.Order = new Order();
                        this.vm.SelectedProduct = temp;
                    }

                    this.vm.Order.AddToTermekList(this.vm.SelectedProduct.Termek, int.Parse(this.avm.Display));
                    this.vm.Order.Total = this.vm.Order.TotalPrice(this.vm.Order.TermekList);
                    this.vm.OrderListEnabled = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    throw;
                }
            }
            else if (number.Equals("Törlés"))
            {
                if (this.avm.Display.Length > 1)
                {
                    this.avm.Display = this.avm.Display.Remove(this.avm.Display.Length - 1);
                }
                else if (this.avm.Display.Length == 1)
                {
                    this.avm.Display = "0";
                }
            }
            else
            {
                if (this.avm.Display.Length < 4)
                {
                    this.avm.Display = this.avm.Display == "0" ? number.ToString() : this.avm.Display + number;
                }
            }
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            if (this.IsVisible)
            {
                this.Close();
            }
        }
    }
}
