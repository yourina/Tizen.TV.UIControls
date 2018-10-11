using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Tizen;

namespace XamarinSDC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThreeDRotation : ContentPage
    {
        private SKMatrix44 rotationMatrix;
        private SKMatrix44 rotationStep;

        private CancellationTokenSource cts;

        private int cnt = 0;

        public ThreeDRotation()
        {
            Log.Debug("Demo", "Enter");
            InitializeComponent();

            // create the base and step 3D rotation matrices (around the y-axis)
            rotationMatrix = SKMatrix44.CreateRotationDegrees(0, 1, 0, 30);
            rotationStep = SKMatrix44.CreateRotationDegrees(0, 1, 0, 5);
        }

        public void InitEvent()
        {
            Log.Debug("Demo" ,"Enter");
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            cts = new CancellationTokenSource();
            var loop = Task.Run(async () =>
            {
                while (!cts.IsCancellationRequested)
                {
                    await OnUpdate(cts.Token, scheduler);

                    new Task(Refresh).Start(scheduler);
                }
            }, cts.Token);
        }

        public event EventHandler RefreshRequested;

        protected void Refresh()
        {
            Log.Debug("Demo", "Enter");

            RefreshRequested?.Invoke(this, EventArgs.Empty);
        }

        protected async Task OnUpdate(CancellationToken token, TaskScheduler mainScheduler)
        {
            Log.Debug("Demo", "Enter");

            await Task.Delay(25, token);

            cnt++;

            // step the rotation matrix
            rotationMatrix.PostConcat(rotationStep);

            if (cnt > 1000)
            {
                //cts.Cancel();
            }
        }

        protected void OnDrawSample(SKCanvas canvas, int width, int height)
        {
            var length = Math.Min(width / 6, height / 6);
            var rect = new SKRect(-length, -length, length, length);
            var side = rotationMatrix.MapPoint(new SKPoint(1, 0)).X > 0;

            canvas.Clear(SampleMedia.Colors.XamarinLightBlue);

            // first do 2D translation to the center of the screen
            canvas.Translate(width / 2, height / 2);

            // then apply the 3D rotation
            var matrix = rotationMatrix.Matrix;
            canvas.Concat(ref matrix);

            var paint = new SKPaint
            {
                Color = side ? SampleMedia.Colors.XamarinPurple : SampleMedia.Colors.XamarinGreen,
                Style = SKPaintStyle.Fill,
                IsAntialias = true
            };

            canvas.DrawRoundRect(rect, 30, 30, paint);

            var shadow = SKShader.CreateLinearGradient(
                new SKPoint(0, 0), new SKPoint(0, length * 2),
                new[] { paint.Color.WithAlpha(127), paint.Color.WithAlpha(0) },
                null,
                SKShaderTileMode.Clamp);
            paint = new SKPaint
            {
                Shader = shadow,
                Style = SKPaintStyle.Fill,
                IsAntialias = true
            };

            rect.Offset(0, length * 2 + 5);
            canvas.DrawRoundRect(rect, 30, 30, paint);

            Log.Debug("Demo", "Enter");

            if (cnt == 0)
            {
                Log.Debug("Demo", "Enter");
                InitEvent();
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