// <copyright file="IMenuItem.cs" company="PlaceholderCompany">
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
    /// Interface to a menu item.
    /// </summary>
    public interface IMenuItem
    {
        /// <summary>
        /// Gets or sets the name of the menu item.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the picture of the menu item.
        /// </summary>
        string Picture { get; set; }
    }
}
