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
	public partial class BitmapLattice : ContentPage
	{
		public BitmapLattice ()
		{
			InitializeComponent ();
		}

        protected void OnDrawSample(SKCanvas canvas, int width, int height)
        {
            canvas.Clear(SKColors.White);

            using (var stream = new SKManagedStream(SampleMedia.Images.NinePatch))
            using (var bitmap = SKBitmap.Decode(stream))
            {
                var patchCenter = new SKRectI(33, 33, 256 - 33, 256 - 33);

                // 2x3 for portrait, or 3x2 for landscape
                var land = width > height;
                var min = land ? Math.Min(width / 3f, height / 2f) : Math.Min(width / 2f, height / 3f);
                var wide = SKRect.Inflate(SKRect.Create(0, land ? min : (min * 2f), min * 2f, min), -20, -20);
                var tall = SKRect.Inflate(SKRect.Create(land ? (min * 2f) : min, 0, min, min * 2f), -20, -20);
                var square = SKRect.Inflate(SKRect.Create(0, 0, min, min), -20, -20);
                var text = SKRect.Create(land ? min : 0, land ? 0 : min, min, min / 5f);
                text.Offset(text.Width / 2f, text.Height * 1.5f);
                text.Right = text.Left;

                // draw the bitmaps
                canvas.DrawBitmapNinePatch(bitmap, patchCenter, square);
                canvas.DrawBitmapNinePatch(bitmap, patchCenter, tall);
                canvas.DrawBitmapNinePatch(bitmap, patchCenter, wide);

                // describe what we see
                using (var paint = new SKPaint())
                {
                    paint.IsAntialias = true;
                    paint.TextAlign = SKTextAlign.Center;
                    paint.TextSize = text.Height * 0.75f;

                    canvas.DrawText("The corners", text.Left, text.Top, paint);
                    text.Offset(0, text.Height);
                    canvas.DrawText("should always", text.Left, text.Top, paint);
                    text.Offset(0, text.Height);
                    canvas.DrawText("be square", text.Left, text.Top, paint);
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