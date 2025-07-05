using System;

namespace EasyChartX.ColorDesigner.Models
{
    /// <summary>
    /// 导出格式枚举
    /// </summary>
    public enum ExportFormat
    {
        /// <summary>
        /// C# 代码
        /// </summary>
        CSharp,
        
        /// <summary>
        /// JSON 配置文件
        /// </summary>
        Json,
        
        /// <summary>
        /// CSS 样式文件
        /// </summary>
        Css,
        
        /// <summary>
        /// JavaScript 代码
        /// </summary>
        JavaScript,
        
        /// <summary>
        /// XML 配置文件
        /// </summary>
        Xml
    }

    /// <summary>
    /// 导出配置模型
    /// </summary>
    public class ExportConfig
    {
        /// <summary>
        /// 导出格式
        /// </summary>
        public ExportFormat Format { get; set; }

        /// <summary>
        /// 导出文件名（不含扩展名）
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 是否包含配置信息
        /// </summary>
        public bool IncludeConfiguration { get; set; }

        /// <summary>
        /// 是否包含使用示例
        /// </summary>
        public bool IncludeExample { get; set; }

        /// <summary>
        /// 是否包含注释
        /// </summary>
        public bool IncludeComments { get; set; }

        /// <summary>
        /// 是否包含时间戳
        /// </summary>
        public bool IncludeTimestamp { get; set; }

        /// <summary>
        /// 是否包含版本信息
        /// </summary>
        public bool IncludeVersionInfo { get; set; }

        /// <summary>
        /// 自定义作者信息
        /// </summary>
        public string AuthorInfo { get; set; }

        /// <summary>
        /// 自定义公司信息
        /// </summary>
        public string CompanyInfo { get; set; }

        /// <summary>
        /// 导出路径
        /// </summary>
        public string ExportPath { get; set; }

        /// <summary>
        /// 是否自动打开导出文件
        /// </summary>
        public bool AutoOpenFile { get; set; }

        /// <summary>
        /// 是否压缩输出（对于JSON等格式）
        /// </summary>
        public bool CompressOutput { get; set; }

        /// <summary>
        /// 编码格式
        /// </summary>
        public string Encoding { get; set; }

        /// <summary>
        /// 构造函数 - 设置默认值
        /// </summary>
        public ExportConfig()
        {
            Format = ExportFormat.CSharp;
            FileName = "easychartx-colors";
            IncludeConfiguration = true;
            IncludeExample = true;
            IncludeComments = true;
            IncludeTimestamp = true;
            IncludeVersionInfo = true;
            AuthorInfo = "EasyChartX 配色设计器";
            CompanyInfo = "简仪科技 (JYTEK)";
            ExportPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            AutoOpenFile = true;
            CompressOutput = false;
            Encoding = "UTF-8";
        }

        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <returns>文件扩展名</returns>
        public string GetFileExtension()
        {
            switch (Format)
            {
                case ExportFormat.CSharp:
                    return ".cs";
                case ExportFormat.Json:
                    return ".json";
                case ExportFormat.Css:
                    return ".css";
                case ExportFormat.JavaScript:
                    return ".js";
                case ExportFormat.Xml:
                    return ".xml";
                default:
                    return ".txt";
            }
        }

        /// <summary>
        /// 获取完整文件名
        /// </summary>
        /// <returns>完整文件名</returns>
        public string GetFullFileName()
        {
            return FileName + GetFileExtension();
        }

        /// <summary>
        /// 获取完整文件路径
        /// </summary>
        /// <returns>完整文件路径</returns>
        public string GetFullFilePath()
        {
            return System.IO.Path.Combine(ExportPath, GetFullFileName());
        }

        /// <summary>
        /// 获取MIME类型
        /// </summary>
        /// <returns>MIME类型</returns>
        public string GetMimeType()
        {
            switch (Format)
            {
                case ExportFormat.CSharp:
                    return "text/x-csharp";
                case ExportFormat.Json:
                    return "application/json";
                case ExportFormat.Css:
                    return "text/css";
                case ExportFormat.JavaScript:
                    return "text/javascript";
                case ExportFormat.Xml:
                    return "application/xml";
                default:
                    return "text/plain";
            }
        }

        /// <summary>
        /// 获取格式描述
        /// </summary>
        /// <returns>格式描述</returns>
        public string GetFormatDescription()
        {
            switch (Format)
            {
                case ExportFormat.CSharp:
                    return "C# 代码文件 - 可直接集成到 EasyChartX 项目";
                case ExportFormat.Json:
                    return "JSON 配置文件 - 通用配置格式";
                case ExportFormat.Css:
                    return "CSS 样式文件 - Web 前端样式";
                case ExportFormat.JavaScript:
                    return "JavaScript 代码 - Web 前端脚本";
                case ExportFormat.Xml:
                    return "XML 配置文件 - 结构化配置格式";
                default:
                    return "未知格式";
            }
        }

        /// <summary>
        /// 验证配置有效性
        /// </summary>
        /// <returns>是否有效</returns>
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(FileName) &&
                   !string.IsNullOrWhiteSpace(ExportPath) &&
                   System.IO.Directory.Exists(ExportPath);
        }

        /// <summary>
        /// 克隆配置
        /// </summary>
        /// <returns>新的配置实例</returns>
        public ExportConfig Clone()
        {
            return new ExportConfig
            {
                Format = this.Format,
                FileName = this.FileName,
                IncludeConfiguration = this.IncludeConfiguration,
                IncludeExample = this.IncludeExample,
                IncludeComments = this.IncludeComments,
                IncludeTimestamp = this.IncludeTimestamp,
                IncludeVersionInfo = this.IncludeVersionInfo,
                AuthorInfo = this.AuthorInfo,
                CompanyInfo = this.CompanyInfo,
                ExportPath = this.ExportPath,
                AutoOpenFile = this.AutoOpenFile,
                CompressOutput = this.CompressOutput,
                Encoding = this.Encoding
            };
        }
    }
}
