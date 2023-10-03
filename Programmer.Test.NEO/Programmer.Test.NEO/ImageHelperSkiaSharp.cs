using SkiaSharp;

namespace Programmer.Test.NEO
{
    public class ImageHelperSkiaSharp
    {
        public static Stream CreateImageFile(SineWave sw)
        {
            int width = 1920; // Ширина изображения
            int height = 1080; // Высота изображения

            double A = sw.A; // Амплитуда синусоиды (в условных единицах)
            double Fs = sw.Fs; // Частота сигнала (в Герцах)
            double Fd = sw.Fd; // Частота дискретизации (точек за секунду)
            int N = sw.N; // Количество периодов

            using (var bitmap = new SKBitmap(width, height))
            {
                using (var canvas = new SKCanvas(bitmap))
                {
                    // Очищаем холст
                    canvas.Clear(SKColors.White);

                    // Задаем красный цвет для графика
                    var paint = new SKPaint
                    {
                        Color = SKColors.Red,
                        Style = SKPaintStyle.Stroke,
                        StrokeWidth = 2
                    };

                    // Вычисляем количество точек
                    int numPoints = (int)(Fd * N);

                    // Вычисляем период сигнала в точках
                    double period = Fd / Fs;

                    // Рисуем синусоиду
                    var path = new SKPath();
                    for (int i = 0; i <= numPoints; i++)
                    {
                        double x = i * width / numPoints;
                        double y = height / 2 - A * (double)Math.Sin(2 * Math.PI * Fs * i / Fd);
                        path.LineTo((float)x, (float)y);
                    }

                    canvas.DrawPath(path, paint);

                    // Сохраняем изображение в файл
                    var image = SKImage.FromBitmap(bitmap);
                    var ms = new MemoryStream();
                    image.Encode(SKEncodedImageFormat.Png, 100).SaveTo(ms);

                    ms.Seek(0, SeekOrigin.Begin);
                    return ms;
                }
            }
        }
    }
}
