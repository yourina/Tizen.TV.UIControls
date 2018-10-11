using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Tizen;

namespace XamarinSDC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColorMatrixColorFilter : ContentPage
    {
        public ColorMatrixColorFilter()
        {
            InitializeComponent();
        }

        protected void OnDrawSample(SKCanvas canvas, int width, int height)
        {
            canvas.Clear(SKColors.White);

            // load the image from the embedded resource stream
            using (var stream = new SKManagedStream(SampleMedia.Images.Baboon))
            using (var bitmap = SKBitmap.Decode(stream))
            {
                var f = new Action<SKRect, float[]>((rect, colorMatrix) =>
                {
                    using (var cf = SKColorFilter.CreateColorMatrix(colorMatrix))
                    using (var paint = new SKPaint())
                    {
                        paint.ColorFilter = cf;

                        canvas.DrawBitmap(bitmap, rect, paint);
                    }
                });

                var colorMatrix1 = new float[20] {
                    0f, 1f, 0f, 0f, 0f,
                    0f, 0f, 1f, 0f, 0f,
                    1f, 0f, 0f, 0f, 0f,
                    0f, 0f, 0f, 1f, 0f
                };
                var grayscale = new float[20] {
                    0.21f, 0.72f, 0.07f, 0.0f, 0.0f,
                    0.21f, 0.72f, 0.07f, 0.0f, 0.0f,
                    0.21f, 0.72f, 0.07f, 0.0f, 0.0f,
                    0.0f,  0.0f,  0.0f,  1.0f, 0.0f
                };
                var colorMatrix3 = new float[20] {
                    -1f,  1f,  1f, 0f, 0f,
                     1f, -1f,  1f, 0f, 0f,
                     1f,  1f, -1f, 0f, 0f,
                     0f,  0f,  0f, 1f, 0f
                };
                var colorMatrix4 = new float[20] {
                    0.0f, 0.5f, 0.5f, 0f, 0f,
                    0.5f, 0.0f, 0.5f, 0f, 0f,
                    0.5f, 0.5f, 0.0f, 0f, 0f,
                    0.0f, 0.0f, 0.0f, 1f, 0f
                };
                var highContrast = new float[20] {
                    4.0f, 0.0f, 0.0f, 0.0f, -4.0f * 255f / (4.0f - 1f),
                    0.0f, 4.0f, 0.0f, 0.0f, -4.0f * 255f / (4.0f - 1f),
                    0.0f, 0.0f, 4.0f, 0.0f, -4.0f * 255f / (4.0f - 1f),
                    0.0f, 0.0f, 0.0f, 1.0f, 0.0f
                };
                var colorMatrix6 = new float[20] {
                    0f, 0f, 1f, 0f, 0f,
                    1f, 0f, 0f, 0f, 0f,
                    0f, 1f, 0f, 0f, 0f,
                    0f, 0f, 0f, 1f, 0f
                };
                var sepia = new float[20] {
                    0.393f, 0.769f, 0.189f, 0.0f, 0.0f,
                    0.349f, 0.686f, 0.168f, 0.0f, 0.0f,
                    0.272f, 0.534f, 0.131f, 0.0f, 0.0f,
                    0.0f,   0.0f,   0.0f,   1.0f, 0.0f
                };
                var inverter = new float[20] {
                    -1f,  0f,  0f, 0f, 255f,
                    0f, -1f,  0f, 0f, 255f,
                    0f,  0f, -1f, 0f, 255f,
                    0f,  0f,  0f, 1f, 0f
                };

                var matices = new[] {
                    colorMatrix1, grayscale, highContrast, sepia,
                    colorMatrix3, colorMatrix4, colorMatrix6, inverter
                };

                var cols = width < height ? 2 : 4;
                var rows = (matices.Length - 1) / cols + 1;
                var w = (float)width / cols;
                var h = (float)height / rows;

                for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < cols; x++)
                    {
                        f(SKRect.Create(x * w, y * h, w, h), matices[y * cols + x]);
                    }
                }
            }
        }

        private void OnPaintSample(object sender, SKPaintSurfaceEventArgs e)
        {
            OnDrawSample(e.Surface.Canvas, e.Info.Width, e.Info.Height);
        }

        private void OnPaintGLSample(object sender, SKPaintGLSurfaceEventArgs e)
        {
            DrawSample(e.Surface.Canvas, e.RenderTarget.Width, e.RenderTarget.Height);

            //lastImage?.Dispose();
            //lastImage = e.Surface.Snapshot();

            var view = sender as SKGLView;
            DrawScaling(view, e.Surface.Canvas, view.CanvasSize);
        }

        protected SKMatrix Matrix = SKMatrix.MakeIdentity();

        public bool IsInitialized { get; private set; } = false;

        public void DrawSample(SKCanvas canvas, int width, int height)
        {
            if (IsInitialized)
            {
                canvas.SetMatrix(Matrix);
                OnDrawSample(canvas, width, height);
            }
        }

        private void DrawScaling(View view, SKCanvas canvas, SKSize canvasSize)
        {
            // make sure no previous transforms still apply
            canvas.ResetMatrix();

            // get the current scale
            var scale = canvasSize.Width / (float)view.Width;

            // write the scale into the bottom left
            using (var paint = new SKPaint())
            {
                paint.IsAntialias = true;
                paint.TextSize = 20 * scale;

                var text = $"Current scaling = {scale:0.0}x";
                var padding = 10 * scale;
                canvas.DrawText(text, padding, canvasSize.Height - padding, paint);
            }
        }
    }
}