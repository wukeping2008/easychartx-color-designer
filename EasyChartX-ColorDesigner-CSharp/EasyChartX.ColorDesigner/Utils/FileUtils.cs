using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EasyChartX.ColorDesigner.Utils
{
    /// <summary>
    /// 文件工具类
    /// </summary>
    public static class FileUtils
    {
        #region 文件操作

        /// <summary>
        /// 安全读取文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="encoding">编码格式，默认为UTF8</param>
        /// <returns>文件内容，失败返回null</returns>
        public static string ReadFileContent(string filePath, Encoding encoding = null)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                    return null;

                encoding = encoding ?? Encoding.UTF8;
                return File.ReadAllText(filePath, encoding);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"读取文件失败：{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 安全写入文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">文件内容</param>
        /// <param name="encoding">编码格式，默认为UTF8</param>
        /// <returns>是否成功</returns>
        public static bool WriteFileContent(string filePath, string content, Encoding encoding = null)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                    return false;

                // 确保目录存在
                var directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                encoding = encoding ?? Encoding.UTF8;
                File.WriteAllText(filePath, content ?? string.Empty, encoding);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"写入文件失败：{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 安全复制文件
        /// </summary>
        /// <param name="sourceFile">源文件路径</param>
        /// <param name="destFile">目标文件路径</param>
        /// <param name="overwrite">是否覆盖已存在的文件</param>
        /// <returns>是否成功</returns>
        public static bool CopyFile(string sourceFile, string destFile, bool overwrite = true)
        {
            try
            {
                if (string.IsNullOrEmpty(sourceFile) || string.IsNullOrEmpty(destFile))
                    return false;

                if (!File.Exists(sourceFile))
                    return false;

                // 确保目标目录存在
                var directory = Path.GetDirectoryName(destFile);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                File.Copy(sourceFile, destFile, overwrite);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"复制文件失败：{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 安全删除文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否成功</returns>
        public static bool DeleteFile(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                    return true; // 文件不存在视为删除成功

                File.Delete(filePath);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"删除文件失败：{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 安全移动文件
        /// </summary>
        /// <param name="sourceFile">源文件路径</param>
        /// <param name="destFile">目标文件路径</param>
        /// <returns>是否成功</returns>
        public static bool MoveFile(string sourceFile, string destFile)
        {
            try
            {
                if (string.IsNullOrEmpty(sourceFile) || string.IsNullOrEmpty(destFile))
                    return false;

                if (!File.Exists(sourceFile))
                    return false;

                // 确保目标目录存在
                var directory = Path.GetDirectoryName(destFile);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                File.Move(sourceFile, destFile);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"移动文件失败：{ex.Message}");
                return false;
            }
        }

        #endregion

        #region 目录操作

        /// <summary>
        /// 确保目录存在
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <returns>是否成功</returns>
        public static bool EnsureDirectoryExists(string directoryPath)
        {
            try
            {
                if (string.IsNullOrEmpty(directoryPath))
                    return false;

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"创建目录失败：{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 安全删除目录
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <param name="recursive">是否递归删除</param>
        /// <returns>是否成功</returns>
        public static bool DeleteDirectory(string directoryPath, bool recursive = true)
        {
            try
            {
                if (string.IsNullOrEmpty(directoryPath) || !Directory.Exists(directoryPath))
                    return true; // 目录不存在视为删除成功

                Directory.Delete(directoryPath, recursive);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"删除目录失败：{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 复制目录
        /// </summary>
        /// <param name="sourceDir">源目录</param>
        /// <param name="destDir">目标目录</param>
        /// <param name="recursive">是否递归复制</param>
        /// <returns>是否成功</returns>
        public static bool CopyDirectory(string sourceDir, string destDir, bool recursive = true)
        {
            try
            {
                if (string.IsNullOrEmpty(sourceDir) || string.IsNullOrEmpty(destDir))
                    return false;

                if (!Directory.Exists(sourceDir))
                    return false;

                // 创建目标目录
                if (!EnsureDirectoryExists(destDir))
                    return false;

                // 复制文件
                var files = Directory.GetFiles(sourceDir);
                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);
                    var destFile = Path.Combine(destDir, fileName);
                    if (!CopyFile(file, destFile))
                        return false;
                }

                // 递归复制子目录
                if (recursive)
                {
                    var dirs = Directory.GetDirectories(sourceDir);
                    foreach (var dir in dirs)
                    {
                        var dirName = Path.GetFileName(dir);
                        var destSubDir = Path.Combine(destDir, dirName);
                        if (!CopyDirectory(dir, destSubDir, true))
                            return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"复制目录失败：{ex.Message}");
                return false;
            }
        }

        #endregion

        #region 路径操作

        /// <summary>
        /// 获取应用程序目录
        /// </summary>
        /// <returns>应用程序目录</returns>
        public static string GetApplicationDirectory()
        {
            return Application.StartupPath;
        }

        /// <summary>
        /// 获取资源目录
        /// </summary>
        /// <returns>资源目录</returns>
        public static string GetResourcesDirectory()
        {
            return Path.Combine(GetApplicationDirectory(), "Resources");
        }

        /// <summary>
        /// 获取模板目录
        /// </summary>
        /// <returns>模板目录</returns>
        public static string GetTemplatesDirectory()
        {
            return Path.Combine(GetResourcesDirectory(), "Templates");
        }

        /// <summary>
        /// 获取配置文件路径
        /// </summary>
        /// <param name="fileName">配置文件名</param>
        /// <returns>配置文件完整路径</returns>
        public static string GetConfigFilePath(string fileName)
        {
            return Path.Combine(GetApplicationDirectory(), fileName);
        }

        /// <summary>
        /// 获取临时目录
        /// </summary>
        /// <returns>临时目录</returns>
        public static string GetTempDirectory()
        {
            return Path.GetTempPath();
        }

        /// <summary>
        /// 获取用户文档目录
        /// </summary>
        /// <returns>用户文档目录</returns>
        public static string GetDocumentsDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        /// <summary>
        /// 获取桌面目录
        /// </summary>
        /// <returns>桌面目录</returns>
        public static string GetDesktopDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        /// <summary>
        /// 组合路径
        /// </summary>
        /// <param name="paths">路径组件</param>
        /// <returns>组合后的路径</returns>
        public static string CombinePath(params string[] paths)
        {
            if (paths == null || paths.Length == 0)
                return string.Empty;

            string result = paths[0] ?? string.Empty;
            for (int i = 1; i < paths.Length; i++)
            {
                if (!string.IsNullOrEmpty(paths[i]))
                {
                    result = Path.Combine(result, paths[i]);
                }
            }

            return result;
        }

        /// <summary>
        /// 获取相对路径
        /// </summary>
        /// <param name="fromPath">起始路径</param>
        /// <param name="toPath">目标路径</param>
        /// <returns>相对路径</returns>
        public static string GetRelativePath(string fromPath, string toPath)
        {
            try
            {
                if (string.IsNullOrEmpty(fromPath) || string.IsNullOrEmpty(toPath))
                    return toPath;

                var fromUri = new Uri(Path.GetFullPath(fromPath));
                var toUri = new Uri(Path.GetFullPath(toPath));

                if (fromUri.Scheme != toUri.Scheme)
                    return toPath;

                var relativeUri = fromUri.MakeRelativeUri(toUri);
                var relativePath = Uri.UnescapeDataString(relativeUri.ToString());

                if (toUri.Scheme.Equals("file", StringComparison.InvariantCultureIgnoreCase))
                {
                    relativePath = relativePath.Replace('/', Path.DirectorySeparatorChar);
                }

                return relativePath;
            }
            catch
            {
                return toPath;
            }
        }

        #endregion

        #region 文件信息

        /// <summary>
        /// 获取文件大小（字节）
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件大小，失败返回-1</returns>
        public static long GetFileSize(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                    return -1;

                var fileInfo = new FileInfo(filePath);
                return fileInfo.Length;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 获取文件大小的友好显示
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件大小的友好显示</returns>
        public static string GetFileSizeString(string filePath)
        {
            long size = GetFileSize(filePath);
            return GetFileSizeString(size);
        }

        /// <summary>
        /// 获取文件大小的友好显示
        /// </summary>
        /// <param name="size">文件大小（字节）</param>
        /// <returns>文件大小的友好显示</returns>
        public static string GetFileSizeString(long size)
        {
            if (size < 0)
                return "未知";

            string[] units = { "B", "KB", "MB", "GB", "TB" };
            double fileSize = size;
            int unitIndex = 0;

            while (fileSize >= 1024 && unitIndex < units.Length - 1)
            {
                fileSize /= 1024;
                unitIndex++;
            }

            return $"{fileSize:F2} {units[unitIndex]}";
        }

        /// <summary>
        /// 获取文件修改时间
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件修改时间，失败返回DateTime.MinValue</returns>
        public static DateTime GetFileModifiedTime(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                    return DateTime.MinValue;

                return File.GetLastWriteTime(filePath);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// 检查文件是否为只读
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否为只读</returns>
        public static bool IsFileReadOnly(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                    return false;

                var attributes = File.GetAttributes(filePath);
                return (attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 文件扩展名

        /// <summary>
        /// 获取安全的文件名（移除非法字符）
        /// </summary>
        /// <param name="fileName">原始文件名</param>
        /// <param name="replacement">替换字符</param>
        /// <returns>安全的文件名</returns>
        public static string GetSafeFileName(string fileName, char replacement = '_')
        {
            if (string.IsNullOrEmpty(fileName))
                return "untitled";

            var invalidChars = Path.GetInvalidFileNameChars();
            var result = fileName;

            foreach (var invalidChar in invalidChars)
            {
                result = result.Replace(invalidChar, replacement);
            }

            // 移除连续的替换字符
            while (result.Contains(new string(replacement, 2)))
            {
                result = result.Replace(new string(replacement, 2), replacement.ToString());
            }

            // 移除开头和结尾的替换字符
            result = result.Trim(replacement);

            return string.IsNullOrEmpty(result) ? "untitled" : result;
        }

        /// <summary>
        /// 获取唯一的文件名（如果文件已存在，则添加数字后缀）
        /// </summary>
        /// <param name="filePath">原始文件路径</param>
        /// <returns>唯一的文件路径</returns>
        public static string GetUniqueFileName(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                return filePath;

            var directory = Path.GetDirectoryName(filePath);
            var fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
            var extension = Path.GetExtension(filePath);

            int counter = 1;
            string newFilePath;

            do
            {
                var newFileName = $"{fileNameWithoutExt}({counter}){extension}";
                newFilePath = Path.Combine(directory ?? string.Empty, newFileName);
                counter++;
            }
            while (File.Exists(newFilePath));

            return newFilePath;
        }

        /// <summary>
        /// 检查文件扩展名是否匹配
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="extensions">扩展名数组（如 ".txt", ".json"）</param>
        /// <returns>是否匹配</returns>
        public static bool HasExtension(string filePath, params string[] extensions)
        {
            if (string.IsNullOrEmpty(filePath) || extensions == null || extensions.Length == 0)
                return false;

            var fileExtension = Path.GetExtension(filePath);
            
            foreach (var extension in extensions)
            {
                if (string.Equals(fileExtension, extension, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        #endregion

        #region 文件对话框

        /// <summary>
        /// 显示打开文件对话框
        /// </summary>
        /// <param name="title">对话框标题</param>
        /// <param name="filter">文件过滤器</param>
        /// <param name="initialDirectory">初始目录</param>
        /// <returns>选择的文件路径，取消返回null</returns>
        public static string ShowOpenFileDialog(string title = "打开文件", string filter = "所有文件 (*.*)|*.*", string initialDirectory = null)
        {
            try
            {
                using (var dialog = new OpenFileDialog())
                {
                    dialog.Title = title;
                    dialog.Filter = filter;
                    dialog.InitialDirectory = initialDirectory ?? GetDocumentsDirectory();
                    dialog.RestoreDirectory = true;

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        return dialog.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"显示打开文件对话框失败：{ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// 显示保存文件对话框
        /// </summary>
        /// <param name="title">对话框标题</param>
        /// <param name="filter">文件过滤器</param>
        /// <param name="defaultFileName">默认文件名</param>
        /// <param name="initialDirectory">初始目录</param>
        /// <returns>选择的文件路径，取消返回null</returns>
        public static string ShowSaveFileDialog(string title = "保存文件", string filter = "所有文件 (*.*)|*.*", string defaultFileName = null, string initialDirectory = null)
        {
            try
            {
                using (var dialog = new SaveFileDialog())
                {
                    dialog.Title = title;
                    dialog.Filter = filter;
                    dialog.FileName = defaultFileName ?? string.Empty;
                    dialog.InitialDirectory = initialDirectory ?? GetDocumentsDirectory();
                    dialog.RestoreDirectory = true;

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        return dialog.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"显示保存文件对话框失败：{ex.Message}");
            }

            return null;
        }

        #endregion

        #region 备份和恢复

        /// <summary>
        /// 创建文件备份
        /// </summary>
        /// <param name="filePath">原文件路径</param>
        /// <param name="backupSuffix">备份后缀</param>
        /// <returns>备份文件路径，失败返回null</returns>
        public static string CreateBackup(string filePath, string backupSuffix = ".bak")
        {
            try
            {
                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                    return null;

                var backupPath = filePath + backupSuffix;
                backupPath = GetUniqueFileName(backupPath);

                if (CopyFile(filePath, backupPath))
                {
                    return backupPath;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"创建备份失败：{ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// 从备份恢复文件
        /// </summary>
        /// <param name="backupPath">备份文件路径</param>
        /// <param name="originalPath">原文件路径</param>
        /// <returns>是否成功</returns>
        public static bool RestoreFromBackup(string backupPath, string originalPath)
        {
            try
            {
                if (string.IsNullOrEmpty(backupPath) || string.IsNullOrEmpty(originalPath))
                    return false;

                if (!File.Exists(backupPath))
                    return false;

                return CopyFile(backupPath, originalPath, true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"从备份恢复失败：{ex.Message}");
                return false;
            }
        }

        #endregion
    }
}
