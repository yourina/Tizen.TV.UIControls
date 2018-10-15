using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace XamarinSDC.SkiaSharpSamples
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PathEffects : ContentPage
    {
        public PathEffects()
        {
            InitializeComponent();
        }
        protected void OnDrawSample(SKCanvas canvas, int width, int height)
        {
            canvas.Clear(SKColors.White);

            var step = height / 4;

            using (var paint = new SKPaint())
            using (var effect = SKPathEffect.CreateDash(new[] { 15f, 5f }, 0))
            {
                paint.IsStroke = true;
                paint.StrokeWidth = 4;
                paint.PathEffect = effect;
                canvas.DrawLine(10, step, width - 10 - 10, step, paint);
            }

            using (var paint = new SKPaint())
            using (var effect = SKPathEffect.CreateDiscrete(10, 10))
            {
                paint.IsStroke = true;
                paint.StrokeWidth = 4;
                paint.PathEffect = effect;
                canvas.DrawLine(10, step * 2, width - 10 - 10, step * 2, paint);
            }

            using (var paint = new SKPaint())
            using (var dashEffect = SKPathEffect.CreateDash(new[] { 15f, 5f }, 0))
            using (var discreteEffect = SKPathEffect.CreateDiscrete(10, 10))
            using (var effect = SKPathEffect.CreateCompose(dashEffect, discreteEffect))
            {
                paint.IsStroke = true;
                paint.StrokeWidth = 4;
                paint.PathEffect = effect;
                canvas.DrawLine(10, step * 3, width - 10 - 10, step * 3, paint);
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