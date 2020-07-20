using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;

namespace Webcam0._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;

        private void pic_Click(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[1].MonikerString);
            FilterInfoCollection VideoCaptureDevices;
            VideoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            videoCaptureDevice.SetCameraProperty(CameraControlProperty.Iris, -4, CameraControlFlags.Manual);
            videoCaptureDevice.SetCameraProperty(CameraControlProperty.Zoom, 7, CameraControlFlags.Manual);
            videoCaptureDevice.SetCameraProperty(CameraControlProperty.Pan, -4, CameraControlFlags.Manual);
            videoCaptureDevice.SetCameraProperty(CameraControlProperty.Tilt, -4, CameraControlFlags.Manual);
            videoCaptureDevice.SetCameraProperty(CameraControlProperty.Exposure, -8, CameraControlFlags.Manual);
            //videoCaptureDevice.DisplayPropertyPage(IntPtr.Zero); //This will display a form with camera controls
            videoCaptureDevice.NewFrame += VideoCaptureDevce_NewFrame;
            videoCaptureDevice.Start();

            

        }

        private void VideoCaptureDevce_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pic.Image = (Bitmap)eventArgs.Frame.Clone();



        }




        private void Form1_Load(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfoCollection)
                cboCamera.Items.Add(filterInfo.Name);
            cboCamera.SelectedIndex = 0;
            videoCaptureDevice = new VideoCaptureDevice();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoCaptureDevice.IsRunning == true)
                videoCaptureDevice.Stop();
        }
    }
}
