using System.Drawing;

namespace Programmer.Test.NEO
{
    public class ImageHelper
    {
        public static Stream CreateImageFile(SineWave sw)
        {
            double A = sw.A; // Амплитуда сигнала
            double Fd = sw.Fd; // Частота дискретизации в точках за секунду
            double Fs = sw.Fs; // Частота сигнала в Герцах
            int N = sw.N; // Количество периодов

            int numPoints = (int)Fd * N;

            double[] signal = new double[numPoints];

            for (int i = 0; i < numPoints; i++)
            {
                double t = (double)i / Fd;
                signal[i] = A * Math.Sin(2 * Math.PI * Fs * t);
            }

            double maxSignal = signal[0];
            double minSignal = signal[0];

            foreach (var value in signal)
            {
                if (value > maxSignal)
                    maxSignal = value;
                if (value < minSignal)
                    minSignal = value;
            }

            Bitmap bitmap = new(1920, 1080);
            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.Clear(Color.White);

            double scaleFactor = bitmap.Height / (maxSignal - minSignal);
            for (int i = 0; i < numPoints; i++)
            {
                signal[i] = (signal[i] - minSignal) * scaleFactor;
            }

            Pen pen = new(Color.Black);
            for (int i = 0; i < numPoints - 1; i++)
            {
                int x1 = i * bitmap.Width / numPoints;
                int x2 = (i + 1) * bitmap.Width / numPoints;
                int y1 = bitmap.Height - (int)signal[i];
                int y2 = bitmap.Height - (int)signal[i + 1];
                graphics.DrawLine(pen, x1, y1, x2, y2);
            }

            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }
    }
}
