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
using MasterMind;
using System.Windows.Controls.Primitives;

namespace MasterMindGraph
{
    /// <summary>
    /// Interaction logic for MMRow.xaml
    /// </summary>
    public partial class MMRow : UserControl
    {
        public MMRow()
        {
            InitializeComponent();
        }


        public void ShowTestKey(string str)
        {
            Brush[] br = new Brush[7] {
                Brushes.LightBlue, Brushes.Blue, Brushes.Yellow, 
                Brushes.Green, Brushes.Red, Brushes.Orange, Brushes.Violet 
            };

            UniformGrid grid = this.gridTestKey as UniformGrid;

            for (int ix = 0; ix < str.Length; ix++) {
                if (ix < grid.Children.Count)
                {
                    Grid cell = grid.Children[ix] as Grid;
                    Ellipse el = cell.Children[0] as Ellipse;
                    Label lab = cell.Children[1] as Label;

                    int value;
                    bool ok = int.TryParse(str[ix].ToString(), out value);
                    
                    if (ok && value >= 1 && value <= 7) {
                        lab.Content = value.ToString();
                        el.Fill = br[value - 1];
                    }
                }
            }
        }


        public void ShowTestResult(MatchResult mr, bool animated)
        {
            UniformGrid grid = this.gridResult as UniformGrid;

            int ix = 0;

            for (int i = 0; i < mr.NumCorrect; i++)
            {
                Ellipse el = grid.Children[ix] as Ellipse;
                el.Fill = Brushes.Black;
                ix += 1;
            }

            for (int i = 0; i < mr.NumSemiCorrect; i++)
            {
                Ellipse el = grid.Children[ix] as Ellipse;
                el.Fill = Brushes.White;
                ix += 1;
            }
        }
    }
}
