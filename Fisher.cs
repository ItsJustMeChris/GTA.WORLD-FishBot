using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Auto_Fish_V2
{
    public class Fisher
    {
        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);
        public static int bottomChatX { get; set; } = 29;
        public static int bottomChatY { get; set; } = 251;
        public static Color bottomChatYellow { get; set; } = Color.FromArgb(255, 255, 0);
        public static string status { get; set; } = "Not Ready";

        public const UInt32 WM_KEYDOWN = 0x0100;
        public const UInt32 WM_KEYUP = 0x101;
        public const int VK_T = 0x54;
        public const int VK_FWDSLSH = 0x6F;
        public const int VK_C = 0x43;
        public const int VK_A = 0x54;
        public const int VK_H = 0x48;
        public const int VK_F = 0x46;
        public const int VK_I = 0x54;
        public const int VK_S = 0x54;
        public const int VK_ENTER = 0x0D;
        public const int VK_SHIFT = 0x10;
        public const int VK_7 = 0x37;


        private static bool hasCasted { get; set; } = false;

        public static Process GTA()
        {
            Process[] processes = Process.GetProcessesByName("GTA5");
            try
            {
                return processes[0];
            }
            catch
            {
                return null;
            }
        }

        public static void fish(object sender, EventArgs e)
        {
            var colorAtChat = Fisher.GetColorAt(new Point(Fisher.bottomChatX, Fisher.bottomChatY));
            if ((colorAtChat.R >= bottomChatYellow.R - 20 && colorAtChat.G >= bottomChatYellow.G - 20 && colorAtChat.B >= bottomChatYellow.B - 20) &&
                    (colorAtChat.R <= bottomChatYellow.R + 20 && colorAtChat.G <= bottomChatYellow.G + 20 && colorAtChat.B <= bottomChatYellow.B + 20) && !Fisher.hasCasted)
            {
                Fisher.hasCasted = true;
                FakeKey(VK_T);
                System.Threading.Thread.Sleep(50);

                FakeKey(VK_SHIFT);
                System.Threading.Thread.Sleep(50);

                FakeKey(VK_FWDSLSH);
                System.Threading.Thread.Sleep(50);

                FakeKey(VK_C);
                System.Threading.Thread.Sleep(50);

                FakeKey(VK_F);
                System.Threading.Thread.Sleep(50);

                FakeKey(VK_ENTER);
                System.Threading.Thread.Sleep(50);

                Fisher.hasCasted = false;
            }
        }

        public static void FakeKey(int key)
        {
            PostMessage(Fisher.GTA().MainWindowHandle, WM_KEYDOWN, key, 0);
        }


        private static Bitmap screenPixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
        public static Color GetColorAt(Point location)
        {
            using (Graphics gdest = Graphics.FromImage(Fisher.screenPixel))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC = gsrc.GetHdc();
                    IntPtr hDC = gdest.GetHdc();
                    int retval = BitBlt(hDC, 0, 0, 1, 1, hSrcDC, location.X, location.Y, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }
            return Fisher.screenPixel.GetPixel(0, 0);
        }
    }
}
