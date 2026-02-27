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
using System.IO;

namespace Example
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Figur figur = null;
        public MainWindow()
        {
            InitializeComponent();

            StreamReader reader = new StreamReader("maze_6x6.txt");
            string inhalt = reader.ReadToEnd();
            string[] zeilen = inhalt.Split('\n');
            

            this.Spielfeld.Background = Brushes.White;

            for (int i = 0; i < zeilen.Length; i++)
            {
                string zeile = zeilen[i];
                for (int j = 0; j < zeile.Length; j++)
                {
                    char c = zeile[j];
                    if (c == '#')
                    {
                        Canvas c1 = new Canvas();
                        c1.Background = Brushes.Black;
                        c1.Width = 20;
                        c1.Height = 20;
                        Canvas.SetTop(c1, i * 20);
                        Canvas.SetLeft(c1, j * 20);
                        Spielfeld.Children.Add(c1);

                    }
                    else if (c == 'X')
                    {
                        figur = new Figur(j * 20, i * 20);
                        Spielfeld.Children.Add(figur.GetEllipse());
                        Canvas.SetLeft(figur.GetEllipse(), j * 20);
                        Canvas.SetTop(figur.GetEllipse(), i * 20);
                    }

                  
                }


            }
        }
        

        private void Window_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.Key == Key.Left)
                {
                    figur.Bewegen(-1, 0);
                }
                else if (e.Key == Key.Right)
                {
                    figur.Bewegen(1, 0);
                }
                else if (e.Key == Key.Up)
                {
                    figur.Bewegen(0, -1);
                }
                else if (e.Key == Key.Down)
                {
                    figur.Bewegen(0, 1);
            }
        }
    }
 }
