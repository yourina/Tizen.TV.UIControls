using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen.TV.UIControls.Forms.Renderer;
using Tizen.TV.Extension.UIControls.Forms.Renderer;
using Tizen.TV.Multimedia;
using TM = Tizen.Multimedia;

[assembly: Xamarin.Forms.Dependency(typeof(TVMediaPlayerImpl))]
namespace Tizen.TV.Extension.UIControls.Forms.Renderer
{
    class TVMediaPlayerImpl : MediaPlayerImpl, ITVMediaPlayer
    {
        public TVMediaPlayerImpl()
        {
            Tizen.Log.Error("XSF", "Etner");

        }

        protected override TM.Player CreateMediaPlayer()
        {
            Tizen.Log.Error("XSF", "Etner");
            return new Player();
        }


    }
}
