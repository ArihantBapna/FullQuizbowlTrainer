using System;
using System.Globalization;
using Xamarin.Forms;

namespace FullQuizbowlTrainer.Services.Converters
{
    public class NameToColourConverter : IValueConverter
    { 
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Label c = (Label)parameter;
            Console.WriteLine(c.Text +" is the parameter value");
            if (c.Text == "Mythology") return Color.Teal;
            else if (c.Text == "Literature") return Color.Cyan;
            else if (c.Text == "Trash") return Color.Crimson;
            else if (c.Text == "Science") return Color.Indigo;
            else if (c.Text == "History") return Color.FromHex("#647687");
            else if (c.Text == "Religion") return Color.FromHex("#0050EF");
            else if (c.Text == "Geography") return Color.Lime;
            else if (c.Text == "Fine Arts") return Color.Orange;
            else if (c.Text == "Social Science") return Color.YellowGreen;
            else if (c.Text == "Philosophy") return Color.Sienna;
            else return Color.Violet;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
