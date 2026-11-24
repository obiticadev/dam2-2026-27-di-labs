using System.Globalization;

namespace Ej4_Tienda.Converters
{
    // Convierte el bool "vip" en el texto "Sí" / "No" para mostrarlo en las listas
    public class VipTextoConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is true ? "Sí" : "No";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value as string == "Sí";
        }
    }

    // Convierte el bool "vip" en un color: estrella amarilla si es VIP, negra si no
    public class VipColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is true ? Color.FromArgb("#E0A800") : Colors.Black;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
