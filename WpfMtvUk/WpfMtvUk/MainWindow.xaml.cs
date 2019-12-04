using Microsoft.Extensions.DependencyInjection;
using MtvCoUkParser;
using MtvCoUkParser.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WpfMtvUk
{
    public partial class MainWindow : Window
    {
        private IMtvDriver _mtvDriver
            => Injections.Provider.GetService<IMtvDriver>();

        private IDictionary<string, string> names;

        public MainWindow()
        {
            InitializeComponent();
            names = new Dictionary<string, string>();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (names.Count != 0) names.Clear();
            names = await _mtvDriver.GetChartNamesAsync();
            Charts.ItemsSource = names.Values;
            Charts.SelectedIndex = 0;
        }

        private async void Charts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = (ComboBox)sender;
            var chart = await _mtvDriver.GetChartAsync(ChartId(cb.SelectedItem.ToString()));
            ChartNameLbl.Content = chart.Name.ToUpper();
            ChartBox.ItemsSource = chart.PlayList.Values;
        }

        private void ChartBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lb = (ListBox)sender;
            TrackInfo((Track)lb.SelectedItem);
        }

        private string ChartId(string chartName)
             => names.FirstOrDefault(c => c.Value == chartName).Key;

        private void TrackInfo(Track track)
        {
            if (track != null)
            {
                TrackPosterImg.Source = Load(track.OverlayImgUrl);
                ArtistameLbl.Content = track.Artist.ToUpper();
                TrackNameLbl.Content = track.Name;
            }
        }

        private BitmapImage Load(string imgPath)
        {
            var bitmap = new BitmapImage();
            try
            {
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
                bitmap.UriSource = new Uri(imgPath, UriKind.Absolute);
                bitmap.EndInit();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return bitmap;
        }
    }
}
