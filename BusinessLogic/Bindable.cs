// <copyright file="Bindable.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Parent of every bindable class.
    /// </summary>
    public class Bindable : INotifyPropertyChanged
    {
        /// <summary>
        /// Event when a property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Invokes the PropertyChanged event.
        /// </summary>
        /// <param name="name">Name of the property that changed.</param>
        protected void OnPropertyChanged(string name = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Sets a property of a bindable class.
        /// </summary>
        /// <typeparam name="T">The type of the field to be changed.</typeparam>
        /// <param name="field">The reference of the field to be changed.</param>
        /// <param name="value">The new value of the property.</param>
        /// <param name="name">The name of the property</param>
        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string name = null)
        {
            field = value;
            this.OnPropertyChanged(name);
        }
    }
}
