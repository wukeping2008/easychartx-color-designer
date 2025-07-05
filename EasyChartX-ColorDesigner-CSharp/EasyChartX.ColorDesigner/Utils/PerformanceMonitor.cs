using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EasyChartX.ColorDesigner.Utils
{
    /// <summary>
    /// 性能监控工具类
    /// </summary>
    public static class PerformanceMonitor
    {
        #region 私有字段

        private static readonly Dictionary<string, Stopwatch> _timers = new Dictionary<string, Stopwatch>();
        private static readonly Dictionary<string, List<long>> _measurements = new Dictionary<string, List<long>>();
        private static readonly object _lock = new object();
        private static bool _isEnabled = true;
        private static string _logFilePath;

        #endregion

        #region 初始化

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static PerformanceMonitor()
        {
            _logFilePath = Path.Combine(Application.StartupPath, "Logs", "Performance.log");
            EnsureLogDirectory();
        }

        /// <summary>
        /// 确保日志目录存在
        /// </summary>
        private static void EnsureLogDirectory()
        {
            try
            {
                var logDir = Path.GetDirectoryName(_logFilePath);
                if (!Directory.Exists(logDir))
                {
                    Directory.CreateDirectory(logDir);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"创建日志目录失败：{ex.Message}");
            }
        }

        #endregion

        #region 公共属性

        /// <summary>
        /// 是否启用性能监控
        /// </summary>
        public static bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; }
        }

        /// <summary>
        /// 日志文件路径
        /// </summary>
        public static string LogFilePath
        {
            get { return _logFilePath; }
            set { _logFilePath = value; EnsureLogDirectory(); }
        }

        #endregion

        #region 计时器操作

        /// <summary>
        /// 开始计时
        /// </summary>
        /// <param name="operationName">操作名称</param>
        public static void StartTimer(string operationName)
        {
            if (!_isEnabled || string.IsNullOrEmpty(operationName))
                return;

            lock (_lock)
            {
                if (_timers.ContainsKey(operationName))
                {
                    _timers[operationName].Restart();
                }
                else
                {
                    _timers[operationName] = Stopwatch.StartNew();
                }
            }
        }

        /// <summary>
        /// 停止计时并记录结果
        /// </summary>
        /// <param name="operationName">操作名称</param>
        /// <returns>耗时（毫秒）</returns>
        public static long StopTimer(string operationName)
        {
            if (!_isEnabled || string.IsNullOrEmpty(operationName))
                return 0;

            lock (_lock)
            {
                if (_timers.ContainsKey(operationName))
                {
                    var timer = _timers[operationName];
                    timer.Stop();
                    var elapsed = timer.ElapsedMilliseconds;

                    // 记录测量结果
                    if (!_measurements.ContainsKey(operationName))
                    {
                        _measurements[operationName] = new List<long>();
                    }
                    _measurements[operationName].Add(elapsed);

                    // 记录到日志
                    LogPerformance(operationName, elapsed);

                    return elapsed;
                }
            }

            return 0;
        }

        /// <summary>
        /// 测量操作执行时间
        /// </summary>
        /// <param name="operationName">操作名称</param>
        /// <param name="action">要执行的操作</param>
        /// <returns>耗时（毫秒）</returns>
        public static long MeasureTime(string operationName, Action action)
        {
            if (!_isEnabled || action == null)
                return 0;

            StartTimer(operationName);
            try
            {
                action();
            }
            catch (Exception ex)
            {
                LogError(operationName, ex);
                throw;
            }
            finally
            {
                StopTimer(operationName);
            }

            return GetLastMeasurement(operationName);
        }

        /// <summary>
        /// 测量函数执行时间
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="operationName">操作名称</param>
        /// <param name="func">要执行的函数</param>
        /// <returns>函数返回值</returns>
        public static T MeasureTime<T>(string operationName, Func<T> func)
        {
            if (!_isEnabled || func == null)
                return default(T);

            StartTimer(operationName);
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                LogError(operationName, ex);
                throw;
            }
            finally
            {
                StopTimer(operationName);
            }
        }

        #endregion

        #region 统计信息

        /// <summary>
        /// 获取最后一次测量结果
        /// </summary>
        /// <param name="operationName">操作名称</param>
        /// <returns>耗时（毫秒）</returns>
        public static long GetLastMeasurement(string operationName)
        {
            if (string.IsNullOrEmpty(operationName))
                return 0;

            lock (_lock)
            {
                if (_measurements.ContainsKey(operationName) && _measurements[operationName].Count > 0)
                {
                    var measurements = _measurements[operationName];
                    return measurements[measurements.Count - 1];
                }
            }

            return 0;
        }

        /// <summary>
        /// 获取平均执行时间
        /// </summary>
        /// <param name="operationName">操作名称</param>
        /// <returns>平均耗时（毫秒）</returns>
        public static double GetAverageTime(string operationName)
        {
            if (string.IsNullOrEmpty(operationName))
                return 0;

            lock (_lock)
            {
                if (_measurements.ContainsKey(operationName) && _measurements[operationName].Count > 0)
                {
                    var measurements = _measurements[operationName];
                    long total = 0;
                    foreach (var measurement in measurements)
                    {
                        total += measurement;
                    }
                    return (double)total / measurements.Count;
                }
            }

            return 0;
        }

        /// <summary>
        /// 获取最小执行时间
        /// </summary>
        /// <param name="operationName">操作名称</param>
        /// <returns>最小耗时（毫秒）</returns>
        public static long GetMinTime(string operationName)
        {
            if (string.IsNullOrEmpty(operationName))
                return 0;

            lock (_lock)
            {
                if (_measurements.ContainsKey(operationName) && _measurements[operationName].Count > 0)
                {
                    var measurements = _measurements[operationName];
                    long min = long.MaxValue;
                    foreach (var measurement in measurements)
                    {
                        if (measurement < min)
                            min = measurement;
                    }
                    return min;
                }
            }

            return 0;
        }

        /// <summary>
        /// 获取最大执行时间
        /// </summary>
        /// <param name="operationName">操作名称</param>
        /// <returns>最大耗时（毫秒）</returns>
        public static long GetMaxTime(string operationName)
        {
            if (string.IsNullOrEmpty(operationName))
                return 0;

            lock (_lock)
            {
                if (_measurements.ContainsKey(operationName) && _measurements[operationName].Count > 0)
                {
                    var measurements = _measurements[operationName];
                    long max = 0;
                    foreach (var measurement in measurements)
                    {
                        if (measurement > max)
                            max = measurement;
                    }
                    return max;
                }
            }

            return 0;
        }

        /// <summary>
        /// 获取执行次数
        /// </summary>
        /// <param name="operationName">操作名称</param>
        /// <returns>执行次数</returns>
        public static int GetExecutionCount(string operationName)
        {
            if (string.IsNullOrEmpty(operationName))
                return 0;

            lock (_lock)
            {
                if (_measurements.ContainsKey(operationName))
                {
                    return _measurements[operationName].Count;
                }
            }

            return 0;
        }

        #endregion

        #region 内存监控

        /// <summary>
        /// 获取当前内存使用量（MB）
        /// </summary>
        /// <returns>内存使用量（MB）</returns>
        public static long GetMemoryUsage()
        {
            try
            {
                using (var process = Process.GetCurrentProcess())
                {
                    return process.WorkingSet64 / 1024 / 1024;
                }
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取GC内存使用量（MB）
        /// </summary>
        /// <returns>GC内存使用量（MB）</returns>
        public static long GetGCMemoryUsage()
        {
            try
            {
                return GC.GetTotalMemory(false) / 1024 / 1024;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 强制垃圾回收
        /// </summary>
        public static void ForceGarbageCollection()
        {
            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"强制垃圾回收失败：{ex.Message}");
            }
        }

        #endregion

        #region 日志记录

        /// <summary>
        /// 记录性能信息
        /// </summary>
        /// <param name="operationName">操作名称</param>
        /// <param name="elapsedMs">耗时（毫秒）</param>
        private static void LogPerformance(string operationName, long elapsedMs)
        {
            try
            {
                var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] PERF: {operationName} - {elapsedMs}ms - Memory: {GetMemoryUsage()}MB";
                WriteToLog(logEntry);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"记录性能日志失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="operationName">操作名称</param>
        /// <param name="exception">异常信息</param>
        private static void LogError(string operationName, Exception exception)
        {
            try
            {
                var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] ERROR: {operationName} - {exception.Message}";
                WriteToLog(logEntry);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"记录错误日志失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 写入日志文件
        /// </summary>
        /// <param name="logEntry">日志条目</param>
        private static void WriteToLog(string logEntry)
        {
            try
            {
                File.AppendAllText(_logFilePath, logEntry + Environment.NewLine, Encoding.UTF8);
            }
            catch
            {
                // 忽略日志写入失败
            }
        }

        #endregion

        #region 报告生成

        /// <summary>
        /// 生成性能报告
        /// </summary>
        /// <returns>性能报告</returns>
        public static string GenerateReport()
        {
            var report = new StringBuilder();
            report.AppendLine("=== EasyChartX 配色设计器性能报告 ===");
            report.AppendLine($"生成时间：{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            report.AppendLine($"当前内存使用：{GetMemoryUsage()} MB");
            report.AppendLine($"GC内存使用：{GetGCMemoryUsage()} MB");
            report.AppendLine();

            lock (_lock)
            {
                if (_measurements.Count == 0)
                {
                    report.AppendLine("暂无性能数据");
                    return report.ToString();
                }

                report.AppendLine("操作性能统计：");
                report.AppendLine("操作名称".PadRight(25) + "执行次数".PadRight(10) + "平均耗时".PadRight(12) + "最小耗时".PadRight(12) + "最大耗时".PadRight(12));
                report.AppendLine(new string('-', 80));

                foreach (var kvp in _measurements)
                {
                    var operationName = kvp.Key;
                    var count = GetExecutionCount(operationName);
                    var avg = GetAverageTime(operationName);
                    var min = GetMinTime(operationName);
                    var max = GetMaxTime(operationName);

                    report.AppendLine(
                        operationName.PadRight(25) +
                        count.ToString().PadRight(10) +
                        $"{avg:F1}ms".PadRight(12) +
                        $"{min}ms".PadRight(12) +
                        $"{max}ms".PadRight(12)
                    );
                }
            }

            return report.ToString();
        }

        /// <summary>
        /// 保存性能报告到文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否成功</returns>
        public static bool SaveReport(string filePath = null)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    filePath = Path.Combine(Application.StartupPath, "Logs", $"PerformanceReport_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
                }

                var report = GenerateReport();
                File.WriteAllText(filePath, report, Encoding.UTF8);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"保存性能报告失败：{ex.Message}");
                return false;
            }
        }

        #endregion

        #region 清理操作

        /// <summary>
        /// 清除指定操作的测量数据
        /// </summary>
        /// <param name="operationName">操作名称</param>
        public static void ClearMeasurements(string operationName)
        {
            if (string.IsNullOrEmpty(operationName))
                return;

            lock (_lock)
            {
                if (_measurements.ContainsKey(operationName))
                {
                    _measurements[operationName].Clear();
                }

                if (_timers.ContainsKey(operationName))
                {
                    _timers[operationName].Stop();
                    _timers.Remove(operationName);
                }
            }
        }

        /// <summary>
        /// 清除所有测量数据
        /// </summary>
        public static void ClearAllMeasurements()
        {
            lock (_lock)
            {
                foreach (var timer in _timers.Values)
                {
                    timer.Stop();
                }
                _timers.Clear();
                _measurements.Clear();
            }
        }

        #endregion
    }
}
