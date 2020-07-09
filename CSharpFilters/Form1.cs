using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;

namespace CSharpFilters
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private const int BRIGHT = 225;
		private System.Drawing.Bitmap m_Bitmap;
		private System.Drawing.Bitmap m_Undo;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem FileLoad;
		private System.Windows.Forms.MenuItem FileSave;
		private System.Windows.Forms.MenuItem FileExit;
		private System.Windows.Forms.MenuItem FilterInvert;
		private System.Windows.Forms.MenuItem FilterGrayScale;
		private System.Windows.Forms.MenuItem FilterGrayToBlack;
		private System.Windows.Forms.MenuItem FilterBrightness;
		private System.Windows.Forms.MenuItem FilterContrast;
		private System.Windows.Forms.MenuItem FilterGamma;
		private System.Windows.Forms.MenuItem FilterColor;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem Zoom25;
		private double Zoom = 1.0;
		private System.Windows.Forms.MenuItem Zoom50;
		private System.Windows.Forms.MenuItem Zoom100;
		private System.Windows.Forms.MenuItem Zoom150;
		private System.Windows.Forms.MenuItem Zoom200;
		private System.Windows.Forms.MenuItem Zoom300;
		private System.Windows.Forms.MenuItem Zoom500;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem FilterSmooth;
		private System.Windows.Forms.MenuItem GaussianBlur;
		private System.Windows.Forms.MenuItem MeanRemoval;
		private System.Windows.Forms.MenuItem Sharpen;
		private System.Windows.Forms.MenuItem EmbossLaplacian;
		private System.Windows.Forms.MenuItem EdgeDetectQuick;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem Undo;
		private System.Windows.Forms.MenuItem FilterCustom;
		private System.Windows.Forms.MenuItem ImageFromText;
		private System.Windows.Forms.MenuItem ImageToTextR1;
		private System.Windows.Forms.MenuItem ImageToBackBone;
		private System.Windows.Forms.MenuItem AverageSquare;
		private System.Windows.Forms.MenuItem Rank;
		private System.Windows.Forms.MenuItem Dictionary;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			InitializeComponent();

			m_Bitmap = new Bitmap(2, 2);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.FileLoad = new System.Windows.Forms.MenuItem();
			this.FileSave = new System.Windows.Forms.MenuItem();
			this.FileExit = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.Undo = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.FilterInvert = new System.Windows.Forms.MenuItem();
			this.FilterGrayScale = new System.Windows.Forms.MenuItem();
			this.FilterGrayToBlack = new System.Windows.Forms.MenuItem();
			this.FilterBrightness = new System.Windows.Forms.MenuItem();
			this.FilterContrast = new System.Windows.Forms.MenuItem();
			this.FilterGamma = new System.Windows.Forms.MenuItem();
			this.FilterColor = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.FilterSmooth = new System.Windows.Forms.MenuItem();
			this.GaussianBlur = new System.Windows.Forms.MenuItem();
			this.MeanRemoval = new System.Windows.Forms.MenuItem();
			this.Sharpen = new System.Windows.Forms.MenuItem();
			this.EmbossLaplacian = new System.Windows.Forms.MenuItem();
			this.EdgeDetectQuick = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.Zoom25 = new System.Windows.Forms.MenuItem();
			this.Zoom50 = new System.Windows.Forms.MenuItem();
			this.Zoom100 = new System.Windows.Forms.MenuItem();
			this.Zoom150 = new System.Windows.Forms.MenuItem();
			this.Zoom200 = new System.Windows.Forms.MenuItem();
			this.Zoom300 = new System.Windows.Forms.MenuItem();
			this.Zoom500 = new System.Windows.Forms.MenuItem();
			this.FilterCustom = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.ImageFromText = new System.Windows.Forms.MenuItem();
			this.ImageToTextR1 = new System.Windows.Forms.MenuItem();
			this.ImageToBackBone = new System.Windows.Forms.MenuItem();
			this.AverageSquare = new System.Windows.Forms.MenuItem();
			this.Rank = new System.Windows.Forms.MenuItem();
			this.Dictionary = new System.Windows.Forms.MenuItem();

			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem5,
																					  this.menuItem4,
																					  this.menuItem3,
																					  this.menuItem2,
																					  this.menuItem6});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.FileLoad,
																					  this.FileSave,
																					  this.FileExit});
			this.menuItem1.Text = "File";
			// 
			// FileLoad
			// 
			this.FileLoad.Index = 0;
			this.FileLoad.Shortcut = System.Windows.Forms.Shortcut.CtrlL;
			this.FileLoad.Text = "Load";
			this.FileLoad.Click += new System.EventHandler(this.File_Load);
			// 
			// FileSave
			// 
			this.FileSave.Index = 1;
			this.FileSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.FileSave.Text = "Save";
			this.FileSave.Click += new System.EventHandler(this.File_Save);
			// 
			// FileExit
			// 
			this.FileExit.Index = 2;
			this.FileExit.Text = "Exit";
			this.FileExit.Click += new System.EventHandler(this.File_Exit);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 1;
			this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.Undo});
			this.menuItem5.Text = "Edit";
			// 
			// Undo
			// 
			this.Undo.Index = 0;
			this.Undo.Text = "Undo";
			this.Undo.Click += new System.EventHandler(this.OnUndo);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.FilterInvert,
																					  this.FilterGrayScale,
																					  this.FilterGrayToBlack,
																					  this.FilterBrightness,
																					  this.FilterContrast,
																					  this.FilterGamma,
																					  this.FilterColor});
			this.menuItem4.Text = "Filter";
			// 
			// FilterInvert
			// 
			this.FilterInvert.Index = 0;
			this.FilterInvert.Text = "Invert";
			this.FilterInvert.Click += new System.EventHandler(this.Filter_Invert);
			// 
			// FilterGrayScale
			// 
			this.FilterGrayScale.Index = 1;
			this.FilterGrayScale.Text = "GrayScale";
			this.FilterGrayScale.Click += new System.EventHandler(this.Filter_GrayScale);
			// 
			// FilterGrayToBlack
			// 
			this.FilterGrayToBlack.Index = 2;
			this.FilterGrayToBlack.Text = "GrayToBlack";
			this.FilterGrayToBlack.Click += new System.EventHandler(this.Filter_GrayToBlack);
			// 
			// FilterBrightness
			// 
			this.FilterBrightness.Index = 3;
			this.FilterBrightness.Text = "Brightness";
			this.FilterBrightness.Click += new System.EventHandler(this.Filter_Brightness);
			// 
			// FilterContrast
			// 
			this.FilterContrast.Index = 4;
			this.FilterContrast.Text = "Contrast";
			this.FilterContrast.Click += new System.EventHandler(this.Filter_Contrast);
			// 
			// FilterGamma
			// 
			this.FilterGamma.Index = 5;
			this.FilterGamma.Text = "Gamma";
			this.FilterGamma.Click += new System.EventHandler(this.Filter_Gamma);
			// 
			// FilterColor
			// 
			this.FilterColor.Index = 6;
			this.FilterColor.Text = "Color";
			this.FilterColor.Click += new System.EventHandler(this.Filter_Color);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 3;
			this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.FilterSmooth,
																					  this.GaussianBlur,
																					  this.MeanRemoval,
																					  this.Sharpen,
																					  this.EmbossLaplacian,
																					  this.EdgeDetectQuick,
																					  this.FilterCustom});
			this.menuItem3.Text = "Convolution";
			// 
			// FilterSmooth
			// 
			this.FilterSmooth.Index = 0;
			this.FilterSmooth.Text = "Smooth";
			this.FilterSmooth.Click += new System.EventHandler(this.OnFilterSmooth);
			// 
			// GaussianBlur
			// 
			this.GaussianBlur.Index = 1;
			this.GaussianBlur.Text = "Gaussian Blur";
			this.GaussianBlur.Click += new System.EventHandler(this.OnGaussianBlur);
			// 
			// MeanRemoval
			// 
			this.MeanRemoval.Index = 2;
			this.MeanRemoval.Text = "Mean Removal";
			this.MeanRemoval.Click += new System.EventHandler(this.OnMeanRemoval);
			// 
			// Sharpen
			// 
			this.Sharpen.Index = 3;
			this.Sharpen.Text = "Sharpen";
			this.Sharpen.Click += new System.EventHandler(this.OnSharpen);
			// 
			// EmbossLaplacian
			// 
			this.EmbossLaplacian.Index = 4;
			this.EmbossLaplacian.Text = "EmbossLaplacian";
			this.EmbossLaplacian.Click += new System.EventHandler(this.OnEmbossLaplacian);
			// 
			// EdgeDetectQuick
			// 
			this.EdgeDetectQuick.Index = 5;
			this.EdgeDetectQuick.Text = "EdgeDetectQuick";
			this.EdgeDetectQuick.Click += new System.EventHandler(this.OnEdgeDetectQuick);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 4;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.Zoom25,
																					  this.Zoom50,
																					  this.Zoom100,
																					  this.Zoom150,
																					  this.Zoom200,
																					  this.Zoom300,
																					  this.Zoom500});
			this.menuItem2.Text = "Zoom";
			// 
			// Zoom25
			// 
			this.Zoom25.Index = 0;
			this.Zoom25.Text = "25%";
			this.Zoom25.Click += new System.EventHandler(this.OnZoom25);
			// 
			// Zoom50
			// 
			this.Zoom50.Index = 1;
			this.Zoom50.Text = "50%";
			this.Zoom50.Click += new System.EventHandler(this.OnZoom50);
			// 
			// Zoom100
			// 
			this.Zoom100.Index = 2;
			this.Zoom100.Text = "100%";
			this.Zoom100.Click += new System.EventHandler(this.OnZoom100);
			// 
			// Zoom150
			// 
			this.Zoom150.Index = 3;
			this.Zoom150.Text = "150%";
			this.Zoom150.Click += new System.EventHandler(this.OnZoom150);
			// 
			// Zoom200
			// 
			this.Zoom200.Index = 4;
			this.Zoom200.Text = "200%";
			this.Zoom200.Click += new System.EventHandler(this.OnZoom200);
			// 
			// Zoom300
			// 
			this.Zoom300.Index = 5;
			this.Zoom300.Text = "300%";
			this.Zoom300.Click += new System.EventHandler(this.OnZoom300);
			// 
			// Zoom500
			// 
			this.Zoom500.Index = 6;
			this.Zoom500.Text = "500%";
			this.Zoom500.Click += new System.EventHandler(this.OnZoom500);
			// 
			// FilterCustom
			// 
			this.FilterCustom.Index = 6;
			this.FilterCustom.Text = "Custom";
			this.FilterCustom.Click += new System.EventHandler(this.OnFilterCustom);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 5;
			this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.ImageFromText,
																					  this.ImageToTextR1,
																					  this.ImageToBackBone,
																					  this.AverageSquare,
                                                                                      this.Rank,
                                                                                      this.Dictionary});
			this.menuItem6.Text = "Image";
			// 
			// ImageFromText
			// 
			this.ImageFromText.Index = 0;
			this.ImageFromText.Text = "ImageFromText";
			this.ImageFromText.Click += new System.EventHandler(this.OnImageFromText);
			// 
			// ImageToTextR1
			// 
			this.ImageToTextR1.Index = 1;
			this.ImageToTextR1.Text = "ImageToTextR1";
			this.ImageToTextR1.Click += new System.EventHandler(this.OnImageToTextR1);
			// 
			// ImageToBackBone
			// 
			this.ImageToBackBone.Index = 2;
			this.ImageToBackBone.Text = "ImageToBackBone";
			this.ImageToBackBone.Click += new System.EventHandler(this.OnImageToBackBone);
			// 
			// AverageSquare
			// 
			this.AverageSquare.Index = 3;
			this.AverageSquare.Text = "AverageSquare";
			this.AverageSquare.Click += new System.EventHandler(this.OnAverageSquare);
			// 
			// Rank
			// 
			this.Rank.Index = 4;
			this.Rank.Text = "Rank";
			this.Rank.Click += new System.EventHandler(this.OnRank);
			// 
			// Dictionary
			// 
			this.Dictionary.Index = 5;
			this.Dictionary.Text = "Dictionary";
			this.Dictionary.Click += new System.EventHandler(this.OnDictionary);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.ClientSize = new System.Drawing.Size(488, 421);
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "Graphic Filters For Dummies";
			this.Load += new System.EventHandler(this.Form1_Load);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.Run(new Form1());
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g = e.Graphics;

			g.DrawImage(m_Bitmap, new Rectangle(this.AutoScrollPosition.X, this.AutoScrollPosition.Y, (int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom)));
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
		}

		private string _sInitialDirectory = "c:\\";
		private void File_Load(object sender, System.EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();

			openFileDialog.InitialDirectory = _sInitialDirectory;
			openFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|All valid files (*.bmp/*.jpg)|*.bmp/*.jpg";
			openFileDialog.FilterIndex = 2;
			openFileDialog.RestoreDirectory = true;

			if (DialogResult.OK == openFileDialog.ShowDialog())
			{
				_sInitialDirectory = System.IO.Path.GetDirectoryName(openFileDialog.FileName);
				m_Bitmap = (Bitmap)Bitmap.FromFile(openFileDialog.FileName, true);
				this.AutoScroll = true;
				this.AutoScrollMinSize = new Size((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
				this.Invalidate();
			}
		}

		private void File_Save(object sender, System.EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();

			saveFileDialog.InitialDirectory = "c:\\";
			saveFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|All valid files (*.bmp/*.jpg)|*.bmp/*.jpg";
			saveFileDialog.FilterIndex = 1;
			saveFileDialog.RestoreDirectory = true;

			if (DialogResult.OK == saveFileDialog.ShowDialog())
			{
				m_Bitmap.Save(saveFileDialog.FileName);
			}
		}

		private void File_Exit(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void Filter_Invert(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if (BitmapFilter.Invert(m_Bitmap))
				this.Invalidate();
		}

		private void Filter_GrayScale(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if (BitmapFilter.GrayScale(m_Bitmap))
				this.Invalidate();
		}

		private void Filter_GrayToBlack(object sender, System.EventArgs e)
		{
			Parameter dlg = new Parameter();
			dlg.nValue = 0;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				if (BitmapFilter.GrayToBlack(m_Bitmap, dlg.nValue))
					this.Invalidate();
			}
		}
		private void Filter_Brightness(object sender, System.EventArgs e)
		{
			Parameter dlg = new Parameter();
			dlg.nValue = 0;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				if (BitmapFilter.Brightness(m_Bitmap, dlg.nValue))
					this.Invalidate();
			}
		}

		private void Filter_Contrast(object sender, System.EventArgs e)
		{
			Parameter dlg = new Parameter();
			dlg.nValue = 0;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				if (BitmapFilter.Contrast(m_Bitmap, (sbyte)dlg.nValue))
					this.Invalidate();
			}

		}

		private void Filter_Gamma(object sender, System.EventArgs e)
		{
			GammaInput dlg = new GammaInput();
			dlg.red = dlg.green = dlg.blue = 1;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				if (BitmapFilter.Gamma(m_Bitmap, dlg.red, dlg.green, dlg.blue))
					this.Invalidate();
			}
		}

		private void Filter_Color(object sender, System.EventArgs e)
		{
			ColorInput dlg = new ColorInput();
			dlg.red = dlg.green = dlg.blue = 0;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				if (BitmapFilter.Color(m_Bitmap, dlg.red, dlg.green, dlg.blue))
					this.Invalidate();
			}

		}

		private void OnZoom25(object sender, System.EventArgs e)
		{
			Zoom = .25;
			this.AutoScrollMinSize = new Size((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnZoom50(object sender, System.EventArgs e)
		{
			Zoom = .5;
			this.AutoScrollMinSize = new Size((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnZoom100(object sender, System.EventArgs e)
		{
			Zoom = 1.0;
			this.AutoScrollMinSize = new Size((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnZoom150(object sender, System.EventArgs e)
		{
			Zoom = 1.5;
			this.AutoScrollMinSize = new Size((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnZoom200(object sender, System.EventArgs e)
		{
			Zoom = 2.0;
			this.AutoScrollMinSize = new Size((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnZoom300(object sender, System.EventArgs e)
		{
			Zoom = 3.0;
			this.AutoScrollMinSize = new Size((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnZoom500(object sender, System.EventArgs e)
		{
			Zoom = 5;
			this.AutoScrollMinSize = new Size((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnFilterSmooth(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if (BitmapFilter.Smooth(m_Bitmap, 1))
				this.Invalidate();
		}

		private void OnGaussianBlur(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if (BitmapFilter.GaussianBlur(m_Bitmap, 4))
				this.Invalidate();
		}

		private void OnMeanRemoval(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if (BitmapFilter.MeanRemoval(m_Bitmap, 9))
				this.Invalidate();
		}

		private void OnSharpen(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if (BitmapFilter.Sharpen(m_Bitmap, 11))
				this.Invalidate();
		}

		private void OnEmbossLaplacian(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if (BitmapFilter.EmbossLaplacian(m_Bitmap))
				this.Invalidate();
		}

		private void OnEdgeDetectQuick(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if (BitmapFilter.EdgeDetectQuick(m_Bitmap))
				this.Invalidate();
		}

		private void OnUndo(object sender, System.EventArgs e)
		{
			Bitmap temp = (Bitmap)m_Bitmap.Clone();
			m_Bitmap = (Bitmap)m_Undo.Clone();
			m_Undo = (Bitmap)temp.Clone();
			this.Invalidate();
		}

		private void OnFilterCustom(object sender, System.EventArgs e)
		{
			Convolution dlg = new Convolution();

			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				if (BitmapFilter.Conv3x3(m_Bitmap, dlg.Matrix))
					this.Invalidate();
			}

		}

		private void OnImageFromText(object sender, System.EventArgs e)
		{
			KanjiInput dlg = new KanjiInput();
			dlg.sValue = "—ˆ";
			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Bitmap = (Bitmap)FontMethods.Render(dlg.sValue);
				BitmapFilter.GrayToBlack(m_Bitmap, BRIGHT);
				this.AutoScroll = true;
				this.AutoScrollMinSize = new Size((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
				this.Invalidate();
			}
		}

		private void OnImageToTextR1(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			BitmapFilter.GrayToBlack(m_Bitmap, BRIGHT);
			Bitmap[] arr = FontMethods.ImageToTextR1(m_Bitmap);
			Bitmap[][] br = new Bitmap[arr.Length][];
			Bitmap[] cr = new Bitmap[arr.Length];
			for (int i = 0; i < arr.Length; i++)
			{
				br[i] = FontMethods.ImageToTextR2(arr[i]);
				cr[i] = FontMethods.JoinBitmap(br[i]);
			}
			m_Bitmap = FontMethods.JoinBitmapH(cr);
			m_Bitmap = br[0][7];
			this.AutoScroll = true;
			this.AutoScrollMinSize = new Size((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnImageToBackBone(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			FontMethods.ImageToBackBone(m_Bitmap);
			m_Bitmap = FontMethods.BoundCore(m_Bitmap);
			this.AutoScroll = true;
			this.AutoScrollMinSize = new Size((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnAverageSquare(object sender, System.EventArgs e)
		{
			m_Undo = FontMethods.BoundCore(m_Undo);
			int[] fra = FontMethods.Fragment(m_Undo);
			//already run OnImageToBackBone
			double[] d = FontMethods.AverageSquare(m_Bitmap);
			double[][] ad = FontMethods.Margin(m_Bitmap);
			string mes = d[0].ToString() + ":" + d[1].ToString() + ":" + d[2].ToString()
				+ ":[" + ad[0][0].ToString("0.00000") + ":" + ad[0][1].ToString("0.00000") + "]"
				+ ":[" + ad[1][0].ToString("0.00000") + ":" + ad[1][1].ToString("0.00000") + "]"
				+ ":[" + ad[2][0].ToString("0.00000") + ":" + ad[2][1].ToString("0.00000") + "]"
				+ ":[" + ad[3][0].ToString("0.00000") + ":" + ad[3][1].ToString("0.00000") + "]"
				+ ":[" + ad[4][0].ToString("0.00000") + ":" + ad[4][1].ToString("0.00000") + "]"
				+ ":[" + ad[5][0].ToString("0.00000") + ":" + ad[5][1].ToString("0.00000") + "]"
				+ ":[" + ad[6][0].ToString("0.00000") + ":" + ad[6][1].ToString("0.00000") + "]"
				+ ":[" + ad[7][0].ToString("0.00000") + ":" + ad[7][1].ToString("0.00000") + "]"
				+ ":" + String.Join(",", fra);
			Clipboard.SetText(mes);
			MessageBox.Show(mes);
		}
		private void OnRank(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			BitmapFilter.GrayToBlack(m_Bitmap, BRIGHT);
			Bitmap[] arr = FontMethods.ImageToTextR1(m_Bitmap);
			Bitmap[][] br = new Bitmap[arr.Length][];
			Bitmap[] cr = new Bitmap[arr.Length];
			for (int i = 0; i < arr.Length; i++)
			{
				br[i] = FontMethods.ImageToTextR2(arr[i]);
				cr[i] = FontMethods.JoinBitmap(br[i]);
			}
			m_Bitmap = FontMethods.JoinBitmapH(cr);
			this.AutoScroll = true;
			this.AutoScrollMinSize = new Size((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();

			Bitmap[][] brcop = new Bitmap[arr.Length][];
			List<ABC> listABC = new List<ABC>();
			for (int i = 0; i < arr.Length; i++)
			{
				Bitmap[] bbcop = new Bitmap[br[i].Length];
				for (int j = 0; j < br[i].Length; j++)
				{
					bbcop[j] = (Bitmap)br[i][j].Clone();
					bbcop[j] = FontMethods.BoundCore(bbcop[j]);
					int[] fra = FontMethods.Fragment(bbcop[j]);

					FontMethods.ImageToBackBone(br[i][j]);
					br[i][j] = FontMethods.BoundCore(br[i][j]);
					double[] d = FontMethods.AverageSquare(br[i][j]);
					double[][] ad = FontMethods.Margin(br[i][j]);
					ABC strABC = new ABC();
					strABC.AverageSquare = d;
					strABC.Margin = ad;
					strABC.Fragment = fra;
					listABC.Add(strABC);
				}
			}
			string mes = String.Join("\n", listABC);
			Clipboard.SetText(mes);
			MessageBox.Show(mes);
		}
		private void OnDictionary(object sender, System.EventArgs e)
		{
			string[] arrdic = new String[] { "田", "字", "奉", "膂", "雪", "こ", "や", "至", "一", "雪", "雪", "だ", "や", "ほ", "’", "ん", "と", "や" };
			Bitmap[] br = new Bitmap[arrdic.Length];
			Bitmap[] brcop = new Bitmap[arrdic.Length];
			List<ABC> listABC = new List<ABC>();

			for (int i = 0; i < arrdic.Length; i++)
			{
				br[i] = (Bitmap)FontMethods.Render(arrdic[i]);
				BitmapFilter.GrayToBlack(br[i], BRIGHT);
				brcop[i] = (Bitmap)br[i].Clone();
				brcop[i] = FontMethods.BoundCore(brcop[i]);
				int[] fra = FontMethods.Fragment(brcop[i]);

				FontMethods.ImageToBackBone(br[i]);
				br[i] = FontMethods.BoundCore(br[i]);
				double[] d = FontMethods.AverageSquare(br[i]);
				double[][] ad = FontMethods.Margin(br[i]);
				ABC strABC = new ABC();
				strABC.AverageSquare = d;
				strABC.Margin = ad;
				strABC.Fragment = fra;
				listABC.Add(strABC);
			}

			m_Bitmap = FontMethods.JoinBitmap(brcop);
			this.AutoScroll = true;
			this.AutoScrollMinSize = new Size((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();

			string mes = String.Join("\n", listABC);
			Clipboard.SetText(mes);
			MessageBox.Show(mes);
		}
		struct ABC
		{
			internal double[] AverageSquare;
			internal double[][] Margin;
			internal int[] Fragment;
			public override string ToString()
			{
				return AverageSquare[0].ToString() + ":" + AverageSquare[1].ToString() + ":" + AverageSquare[2].ToString()
						+ ":[" + Margin[0][0].ToString("0.00000") + ":" + Margin[0][1].ToString("0.00000") + "]"
						+ ":[" + Margin[1][0].ToString("0.00000") + ":" + Margin[1][1].ToString("0.00000") + "]"
						+ ":[" + Margin[2][0].ToString("0.00000") + ":" + Margin[2][1].ToString("0.00000") + "]"
						+ ":[" + Margin[3][0].ToString("0.00000") + ":" + Margin[3][1].ToString("0.00000") + "]"
						+ ":[" + Margin[4][0].ToString("0.00000") + ":" + Margin[4][1].ToString("0.00000") + "]"
						+ ":[" + Margin[5][0].ToString("0.00000") + ":" + Margin[5][1].ToString("0.00000") + "]"
						+ ":[" + Margin[6][0].ToString("0.00000") + ":" + Margin[6][1].ToString("0.00000") + "]"
						+ ":[" + Margin[7][0].ToString("0.00000") + ":" + Margin[7][1].ToString("0.00000") + "]"
						+ ":" + String.Join(",", Fragment);
			}
		}
	}
}

