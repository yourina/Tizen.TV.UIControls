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
	public partial class BitmapAnnotation : ContentPage
	{
		public BitmapAnnotation ()
		{
			InitializeComponent ();
        }

        protected void OnDrawSample(SKCanvas canvas, int width, int height)
        {
            canvas.Clear(SKColors.White);

            // decode the bitmap
            var desiredInfo = new SKImageInfo(386, 395, SKImageInfo.PlatformColorType, SKAlphaType.Premul);
            using (var stream = new SKManagedStream(SampleMedia.Images.BabyTux))
            using (var bitmap = SKBitmap.Decode(stream, desiredInfo))
            {
                // draw directly on the bitmap
                using (var annotationCanvas = new SKCanvas(bitmap))
                using (var paint = new SKPaint())
                {
                    paint.StrokeWidth = 3;
                    paint.Color = SampleMedia.Colors.XamarinLightBlue;
                    paint.Style = SKPaintStyle.Stroke;

                    var face = SKRectI.Create(100, 50, 190, 170);
                    annotationCanvas.DrawRect(face, paint);
                }

                // draw the modified bitmap to the screen
                canvas.DrawBitmap(bitmap, 10, 10);
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