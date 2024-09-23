using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Events_1___Interest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            decimal capital = 0.0M;
            decimal desiredCapital = 0.0M;
            decimal interest = 0.0M;
            bool isFailed = !decimal.TryParse(startCapitalTextBox.Text, out capital) ||
                !decimal.TryParse(desiredEndCapitalTextBox.Text, out desiredCapital) ||
                !decimal.TryParse(interestTextBox.Text, out interest);

            if (isFailed)
            {
                clearButton_Click(this, null);
                return;
            }

            if (capital >= desiredCapital)
            {
                resultTextBox.Text = $"Waarde na 0 jaren: {capital}";
                return;
            }

            int numberOfYears = 0;

            StringBuilder sb = new StringBuilder();
            do
            {
                numberOfYears++;
                capital *= (1.0M + (interest / 100.0M)); // capital += (interest / 100.0M) * capital;
                sb.AppendLine($"Waarde na {numberOfYears,2} jaren: {capital:c}");
            } while (capital < desiredCapital);
            resultTextBox.Text = sb.ToString();
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            startCapitalTextBox.Clear();
            desiredEndCapitalTextBox.Clear();
            interestTextBox.Clear();
            resultTextBox.Clear();
            startCapitalTextBox.Focus();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (
                (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                (e.Key >= Key.D0 && e.Key <= Key.D9 && e.KeyboardDevice.Modifiers == ModifierKeys.Shift) ||
                e.Key == Key.OemComma ||
                e.Key == Key.Tab
               )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
