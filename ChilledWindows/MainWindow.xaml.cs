using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using ChilledWindows.Properties;

namespace ChilledWindows
{
	// Token: 0x02000003 RID: 3
	public partial class MainWindow : Window
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002080 File Offset: 0x00000280
		public MainWindow()
		{
			Type typeFromProgID = Type.GetTypeFromProgID("Shell.Application");
			object target = Activator.CreateInstance(typeFromProgID);
			typeFromProgID.InvokeMember("MinimizeAll", BindingFlags.InvokeMethod, null, target, null);
			Thread.Sleep(300);
			this.screenWidth = (int)SystemParameters.PrimaryScreenWidth;
			this.screenHeight = (int)SystemParameters.PrimaryScreenHeight;
			Bitmap bitmap = new Bitmap(this.screenWidth, this.screenHeight);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
			}
			this.InitializeComponent();
			base.WindowState = WindowState.Normal;
			base.WindowStyle = WindowStyle.None;
			base.Topmost = true;
			base.WindowState = WindowState.Maximized;
			ImageSource source = this.BitmapToImageSource(bitmap);
			this.firstBg.Source = source;
			this.bg2.Source = source;
			this.bg3.Source = source;
			this.firstBg.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
			this.fTransformGroup.Children.Add(this.fFlipTrans);
			this.fTransformGroup.Children.Add(this.fRotateTrans);
			this.firstBg.RenderTransform = this.fTransformGroup;
			this.bg2.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
			this.bg2.RenderTransform = this.FlipTrans1;
			this.bg3.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
			this.bg3.RenderTransform = this.FlipTrans2;
			this.twoGrid.RenderTransformOrigin = new System.Windows.Point(0.0, 0.0);
			this.gTransGroup.Children.Add(this.gTransTransform);
			this.gTransGroup.Children.Add(this.gScaleTransform);
			this.twoGrid.RenderTransform = this.gTransGroup;
			File.WriteAllBytes("chilledwindows.mp4", ChilledWindows.Properties.Resources.Chilled_Windows);
			this.mediaElement.Source = new Uri("chilledwindows.mp4", UriKind.Relative);
			this.dt.Tick += this.Dt_Tick;
			this.dt.Interval = new TimeSpan(0, 0, 0, 0, 10);
			this.dt.Start();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000023A8 File Offset: 0x000005A8
		private void Dt_Tick(object sender, EventArgs e)
		{
			this.frameIndex = (int)Math.Floor(this.mediaElement.Position.TotalMilliseconds / 33.33333);
			this.label.Content = "Frame:" + this.frameIndex;
			if (this.frameIndex == 438)
			{
				this.refreshFirstFlips = false;
				this.firstBg.Visibility = Visibility.Hidden;
				this.twoGrid.Visibility = Visibility.Visible;
			}
			if (this.frameIndex == 585)
			{
				this.refreshSecondFlips = false;
			}
			if (this.frameIndex == 622)
			{
				this.bg.Visibility = Visibility.Hidden;
				double num = (double)this.screenWidth * 0.13817330210772832;
				double num2 = (double)this.screenHeight * 0.3541666666666667;
				DoubleAnimation animation = new DoubleAnimation(0.0, num * 3.0, TimeSpan.FromMilliseconds(500.0));
				DoubleAnimation animation2 = new DoubleAnimation(0.0, num2 * 3.0, TimeSpan.FromMilliseconds(500.0));
				DoubleAnimation animation3 = new DoubleAnimation(1.0, 0.3, TimeSpan.FromMilliseconds(500.0));
				DoubleAnimation animation4 = new DoubleAnimation(1.0, 0.3, TimeSpan.FromMilliseconds(500.0));
				this.gTransTransform.BeginAnimation(TranslateTransform.XProperty, animation);
				this.gTransTransform.BeginAnimation(TranslateTransform.YProperty, animation2);
				this.gScaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation3);
				this.gScaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation4);
			}
			if (this.frameIndex == 665)
			{
				this.twoGrid.Visibility = Visibility.Hidden;
			}
			if (this.frameIndex == 1260)
			{
				File.Delete("chilledwindows.mp4");
				Application.Current.Shutdown();
			}
			if (this.refreshFirstFlips)
			{
				if (this.flipTimes[this.flipIndex] <= this.frameIndex)
				{
					this.flipIndex++;
					this.fFlipTrans.ScaleX = (double)((this.fFlipTrans.ScaleX == -1.0) ? 1 : -1);
				}
				if (this.frameIndex == 286)
				{
					this.fRotateTrans.Angle = -20.0;
					return;
				}
			}
			else if (this.refreshSecondFlips)
			{
				if (this.flipTimes1[this.flipIndex1] <= this.frameIndex)
				{
					this.flipIndex1++;
					this.FlipTrans1.ScaleX = (double)((this.FlipTrans1.ScaleX == -1.0) ? 1 : -1);
				}
				if (this.flipTimes2[this.flipIndex2] <= this.frameIndex)
				{
					this.flipIndex2++;
					this.FlipTrans2.ScaleX = (double)((this.FlipTrans2.ScaleX == -1.0) ? 1 : -1);
				}
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000026C8 File Offset: 0x000008C8
		private BitmapImage BitmapToImageSource(Bitmap bitmap)
		{
			BitmapImage result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				bitmap.Save(memoryStream, ImageFormat.Bmp);
				memoryStream.Position = 0L;
				BitmapImage bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.StreamSource = memoryStream;
				bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapImage.EndInit();
				result = bitmapImage;
			}
			return result;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000272C File Offset: 0x0000092C
		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Space)
			{
				this.fFlipTrans.ScaleX = (double)((this.fFlipTrans.ScaleX == -1.0) ? 1 : -1);
			}
		}

		// Token: 0x04000001 RID: 1
		private TransformGroup fTransformGroup = new TransformGroup();

		// Token: 0x04000002 RID: 2
		private TransformGroup gTransGroup = new TransformGroup();

		// Token: 0x04000003 RID: 3
		private TranslateTransform gTransTransform = new TranslateTransform();

		// Token: 0x04000004 RID: 4
		private ScaleTransform gScaleTransform = new ScaleTransform();

		// Token: 0x04000005 RID: 5
		private ScaleTransform fFlipTrans = new ScaleTransform();

		// Token: 0x04000006 RID: 6
		private ScaleTransform FlipTrans1 = new ScaleTransform();

		// Token: 0x04000007 RID: 7
		private ScaleTransform FlipTrans2 = new ScaleTransform();

		// Token: 0x04000008 RID: 8
		private RotateTransform fRotateTrans = new RotateTransform();

		// Token: 0x04000009 RID: 9
		private int screenWidth;

		// Token: 0x0400000A RID: 10
		private int screenHeight;

		// Token: 0x0400000B RID: 11
		private DispatcherTimer dt = new DispatcherTimer();

		// Token: 0x0400000C RID: 12
		private int[] flipTimes = new int[]
		{
			126,
			129,
			133,
			136,
			140,
			143,
			147,
			150,
			155,
			158,
			162,
			165,
			169,
			172,
			176,
			179,
			184,
			187,
			191,
			194,
			198,
			201,
			205,
			208,
			213,
			216,
			220,
			223,
			227,
			230,
			234,
			237,
			242,
			245,
			249,
			252,
			256,
			259,
			263,
			266,
			271,
			274,
			278,
			281,
			285,
			286,
			288,
			292,
			295,
			297,
			300,
			303,
			307,
			310,
			314,
			317,
			321,
			324,
			329,
			332,
			336,
			339,
			343,
			346,
			350,
			353,
			355,
			358,
			361,
			365,
			368,
			372,
			375,
			379,
			382,
			387,
			390,
			394,
			397,
			401,
			404,
			408,
			411,
			416,
			419,
			423,
			426,
			430,
			433,
			437,
			0,
			0,
			0,
			0,
			0,
			0
		};

		// Token: 0x0400000D RID: 13
		private int[] flipTimes2 = new int[]
		{
			440,
			443,
			449,
			454,
			456,
			460,
			468,
			476,
			481,
			486,
			497,
			501,
			504,
			506,
			512,
			514,
			518,
			522,
			527,
			531,
			533,
			536,
			540,
			548,
			552,
			557,
			561,
			563,
			569,
			572,
			576,
			580,
			582,
			585,
			0,
			0,
			0,
			0,
			0,
			0
		};

		// Token: 0x0400000E RID: 14
		private int[] flipTimes1 = new int[]
		{
			454,
			476,
			497,
			512,
			531,
			548,
			569,
			585,
			0,
			0,
			0,
			0,
			0,
			0
		};

		// Token: 0x0400000F RID: 15
		private int flipIndex;

		// Token: 0x04000010 RID: 16
		private int flipIndex1;

		// Token: 0x04000011 RID: 17
		private int flipIndex2;

		// Token: 0x04000012 RID: 18
		private int frameIndex;

		// Token: 0x04000013 RID: 19
		private bool refreshFirstFlips = true;

		// Token: 0x04000014 RID: 20
		private bool refreshSecondFlips = true;
	}
}
