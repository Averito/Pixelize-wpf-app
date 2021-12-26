using System;
using System.Windows;
using System.Drawing;
using System.Windows.Media.Imaging;

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
	}
}