using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using System.Windows.Shapes;
using ENCX;
using Encx.Wpf;
using System;
using System.Drawing.Drawing2D;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Text.RegularExpressions;
using System.Threading;

namespace NC.Control.Samples
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //private UserService _mainToolbarFacade;

        //public MainWindow()
        //{
        //    InitializeComponent();
        //    _mainToolbarFacade = new UserService();
        //    Loaded+=MainWindow_Loaded;
        //}

        //private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        //{
        //    _mainToolbarFacade.MainToolbarButtonClickEvent+=_mainToolbarFacade_MainToolbarButtonClickEvent;
        //}

        //private void _mainToolbarFacade_MainToolbarButtonClickEvent(object sender, Panel.Service.MainToolbarButtonClickEventArgs args)
        //{

        //}
        public MainWindow()
        {
            InitializeComponent();          
            NavCanvasStyle();
            stkNav.Visibility = Visibility.Hidden;
            cvNav.Visibility = Visibility.Hidden;

        }

        /// <summary>
        /// 界面辅助:处理导航Canvas样式
        /// </summary>
        public void NavCanvasStyle()
        {
            //导航栏按钮
            btnNav.MouseEnter += new MouseEventHandler((w, e) =>
            {
                this.btnNav.Opacity = 0.3;
            });
            btnNav.MouseLeave += new MouseEventHandler((w, e) =>
            {
                this.btnNav.Opacity = 0;
            });

            btnNav.Click += new RoutedEventHandler((w, e) =>
            {
                if (stkNav.Visibility == Visibility.Hidden && cvNav.Visibility == Visibility.Hidden)
                {
                    cvNav.Visibility = Visibility.Visible;
                    stkNav.Visibility = Visibility.Visible;
                }
                else
                {
                    cvNav.Visibility = Visibility.Hidden;
                    stkNav.Visibility = Visibility.Hidden;
                }
            });



            btnNav02.MouseEnter += new MouseEventHandler((w, e) =>
            {
                cvNav02Console.Visibility = Visibility.Visible;
                cvNav03Console.Visibility = Visibility.Hidden;
                cvNav04Console.Visibility = Visibility.Hidden;
                cvNav05Console.Visibility = Visibility.Hidden;
                cvNav05_02Console.Visibility = Visibility.Hidden;
            });


            btnNav03.MouseEnter += new MouseEventHandler((w, e) =>
            {
                cvNav02Console.Visibility = Visibility.Hidden;
                cvNav04Console.Visibility = Visibility.Hidden;
                cvNav03Console.Visibility = Visibility.Visible;
                cvNav05Console.Visibility = Visibility.Hidden;
                cvNav05_02Console.Visibility = Visibility.Hidden;
            });


            btnNav04.MouseEnter += new MouseEventHandler((w, e) =>
            {
                cvNav04Console.Visibility = Visibility.Visible;
                cvNav02Console.Visibility = Visibility.Hidden;
                cvNav03Console.Visibility = Visibility.Hidden;
                cvNav05Console.Visibility = Visibility.Hidden;    
                 cvNav05_02Console.Visibility = Visibility.Hidden;
            });

            btnNav05.MouseEnter += new MouseEventHandler((w,e)=> 
            {
                cvNav05Console.Visibility = Visibility.Visible;
                cvNav02Console.Visibility = Visibility.Hidden;
                cvNav03Console.Visibility = Visibility.Hidden;
                cvNav04Console.Visibility = Visibility.Hidden;
            });


            //btnNav02.MouseLeave += new MouseEventHandler((w, e) =>
            //{
            //    cvNav02Console.Visibility = Visibility.Hidden;
            //});

            //foreach (Button item in stkNav.Children)
            //{
            //    item.MouseEnter += new MouseEventHandler((w, e) =>
            //    {
            //        cvNav02Console.Visibility = Visibility.Visible;
            //    });
            //    item.MouseLeave += new MouseEventHandler((w, e) =>
            //    {
            //        cvNav02Console.Visibility = Visibility.Hidden;
            //    });
            //}

            //btnNav01.Click += new RoutedEventHandler((w, e) =>
            //{
            //    //ImageBrush brush = new ImageBrush();
            //    //brush.ImageSource = new BitmapImage(new Uri("./Icon/nav01.png", UriKind.Relative));
            //    //btnNav01.Background = brush;
            //    //btnNav01.Opacity = 1.0;
            //    //btnNav01.Width = 110;
            //});


            //关闭系统
            btnNav10.Click += new RoutedEventHandler((w, e) =>
            {
                this.Close();
            });

            //关闭导航2控制台
            btnCvNav02ConsoleClose.Click += new RoutedEventHandler((w, e) =>
            {
                cvNav02Console.Visibility = Visibility.Hidden;
            });

            //关闭导航3控制台
            btnNav03ConsoleClose.Click += new RoutedEventHandler((w, e) =>
            {
                cvNav03Console.Visibility = Visibility.Hidden;
            });

            //关闭导航4控制台,即预设航线控制台
            btnNav04ConsoleClose.Click += new RoutedEventHandler((w, e) =>
            {
                cvNav04Console.Visibility = Visibility.Hidden;
            });

            //关闭导航5控制台，即模式变更控制台
            btnNav05ConsoleClose.Click += new RoutedEventHandler((w,e)=> 
            {
                cvNav05Console.Visibility = Visibility.Hidden;                
            });


            //初始化辅助Label选中第一项
            lbNav04ComboxSelectedValue.Content = combNav04_01.Items.Count > 0 ? (combNav04_01.Items[0] as ComboBoxItem).Content : "";

            //导航4的ComboBox辅助处理选项选中时
            combNav04_01.SelectionChanged += new SelectionChangedEventHandler((w, e) =>
            {
                if (combNav04_01.SelectedIndex != -1)
                    lbNav04ComboxSelectedValue.Content = ((ComboBoxItem)combNav04_01.SelectedItem).Content;
            });


            //导航4控制台,即预设航线控制台的确认和取消样式
            //btnNav04Submit.MouseLeftButtonDown += new MouseButtonEventHandler((w, e) =>
            //{
            //    btnNav04Submit.Opacity = 1;
            //});
            //btnNav04Submit.MouseLeftButtonUp += new MouseButtonEventHandler((w, e) =>
            //{
            //    btnNav04Submit.Opacity = 0;
            //});
            //btnNav04Cancel.MouseLeftButtonDown += new MouseButtonEventHandler((w, e) =>
            //{
            //    btnNav04Cancel.Opacity = 1;
            //});
            //btnNav04Cancel.MouseLeftButtonUp += new MouseButtonEventHandler((w, e) =>
            //{
            //    btnNav04Cancel.Opacity = 0;
            //});       









            bool statusNav03_01_01 = false;
            bool statusNav03_01_02 = false;
            bool statusNav03_01_03 = false;
            bool statusNav03_01_04 = false;
            bool statusNav03_01_05 = false;
            bool statusNav03_01_06 = false;
            bool statusNav03_01_07 = false;
            btnNav03_01_01.Click += new RoutedEventHandler((w, e) =>
            {
                statusNav03_01_01 = !statusNav03_01_01;
                if (statusNav03_01_01) btnNav03_01_01.Opacity = 1;
                else btnNav03_01_01.Opacity = 0;
            });
            btnNav03_01_02.Click += new RoutedEventHandler((w, e) =>
            {
                statusNav03_01_02 = !statusNav03_01_02;
                if (statusNav03_01_02) btnNav03_01_02.Opacity = 1;
                else btnNav03_01_02.Opacity = 0;
            });
            btnNav03_01_03.Click += new RoutedEventHandler((w, e) =>
            {
                statusNav03_01_03 = !statusNav03_01_03;
                if (statusNav03_01_03) btnNav03_01_03.Opacity = 1;
                else btnNav03_01_03.Opacity = 0;
            });
            btnNav03_01_04.Click += new RoutedEventHandler((w, e) =>
            {
                statusNav03_01_04 = !statusNav03_01_04;
                if (statusNav03_01_04) btnNav03_01_04.Opacity = 1;
                else btnNav03_01_04.Opacity = 0;
            });
            btnNav03_01_05.Click += new RoutedEventHandler((w, e) =>
            {
                statusNav03_01_05 = !statusNav03_01_05;
                if (statusNav03_01_05) btnNav03_01_05.Opacity = 1;
                else btnNav03_01_05.Opacity = 0;
            });
            btnNav03_01_06.Click += new RoutedEventHandler((w, e) =>
            {
                statusNav03_01_06 = !statusNav03_01_06;
                if (statusNav03_01_06) btnNav03_01_06.Opacity = 1;
                else btnNav03_01_06.Opacity = 0;
            });
            btnNav03_01_07.Click += new RoutedEventHandler((w, e) =>
            {
                statusNav03_01_07 = !statusNav03_01_07;
                if (statusNav03_01_07) btnNav03_01_07.Opacity = 1;
                else btnNav03_01_07.Opacity = 0;
            });


            btnNav05_01.MouseEnter += new MouseEventHandler((w,e)=> 
            {                
                cvNav05_02Console.Visibility = Visibility.Visible;
                //cvNav05Console.Visibility = Visibility.Hidden;
            });

            //定点参数界面辅助
            btnNav05SpeedAdd.Click += new RoutedEventHandler((w,e)=> 
            {
                if (Regex.IsMatch(txbNav05Speed.Text, @"^[+-]?\d*$")&& txbNav05Speed.Text.Length!=0)
                    txbNav05Speed.Text = (Convert.ToInt32(txbNav05Speed.Text) + 1).ToString(); 
            });
            btnNav05SpeedSub.Click += new RoutedEventHandler((w, e) =>
            {
                if (Regex.IsMatch(txbNav05Speed.Text, @"^[+-]?\d*$") && txbNav05Speed.Text.Length != 0)
                    txbNav05Speed.Text = (Convert.ToInt32(txbNav05Speed.Text) - 1).ToString();
            });

            //关闭顶点参数
            btnNav05_02ConsoleClose.Click += new RoutedEventHandler((w,e)=> 
            {
                cvNav05_02Console.Visibility = Visibility.Hidden;              
            });




        }



        /// <summary>
        /// 界面辅助：拖动窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void DragWindow(object sender, MouseButtonEventArgs args)
        {
            this.DragMove();
        }  
    }
}

