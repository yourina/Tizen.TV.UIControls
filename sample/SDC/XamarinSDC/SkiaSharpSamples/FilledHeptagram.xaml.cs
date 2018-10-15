using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace XamarinSDC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilledHeptagram : ContentPage
    {
        public FilledHeptagram()
        {
            InitializeComponent();
        }

        private void OnDrawSample(SKCanvas canvas, int width, int height)
        {
            var size = ((float)height > width ? width : height) * 0.75f;
            var R = 0.45f * size;
            var TAU = 6.2831853f;

            using (var path = new SKPath())
            {
                path.MoveTo(R, 0.0f);
                for (int i = 1; i < 7; ++i)
                {
                    var theta = 3f * i * TAU / 7f;
                    path.LineTo(R * (float)Math.Cos(theta), R * (float)Math.Sin(theta));
                }
                path.Close();

                using (var paint = new SKPaint())
                {
                    paint.IsAntialias = true;
                    canvas.Clear(SKColors.White);
                    canvas.Translate(width / 2f, height / 2f);
                    canvas.DrawPath(path, paint);
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