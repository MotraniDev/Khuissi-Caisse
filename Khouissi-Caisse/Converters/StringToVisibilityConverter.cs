using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Khouissi_Caisse.Converters;

/// <summary>
/// Converts a string value to a Visibility value (non-empty string = Visible, null or empty = Collapsed)
/// </summary>
public class StringToVisibilityConverter : IValueConverter
{
    /// <summary>
    /// Converts a string to a Visibility
    /// </summary>
    /// <param name="value">The string value</param>
    /// <param name="targetType">The target type (Visibility)</param>
    /// <param name="parameter">Optional parameter to invert the conversion (if "invert" or "reverse")</param>
    /// <param name="culture">The culture info</param>
    /// <returns>Visibility.Visible if string is not null or empty, Visibility.Collapsed otherwise (or reversed if parameter is specified)</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool hasValue = !string.IsNullOrWhiteSpace(value as string);
        bool isInverted = IsInvertParameter(parameter);
        
        return (hasValue ^ isInverted) ? Visibility.Visible : Visibility.Collapsed;
    }

    /// <summary>
    /// Converts a Visibility to a string (not implemented)
    /// </summary>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException("Converting Visibility to string is not supported");
    }

    private static bool IsInvertParameter(object parameter)
    {
        return parameter is string paramString && 
               (paramString.Equals("invert", StringComparison.OrdinalIgnoreCase) || 
                paramString.Equals("reverse", StringComparison.OrdinalIgnoreCase));
    }
}