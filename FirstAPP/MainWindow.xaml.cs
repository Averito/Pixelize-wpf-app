using System;
using System.Windows;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Drawing.Color;

namespace FirstAPP
{
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Button1_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				int pixelizeDegreeInt;
				if (pixelizeDegree.Text == string.Empty || int.TryParse(pixelizeDegree.Text, out pixelizeDegreeInt) == false)
				{
					MessageBox.Show("Ожидается целое число (степень пикселизации)");
					return;
				}
			
				Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
			
				dlg.DefaultExt = ".png";
				dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

				Nullable<bool> result = dlg.ShowDialog();

				if (result == false) return;

				Bitmap beforeImg = new Bitmap(dlg.FileName);
				Bitmap afterImg = GraphicAlgoritms.Pixelize(beforeImg, pixelizeDegreeInt);
				BitmapImage normalizeImg = GraphicAlgoritms.BitmapToImageSource(afterImg);
				
				string filename = dlg.FileName;
				image1.Source = normalizeImg;
			}
			catch (ArgumentOutOfRangeException err)
			{
				MessageBox.Show(err.Message);
			}
		}
		private void Button1_MouseEnter(object sender, RoutedEventArgs e)
		{
			Button1.Background = new SolidColorBrush(Colors.Transparent);
			Button1.Foreground = new SolidColorBrush(Colors.Crimson);
		}
		private void Button1_MouseLeave(object sender, RoutedEventArgs e)
		{
			Button1.Background = new SolidColorBrush(Colors.Crimson);
			Button1.Foreground = new SolidColorBrush(Colors.White);
		}
		private void Button2_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				int pixelizeDegreeInt;
				if (pixelizeDegree.Text == string.Empty ||
				    int.TryParse(pixelizeDegree.Text, out pixelizeDegreeInt) == false)
				{
					MessageBox.Show("Ожидается целое число (степень пикселизации)");
					return;
				}

				if (image1.Source.ToString() == string.Empty) throw new ArgumentNullException("Картинка не выбрана");

				BitmapImage bitmapImage = image1.Source as BitmapImage;

				Bitmap beforeNewPixelize = GraphicAlgoritms.BitmapImageToBitmap(bitmapImage);
				Bitmap afterNewPixelize = GraphicAlgoritms.Pixelize(beforeNewPixelize, pixelizeDegreeInt);
				BitmapImage normilizeNewPixelizePicture = GraphicAlgoritms.BitmapToImageSource(afterNewPixelize);

				image1.Source = normilizeNewPixelizePicture;
			}
			catch (Exception err)
			{
				MessageBox.Show(err.Message);
			}
		}
		private void Button2_MouseEnter(object sender, RoutedEventArgs e)
		{
			Button2.Background = new SolidColorBrush(Colors.Transparent);
			Button2.Foreground = new SolidColorBrush(Colors.Crimson);
		}
		private void Button2_MouseLeave(object sender, RoutedEventArgs e)
		{
			Button2.Background = new SolidColorBrush(Colors.Crimson);
			Button2.Foreground = new SolidColorBrush(Colors.White);
		}
	}
}