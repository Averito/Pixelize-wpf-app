using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;
using System.Windows;


namespace FirstAPP
{
	public class GraphicAlgoritms
	{
		public static Bitmap Pixelize(Bitmap image, int pixelDegree)
		{
			if (pixelDegree < 1) throw new ArgumentOutOfRangeException("Слишком маленькое число, требуется не менее 1.");
			if (pixelDegree == 1) return image;
			
			int pixelizeStep = pixelDegree;

			for (int x = 0; x < image.Width; x += pixelizeStep)
			for (int y = 0; y < image.Height; y += pixelizeStep)
			{
				int remainderPixelizeWidth = pixelizeStep;
				int remainderPixelizeHeight = pixelizeStep;
				if (x + pixelizeStep > image.Width)
				{
					remainderPixelizeWidth = image.Height - x;
				}

				if (y + pixelizeStep > image.Height)
				{
					remainderPixelizeHeight = image.Height - y;
				}

				if (x >= image.Width) MessageBox.Show("Попався");

				var currentColor = image.GetPixel(Clamp(x, image.Width), Clamp(y, image.Height));

				for (int xTwo = 0; xTwo < remainderPixelizeWidth; xTwo++)
				for (int yTwo = 0; yTwo < remainderPixelizeHeight; yTwo++)
				{
					image.SetPixel(Clamp(x + xTwo, image.Width), Clamp(y + yTwo, image.Height), currentColor);
				}
			}

			return image;
		}

		public static BitmapImage BitmapToImageSource(Bitmap bitmap)
		{
			using (MemoryStream memory = new MemoryStream())
			{
				bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
				memory.Position = 0;
				BitmapImage bitmapimage = new BitmapImage();
				bitmapimage.BeginInit();
				bitmapimage.StreamSource = memory;
				bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapimage.EndInit();

				return bitmapimage;
			}
		}

		private static int Clamp(int first, int second)
		{
			if (first.CompareTo(second) >= 0) return second - 1;
			if (first.CompareTo(second) < 0) return first;
			return 0;
		}
	}
}