using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace CSharpFilters
{
	internal static class FontMethods
	{
		public static Image Render1()
		{
			// create the final image to render into
			var image = new Bitmap(25, 25, PixelFormat.Format24bppRgb);
			using (SolidBrush brush = new SolidBrush(Color.Black))
			{
				using (Graphics graphics = Graphics.FromImage(image))
				{
					//graphics.FillRectangle(brush, 0, 0, image.Width, image.Height);
					graphics.DrawString("書", new Font("MS gothic", 24), brush, new Point(5, 5));
				}
			}
			return image;
		}
		public static Image Render(string text = "来")
		{
			// create the final image to render into
			var image = new Bitmap(50, 50, PixelFormat.Format24bppRgb);

			// create memory buffer from desktop handle that supports alpha channel
			IntPtr dib;
			var memoryHdc = CreateMemoryHdc(IntPtr.Zero, image.Width, image.Height, out dib);
			try
			{
				// create memory buffer graphics to use for HTML rendering
				using (var memoryGraphics = Graphics.FromHdc(memoryHdc))
				{
					memoryGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
					//memoryGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
					// must not be transparent background 
					memoryGraphics.Clear(Color.Black);

					// execute GDI text rendering
					TextRenderer.DrawText(memoryGraphics, text, new Font("Yu Gothic UI", 30, FontStyle.Bold), new Point(0, 0), Color.White, Color.Black);
					//memoryGraphics.DrawString(text, new Font("MS gothic", 24), new SolidBrush(Color.White), new PointF(0, 0));
					//memoryGraphics.DrawString(text, new Font("Yu Gothic UI", 30, FontStyle.Bold), new SolidBrush(Color.White), new PointF(0, 0));
				}

				// copy from memory buffer to image
				using (var imageGraphics = Graphics.FromImage(image))
				{
					var imgHdc = imageGraphics.GetHdc();
					BitBlt(imgHdc, 0, 0, image.Width, image.Height, memoryHdc, 0, 0, 0x00CC0020);
					imageGraphics.ReleaseHdc(imgHdc);
				}
			}
			finally
			{
				// release memory buffer
				DeleteObject(dib);
				DeleteDC(memoryHdc);
			}

			return image;
		}

		public static Bitmap[] ImageToTextR1(Bitmap b)
		{
			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			List<Rectangle> brow = new List<Rectangle>();
			unsafe
			{
				byte* p = (byte*)(void*)Scan0;

				int nOffset = stride - b.Width * 3;

				byte red, green, blue;
				int rx, ry, rw, rh;
				rx = ry = rw = rh = 0;
				bool fs = false;
				int[] fr = new int[b.Height];
				for (int y = 0; y < b.Height; ++y)
				{
					for (int x = 0; x < b.Width; ++x)
					{
						blue = p[0];
						green = p[1];
						red = p[2];
						if (blue == 0 && green == 0 && red == 0)
						{
							fr[y]++;
						}
						p += 3;
					}
					p += nOffset;
				}
				for (int y = 0; y < b.Height; ++y)
				{
					//first possition has byte -> start row
					if ((!fs) && fr[y] < b.Width)
					{
						rx = 0;
						ry = y;
						rw = b.Width;
						fs = true;
					}
					else
					{
						//fisrt possition no byte after has byte -> end row 
						if (fs && ((fr[y] == b.Width) || (y + 1 == b.Height)))
						{
							rh = y - ry + 1;
							Rectangle bm = new Rectangle(rx, ry, rw, rh);
							brow.Add(bm);
							if (brow.Count > 1)
							{
								Rectangle f = brow[brow.Count - 1];
								Rectangle l = brow[brow.Count - 2];
								//minimum distance between row 4px
								if (f.Y + 1 - l.Y - l.Height - 4 < 0)
								{
									Rectangle n = new Rectangle(l.X, l.Y, f.Width, l.Height + f.Y + 1 - l.Y);
									brow.RemoveRange(brow.Count - 2, 2);
									brow.Add(n);
								}
							}
							rx = ry = rw = rh = 0;
							fs = false;
						}
					}
				}
			}

			b.UnlockBits(bmData);

			Bitmap[] ar = FontMethods.CropBitmap(b, brow.ToArray());
			return ar;
		}

		public static Bitmap[] ImageToTextR2(Bitmap b)
		{
			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			List<Rectangle> brow = new List<Rectangle>();
			unsafe
			{
				byte* p = (byte*)(void*)Scan0;

				int nOffset = stride - b.Width * 3;

				byte red, green, blue;
				int rx, ry, rw, rh;
				rx = ry = rw = rh = 0;
				bool fs = false;
				int[] fr = new int[b.Width];
				for (int y = 0; y < b.Height; ++y)
				{
					for (int x = 0; x < b.Width; ++x)
					{
						blue = p[0];
						green = p[1];
						red = p[2];
						if (blue == 0 && green == 0 && red == 0)
						{
							fr[x]++;
						}
						p += 3;
					}
					p += nOffset;
				}
				for (int x = 0; x < b.Width; ++x)
				{
					//first possition has byte -> start column
					if ((!fs) && fr[x] < b.Height)
					{
						rx = x;
						ry = 0;
						rh = b.Height;
						fs = true;
					}
					else
					{
						//fisrt possition no byte after has byte -> end column 
						if (fs && ((fr[x] == b.Height) || (x + 1 == b.Width)))
						{
							rw = x - rx + 1;
							Rectangle bm = new Rectangle(rx, ry, rw, rh);
							brow.Add(bm);
							if (brow.Count > 1)
							{
								Rectangle f = brow[brow.Count - 1];
								Rectangle l = brow[brow.Count - 2];
								//minimum distance between word 4px
								if (f.X + 1 - l.X - l.Width - 4 < 0)
								{
									Rectangle n = new Rectangle(l.X, l.Y, f.Width + f.X + 1 - l.X, l.Height);
									brow.RemoveRange(brow.Count - 2, 2);
									brow.Add(n);
								}
							}
							rx = ry = rw = rh = 0;
							fs = false;
						}
					}
				}
			}

			b.UnlockBits(bmData);

			Bitmap[] ar = FontMethods.CropBitmap(b, brow.ToArray());
			return ar;
		}
		public static Bitmap JoinBitmap(Bitmap[] ab)
		{
			if (ab == null || ab.Length == 0) return null;
			int w = ab.Max(m => m.Width);
			int h = ab.Sum(m => m.Height);
			Bitmap target = new Bitmap(w, h, PixelFormat.Format24bppRgb);

			using (Graphics g = Graphics.FromImage(target))
			{
				g.Clear(Color.Black);
				int y = 0;
				for (int i = 0; i < ab.Length; i++)
				{
					Rectangle cropRect = new Rectangle(0, y, ab[i].Width, ab[i].Height);
					g.DrawImage(ab[i], cropRect,
								 new Rectangle(0, 0, ab[i].Width, ab[i].Height),
								 GraphicsUnit.Pixel);
					y += ab[i].Height;
				}
			}
			return target;
		}
		public static Bitmap JoinBitmapH(Bitmap[] ab)
		{
			if (ab == null || ab.Length == 0) return null;
			int w = ab.Sum(m => m.Width);
			int h = ab.Max(m => m.Height);
			Bitmap target = new Bitmap(w, h, PixelFormat.Format24bppRgb);

			using (Graphics g = Graphics.FromImage(target))
			{
				g.Clear(Color.Black);
				int x = 0;
				for (int i = 0; i < ab.Length; i++)
				{
					Rectangle cropRect = new Rectangle(x, 0, ab[i].Width, ab[i].Height);
					g.DrawImage(ab[i], cropRect,
								 new Rectangle(0, 0, ab[i].Width, ab[i].Height),
								 GraphicsUnit.Pixel);
					x += ab[i].Width;
				}
			}
			return target;
		}
		public static bool ImageToBackBone(Bitmap b)
		{
			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			int[][] mr = new int[b.Width][];
			int[][] fr = new int[b.Height][];
			unsafe
			{
				byte* p = (byte*)(void*)Scan0;

				int nOffset = stride - b.Width * 3;

				byte red, green, blue;
				for (int x = 0; x < b.Width; ++x)
				{
					mr[x] = new int[b.Height];
				}
				for (int y = 0; y < b.Height; ++y)
				{
					fr[y] = new int[b.Width];
					for (int x = 0; x < b.Width; ++x)
					{
						blue = p[0];
						green = p[1];
						red = p[2];
						if (blue == 255 && green == 255 && red == 255)
						{
							mr[x][y] = 1;
							fr[y][x] = 1;
						}
						p += 3;
					}
					p += nOffset;
				}
				FontMethods.Converge(mr, 5);
				FontMethods.Converge(fr, 5);
			}

			unsafe
			{
				byte* p = (byte*)(void*)Scan0;

				int nOffset = stride - b.Width * 3;

				byte red, green, blue;
				for (int y = 0; y < b.Height; ++y)
				{
					for (int x = 0; x < b.Width; ++x)
					{
						blue = p[0];
						green = p[1];
						red = p[2];
						if (blue == 255 && green == 255 && red == 255)
						{
							if (mr[x][y] == 0)
								p[0] = p[1] = p[2] = 0;
							if (fr[y][x] == 0)
								p[0] = p[1] = p[2] = 0;
						}
						p += 3;
					}
					p += nOffset;
				}
			}

			b.UnlockBits(bmData);

			_ImageToBackBone(b, mr, fr);

			return true;
		}

		private static bool _ImageToBackBone(Bitmap b, int[][] mr, int[][] fr)
		{
			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			unsafe
			{
				byte* p = (byte*)(void*)Scan0;

				int nOffset = stride - b.Width * 3;

				byte red, green, blue;
				for (int y = 0; y < b.Height; ++y)
				{
					for (int x = 0; x < b.Width; ++x)
					{
						blue = p[0];
						green = p[1];
						red = p[2];
						if (blue == 255 && green == 255 && red == 255)
						{
							if (mr[x][y] == 0)
								p[0] = p[1] = p[2] = 0;
							if (fr[y][x] == 0)
								p[0] = p[1] = p[2] = 0;
						}
						p += 3;
					}
					p += nOffset;
				}
			}

			b.UnlockBits(bmData);

			return true;
		}
		public static Bitmap BoundCore(Bitmap b)
		{
			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			int rw, rn, re, rs;
			rw = rn = re = rs = -1;
			unsafe
			{
				byte* p = (byte*)(void*)Scan0;

				int nOffset = stride - b.Width * 3;

				byte red, green, blue;
				for (int y = 0; y < b.Height; ++y)
				{
					for (int x = 0; x < b.Width; ++x)
					{
						blue = p[0];
						green = p[1];
						red = p[2];
						if (blue == 255 && green == 255 && red == 255)
						{
							if (rw == -1)
								rw = x;
							rw = Math.Min(rw, x);
							if (re == -1)
								re = x;
							re = Math.Max(re, x);
							if (rn == -1)
								rn = y;
							rn = Math.Min(rn, y);
							if (rs == -1)
								rs = y;
							rs = Math.Max(rs, y);
						}
						p += 3;
					}
					p += nOffset;
				}
			}

			b.UnlockBits(bmData);
			int d = Math.Max(re + 1 - rw, rs + 1 - rn);
			return FontMethods.CropBitmap(b, new Rectangle(rw, rn, d, d));
		}

		//not applicable, less accurate
		public static double[][] Margin(Bitmap b, bool remain = false)
		{
			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			double[] rw, rn, re, rs;
			double[] rwn, rne, res, rsw;
			rw = new double[] { -1, -1 };
			rn = new double[] { -1, -1 };
			re = new double[] { -1, -1 };
			rs = new double[] { -1, -1 };
			rwn = new double[] { -1, -1 };
			rne = new double[] { -1, -1 };
			res = new double[] { -1, -1 };
			rsw = new double[] { -1, -1 };
			unsafe
			{
				byte* p = (byte*)(void*)Scan0;

				int nOffset = stride - b.Width * 3;

				byte red, green, blue;
				for (int y = 0; y < b.Height; ++y)
				{
					for (int x = 0; x < b.Width; ++x)
					{
						blue = p[0];
						green = p[1];
						red = p[2];
						if (blue == 255 && green == 255 && red == 255)
						{
							if (rw[0] == -1)
							{
								rw[0] = x;
								rw[1] = y;
							}
							if (rw[0] > x)
							{
								rw[0] = x;
								rw[1] = y;
							}

							if (re[0] == -1)
							{
								re[0] = x;
								re[1] = y;
							}
							if (re[0] < x)
							{
								re[0] = x;
								re[1] = y;
							}

							if (rn[1] == -1)
							{
								rn[1] = y;
								rn[0] = x;
							}
							if (rn[1] > y)
							{
								rn[1] = y;
								rn[0] = x;
							}

							if (rs[1] == -1)
							{
								rs[1] = y;
								rs[0] = x;
							}
							if (rs[1] < y)
							{
								rs[1] = y;
								rs[0] = x;
							}
							if (rwn[0] == -1)
							{
								rwn[0] = x;
								rwn[1] = y;
							}

							if (rwn[0] * rwn[0] + rwn[1] * rwn[1] > x * x + y * y)
							{
								rwn[0] = x;
								rwn[1] = y;
							}

							if (rne[0] == -1)
							{
								rne[0] = x;
								rne[1] = y;
							}
							if ((b.Width - 1 - rne[0]) * (b.Width - 1 - rne[0]) + rne[1] * rne[1] > (b.Width - 1 - x) * (b.Width - 1 - x) + y * y)
							{
								rne[0] = x;
								rne[1] = y;
							}

							if (res[0] == -1)
							{
								res[0] = x;
								res[1] = y;
							}
							if ((b.Width - 1 - res[0]) * (b.Width - 1 - res[0]) + (b.Height - 1 - res[1]) * (b.Height - 1 - res[1]) > (b.Width - 1 - x) * (b.Width - 1 - x) + (b.Height - 1 - y) * (b.Height - 1 - y))
							{
								res[0] = x;
								res[1] = y;
							}

							if (rsw[0] == -1)
							{
								rsw[0] = x;
								rsw[1] = y;
							}
							if (rsw[0] * rsw[0] + (b.Height - 1 - rsw[1]) * (b.Height - 1 - rsw[1]) > x * x + (b.Height - 1 - y) * (b.Height - 1 - y))
							{
								rsw[0] = x;
								rsw[1] = y;
							}
						}
						p += 3;
					}
					p += nOffset;
				}
			}

			b.UnlockBits(bmData);
			if (!remain)
			{
				rw[0] = rw[0] / b.Width;
				rn[0] = rn[0] / b.Width;
				re[0] = re[0] / b.Width;
				rs[0] = rs[0] / b.Width;
				rwn[0] = rwn[0] / b.Width;
				rne[0] = rne[0] / b.Width;
				res[0] = res[0] / b.Width;
				rsw[0] = rsw[0] / b.Width;
				rw[1] = rw[1] / b.Height;
				rn[1] = rn[1] / b.Height;
				re[1] = re[1] / b.Height;
				rs[1] = rs[1] / b.Height;
				rwn[1] = rwn[1] / b.Height;
				rne[1] = rne[1] / b.Height;
				res[1] = res[1] / b.Height;
				rsw[1] = rsw[1] / b.Height;
			}
			return new double[][] { rw, rn, re, rs, rwn, rne, res, rsw };
		}

		//not applicable, not accurate
		public static double[] AverageSquare(Bitmap b)
		{
			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			double sq, ss, sx, sy, sd;
			sq = ss = sx = sy = sd = 0;
			sq = (double)(b.Width * (b.Width - 1)) / 2 * b.Height + b.Width * b.Height + (double)(b.Height * (b.Height - 1)) / 2 * b.Width;
			ss = b.Width * b.Height;
			unsafe
			{
				byte* p = (byte*)(void*)Scan0;

				int nOffset = stride - b.Width * 3;

				byte red, green, blue;
				for (int y = 0; y < b.Height; ++y)
				{
					for (int x = 0; x < b.Width; ++x)
					{
						blue = p[0];
						green = p[1];
						red = p[2];
						if (blue == 255 && green == 255 && red == 255)
						{
							if (x >= y)
								sx += (x + 1) * (y + 1);
							if (y >= x)
								sy += (x + 1) * (y + 1);

							sd++;
						}
						p += 3;
					}
					p += nOffset;
				}
			}

			b.UnlockBits(bmData);

			return new double[] { sx / sq, sy / sq, sd / ss };
		}

		public static int[] Fragment(Bitmap b)
		{
			double[] rw, rn, re, rs;
			double[][] ad = FontMethods.Margin(b, true);
			rw = ad[0];
			rn = ad[1];
			re = ad[2];
			rs = ad[3];
			// use bitmap before backbone
			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;
			List<int> r = new List<int>();
			List<int> r1 = new List<int>();
			int[][] mr = new int[b.Width][];
			int[][] fr = new int[b.Height][];
			unsafe
			{
				byte* p = (byte*)(void*)Scan0;

				int nOffset = stride - b.Width * 3;

				byte red, green, blue;
				for (int x = 0; x < b.Width; ++x)
				{
					mr[x] = new int[b.Height];
				}
				for (int y = 0; y < b.Height; ++y)
				{
					fr[y] = new int[b.Width];
					for (int x = 0; x < b.Width; ++x)
					{
						blue = p[0];
						green = p[1];
						red = p[2];
						if (blue == 255 && green == 255 && red == 255)
						{
							mr[x][y] = 1;
							fr[y][x] = 1;
						}
						p += 3;
					}
					p += nOffset;
				}

			}

			b.UnlockBits(bmData);

			for (int x = Math.Max(b.Width / 10, (int)rw[0] + 1); x < Math.Min(9 * b.Width / 10, (int)re[0] - 1); ++x)
			{
				if (mr[x].Sum() == 0)
				{
					if (b.Width / 10 <= x && x < 2 * b.Width / 10)
						r.Add(1);
					else if (2 * b.Width / 10 <= x && x < 3 * b.Width / 10)
						r.Add(2);
					else if (3 * b.Width / 10 <= x && x < 4 * b.Width / 10)
						r.Add(3);
					else if (4 * b.Width / 10 <= x && x < 5 * b.Width / 10)
						r.Add(4);
					else if (5 * b.Width / 10 <= x && x < 6 * b.Width / 10)
						r.Add(5);
					else if (6 * b.Width / 10 <= x && x < 7 * b.Width / 10)
						r.Add(6);
					else if (7 * b.Width / 10 <= x && x < 8 * b.Width / 10)
						r.Add(7);
					else if (8 * b.Width / 10 <= x && x < 9 * b.Width / 10)
						r.Add(8);
				}
				if (mr[x].Sum() == 1 || mr[x].Sum() == 2)
				{
					if (b.Width / 10 <= x && x < 2 * b.Width / 10)
						r1.Add(1);
					else if (2 * b.Width / 10 <= x && x < 3 * b.Width / 10)
						r1.Add(2);
					else if (3 * b.Width / 10 <= x && x < 4 * b.Width / 10)
						r1.Add(3);
					else if (4 * b.Width / 10 <= x && x < 5 * b.Width / 10)
						r1.Add(4);
					else if (5 * b.Width / 10 <= x && x < 6 * b.Width / 10)
						r1.Add(5);
					else if (6 * b.Width / 10 <= x && x < 7 * b.Width / 10)
						r1.Add(6);
					else if (7 * b.Width / 10 <= x && x < 8 * b.Width / 10)
						r1.Add(7);
					else if (8 * b.Width / 10 <= x && x < 9 * b.Width / 10)
						r1.Add(8);
				}
				if (mr[x].Sum() <= 2)
				{
					int lx = mr[x].Where((m, i) => i <= 5 * b.Height / 6).Sum();
					if (x < b.Width / 3 && lx == 0)
					{
						for (int y = 5 * b.Height / 6; y < b.Height; ++y)
						{
							int hy = fr[y].Where((m, i) => i > x).Sum();
							if (hy == 0)
								r.Add(100);
						}
					}
				}
			}

			for (int y = Math.Max(b.Height / 10, (int)rn[1] + 1); y < Math.Min(9 * b.Height / 10, (int)rs[1] - 1); ++y)
			{
				if (fr[y].Sum() == 0)
				{
					if (b.Height / 10 <= y && y < 2 * b.Height / 10)
						r.Add(101);
					else if (2 * b.Height / 10 <= y && y < 3 * b.Height / 10)
						r.Add(102);
					else if (3 * b.Height / 10 <= y && y < 4 * b.Height / 10)
						r.Add(103);
					else if (4 * b.Height / 10 <= y && y < 5 * b.Height / 10)
						r.Add(104);
					else if (5 * b.Height / 10 <= y && y < 6 * b.Height / 10)
						r.Add(105);
					else if (6 * b.Height / 10 <= y && y < 7 * b.Height / 10)
						r.Add(106);
					else if (7 * b.Height / 10 <= y && y < 8 * b.Height / 10)
						r.Add(107);
					else if (8 * b.Height / 10 <= y && y < 9 * b.Height / 10)
						r.Add(108);
				}
				if (fr[y].Sum() == 1 || fr[y].Sum() == 2)
				{
					if (b.Height / 10 <= y && y < 2 * b.Height / 10)
						r1.Add(101);
					else if (2 * b.Height / 10 <= y && y < 3 * b.Height / 10)
						r1.Add(102);
					else if (3 * b.Height / 10 <= y && y < 4 * b.Height / 10)
						r1.Add(103);
					else if (4 * b.Height / 10 <= y && y < 5 * b.Height / 10)
						r1.Add(104);
					else if (5 * b.Height / 10 <= y && y < 6 * b.Height / 10)
						r1.Add(105);
					else if (6 * b.Height / 10 <= y && y < 7 * b.Height / 10)
						r1.Add(106);
					else if (7 * b.Height / 10 <= y && y < 8 * b.Height / 10)
						r1.Add(107); 
					else if (8 * b.Height / 10 <= y && y < 9 * b.Height / 10)
						r1.Add(108);
				}
			}

			if (r.Where(m => m >= 1 && m <= 8).Count() == 0)
			{
				if (r1.Where(m => m >= 4 && m <= 5).Count() > 0)
					r.Add(r1.Where(m => m >= 4 && m <= 5).First());
				else if (r1.Where(m => m >= 1 && m <= 3).Count() > 0)
					r.Add(r1.Where(m => m >= 1 && m <= 3).First());
				else if (r1.Where(m => m >= 6 && m <= 8).Count() > 0)
					r.Add(r1.Where(m => m >= 6 && m <= 8).First());
			}
			if (r.Where(m => m >= 101 && m <= 108).Count() == 0)
			{
				if(r1.Where(m => m >= 104 && m <= 105).Count() > 0)
					r.Add(r1.Where(m => m >= 104 && m <= 105).First());
				else if(r1.Where(m => m >= 101 && m <= 103).Count() > 0)
					r.Add(r1.Where(m => m >= 101 && m <= 103).First());
				else if (r1.Where(m => m >= 106 && m <= 108).Count() > 0)
					r.Add(r1.Where(m => m >= 106 && m <= 108).First());
			}
			return r.Distinct().ToArray();
		}
		private static bool Converge(int[][] b, int range = 4)
		{
			for (int i = 0; i < b.Length; i++)
			{
				int[] c = b[i];
				int r = 0;
				for (int j = 0; j < c.Length; j++)
				{
					int x = c[j];
					if (x == 1)
					{
						r++;
					}
					else
					{
						if (r <= range && r > 1)
						{
							for (int k = 1; k <= r; k++)
							{
								c[j - k] = 0;
							}
							if (j - 1 - r / 2 >= 0) c[j - 1 - r / 2] = 1;
						}
						r = 0;
					}
				}
			}
			return true;
		}
		private static Bitmap CropBitmap(Bitmap b, Rectangle cropRect)
		{
			Bitmap target = new Bitmap(cropRect.Width, cropRect.Height, PixelFormat.Format24bppRgb);

			using (Graphics g = Graphics.FromImage(target))
			{
				// must not be transparent background 
				g.Clear(Color.Black);
				g.DrawImage(b, new Rectangle(0, 0, target.Width, target.Height),
								 cropRect,
								 GraphicsUnit.Pixel);
			}
			return target;
		}

		private static Bitmap[] CropBitmap(Bitmap b, Rectangle[] cropRect)
		{
			if (cropRect == null || cropRect.Length == 0)
			{
				return null;
			}
			Bitmap[] target = new Bitmap[cropRect.Length];
			for (int i = 0; i < cropRect.Length; i++)
			{
				target[i] = new Bitmap(cropRect[i].Width, cropRect[i].Height, PixelFormat.Format24bppRgb);
				using (Graphics g = Graphics.FromImage(target[i]))
				{
					// must not be transparent background 
					g.Clear(Color.Black);
					g.DrawImage(b, new Rectangle(0, 0, target[i].Width, target[i].Height),
									 cropRect[i],
									 GraphicsUnit.Pixel);
				}
			}
			return target;
		}
		private static IntPtr CreateMemoryHdc(IntPtr hdc, int width, int height, out IntPtr dib)
		{
			// Create a memory DC so we can work off-screen
			IntPtr memoryHdc = CreateCompatibleDC(hdc);
			SetBkMode(memoryHdc, 1);

			// Create a device-independent bitmap and select it into our DC
			var info = new BitMapInfo();
			info.biSize = Marshal.SizeOf(info);
			info.biWidth = width;
			info.biHeight = -height;
			info.biPlanes = 1;
			info.biBitCount = 32;
			info.biCompression = 0; // BI_RGB
			IntPtr ppvBits;
			dib = CreateDIBSection(hdc, ref info, 0, out ppvBits, IntPtr.Zero, 0);
			SelectObject(memoryHdc, dib);

			return memoryHdc;
		}

		[DllImport("gdi32.dll")]
		public static extern int SetBkMode(IntPtr hdc, int mode);

		[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

		[DllImport("gdi32.dll")]
		private static extern IntPtr CreateDIBSection(IntPtr hdc, [In] ref BitMapInfo pbmi, uint iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

		[DllImport("gdi32.dll")]
		public static extern int SelectObject(IntPtr hdc, IntPtr hgdiObj);

		[DllImport("gdi32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

		[DllImport("gdi32.dll")]
		public static extern bool DeleteObject(IntPtr hObject);

		[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern bool DeleteDC(IntPtr hdc);

		[StructLayout(LayoutKind.Sequential)]
		internal struct BitMapInfo
		{
			public int biSize;
			public int biWidth;
			public int biHeight;
			public short biPlanes;
			public short biBitCount;
			public int biCompression;
			public int biSizeImage;
			public int biXPelsPerMeter;
			public int biYPelsPerMeter;
			public int biClrUsed;
			public int biClrImportant;
			public byte bmiColors_rgbBlue;
			public byte bmiColors_rgbGreen;
			public byte bmiColors_rgbRed;
			public byte bmiColors_rgbReserved;
		}

	}
}
