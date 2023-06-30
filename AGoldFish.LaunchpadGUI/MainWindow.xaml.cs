using MaterialDesignThemes.Wpf;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AGoldFish.Launchpad;
using System.Threading;
using SpecialCases;
using System.Diagnostics;

namespace AGoldFish.LaunchpadGUI
{
    /// <summary>
    /// A WPF application for EDI developers to open commonly used applications on the fly
    /// </summary>
    public partial class MainWindow : Window
    {
        LaunchpadDevice device;
        LaunchCoords LaunchCoords;
        bool StateClosed = true;
        
        public MainWindow()
        {
            InitializeComponent();
            LaunchCoords = new LaunchCoords();
            AddMenuItems();
            CreateGrid();
            InitLaunchpad();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        //menu items
        const string menu1 = "Vol";
        const string menu2 = "Pan";
        const string menu3 = "Snd A";
        const string menu4 = "Snd B";
        const string menu5 = "Stop";
        const string menu6 = "Trk On";
        const string menu7 = "Solo";
        const string menu8 = "Arm";

        private void AddMenuItems()
        {
            List<MenuItem> menu = new List<MenuItem>();

            menu.Add(new MenuItem(menu1, PackIconKind.MenuRightOutline));
            menu.Add(new MenuItem(menu2, PackIconKind.MenuRightOutline));
            menu.Add(new MenuItem(menu3, PackIconKind.MenuRightOutline));
            menu.Add(new MenuItem(menu4, PackIconKind.MenuRightOutline));
            menu.Add(new MenuItem(menu5, PackIconKind.MenuRightOutline));
            menu.Add(new MenuItem(menu6, PackIconKind.MenuRightOutline));
            menu.Add(new MenuItem(menu7, PackIconKind.MenuRightOutline));
            menu.Add(new MenuItem(menu8, PackIconKind.MenuRightOutline));
            ListViewMenu.ItemsSource = menu;

        }

        private void OnMouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            ListViewItem lbi = sender as ListViewItem;
            if (lbi != null)
            {
                MenuItem mi = lbi.Content as MenuItem;
                switch (mi.Name)
                {
                    case menu1:
                        break;
                    case menu2:
                        break;
                    case menu3:
                        break;
                    case menu4:
                        break;
                    case menu5:
                        break;
                    case menu6:
                        break;
                    case menu7:
                        break;
                    case menu8:
                        break;
                }
            }
        }

        private void CreateGrid()
        {
            //set up grid definitions
            for (int i = 0; i < 8; ++i)
            {
                LaunchpadGrid.ColumnDefinitions.Add(new ColumnDefinition());
                var btnHeight = new RowDefinition();
                btnHeight.Height = new GridLength(60, GridUnitType.Pixel);
                LaunchpadGrid.RowDefinitions.Add(btnHeight);
            }

            //add btns
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    Button coordBtn = new Button();
                    //coordBtn.Content = i + "_ " + j;
                    coordBtn.Name = "c" + i + "_" + j;
                    coordBtn.Click += GridPressed;
                    coordBtn.Height = 60;
                    coordBtn.Margin = new Thickness(3);
                    Grid.SetColumn(coordBtn, j);
                    Grid.SetRow(coordBtn, i);
                    LaunchpadGrid.Children.Add(coordBtn);
                }
        }

        private void GridPressed(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)e.Source;
            string btnName = btn.Name;
            var removeLetterC = btnName.Remove(0, 1);

            string[] coords = removeLetterC.Split('_');
            //string message = coords[0] + " X " + coords[1] + " Y pressed";
            int y = Int32.Parse(coords[0]);
            int x = Int32.Parse(coords[1]);
            LaunchCoords.startCoords(x, y);
            //MessageBox.Show(message);

        }

        private void InitLaunchpad()
        {
            try
            {
                device = new LaunchpadDevice();
                device.DoubleBuffered = true;

                Console.WriteLine("Launchpad found");
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Btn_Press btn_Press = new Btn_Press(device);
                    btn_Press.Run();
                }).Start();
            }
            catch
            {
                Console.WriteLine("No launchpad found");
                return;
            }
        }

        private void ButtonMinScreen_Click(object sender, RoutedEventArgs e)
        {
            WindowState = System.Windows.WindowState.Minimized;
        }

        private void ButtonFullScreen_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Maximized)
                WindowState = System.Windows.WindowState.Normal;
            else
                WindowState = System.Windows.WindowState.Maximized;
        }

        private void ButtonQuit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            if (StateClosed)
            {
                Storyboard sb = this.FindResource("CloseMenu") as Storyboard;
                sb.Begin();
            }
            else
            {
                Storyboard sb = this.FindResource("OpenMenu") as Storyboard;
                sb.Begin();
            }

            StateClosed = !StateClosed;
        }
    }
}
