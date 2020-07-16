using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Hardware.Display;
using Android.Media.Projection;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using System.IO;
using Xamarin.Forms;
using Android.Media;
using Android.Graphics;
using System.Threading.Tasks;
using Java.Interop;

namespace XamarinCrash.Droid
{
    public static class AndroidScreenShotService
    {
        private static Intent _resutltData;
        private static MediaProjectionManager _projectionManager;
        private static MediaProjection _mediaProjection;
        private static VirtualDisplay _virtualDisplay;
        private static ImageReader _imgReader;

        private static int _width = Convert.ToInt32(Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Width);
        private static int _height = Convert.ToInt32(Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Height);
        private static int _density = Convert.ToInt32(Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density);

        /// <summary>
        /// Setup
        /// </summary>
        public static void Setup()
        {
            _projectionManager = (MediaProjectionManager)Xamarin.Essentials.Platform.CurrentActivity.GetSystemService(Context.MediaProjectionService);
            Xamarin.Essentials.Platform.CurrentActivity.StartActivityForResult(_projectionManager.CreateScreenCaptureIntent(), 1);
        }

        /// <summary>
        /// Creates the media projection.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="data">The data.</param>
        public static void CreateMediaProjection(int result, Intent data)
        {
            _resutltData = data;

            var metrics = new DisplayMetrics();
            Xamarin.Essentials.Platform.CurrentActivity.WindowManager.DefaultDisplay.GetMetrics(metrics);

            _mediaProjection = _projectionManager.GetMediaProjection(result, _resutltData);
            _imgReader = ImageReader.NewInstance(_width, _height, ImageFormatType.Jpeg, 5);
            _imgReader.SetOnImageAvailableListener(new ImageAvailableListener(), null);
            _virtualDisplay = _mediaProjection.CreateVirtualDisplay("ScreenCapture", _width, _height, _density, (DisplayFlags)VirtualDisplayFlags.AutoMirror, _imgReader.Surface, null, null);
        }

        public class ImageAvailableListener : Java.Lang.Object, ImageReader.IOnImageAvailableListener
        {
            public void OnImageAvailable(ImageReader reader)
            {
                var img = _imgReader.AcquireLatestImage();
                var planes = img.GetPlanes(); // <- it crashes here
            }
        }
    }
}