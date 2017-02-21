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

namespace MasterMindGraph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _trys = 0;
        private MasterMindModel model = new MasterMindModel();
        int _pos = 9;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void tstKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool containsBadNumbers = false;

            foreach (char c in tstKey.Text)
		        if (c < '1' || c > '7')
                    containsBadNumbers = true;

            int value;

            if (int.TryParse(tstKey.Text, out value))
                if (tstKey.Text.Length == 4 && !containsBadNumbers)
                    buttKlar.IsEnabled = true;

            else
                buttKlar.IsEnabled = false;
        }

        private void buttKlar_Click(object sender, RoutedEventArgs e)
        {
            _trys++;
            string testK = tstKey.Text;
            MatchResult mRes = MasterMindModel.MatchKeys(model._secretKey, testK);
              
            showTestKey(_pos, testK);
            showTestResult(_pos, mRes);
                
            _pos--;
            tstKey.Text = "";

            if (mRes.NumCorrect == 4)
                MessageBox.Show("You won. You beat the game in " + _trys + " trys");

            else if (_trys == 10)
                MessageBox.Show("You lost.");
        }


        private MMRow getMMRowOfPosition(int pos)
        {
            int row = (theGrid.Children.Count - 2) - pos;
            MMRow mmRow = theGrid.Children[row] as MMRow;
            return mmRow;
        }

        private void showTestKey(int pos, string testKey)
        {
            MMRow mmRow = getMMRowOfPosition(_pos);
            mmRow.ShowTestKey(testKey);
        }

        private void showTestResult(int pos, MatchResult mr)
        {
            MMRow mmRow = getMMRowOfPosition(_pos);
            mmRow.ShowTestResult(mr, false);
        }
    }
}
