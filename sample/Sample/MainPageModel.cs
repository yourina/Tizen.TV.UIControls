﻿using System.Collections.Generic;
using Tizen.TV.UIControls.Forms;

namespace Sample
{
    public class MainPageModel
    {
        public MainPageModel()
        {
            TestList = new List<TestModel>
            {
                new TestModel("Embedding control test", typeof(TestEmbeddingControl), MediaSource.FromUri(new System.Uri("http://10.113.111.170/~abyss/d.mp4"))),
                new TestModel("Embedding control Page test", typeof(TestEmbeddingControl2), MediaSource.FromUri(new System.Uri("http://10.113.111.170/~abyss/a.mp4"))),
                new TestModel("Simple Player test", typeof(SimplePlayerPage), MediaSource.FromUri(new System.Uri("http://10.113.111.170/~abyss/d.mp4"))),
                new TestModel("Simple Player test2", typeof(SimplePlayerPage), MediaSource.FromUri(new System.Uri("http://www.html5videoplayer.net/videos/madagascar3.mp4"))),
                new TestModel("Overlay page test", typeof(TestOverlayPage), MediaSource.FromFile("c.mp4")),
                new TestModel("Overlay page test with code", typeof(TestOverlayPage2), MediaSource.FromFile("c.mp4")),
                new TestModel("Overlay view test", typeof(TestOverlayView), MediaSource.FromFile("c.mp4")),
                new TestModel("Media view test", typeof(TestMediaView), MediaSource.FromFile("d.mp4")),
                new TestModel("Aspect test", typeof(TestAspect), MediaSource.FromFile("d.mp4")),
                new TestModel("Url test(slow)", typeof(TestOverlayPage), MediaSource.FromUri(new System.Uri("http://www.html5videoplayer.net/videos/toystory.mp4"))),
                new TestModel("Url test(fast)", typeof(TestOverlayPage), MediaSource.FromUri(new System.Uri("http://10.113.111.170/~abyss/d.mp4")))
            };
        }

        public List<TestModel> TestList { get; set; }
    }
}
