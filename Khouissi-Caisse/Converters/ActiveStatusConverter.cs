using System;
using System.Globalization;
using System.Windows.Data;

namespace Khouissi_Caisse.Converters;

/// <summary>
/// Converts a boolean active status to a display string
/// </summary>
public class ActiveStatusConverter : IValueConverter
{
    /// <summary>
    /// Converts a boolean to a status string
    /// </summary>
    /// <param name="value">The boolean active status</param>
    /// <param name="targetType">The target type</param>
    /// <param name="parameter">Optional parameter</param>
    /// <param name="culture">The culture info</param>
    /// <returns>"نشط" if true, "غير نشط" if false</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isActive)
        {
            return isActive ? "نشط" : "غير نشط";
        }
        return string.Empty;
    }

    /// <summary>
    /// Converts back from a status string to a boolean (not implemented)
    /// </summary>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
