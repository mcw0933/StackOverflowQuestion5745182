using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.IO;

namespace StackOverflowWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<System.Drawing.Image> images = new List<System.Drawing.Image>();
        private int currImage = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            button1.IsEnabled = false;

            HtmlPaginator pagr = new HtmlPaginator();
            pagr.PageReady += new EventHandler<HtmlPaginator.PageImageEventArgs>(pagr_PageReady);
            //pagr.GeneratePages(StackOverflowWPF.Properties.Resources.bigHtmlString);
            pagr.GeneratePages("http://www.stackoverflow.com");
        }

        void pagr_PageReady(object sender, HtmlPaginator.PageImageEventArgs e)
        {
            images.Add(e.PageImage);
            button1.IsEnabled = true;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (currImage >= images.Count)
            {
                currImage = 0;
            }

            MemoryStream ms = new MemoryStream();
            images[currImage].Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = new MemoryStream(ms.ToArray());
            bitmap.EndInit();

            image1.Source = bitmap;

            currImage += 1;
        }
    }
}
