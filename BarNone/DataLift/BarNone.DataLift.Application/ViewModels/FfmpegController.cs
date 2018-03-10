using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace BarNone.DataLift.UI.ViewModels
{
    public class FfmpegController : IDisposable
    {
        #region Fields
        private Process _recordProcess;
        private List<string> _createdFiles = new List<string>();
        private Action _firstFrameRecievedAction;

        public DateTime? FirstFrameTime { get; private set; }
        #endregion

        #region Regular Expressions
        /// <summary>
        /// Used to get the first frames origin time
        /// </summary>
        Regex FrameTimeStampRegex = new Regex(@".*dshow.*orig\stimestamp\s([0-9]+)", RegexOptions.Compiled);

        #endregion

        #region FFMPEG Controls
        /// <summary>
        /// Starts an FFMPEG Recording
        /// </summary>
        /// <param name="fname">Output File Name</param>
        public void StartFfmpegRecord(string fname, Action firstFrameAction)
        {
            //Clean up
            if (File.Exists(fname))
                File.Delete(fname);

            FirstFrameTime = null;

            _firstFrameRecievedAction = firstFrameAction;

            //Create the process, the order of events below matters, do not change!
            //ffmpeg.exe -f dshow -video_size 1920x1080 -framerate 30 -vcodec mjpeg -i video = "C922 Pro Stream Webcam" out.avi
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = $"{Directory.GetCurrentDirectory()}/res/ffmpeg.exe",
                //Arguments = $"-loglevel verbose -f dshow -video_size 1920x1080 -framerate 30 -vcodec mjpeg -i video=\"C922 Pro Stream Webcam\" {fname}", //Prefered Webcam
                Arguments = $"-loglevel verbose -f dshow -video_size 1920x1080 -framerate 15 -vcodec mjpeg -i video=\"Microsoft LifeCam Rear\" {fname}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            _recordProcess = new Process
            {
                StartInfo = psi,
                EnableRaisingEvents = true
            };

            _recordProcess.OutputDataReceived += FfmpegRecivedDShowLine;
            _recordProcess.ErrorDataReceived += FfmpegRecivedDShowLine;

            _recordProcess.Start();

            _recordProcess.BeginOutputReadLine();
            _recordProcess.BeginErrorReadLine();

            _createdFiles.Add(fname);
        }

        private void FfmpegRecivedDShowLine(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null || FirstFrameTime != null)
                return;
            Console.WriteLine(e.Data);
            if (FrameTimeStampRegex.IsMatch(e.Data))
            {
                FirstFrameTime = DateTime.Now.AddMilliseconds(-Environment.TickCount + Convert.ToInt64(FrameTimeStampRegex.Match(e.Data).Groups[1].Value) / 10000);
                _recordProcess.OutputDataReceived -= FfmpegRecivedDShowLine;
                _recordProcess.ErrorDataReceived -= FfmpegRecivedDShowLine;
                _firstFrameRecievedAction.Invoke();
            }
        }

        /// <summary>
        /// Forces the FFMPEG Recording to Close
        /// </summary>
        public void StopFfmpegRecord()
        {
            if (_recordProcess.HasExited)
                throw new Exception("The FFMPEG instance has exited implying a runtime or execution time error occured, review before continuing!");

            _recordProcess.Kill();
            _recordProcess = null;
        }
        
        #endregion

        #region IDisposable Support
        /// <summary>
        /// Determines redundant dispose call
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                _recordProcess?.Kill();
                _recordProcess = null;

                _createdFiles.ForEach(f => File.Delete(f));

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
