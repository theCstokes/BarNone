using System;
using System.Diagnostics;
using System.IO;

namespace BarNone.DataLift.UI.ViewModels
{
    public class FfmpegController : IDisposable
    {
        #region Fields
        private Process _recordProcess;

        #endregion

        #region FFMPEG Controls
        /// <summary>
        /// Starts an FFMPEG Recording
        /// </summary>
        /// <param name="fname">Output File Name</param>
        public void StartFfmpegRecord(string fname)
        {
            //ffmpeg.exe - f dshow - video_size 1920x1080 - framerate 30 - vcodec mjpeg - i video = "C922 Pro Stream Webcam" out.avi
            ProcessStartInfo psi = new ProcessStartInfo($"{Directory.GetCurrentDirectory()}/res/ffmpeg.exe", $"-f dshow -video_size 1920x1080 -framerate 30 -vcodec mjpeg -i video=\"C922 Pro Stream Webcam\" {fname}");
            _recordProcess = Process.Start(psi);
        }

        /// <summary>
        /// Forces the FFMPEG Recording to Close
        /// </summary>
        public void StopFfmpegRecord()
        {
            if (!_recordProcess.HasExited)
                _recordProcess.Kill();
            _recordProcess = null;
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                _recordProcess?.Kill();
                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        ~FfmpegController()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    }
}
