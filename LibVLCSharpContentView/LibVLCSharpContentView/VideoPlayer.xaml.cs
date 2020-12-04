using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LibVLCSharpContentView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoPlayer : ContentView
    {
        public static readonly BindableProperty StreamUriProperty = BindableProperty.Create(
            propertyName: nameof(StreamUri),
            returnType: typeof(string),
            declaringType: typeof(VideoPlayer),
            defaultValue: default(string),
            defaultBindingMode: BindingMode.TwoWay);
        public string StreamUri
        {
            get => (string)GetValue(StreamUriProperty);
            set => SetValue(StreamUriProperty, value);
        }

        public MediaPlayer MediaPlayer { get; private set; }
        public LibVLC LibVLC { get; private set; }

        public VideoPlayer()
        {
            InitializeComponent();
        }


        public void Start()
        {
            LibVLCSharp.Shared.Core.Initialize();

            LibVLC = new LibVLC();

            using (var media = new Media(LibVLC, new Uri(StreamUri)))
            {
                media.AddOption(new MediaConfiguration { NetworkCaching = 50 });

                MediaPlayer = new MediaPlayer(media)
                {
                    EnableHardwareDecoding = true,
                };
                MediaPlayer.SetMarqueeString(VideoMarqueeOption.Text, "Loading...");
                MediaPlayer.SetMarqueeInt(VideoMarqueeOption.X, 0);
                MediaPlayer.SetMarqueeInt(VideoMarqueeOption.Y, 1);
                MediaPlayer.SetMarqueeInt(VideoMarqueeOption.Enable, 1);
                MediaPlayer.EnableMouseInput = false;
                MediaPlayer.EnableKeyInput = false;

                MediaPlayer.Playing += MediaPlayer_Playing;
                MediaPlayer.Play();
            }
        }

        public void Stop()
        {
            if (MediaPlayer != null)
            {
                MediaPlayer.Playing -= MediaPlayer_Playing;

                MediaPlayer.Stop();
                MediaPlayer.Dispose();
                MediaPlayer = null;
            }

            if (LibVLC != null)
            {
                LibVLC.Dispose();
                LibVLC = null;
            }
        }

        private void MediaPlayer_Playing(object sender, EventArgs e)
        {
            MediaPlayer.SetMarqueeInt(VideoMarqueeOption.Enable, 0);
        }
    }
}