// <copyright file="EditorViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Szt2.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessLogic;

    /// <summary>
    /// The view model of the editor window.
    /// </summary>
    public class EditorViewModel : Bindable
    {
        private IMenuItem menuitem;
        private Termek termek;
        private Category category;
        private string pictureSource;
        private bool isProduct;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorViewModel"/> class.
        /// </summary>
        public EditorViewModel()
        {
            this.menuitem = new Termek();
            this.termek = new Termek();
            this.category = new Category();
        }

        /// <summary>
        /// Gets or sets the menu item.
        /// </summary>
        public IMenuItem MenuItem { get => this.menuitem; set => this.SetProperty(ref this.menuitem, value); }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        public Termek Termek { get => this.termek; set => this.SetProperty(ref this.termek, value); }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public Category Category { get => this.category; set => this.SetProperty(ref this.category, value); }

        /// <summary>
        /// Gets or sets the source of the picture.
        /// </summary>
        public string PictureSource { get => this.pictureSource; set => this.SetProperty(ref this.pictureSource, value); }

        /// <summary>
        /// Gets or sets a value indicating whether the menu item is a product.
        /// </summary>
        public bool IsProduct { get => this.isProduct; set => this.SetProperty(ref this.isProduct, value); }
    }
}
