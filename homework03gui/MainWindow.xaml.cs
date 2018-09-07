using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Substrate;
using Substrate.Core;
using System.Net;
using System.Windows.Ink;
using System.Windows.Media.Animation;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace homework03gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Used substrate to read in map files
    /// Used https://blog.scottlogic.com/2010/11/15/using-a-grid-as-the-panel-for-an-itemscontrol.html and modified it for the central map grid. I hate people who use spaces in their code haha, but I'm glad he made what he did. Tricky, his stuff is the grid utils griditemscontrol and phantom panel
    /// and is just for binding a grid inside a itemscontrol which is way harder than it should be
    /// Everything is extremely tightly coupled and the program is arguably garbage but I only had one night so take it easy, also tons of code behind LMAO
    /// </summary>
    public partial class MainWindow : Window
    {
        mapReader mcReader = new mapReader("./a/a/");

        public MainWindow()
        {
            this.DataContext = mcReader;
            InitializeComponent();
        }

        private void goNorth(object sender, RoutedEventArgs e)
        {
            var test = mcReader.goNorth();
            if (test != true) MessageBox.Show("Chunk does not exist!");
        }

        private void goSouth(object sender, RoutedEventArgs e)
        {
            var test = mcReader.goSouth();
            if (test != true) MessageBox.Show("Chunk does not exist!");
        }

        private void goEast(object sender, RoutedEventArgs e)
        {
            var test = mcReader.goEast();
            if (test != true) MessageBox.Show("Chunk does not exist!");
        }

        private void goWest(object sender, RoutedEventArgs e)
        {
            var test = mcReader.goWest();
            if (test != true) MessageBox.Show("Chunk does not exist!");
        }

        private void UpALayer_Click(object sender, RoutedEventArgs e)
        {
            if (mcReader.CurrentHeight < 255)
            {
                mcReader.CurrentHeight++;
                mcReader.readASlice();
            }
        }

        private void DownALayer_Click(object sender, RoutedEventArgs e)
        {
            if (mcReader.CurrentHeight > 0)
            {
                mcReader.CurrentHeight--;
                mcReader.readASlice();
            }
        }
    }
}
