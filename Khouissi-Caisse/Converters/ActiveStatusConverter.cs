using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Khouissi_Caisse.Converters;

/// <summary>
/// Converts a boolean active status to a display string or color
/// </summary>
public class ActiveStatusConverter : IValueConverter
{
    /// <summary>
    /// Converts a boolean to a status string or color
    /// </summary>
    /// <param name="value">The boolean active status</param>
    /// <param name="targetType">The target type</param>
    /// <param name="parameter">Optional parameter</param>
    /// <param name="culture">The culture info</param>
    /// <returns>"فعال" if true, "غير فعال" if false, or a color if parameter is "color"</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isActive)
        {
            if (parameter != null && parameter.ToString() == "color")
            {
                return isActive ? Brushes.Green : Brushes.Red;
            }
            
            return isActive ? "فعال" : "غير فعال";
        }
        
        return DependencyProperty.UnsetValue;
    }

    /// <summary>
    /// Converts back from a status string to a boolean (not implemented)
    /// </summary>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
