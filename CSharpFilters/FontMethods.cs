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
        public static Image Render()
        {
            // create the final image to render into
            var image = new Bitmap(40, 40, PixelFormat.Format24bppRgb);

            // create memory buffer from desktop handle that supports alpha channel
            IntPtr dib;
            var memoryHdc = CreateMemoryHdc(IntPtr.Zero, image.Width, image.Height, out dib);
            try
            {
                // create memory buffer graphics to use for HTML rendering
                using (var memoryGraphics = Graphics.FromHdc(memoryHdc))
                {
                    memoryGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    // must not be transparent background 
                    memoryGraphics.Clear(Color.White);

                    // execute GDI text rendering
                    TextRenderer.DrawText(memoryGraphics, "嬲", new Font("MS gothic", 24), new Point(0, 0), Color.White, Color.Black);
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
                for (int y = 0; y < b.Height; ++y)
                {
                    int fr = 0;
                    for (int x = 0; x < b.Width; ++x)
                    {
                        blue = p[0];
                        green = p[1];
                        red = p[2];
                        if (blue == 0 && green == 0 && red == 0)
                        {
                            fr ++;
                        }
                        p += 3;
                    }
                    p += nOffset;
                    //first possition has byte -> start line
                    if ((!fs) && fr < b.Width)
                    {
                        rx = 0;
                        ry = y;
                        rw = b.Width;
                        fs = true;
                    }
                    else
                    {
                        //fisrt possition no byte after has byte -> end line 
                        if (fs && ((fr == b.Width) || (y + 1 == b.Height)))
                        {
                            rh = y - ry;
                            Rectangle bm = new Rectangle(rx, ry, rw, rh);
                            brow.Add(bm);
                            rx = ry = rw = rh = 0;
                            fs = false;
                        }
                    }
                }
            }

            b.UnlockBits(bmData);

            Bitmap[] ar = new Bitmap[brow.Count];
            for (int i = 0; i < brow.Count; i++)
            {
                ar[i] = FontMethods.CropBitmap(b, brow[i]);
            }
            return ar;
        }

        public static Bitmap JoinBitmap(Bitmap[] ab)
        {
            if (ab == null || ab.Length == 0) return null;
            int w = ab.Max(m => m.Width);
            int h = ab.Sum(m => m.Height);
            Bitmap target = new Bitmap(w, h);

            using (Graphics g = Graphics.FromImage(target))
            {
                int y = 0;
                for (int i = 0; i < ab.Length; i++)
                {
                    Rectangle cropRect = new Rectangle(0, y, ab[i].Width, ab[i].Height);
                    g.DrawImage(ab[i], cropRect,
                                 cropRect,
                                 GraphicsUnit.Pixel);
                    y += ab[i].Height;
                }
            }
            return target;
        }
        private static Bitmap CropBitmap(Bitmap b, Rectangle cropRect)
        {
            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(b, new Rectangle(0, 0, target.Width, target.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
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
