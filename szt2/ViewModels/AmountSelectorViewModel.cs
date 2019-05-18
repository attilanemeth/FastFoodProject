// <copyright file="AmountSelectorViewModel.cs" company="PlaceholderCompany">
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
    /// The view model of the AmountSelector window.
    /// </summary>
    public class AmountSelectorViewModel : Bindable
    {
        private string display;

        /// <summary>
        /// Initializes a new instance of the <see cref="AmountSelectorViewModel"/> class.
        /// </summary>
        public AmountSelectorViewModel() => this.Display = "0";

        /// <summary>
        /// Gets or sets the number value on the display.
        /// </summary>
        public string Display { get => this.display; set => this.SetProperty(ref this.display, value); }
    }
}
