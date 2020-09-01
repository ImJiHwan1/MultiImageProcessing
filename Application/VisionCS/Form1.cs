using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace VisionCS
{

    public partial class Form1 : Form
    {
        int m_hDLL;
        IntPtr intPtr;
        Bitmap m_bmp;

        [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
        private extern static int LoadLibrary(string libraryname);

        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
        private extern static IntPtr GetProcAddress(int hwnd, string proname);

        [DllImport("kernel32.dll", EntryPoint = "FreeLibrary")]
        private extern static bool FreeLibrary(int hModule);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int EngineVisionYUVCS(byte[] Y, byte[] U, byte[] V, 
                                                                byte[] col, int w, int h);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int EngineVisionRGBCS( byte[] R, byte[] G, byte[] B,
                                                                 byte[] col, int w, int h);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int EngineImageProcessCS(
                                                                        byte[] ret, 
                                                                        byte[] col, int w, int h, int BitsPerPixel
                                                                      );
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int EngineHistogramVisionCS(byte[] ret, byte[] gry, int w, int h);
        public Form1()
        {
            unsafe
            {
                m_hDLL = LoadLibrary("VisionDll.dll");
                if (m_hDLL == 0) MessageBox.Show("Error dll load!");
            }
            InitializeComponent();
        }
        
        private void ButtonYUVVision_Click(object sender, EventArgs e)
        {
            if (PictureBoxMain.Image == null) return;
            int w = m_bmp.Width;
            int h = m_bmp.Height;

            byte[] col = Get3BytePixelDataFromBitmap(m_bmp, w, h);

            byte[] Y = new byte[w * h * 3];
            byte[] U = new byte[w * h * 3];
            byte[] V = new byte[w * h * 3];

            intPtr = GetProcAddress(m_hDLL, "EngineSplitYUVchannels");
            EngineVisionYUVCS SplitYUVchannels =
                (EngineVisionYUVCS)Marshal.GetDelegateForFunctionPointer(intPtr,
                typeof(EngineVisionYUVCS));

            SplitYUVchannels(Y, U, V, col, w, h);

            Bitmap bmpY = Get32BitBitmapFrom24BitPixelData(Y, w, h, 24);
            Bitmap bmpU = Get32BitBitmapFrom24BitPixelData(U, w, h, 24);
            Bitmap bmpV = Get32BitBitmapFrom24BitPixelData(V, w, h, 24);

            bmpY.Save("Y.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            bmpU.Save("U.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            bmpV.Save("V.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            PictureBoxMain.Image = bmpY;
            PictureBoxMain.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBoxMain.Refresh();

            label_message.Text = "w= " + PictureBoxMain.Image.Width + " h= " + PictureBoxMain.Image.Height;
        }

        private void ButtonRGBVision_Click(object sender, EventArgs e)
        {
            if (PictureBoxMain.Image == null) return;
            int w = m_bmp.Width;
            int h = m_bmp.Height;

            byte[] col = Get3BytePixelDataFromBitmap(m_bmp, w, h);

            byte[] R = new byte[w * h * 3];
            byte[] G = new byte[w * h * 3];
            byte[] B = new byte[w * h * 3];

            intPtr = GetProcAddress(m_hDLL, "EngineSplitRGBchannels");
            EngineVisionRGBCS SplitRGBchannels =
                (EngineVisionRGBCS)Marshal.GetDelegateForFunctionPointer(intPtr,
                typeof(EngineVisionRGBCS));

            SplitRGBchannels(R, G, B, col, w, h);

            Bitmap bmpR = Get32BitBitmapFrom24BitPixelData(R, w, h, 24);
            Bitmap bmpG = Get32BitBitmapFrom24BitPixelData(G, w, h, 24);
            Bitmap bmpB = Get32BitBitmapFrom24BitPixelData(B, w, h, 24);

            bmpR.Save("R.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            bmpG.Save("G.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            bmpB.Save("B.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            PictureBoxMain.Image = bmpG;
            PictureBoxMain.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBoxMain.Refresh();

            label_message.Text = "w= " + PictureBoxMain.Image.Width + " h= " + PictureBoxMain.Image.Height;
        }

        private void ButtonVerMirroring_Click(object sender, EventArgs e)
        {
            if (PictureBoxMain.Image == null) return;

            int w = PictureBoxMain.Image.Width;
            int h = PictureBoxMain.Image.Height;
            Bitmap bmp = new Bitmap(PictureBoxMain.Image);
            byte[] col = Get3BytePixelDataFromBitmap(bmp, w, h);

            byte[] ret = new byte[w * h * 3];
            intPtr = GetProcAddress(m_hDLL, "EngineVerImageMirror");
            EngineImageProcessCS GetVerMirrorImage =
                (EngineImageProcessCS)Marshal.GetDelegateForFunctionPointer(intPtr,
                        typeof(EngineImageProcessCS));
            GetVerMirrorImage(ret, col, w, h, 24);

            Display3BytePixelDataToPictureBox(ret, w, h, 24);

            Bitmap bmpret = Get32BitBitmapFrom24BitPixelData(ret, w, h, 24);

            bmpret.Save("O.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            
            label_message.Text = "수직방향 대칭변환 이미지 !!";
        }

        private void ButtonHorMirroring_Click(object sender, EventArgs e)
        {
            if (PictureBoxMain.Image == null) return;

            int w = PictureBoxMain.Image.Width;
            int h = PictureBoxMain.Image.Height;
            Bitmap bmp = new Bitmap(PictureBoxMain.Image);

            byte[] col = Get3BytePixelDataFromBitmap(bmp, w, h); //원본영상 가져오기

            byte[] ret = new byte[w * h * 3]; // 변환영상 가져오기

            // Dll함수 호출
            intPtr = GetProcAddress(m_hDLL, "EngineHorImageMirror");
            EngineImageProcessCS GetHorMirrorImage =
                (EngineImageProcessCS)Marshal.GetDelegateForFunctionPointer(intPtr,
                        typeof(EngineImageProcessCS));
            GetHorMirrorImage(ret, col, w, h, 24);

            Display3BytePixelDataToPictureBox(ret, w, h, 24);

            Bitmap bmpret = Get32BitBitmapFrom24BitPixelData(ret, w, h, 24);

            bmpret.Save("M.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            
            label_message.Text = "수평방향 대칭변환 이미지 !!";
        }

        private void ButtonEmbossFiltering_Click(object sender, EventArgs e)
        {
            if (PictureBoxMain.Image == null) return;
            // Photo Width,Height Setting
            int w = PictureBoxMain.Image.Width;
            int h = PictureBoxMain.Image.Height;
            Bitmap bmp = new Bitmap(PictureBoxMain.Image);
            byte[] col = Get3BytePixelDataFromBitmap(bmp, w, h);

            byte[] ret = new byte[w * h * 3];

            // EngineImageEmbossing Callback
            intPtr = GetProcAddress(m_hDLL, "EngineImageEmbossing");
            EngineImageProcessCS GetEmbossingImage =
                (EngineImageProcessCS)Marshal.GetDelegateForFunctionPointer(intPtr,
                        typeof(EngineImageProcessCS));
            GetEmbossingImage(ret, col, w, h, 24);

            Display3BytePixelDataToPictureBox(ret, w, h, 24);

            Bitmap bmpret = Get32BitBitmapFrom24BitPixelData(ret, w, h, 24);

            bmpret.Save("E.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
           
            label_message.Text = "w= " + PictureBoxMain.Image.Width + " h= " + PictureBoxMain.Image.Height;
        }

        private void ButtonHistogramVision_Click(object sender, EventArgs e)
        {
            if (PictureBoxMain.Image == null) return;
            int w = m_bmp.Width;
            int h = m_bmp.Height;

            byte[] col = Get3BytePixelDataFromBitmap(m_bmp, w, h);

            byte[] ret = new byte[w * h * 3];

            intPtr = GetProcAddress(m_hDLL, "EngineHistogramEqualization");
            EngineHistogramVisionCS HistogramEqualization = (EngineHistogramVisionCS)Marshal.GetDelegateForFunctionPointer
                                                                                                (intPtr, typeof(EngineHistogramVisionCS));
            HistogramEqualization(ret, col, w, h);

            Display3BytePixelDataToPictureBox(ret, w, h, 24);

            Bitmap bmpret = Get32BitBitmapFrom24BitPixelData(ret, w, h, 24);

            bmpret.Save("H.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            label_message.Text = "w= " + PictureBoxMain.Image.Width + " h= " + PictureBoxMain.Image.Height;
        }
       

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            FreeLibrary(m_hDLL);
            this.Close();
        }

        private void ButtonLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog MyOpenFileDialog = new OpenFileDialog();
            MyOpenFileDialog.ShowDialog();
            string filePath = MyOpenFileDialog.FileName;

            LoadBitmapDisplayViewScreen(filePath);
        }
        private void Display3BytePixelDataToPictureBox(byte[] col, int w, int h, int BitsPerPixel)
        {
            int x, y;
            byte[] raw = new byte[w * h * 4];

            for (y = 0; y < h; y++)
            {
                for (x = 0; x < w; x++)
                {
                   raw[(4 * w) * (y) + (4 * x) + 0] = col[(3 * w) * (y) + (3 * x) + 0];
                   raw[(4 * w) * (y) + (4 * x) + 1] = col[(3 * w) * (y) + (3 * x) + 1];
                   raw[(4 * w) * (y) + (4 * x) + 2] = col[(3 * w) * (y) + (3 * x) + 2];
                }
            }
            Bitmap graybmp = RawTo32BitsBitmap(raw, w, h);

            PictureBoxMain.Image = graybmp;
            PictureBoxMain.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBoxMain.Refresh();
        }


        private void LoadBitmapDisplayViewScreen(string filePath)
        {
            Image image = Image.FromFile(filePath);

            PictureBoxMain.Image = image;
            PictureBoxMain.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBoxMain.Refresh();

            label_message.Text = Path.GetFileName(filePath);

            m_bmp = new Bitmap(image);
            label_message.Text = "w= " + m_bmp.Width + " h= " + m_bmp.Height;
        }

        private byte[] Get3BytePixelDataFromBitmap(Bitmap bmp, int w, int h)
        {
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly,
                                            bmp.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;

            byte[] colValue = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, colValue, 0, bytes);

            bmp.UnlockBits(bmpData);

            byte[] col = new byte[w * h * 3]; // 3bytes/pixel data
            for (int i = 0; i < w * h; i++)
            {
                col[i * 3 + 0] = colValue[i * 4 + 0]; //B
                col[i * 3 + 1] = colValue[i * 4 + 1]; // G
                col[i * 3 + 2] = colValue[i * 4 + 2]; // R
            }
            return col;
        }

        private Bitmap Get32BitBitmapFrom24BitPixelData(byte[] col, int w, int h, int BitsperPixel)
        {
            int x, y;
            byte[] raw = new byte[w * h * 4];

            for (y = 0; y < h; y++)
            {
                for (x = 0; x < w; x++)
                {
                   raw[(4 * w) * (y) + (4 * x) + 0] = col[(3 * w) * (y) + (3 * x) + 0];
                   raw[(4 * w) * (y) + (4 * x) + 1] = col[(3 * w) * (y) + (3 * x) + 1];
                   raw[(4 * w) * (y) + (4 * x) + 2] = col[(3 * w) * (y) + (3 * x) + 2];
                }
            }
            Bitmap bmp = RawTo32BitsBitmap(raw, w, h);
            return bmp;
        }

        private Bitmap RawTo32BitsBitmap(byte[] raw, int w, int h)
        {
            Bitmap res = new Bitmap(w, h, PixelFormat.Format32bppRgb);
            BitmapData data = res.LockBits(new Rectangle(0, 0, w, h),
                                                            ImageLockMode.ReadOnly,
                                                             PixelFormat.Format32bppRgb);
            IntPtr ptr = data.Scan0;
            Marshal.Copy(raw, 0, ptr, w * h * 4);
            res.UnlockBits(data);
            return res;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
