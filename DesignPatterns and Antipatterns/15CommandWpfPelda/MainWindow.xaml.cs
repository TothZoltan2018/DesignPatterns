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

namespace _15CommandWpfPelda
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// A mukodes a felhasznaloi felulet mogott.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            //A MainWindowViewModel.cs-ben definialt Start() fgv-t hivja meg.
            ((MainWindowViewModel)this.DataContext).Start();
            
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            //A MainWindowViewModel.cs-ben definialt Stop() fgv-t hivja meg.
            ((MainWindowViewModel)this.DataContext).Stop();

        }
    }
}
