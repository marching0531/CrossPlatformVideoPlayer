using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Tizen.Multimedia;
using VideoPlayer.Tizen.Tizen.TV.Renderer;
using VideoPlayer.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(MediaRenderingView), typeof(MediaRenderingViewRenderer))]
namespace VideoPlayer.Tizen.Tizen.TV.Renderer
{
    public class MediaRenderingViewRenderer : ViewRenderer<MediaRenderingView, MediaView>
    {
        private Player player;

        protected override void OnElementChanged(ElementChangedEventArgs<MediaRenderingView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                SetNativeControl(new MediaView(Forms.Context.MainWindow));
            }

            if (e.OldElement != null)
            {
                player.PlaybackCompleted -= PlaybackCompleted;
                player.PlaybackInterrupted -= PlaybackInterrupted;
            }

            if (e.NewElement != null)
            {
                player = new Player();

                player.PlaybackCompleted += PlaybackCompleted;
                player.PlaybackInterrupted += PlaybackInterrupted;
            }
        }

        private void PlaybackInterrupted(object sender, PlaybackInterruptedEventArgs e)
        {
            ((IElementController)Element).SetValueFromRenderer(MediaRenderingView.CurrentStatusProperty, PlayerStatus.Stopped);
        }

        private void PlaybackCompleted(object sender, EventArgs e)
        {
            ((IElementController)Element).SetValueFromRenderer(MediaRenderingView.CurrentStatusProperty, PlayerStatus.Stopped);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == MediaRenderingView.CurrentVideoProperty.PropertyName)
            {
                PlayVideo();
            }
            else if (e.PropertyName == MediaRenderingView.CurrentStatusProperty.PropertyName)
            {
                switch (Element.CurrentStatus)
                {
                    case PlayerStatus.Playing:
                        if (player.State == PlayerState.Idle)
                        {
                            PlayVideo();
                        }

                        if (player.State == PlayerState.Ready || player.State == PlayerState.Paused)
                        {
                            player.Start();
                        }
                        break;
                    case PlayerStatus.Paused:
                        if (player.State == PlayerState.Playing)
                        {
                            player.Pause();
                        }
                        break;
                    case PlayerStatus.Stopped:
                        if (player.State == PlayerState.Playing || player.State == PlayerState.Paused)
                        {
                            player.Stop();
                        }

                        if (player.State == PlayerState.Ready)
                        {
                            player.Unprepare();
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private async void PlayVideo()
        {
            if (player.State == PlayerState.Playing || player.State == PlayerState.Paused)
            {
                player.Stop();
            }

            if (player.State == PlayerState.Ready)
            {
                player.Unprepare();
            }

            try
            {
                player.Display = new Display(Control);
                player.SetSource(new MediaUriSource(Element.VideoPath));
            }
            catch (Exception e)
            {
                Console.WriteLine("Player: " + e.Message);
            }

             ((IElementController)Element).SetValueFromRenderer(MediaRenderingView.CurrentStatusProperty, PlayerStatus.Preparing);

            try
            {
                await player.PrepareAsync();
                ((IElementController)Element).SetValueFromRenderer(MediaRenderingView.CurrentStatusProperty, PlayerStatus.Playing);
            }
            catch (Exception e)
            {
                ((IElementController)Element).SetValueFromRenderer(MediaRenderingView.CurrentStatusProperty, PlayerStatus.Stopped);
            }
        }
    }
}