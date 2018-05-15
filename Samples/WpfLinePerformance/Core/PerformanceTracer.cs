namespace Core
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    public class PerformanceTracer:IDisposable
    {
        private readonly string _message;

        private static readonly string CpuCategory = "Processor";

        private static readonly string MemoryCategory = "Memory";

        private static readonly string CpuCount = "% Processor Time";

        private static readonly string MemoryCount = "Available MBytes";

        private static readonly string CpuInstance = "_Total";

        private readonly PerformanceCounter _cpuCounter;

        private readonly PerformanceCounter _memoryCounter;

        private readonly Stopwatch _stopwatch;

        public static IDisposable StartNew(string message)
        {
            return new PerformanceTracer(message);
        }

        private PerformanceTracer(string message)
        {
            _message = message;
            _cpuCounter = new PerformanceCounter(CpuCategory, CpuCount, CpuInstance);
            _memoryCounter = new PerformanceCounter(MemoryCategory, MemoryCount);

            _cpuCounter.NextValue();
            _memoryCounter.NextValue();

            _stopwatch = Stopwatch.StartNew();
        }

        #region IDisposable Support

        private bool disposedValue = false; // 重複する呼び出しを検出するには

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }
                
                _stopwatch.Stop();
                var elapsed = _stopwatch.Elapsed;
                var cpu = _cpuCounter.NextValue();
                var memory = _memoryCounter.NextValue();
                
                _cpuCounter.Dispose();
                _memoryCounter.Dispose();

                var message = $"> [Performance Log] {DateTime.Now:HH:mm:ss.fff} {_message}, Elapsed:{elapsed}, CPU:{cpu:F}%, Memory:{memory:F}MBytes";
                using (var file = new StreamWriter(FilePath, true, Encoding.UTF8))
                {
                    file.WriteLine(message);
                }

                disposedValue = true;
            }
        }

        private static readonly string FilePath = Path.Combine(Environment.CurrentDirectory, "Performance.txt");

        ~PerformanceTracer()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
             GC.SuppressFinalize(this);
        }

        #endregion
    }
}