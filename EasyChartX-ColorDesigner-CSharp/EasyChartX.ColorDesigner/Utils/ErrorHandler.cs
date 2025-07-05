using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace EasyChartX.ColorDesigner.Utils
{
    /// <summary>
    /// 错误处理工具类
    /// </summary>
    public static class ErrorHandler
    {
        #region 私有字段

        private static string _logFilePath;
        private static bool _isEnabled = true;
        private static bool _showErrorDialog = true;
        private static readonly object _lock = new object();

        #endregion

        #region 初始化

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static ErrorHandler()
        {
            _logFilePath = Path.Combine(Application.StartupPath, "Logs", "Error.log");
            EnsureLogDirectory();
            
            // 注册全局异常处理
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
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
                Debug.WriteLine($"创建错误日志目录失败：{ex.Message}");
            }
        }

        #endregion

        #region 公共属性

        /// <summary>
        /// 是否启用错误处理
        /// </summary>
        public static bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; }
        }

        /// <summary>
        /// 是否显示错误对话框
        /// </summary>
        public static bool ShowErrorDialogs
        {
            get { return _showErrorDialog; }
            set { _showErrorDialog = value; }
        }

        /// <summary>
        /// 错误日志文件路径
        /// </summary>
        public static string LogFilePath
        {
            get { return _logFilePath; }
            set { _logFilePath = value; EnsureLogDirectory(); }
        }

        #endregion

        #region 全局异常处理

        /// <summary>
        /// 应用程序线程异常处理
        /// </summary>
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception, "应用程序线程异常");
        }

        /// <summary>
        /// 应用程序域未处理异常
        /// </summary>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                HandleException(ex, "应用程序域未处理异常", e.IsTerminating);
            }
        }

        #endregion

        #region 异常处理方法

        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="exception">异常对象</param>
        /// <param name="context">异常上下文</param>
        /// <param name="isTerminating">是否导致程序终止</param>
        public static void HandleException(Exception exception, string context = null, bool isTerminating = false)
        {
            if (!_isEnabled || exception == null)
                return;

            try
            {
                // 记录到日志
                LogException(exception, context, isTerminating);

                // 显示错误对话框
                if (_showErrorDialog && !isTerminating)
                {
                    ShowErrorDialog(exception, context);
                }

                // 如果是致命错误，保存性能报告
                if (isTerminating)
                {
                    try
                    {
                        PerformanceMonitor.SaveReport();
                    }
                    catch
                    {
                        // 忽略保存报告时的错误
                    }
                }
            }
            catch (Exception ex)
            {
                // 防止错误处理本身出错
                Debug.WriteLine($"错误处理失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 处理并记录错误
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="context">错误上下文</param>
        public static void LogError(string message, string context = null)
        {
            if (!_isEnabled || string.IsNullOrEmpty(message))
                return;

            try
            {
                var logEntry = FormatLogEntry(message, context, null, false);
                WriteToLog(logEntry);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"记录错误失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 处理并记录警告
        /// </summary>
        /// <param name="message">警告消息</param>
        /// <param name="context">警告上下文</param>
        public static void LogWarning(string message, string context = null)
        {
            if (!_isEnabled || string.IsNullOrEmpty(message))
                return;

            try
            {
                var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] WARN: {context ?? "Unknown"} - {message}";
                WriteToLog(logEntry);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"记录警告失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 处理并记录信息
        /// </summary>
        /// <param name="message">信息消息</param>
        /// <param name="context">信息上下文</param>
        public static void LogInfo(string message, string context = null)
        {
            if (!_isEnabled || string.IsNullOrEmpty(message))
                return;

            try
            {
                var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] INFO: {context ?? "Unknown"} - {message}";
                WriteToLog(logEntry);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"记录信息失败：{ex.Message}");
            }
        }

        #endregion

        #region 安全执行方法

        /// <summary>
        /// 安全执行操作
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <param name="context">操作上下文</param>
        /// <param name="showError">是否显示错误</param>
        /// <returns>是否执行成功</returns>
        public static bool SafeExecute(Action action, string context = null, bool showError = true)
        {
            if (action == null)
                return false;

            try
            {
                action();
                return true;
            }
            catch (Exception ex)
            {
                if (showError)
                {
                    HandleException(ex, context ?? "SafeExecute");
                }
                else
                {
                    LogException(ex, context ?? "SafeExecute", false);
                }
                return false;
            }
        }

        /// <summary>
        /// 安全执行函数
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="func">要执行的函数</param>
        /// <param name="defaultValue">默认返回值</param>
        /// <param name="context">操作上下文</param>
        /// <param name="showError">是否显示错误</param>
        /// <returns>函数返回值或默认值</returns>
        public static T SafeExecute<T>(Func<T> func, T defaultValue = default(T), string context = null, bool showError = true)
        {
            if (func == null)
                return defaultValue;

            try
            {
                return func();
            }
            catch (Exception ex)
            {
                if (showError)
                {
                    HandleException(ex, context ?? "SafeExecute");
                }
                else
                {
                    LogException(ex, context ?? "SafeExecute", false);
                }
                return defaultValue;
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 记录异常到日志
        /// </summary>
        /// <param name="exception">异常对象</param>
        /// <param name="context">异常上下文</param>
        /// <param name="isTerminating">是否导致程序终止</param>
        private static void LogException(Exception exception, string context, bool isTerminating)
        {
            try
            {
                var logEntry = FormatLogEntry(exception.Message, context, exception, isTerminating);
                WriteToLog(logEntry);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"记录异常日志失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 格式化日志条目
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="context">上下文</param>
        /// <param name="exception">异常对象</param>
        /// <param name="isTerminating">是否导致程序终止</param>
        /// <returns>格式化的日志条目</returns>
        private static string FormatLogEntry(string message, string context, Exception exception, bool isTerminating)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] ERROR: {context ?? "Unknown"}");
            sb.AppendLine($"Message: {message}");
            
            if (isTerminating)
            {
                sb.AppendLine("*** FATAL ERROR - APPLICATION TERMINATING ***");
            }

            if (exception != null)
            {
                sb.AppendLine($"Exception Type: {exception.GetType().FullName}");
                sb.AppendLine($"Stack Trace: {exception.StackTrace}");
                
                if (exception.InnerException != null)
                {
                    sb.AppendLine($"Inner Exception: {exception.InnerException.Message}");
                    sb.AppendLine($"Inner Stack Trace: {exception.InnerException.StackTrace}");
                }
            }

            // 添加系统信息
            sb.AppendLine($"Memory Usage: {PerformanceMonitor.GetMemoryUsage()} MB");
            sb.AppendLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            sb.AppendLine(new string('-', 80));

            return sb.ToString();
        }

        /// <summary>
        /// 写入日志文件
        /// </summary>
        /// <param name="logEntry">日志条目</param>
        private static void WriteToLog(string logEntry)
        {
            lock (_lock)
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
        }

        /// <summary>
        /// 显示错误对话框
        /// </summary>
        /// <param name="exception">异常对象</param>
        /// <param name="context">异常上下文</param>
        private static void ShowErrorDialog(Exception exception, string context)
        {
            try
            {
                var message = $"发生错误：{exception.Message}";
                if (!string.IsNullOrEmpty(context))
                {
                    message = $"在 {context} 中发生错误：{exception.Message}";
                }

                var result = MessageBox.Show(
                    $"{message}\n\n是否查看详细信息？",
                    "错误",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Error
                );

                if (result == DialogResult.Yes)
                {
                    ShowDetailedErrorDialog(exception, context);
                }
            }
            catch
            {
                // 防止显示错误对话框时出错
            }
        }

        /// <summary>
        /// 显示详细错误对话框
        /// </summary>
        /// <param name="exception">异常对象</param>
        /// <param name="context">异常上下文</param>
        private static void ShowDetailedErrorDialog(Exception exception, string context)
        {
            try
            {
                var detailForm = new Form
                {
                    Text = "错误详情",
                    Size = new System.Drawing.Size(600, 400),
                    StartPosition = FormStartPosition.CenterParent,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                var textBox = new TextBox
                {
                    Multiline = true,
                    ScrollBars = ScrollBars.Both,
                    ReadOnly = true,
                    Dock = DockStyle.Fill,
                    Font = new System.Drawing.Font("Consolas", 9),
                    Text = FormatLogEntry(exception.Message, context, exception, false)
                };

                var panel = new Panel
                {
                    Dock = DockStyle.Bottom,
                    Height = 40
                };

                var btnClose = new Button
                {
                    Text = "关闭",
                    DialogResult = DialogResult.OK,
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                    Size = new System.Drawing.Size(75, 23)
                };
                btnClose.Location = new System.Drawing.Point(panel.Width - btnClose.Width - 10, 8);

                var btnCopy = new Button
                {
                    Text = "复制",
                    Size = new System.Drawing.Size(75, 23),
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Right
                };
                btnCopy.Location = new System.Drawing.Point(btnClose.Left - btnCopy.Width - 10, 8);
                btnCopy.Click += (s, e) =>
                {
                    try
                    {
                        Clipboard.SetText(textBox.Text);
                        MessageBox.Show("错误信息已复制到剪贴板", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        // 忽略复制失败
                    }
                };

                panel.Controls.Add(btnClose);
                panel.Controls.Add(btnCopy);
                detailForm.Controls.Add(textBox);
                detailForm.Controls.Add(panel);

                detailForm.ShowDialog();
            }
            catch
            {
                // 防止显示详细错误对话框时出错
            }
        }

        #endregion

        #region 日志管理

        /// <summary>
        /// 清理旧日志文件
        /// </summary>
        /// <param name="daysToKeep">保留天数</param>
        public static void CleanupOldLogs(int daysToKeep = 30)
        {
            try
            {
                var logDir = Path.GetDirectoryName(_logFilePath);
                if (!Directory.Exists(logDir))
                    return;

                var cutoffDate = DateTime.Now.AddDays(-daysToKeep);
                var logFiles = Directory.GetFiles(logDir, "*.log");

                foreach (var logFile in logFiles)
                {
                    var fileInfo = new FileInfo(logFile);
                    if (fileInfo.LastWriteTime < cutoffDate)
                    {
                        try
                        {
                            File.Delete(logFile);
                        }
                        catch
                        {
                            // 忽略删除失败
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"清理旧日志失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 获取日志文件大小
        /// </summary>
        /// <returns>日志文件大小（字节）</returns>
        public static long GetLogFileSize()
        {
            try
            {
                if (File.Exists(_logFilePath))
                {
                    var fileInfo = new FileInfo(_logFilePath);
                    return fileInfo.Length;
                }
            }
            catch
            {
                // 忽略获取文件大小失败
            }

            return 0;
        }

        /// <summary>
        /// 压缩日志文件
        /// </summary>
        /// <returns>是否成功</returns>
        public static bool ArchiveLogFile()
        {
            try
            {
                if (!File.Exists(_logFilePath))
                    return true;

                var archivePath = _logFilePath.Replace(".log", $"_{DateTime.Now:yyyyMMdd_HHmmss}.log");
                File.Move(_logFilePath, archivePath);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"压缩日志文件失败：{ex.Message}");
                return false;
            }
        }

        #endregion
    }
}
