using System;
using Xamarin.Forms;

namespace XamarinSDC
{
	public partial class RelativeScaleAnimation : ContentPage
	{
		public RelativeScaleAnimation ()
		{
			InitializeComponent ();
		}

		void SetIsEnabledButtonState (bool startButtonState, bool cancelButtonState)
		{
			startButton.IsEnabled = startButtonState;
			cancelButton.IsEnabled = cancelButtonState;
		}

		async void OnStartAnimationButtonClicked (object sender, EventArgs e)
		{
			SetIsEnabledButtonState (false, true);
			await image.RelScaleTo (2, 2000);
			SetIsEnabledButtonState (true, false);
		}

		void OnCancelAnimationButtonClicked (object sender, EventArgs e)
		{
			ViewExtensions.CancelAnimations (image);
			SetIsEnabledButtonState (true, false);
		}
	}
}
