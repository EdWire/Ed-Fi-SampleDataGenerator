using log4net;
using System;
using System.Diagnostics;

namespace EdFi.SampleDataGenerator.Core.ApplicationPerformanceLog
{
    public class PerformanceLogger
    {
        public PerformanceLog Start(string taskName)
        {
            var stopwatch = Stopwatch.StartNew();

            var report = new PerformanceResult
            {
                TaskName = taskName,
                StartDate = DateTime.Now
            };

            return new PerformanceLog(stopwatch, report, LogTarget.GetInstance());           
        }        
    }

    public class PerformanceLog 
    {
        private readonly Stopwatch _stopwatch;
        private readonly PerformanceResult _report;
        private readonly LogTarget _target;
        public PerformanceLog(
            Stopwatch stopwatch,
            PerformanceResult report,
            LogTarget target)
        {
            _stopwatch = stopwatch;
            _report = report;
            _target = target;
        }
        
        public void End()
        {            
            _stopwatch.Stop();
            _report.Duration = _stopwatch.Elapsed;                      
            _target.Log(_report);
        }       
    }

    public class PerformanceResult
    {
        public string TaskName { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan Duration { get; set; }       
    }

    public class LogTarget
    {
        private static ILog _logTarget;       

        private LogTarget()
        {
            _logTarget = LogManager.GetLogger("SDGPerformanceLog");
        }

        public static LogTarget GetInstance()
        {
            return new LogTarget();
        }         

        public void Log(PerformanceResult report)
        {            
            try
            {
                _logTarget.Info($"TaskName:{report.TaskName}, StartDateAndTime:{report.StartDate}, Duration:{report.Duration}");
            }
            catch (Exception ex)
            {
                if (_logTarget != null)
                    _logTarget.Warn($"Failed to log the report on {report.TaskName}.", ex);
            }           
        }       
    }
}
