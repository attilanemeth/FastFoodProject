// <copyright file="OrderListConverter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Szt2.Converter
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using System.Windows.Data;

    /// <summary>
    /// Converter for the order list header width.
    /// </summary>
    public class OrderListConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ListView listview = value as ListView;
            double width = 5;

            if (listview.IsVisible)
            {
                width = listview.ActualWidth;
                GridView gv = listview.View as GridView;
                for (int i = 0; i < gv.Columns.Count; i++)
                {
                    if (!double.IsNaN(gv.Columns[i].Width))
                    {
                        width -= gv.Columns[i].Width;
                    }
                }
            }

            return width - 10;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
