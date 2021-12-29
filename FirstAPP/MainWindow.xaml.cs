using System;
using System.Windows;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Color = System.Drawing.Color;

namespace FirstAPP
{
	public partial class MainWindow
	{
		private string CurrentImage { get; set; }

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
			
				dlg.DefaultExt = ".jpg";
				dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

				Nullable<bool> result = dlg.ShowDialog();

				if (result == false) return;

				Bitmap beforeImg = new Bitmap(dlg.FileName);
				Bitmap afterImg = GraphicAlgoritms.Pixelize(beforeImg, pixelizeDegreeInt);
				BitmapImage normalizeImg = GraphicAlgoritms.BitmapToImageSource(afterImg);
				
				string filename = dlg.FileName;
				CurrentImage = dlg.FileName;
				Image1.Source = normalizeImg;
			}
			catch (ArgumentOutOfRangeException err)
			{
				MessageBox.Show(err.Message);
			}
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

				if (CurrentImage == string.Empty)
				{
					MessageBox.Show("Сохранять нечего");
					return;
				}

				Bitmap beforeNewPixelize = new Bitmap(CurrentImage);
				Bitmap afterNewPixelize = GraphicAlgoritms.Pixelize(beforeNewPixelize, pixelizeDegreeInt);
				BitmapImage normilizeNewPixelizePicture = GraphicAlgoritms.BitmapToImageSource(afterNewPixelize);

				Image1.Source = normilizeNewPixelizePicture;
			}
			catch (Exception err)
			{
				MessageBox.Show(err.Message);
			}
		}
		private void Button3_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (CurrentImage == string.Empty)
				{
					MessageBox.Show("Сохранять нечего");
					return;
				}

				SaveFileDialog dlgMenu = new SaveFileDialog();

				string fileName = "";
				for (var i = 0; i < new Random().Next(50); i++) fileName += new Random().Next(10);
				dlgMenu.FileName = fileName;
				dlgMenu.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


				Nullable<bool> resultDlgMenu = dlgMenu.ShowDialog();
				if (resultDlgMenu == false) return;
				
				BitmapImage bitmapImage = Image1.Source as BitmapImage;
				
				Bitmap photoForSave = GraphicAlgoritms.BitmapImageToBitmap(bitmapImage);
				photoForSave.Save(dlgMenu.FileName);
				MessageBox.Show("Успешно сохранено!");
			}
			catch (Exception err)
			{
				MessageBox.Show(err.Message);
			}
		}
		private void Button_MouseEnter(object sender, RoutedEventArgs e)
		{
			(sender as Button).Background = new SolidColorBrush(Colors.Transparent);
			(sender as Button).Foreground = new SolidColorBrush(Colors.Crimson);
		}
		private void Button_MouseLeave(object sender, RoutedEventArgs e)
		{
			(sender as Button).Background = new SolidColorBrush(Colors.Crimson);
			(sender as Button).Foreground = new SolidColorBrush(Colors.White);
		}
	}
}