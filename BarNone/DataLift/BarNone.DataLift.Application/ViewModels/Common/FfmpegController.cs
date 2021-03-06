﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BarNone.DataLift.UI.ViewModels.Common
{
    public class FfmpegController : IDisposable
    {
        #region Fields
        private Process _recordProcess;
        private Process _videoInformationProcess;
        private List<string> _createdFiles = new List<string>();
        private Action _firstFrameRecievedAction;
        private CurrentLiftDataVM _currentLiftDataVM = CurrentLiftDataVMSingleton.GetInstance();

        private string _currentVideoFile;

        //Will be unused
        public long durationInMs = 0;
        long prevTicks;
        //Regex VideoWriterFrameArrivalRegex = new Regex(@"^frame=([0 - 9]+).*time=([0 - 9]{2}:[0-9]{2}:[0-9]{2}\.[0-9]+).*", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        Regex VideoWriterFrameArrivalRegex = new Regex(@"^.*timestamp\s*([0-9]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

        //TODO add resets etc this is not handled
        private bool isFirstFrame = true;

        #endregion

        #region Regular Expressions
        /// <summary>
        /// Used to get the first frames origin time
        /// </summary>
        Regex FrameTimeStampRegex = new Regex(@".*dshow.*orig\stimestamp\s([0-9]+)", RegexOptions.Compiled);

        #endregion

        #region Video Recording FFMPEG Controls
        /// <summary>
        /// Starts an FFMPEG Recording
        /// </summary>
        /// <param name="fname">Output File Name</param>
        public string StartFfmpegRecord(Action firstFrameAction)
        {
            string fname = $"__{Guid.NewGuid().ToString()}.avi";

            //Clean up
            if (File.Exists(fname))
                File.Delete(fname);

            //FirstFrameTime = null;

            _firstFrameRecievedAction = firstFrameAction;
            _currentVideoFile = fname;
            //Create the process, the order of events below matters, do not change!
            //ffmpeg.exe -f dshow -video_size 1920x1080 -framerate 30 -vcodec mjpeg -i video = "C922 Pro Stream Webcam" out.avi
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = $"{Directory.GetCurrentDirectory()}/res/ffmpeg.exe",
                Arguments = $"-loglevel verbose -y -f dshow -video_size 1920x1080 -framerate 30 -rtbufsize 500000k -vcodec mjpeg -i video=\"C922 Pro Stream Webcam\" -vf \"vflip\" -vcodec h264_qsv -threads 0 {_currentVideoFile}", //Prefered Webcam
                //Arguments = $"-loglevel verbose -f dshow -video_size 1920x1080 -framerate 15 -vcodec mjpeg -i video=\"Microsoft LifeCam Rear\" {fname}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                CreateNoWindow = true
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
            _currentLiftDataVM.ParentLiftVideoName = fname;
            return fname;
        }

        /// <summary>
        /// Event for recieving data from the video recording ffmpeg process
        /// </summary>
        /// <param name="sender">Process output stream</param>
        /// <param name="e">Data from the process output</param>
        private void FfmpegRecivedDShowLine(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null)
                return;
            if (VideoWriterFrameArrivalRegex.IsMatch(e.Data))
            {
                var ticks = long.Parse(VideoWriterFrameArrivalRegex.Match(e.Data).Groups[1].Value);
                //VideoDuration = TimeSpan.ParseExact(VideoWriterFrameArrivalRegex.Match(e.Data).Groups[2].Value, "c", System.Globalization.CultureInfo.InvariantCulture);

                if (isFirstFrame)
                {
                    prevTicks = ticks;
                    durationInMs += 33; // Use regex to get FPS if slightly off!
                    isFirstFrame = false;
                    _firstFrameRecievedAction.Invoke();
                }
                else
                {
                    durationInMs += (ticks - prevTicks) / 10000;
                    prevTicks = ticks;
                }
            }
        }

        /// <summary>
        /// Forces the FFMPEG Recording to Close
        /// </summary>
        public void StopFfmpegRecord()
        {
            if (_recordProcess.HasExited)
                throw new Exception("The FFMPEG instance has exited implying a runtime or execution time error occured, review before continuing!");

            var recordUntil = _currentLiftDataVM.DataLength() + _currentLiftDataVM.ColorDataOffset;
            double duration = 0.0d;

            while (duration < recordUntil)
            {
                duration = GetVideoDuration(_currentVideoFile, recordUntil);
                //Arbitrarily record 4 frames
                System.Threading.Thread.Sleep(120);
            }

            _recordProcess.ErrorDataReceived -= FfmpegRecivedDShowLine;
            _recordProcess.OutputDataReceived -= FfmpegRecivedDShowLine;
            _recordProcess.Kill();
            //_recordProcess.StandardInput.Write("q\n");
            _recordProcess.WaitForExit();
            Console.WriteLine($"{recordUntil} && ${durationInMs}");
            _recordProcess = null;
        }

        #endregion

        #region Video Info Controller
        private static Regex NewVideoFrameTimeRegex = new Regex(@"frame,([0-9\.]+)", RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

        private double GetVideoDuration(string fname, double breakCondition)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = $"{Directory.GetCurrentDirectory()}/res/ffprobe.exe",
                Arguments = $"-select_streams v -read_intervals {breakCondition / 1000d} -show_frames -count_frames -show_entries frame=best_effort_timestamp_time -of csv {_currentVideoFile}", //Prefered Webcam
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true

            };

            _videoInformationProcess = new Process
            {
                StartInfo = psi,
                EnableRaisingEvents = true
            };
            double curTime = 0;

            _videoInformationProcess.OutputDataReceived += (s, e) =>
            {
                if (e.Data == null)
                    return;
                Debug.WriteLine(e.Data);
                if (NewVideoFrameTimeRegex.IsMatch(e.Data))
                {
                    curTime = 1000d * double.Parse(NewVideoFrameTimeRegex.Match(e.Data).Groups[1].Value);
                    try
                    {
                        if (curTime > breakCondition && !_videoInformationProcess.HasExited)
                            _videoInformationProcess.Close();
                    }
                    catch
                    {
                        //Do nuffin'
                    }
                }
            };

            _videoInformationProcess.Start();

            _videoInformationProcess.BeginOutputReadLine();

            _videoInformationProcess.WaitForExit();

            //Return total ms
            return curTime;

        }

        #endregion

        #region Video Splitter
        /// <summary>
        /// Takes the parent recording and divides it into
        /// </summary>
        /// <param name="fname">Name of the file being split</param>
        /// <param name="startTime">Time in the video to start seeking from</param>
        /// <param name="length">Length of the recording</param>
        /// <returns>Name of the split video created</returns>
        public async Task<string> SplitVideo(string fname, double startTime, double length)
        {
            string cutFile = $"{Guid.NewGuid().ToString()}.mp4";

            await Task.Run(() =>
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = $"{Directory.GetCurrentDirectory()}/res/ffmpeg.exe",
                    Arguments = $"-i {fname} -ss {startTime} -t {length} -vcodec libx264 {cutFile}",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                var proc = new Process
                {
                    StartInfo = psi,
                    EnableRaisingEvents = true
                };

                proc.Start();
                proc.WaitForExit();
            });

            return cutFile;
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

                _createdFiles
                    .Distinct()
                    .Where(f => File.Exists(f))
                    .ToList()
                    .ForEach(f => File.Delete(f));
                
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
