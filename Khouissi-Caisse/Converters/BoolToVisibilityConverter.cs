using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Khouissi_Caisse.Converters;

/// <summary>
/// Converts a boolean value to a Visibility value (true = Visible, false = Collapsed)
/// </summary>
public class BoolToVisibilityConverter : IValueConverter
{
    /// <summary>
    /// Converts a boolean to a Visibility
    /// </summary>
    /// <param name="value">The boolean value</param>
    /// <param name="targetType">The target type (Visibility)</param>
    /// <param name="parameter">Optional parameter to invert the conversion (if "invert" or "reverse")</param>
    /// <param name="culture">The culture info</param>
    /// <returns>Visibility.Visible if true, Visibility.Collapsed if false (or reversed if parameter is specified)</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            bool isInverted = IsInvertParameter(parameter);
            return (boolValue ^ isInverted) ? Visibility.Visible : Visibility.Collapsed;
        }

        return Visibility.Collapsed;
    }

    /// <summary>
    /// Converts a Visibility to a boolean
    /// </summary>
    /// <param name="value">The Visibility value</param>
    /// <param name="targetType">The target type (boolean)</param>
    /// <param name="parameter">Optional parameter to invert the conversion (if "invert" or "reverse")</param>
    /// <param name="culture">The culture info</param>
    /// <returns>true if Visible, false if Collapsed (or reversed if parameter is specified)</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility visibility)
        {
            bool isInverted = IsInvertParameter(parameter);
            return (visibility == Visibility.Visible) ^ isInverted;
        }

        return false;
    }

    private static bool IsInvertParameter(object parameter)
    {
        return parameter is string paramString && 
               (paramString.Equals("invert", StringComparison.OrdinalIgnoreCase) || 
                paramString.Equals("reverse", StringComparison.OrdinalIgnoreCase));
    }
}