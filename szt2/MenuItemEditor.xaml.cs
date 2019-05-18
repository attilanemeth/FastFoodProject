// <copyright file="MenuItemEditor.xaml.cs" company="PlaceholderCompany">
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
    using Microsoft.Win32;
    using Szt2.ViewModels;

    /// <summary>
    /// Interaction logic for MenuItemEditor.xaml
    /// </summary>
    public partial class MenuItemEditor : Window
    {
        private EditorViewModel evm;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemEditor"/> class.
        /// </summary>
        public MenuItemEditor()
        {
            this.InitializeComponent();
            this.evm = this.FindResource("vm") as EditorViewModel;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemEditor"/> class.
        /// </summary>
        /// <param name="isProduct">A value indicating whether the item to edit is a product or not.</param>
        public MenuItemEditor(bool isProduct)
            : this()
        {
            if (isProduct)
            {
                this.priceLabel.Visibility = Visibility.Visible;
                this.priceTextBox.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemEditor"/> class.
        /// </summary>
        /// <param name="menuitem">The menu item to edit.</param>
        public MenuItemEditor(IMenuItem menuitem)
            : this()
        {
            this.evm.MenuItem = menuitem;

            this.evm.Termek = menuitem as Termek;

            if (this.evm.Termek != null)
            {
                this.priceLabel.Visibility = Visibility.Visible;
                this.priceTextBox.Visibility = Visibility.Visible;
            }
            else
            {
                this.evm.Category = menuitem as Category;
            }

            this.evm.PictureSource = menuitem.Picture;
        }

        /// <summary>
        /// Gets the menu item.
        /// </summary>
        public IMenuItem MenuItem
        {
            get { return this.evm.MenuItem; }
        }

        /// <summary>
        /// Gets the product.
        /// </summary>
        public Termek Product
        {
            get { return this.evm.Termek; }
        }

        /// <summary>
        /// Gets the category.
        /// </summary>
        public Category Category
        {
            get { return this.evm.Category; }
        }

        private void SelectImageClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                DefaultExt = "Image files|*.jpeg;*.jpg;*.png;*.bmp",
                Filter = "Image files|*.jpeg;*.jpg;*.png;*.bmp|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|BMP Files (*.bmp)|*.bmp"
            };

            bool? result = fileDialog.ShowDialog();

            if (result == true)
            {
                this.evm.PictureSource = fileDialog.FileName;
            }
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {
            if (this.evm.PictureSource != null)
            {
                this.evm.MenuItem.Picture = this.evm.PictureSource;
            }

            if (this.Product != null)
            {
                this.Product.Name = this.MenuItem.Name;
                this.Product.Picture = this.MenuItem.Picture;
            }

            if (this.Category != null)
            {
                this.Category.Name = this.MenuItem.Name;
                this.Category.Picture = this.MenuItem.Picture;
            }

            this.DialogResult = true;
        }
    }
}
