using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace RobotProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    
        public enum Direction
        {
            right = 1,
            left,
            up,
            down
        }
    
    public partial class MainWindow : Window
    {

        private void NumericOnly(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        int sumRight = 0;
        int sumLeft = 0;
        int sumDown = 0;
        int sumUp = 0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string[] meters = {txt0.Text,txt1.Text, txt2.Text, txt3.Text, txt4.Text,txt5.Text, txt6.Text, txt7.Text, txt8.Text,
                                    txt9.Text, txt10.Text, txt11.Text,txt12.Text,txt13.Text,txt14.Text };

              string [] directions = {cbo0.SelectedValue.ToString(), cbo1.SelectedValue?.ToString(), cbo2.SelectedValue?.ToString(), cbo3.SelectedValue?.ToString(),
                                     cbo4.SelectedValue?.ToString(),cbo5.SelectedValue?.ToString(),cbo6.SelectedValue?.ToString(),cbo7.SelectedValue?.ToString(),
                                     cbo8.SelectedValue?.ToString(),cbo9.SelectedValue?.ToString(),cbo10.SelectedValue?.ToString(),cbo11.SelectedValue?.ToString(),
                                      cbo12.SelectedValue?.ToString(),cbo13.SelectedValue?.ToString(),cbo14.SelectedValue?.ToString()};
           
            if (!isEmptyStringArray(meters, directions))
            {
                lblError.Content = "one of the meter values is filled without a direction";
                return;
            }

            var finalMeters = meters.Where(x => !string.IsNullOrEmpty(x));
            var finalDirections = directions.Where(x => !string.IsNullOrEmpty(x));
           // bool isValid = isEmptyStringArray(finalMeters.ToArray(), finalDirections.ToArray);

            var MetersAndDirection = finalMeters.Zip(finalDirections, (n, w) => new { Meter = n, Direction = w });

            foreach (var item in MetersAndDirection)
            {
                switch (item.Direction)
                {
                    case "right":
                        sumRight += int.Parse(item.Meter);
                        break;
                    case "left":
                        sumLeft += int.Parse(item.Meter);
                        break;
                    case "up":
                        sumDown += int.Parse(item.Meter);
                        break;
                    case "down":
                        sumUp += int.Parse(item.Meter);
                        break;
                    default:
                        lblError.Content = "please rewrite instructions as follows: right/left/up/down and than one space and a number";
                        return;
                }
            }

            int totalRightLeft = sumRight - sumLeft;
            int totalUpDown = sumDown - sumUp;
            lblError.Content = "The robot position is " + totalRightLeft + " and " + totalUpDown;
        }

        static bool isEmptyStringArray(string[] meters, string [] directions)
        {
            for (int i = 0; i < meters.Length; i++)
            {
                if (!string.IsNullOrEmpty(meters[i])&& directions[i]==null)
                {
                    return false;
                }
            }
            return true;
        }

    }
  
}
