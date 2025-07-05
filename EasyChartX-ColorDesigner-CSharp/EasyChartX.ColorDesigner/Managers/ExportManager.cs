using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using EasyChartX.ColorDesigner.Models;

namespace EasyChartX.ColorDesigner.Managers
{
    /// <summary>
    /// 导出管理器
    /// </summary>
    public class ExportManager
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ExportManager()
        {
            // 移除模板文件依赖，直接使用代码生成
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 导出配色方案
        /// </summary>
        /// <param name="scheme">配色方案</param>
        /// <param name="config">波形配置</param>
        /// <param name="exportConfig">导出配置</param>
        /// <returns>导出是否成功</returns>
        public bool ExportColorScheme(ColorScheme scheme, WaveformConfig config, ExportConfig exportConfig)
        {
            try
            {
                if (scheme == null || exportConfig == null)
                {
                    MessageBox.Show("配色方案或导出配置不能为空", "错误", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                string content = string.Empty;
                string defaultFileName = $"{scheme.Name}_{DateTime.Now:yyyyMMdd_HHmmss}";

                switch (exportConfig.Format)
                {
                    case ExportFormat.CSharp:
                        content = GenerateCSharpCode(scheme, config, exportConfig);
                        defaultFileName += ".cs";
                        break;

                    case ExportFormat.Json:
                        content = GenerateJsonCode(scheme, config, exportConfig);
                        defaultFileName += ".json";
                        break;

                    case ExportFormat.Css:
                        content = GenerateCssCode(scheme, config, exportConfig);
                        defaultFileName += ".css";
                        break;

                    case ExportFormat.JavaScript:
                        content = GenerateJavaScriptCode(scheme, config, exportConfig);
                        defaultFileName += ".js";
                        break;

                    default:
                        MessageBox.Show("不支持的导出格式", "错误", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                }

                return SaveToFile(content, defaultFileName, exportConfig);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"导出失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

        #region 私有方法


        /// <summary>
        /// 生成C#代码
        /// </summary>
        /// <param name="scheme">配色方案</param>
        /// <param name="config">波形配置</param>
        /// <param name="exportConfig">导出配置</param>
        /// <returns>C#代码</returns>
        private string GenerateCSharpCode(ColorScheme scheme, WaveformConfig config, ExportConfig exportConfig)
        {
            var sb = new StringBuilder();

            // 文件头注释
            if (exportConfig.IncludeComments)
            {
                sb.AppendLine("/*");
                sb.AppendLine($" * EasyChartX 配色方案：{scheme.Name}");
                sb.AppendLine($" * 描述：{scheme.Description}");
                sb.AppendLine($" * 分类：{scheme.Category}");
                sb.AppendLine($" * 生成时间：{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                sb.AppendLine($" * 通道数量：{config.ChannelCount}");
                sb.AppendLine(" * 由 EasyChartX 配色方案设计器生成");
                sb.AppendLine(" */");
                sb.AppendLine();
            }

            // 命名空间和类
            sb.AppendLine("using System.Drawing;");
            sb.AppendLine("using SeeSharpTools.JY.GUI;");
            sb.AppendLine();
            sb.AppendLine("namespace EasyChartX.ColorSchemes");
            sb.AppendLine("{");
            sb.AppendLine($"    /// <summary>");
            sb.AppendLine($"    /// {scheme.Name} 配色方案");
            sb.AppendLine($"    /// </summary>");
            sb.AppendLine($"    public static class {GetValidClassName(scheme.Name)}ColorScheme");
            sb.AppendLine("    {");

            // 颜色数组
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 配色方案颜色数组");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        public static readonly Color[] Colors = new Color[]");
            sb.AppendLine("        {");

            var colors = scheme.GetColors(config.ChannelCount);
            for (int i = 0; i < colors.Length; i++)
            {
                var color = colors[i];
                var colorName = $"Color.FromArgb({color.R}, {color.G}, {color.B})";
                
                if (exportConfig.IncludeComments)
                {
                    sb.AppendLine($"            {colorName}, // Channel {i + 1}: #{color.R:X2}{color.G:X2}{color.B:X2}");
                }
                else
                {
                    sb.AppendLine($"            {colorName}{(i < colors.Length - 1 ? "," : "")}");
                }
            }

            sb.AppendLine("        };");
            sb.AppendLine();

            // 应用方法
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 应用配色方案到 EasyChartX 控件");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <param name=\"chart\">EasyChartX 控件</param>");
            sb.AppendLine("        public static void ApplyToChart(EasyChartX chart)");
            sb.AppendLine("        {");
            sb.AppendLine("            if (chart == null) return;");
            sb.AppendLine();
            sb.AppendLine("            // 确保有足够的系列");
            sb.AppendLine("            chart.Series.AdaptSeriesCount(Colors.Length);");
            sb.AppendLine();
            sb.AppendLine("            // 应用颜色");
            sb.AppendLine("            for (int i = 0; i < Colors.Length && i < chart.Series.Count; i++)");
            sb.AppendLine("            {");
            sb.AppendLine("                chart.Series[i].Color = Colors[i];");
            
            if (config.LineWidth > 0)
            {
                var lineWidth = GetEasyChartXLineWidth(config.LineWidth);
                sb.AppendLine($"                chart.Series[i].Width = EasyChartXSeries.LineWidth.{lineWidth};");
            }
            
            sb.AppendLine("            }");

            // 背景设置
            if (config.BackgroundType != BackgroundType.White)
            {
                sb.AppendLine();
                sb.AppendLine("            // 设置背景色");
                var bgColor = config.GetBackgroundColor();
                sb.AppendLine($"            chart.BackColor = Color.FromArgb({bgColor.R}, {bgColor.G}, {bgColor.B});");
                
                if (config.BackgroundType == BackgroundType.Black || config.BackgroundType == BackgroundType.Dark)
                {
                    sb.AppendLine("            chart.AxisX.Color = Color.White;");
                    sb.AppendLine("            chart.AxisY.Color = Color.White;");
                    sb.AppendLine("            chart.LegendForeColor = Color.White;");
                }
            }

            // 网格设置
            if (config.ShowGrid)
            {
                sb.AppendLine();
                sb.AppendLine("            // 设置网格");
                sb.AppendLine("            chart.AxisX.MajorGridEnabled = true;");
                sb.AppendLine("            chart.AxisY.MajorGridEnabled = true;");
                
                var gridColor = config.GetSmartGridColor();
                sb.AppendLine($"            chart.AxisX.MajorGridColor = Color.FromArgb({(int)(config.GridOpacity * 255)}, {gridColor.R}, {gridColor.G}, {gridColor.B});");
                sb.AppendLine($"            chart.AxisY.MajorGridColor = Color.FromArgb({(int)(config.GridOpacity * 255)}, {gridColor.R}, {gridColor.G}, {gridColor.B});");
            }

            sb.AppendLine("        }");

            // 获取颜色方法
            sb.AppendLine();
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 获取指定索引的颜色（支持循环）");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <param name=\"index\">颜色索引</param>");
            sb.AppendLine("        /// <returns>颜色值</returns>");
            sb.AppendLine("        public static Color GetColor(int index)");
            sb.AppendLine("        {");
            sb.AppendLine("            if (Colors.Length == 0) return Color.Black;");
            sb.AppendLine("            return Colors[index % Colors.Length];");
            sb.AppendLine("        }");

            sb.AppendLine("    }");
            sb.AppendLine("}");

            // 使用示例
            if (exportConfig.IncludeExample)
            {
                sb.AppendLine();
                sb.AppendLine("/*");
                sb.AppendLine(" * 使用示例：");
                sb.AppendLine(" * ");
                sb.AppendLine($" * // 应用配色方案到图表");
                sb.AppendLine($" * {GetValidClassName(scheme.Name)}ColorScheme.ApplyToChart(easyChartX1);");
                sb.AppendLine(" * ");
                sb.AppendLine(" * // 获取特定颜色");
                sb.AppendLine($" * Color color = {GetValidClassName(scheme.Name)}ColorScheme.GetColor(0);");
                sb.AppendLine(" * ");
                sb.AppendLine(" * // 手动设置系列颜色");
                sb.AppendLine(" * for (int i = 0; i < easyChartX1.Series.Count; i++)");
                sb.AppendLine(" * {");
                sb.AppendLine($" *     easyChartX1.Series[i].Color = {GetValidClassName(scheme.Name)}ColorScheme.GetColor(i);");
                sb.AppendLine(" * }");
                sb.AppendLine(" */");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 生成JSON代码
        /// </summary>
        /// <param name="scheme">配色方案</param>
        /// <param name="config">波形配置</param>
        /// <param name="exportConfig">导出配置</param>
        /// <returns>JSON代码</returns>
        private string GenerateJsonCode(ColorScheme scheme, WaveformConfig config, ExportConfig exportConfig)
        {
            var sb = new StringBuilder();

            // 文件头注释
            if (exportConfig.IncludeComments)
            {
                sb.AppendLine("/*");
                sb.AppendLine($" * EasyChartX 配色方案：{scheme.Name}");
                sb.AppendLine($" * 描述：{scheme.Description}");
                sb.AppendLine($" * 分类：{scheme.Category}");
                sb.AppendLine($" * 生成时间：{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                sb.AppendLine(" * 由 EasyChartX 配色方案设计器生成");
                sb.AppendLine(" */");
                sb.AppendLine();
            }

            // 手动构建JSON
            sb.AppendLine("{");
            sb.AppendLine($"  \"name\": \"{EscapeJsonString(scheme.Name)}\",");
            sb.AppendLine($"  \"description\": \"{EscapeJsonString(scheme.Description)}\",");
            sb.AppendLine($"  \"category\": \"{EscapeJsonString(scheme.Category)}\",");
            sb.AppendLine($"  \"recommended\": {scheme.IsRecommended.ToString().ToLower()},");
            
            // 颜色数组
            sb.AppendLine("  \"colors\": [");
            for (int i = 0; i < scheme.ColorHexValues.Length; i++)
            {
                sb.Append($"    \"{scheme.ColorHexValues[i]}\"");
                if (i < scheme.ColorHexValues.Length - 1)
                    sb.AppendLine(",");
                else
                    sb.AppendLine();
            }
            sb.AppendLine("  ],");
            
            sb.AppendLine($"  \"channelCount\": {config.ChannelCount},");
            
            // 波形配置
            sb.AppendLine("  \"waveformConfig\": {");
            sb.AppendLine($"    \"waveformType\": \"{config.WaveformType}\",");
            sb.AppendLine($"    \"lineWidth\": {config.LineWidth},");
            sb.AppendLine($"    \"backgroundType\": \"{config.BackgroundType}\",");
            
            var bgColor = config.GetBackgroundColor();
            sb.AppendLine($"    \"backgroundColor\": \"#{bgColor.R:X2}{bgColor.G:X2}{bgColor.B:X2}\",");
            sb.AppendLine($"    \"showGrid\": {config.ShowGrid.ToString().ToLower()},");
            sb.AppendLine($"    \"gridOpacity\": {config.GridOpacity},");
            sb.AppendLine($"    \"showLegend\": {config.ShowLegend.ToString().ToLower()},");
            sb.AppendLine($"    \"enableAnimation\": {config.EnableAnimation.ToString().ToLower()}");
            sb.AppendLine("  },");
            
            sb.AppendLine($"  \"generatedAt\": \"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\",");
            sb.AppendLine($"  \"generatedBy\": \"EasyChartX 配色方案设计器\"");
            sb.AppendLine("}");

            return sb.ToString();
        }

        /// <summary>
        /// 转义JSON字符串
        /// </summary>
        private string EscapeJsonString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            return input.Replace("\\", "\\\\")
                       .Replace("\"", "\\\"")
                       .Replace("\n", "\\n")
                       .Replace("\r", "\\r")
                       .Replace("\t", "\\t");
        }

        /// <summary>
        /// 生成CSS代码
        /// </summary>
        /// <param name="scheme">配色方案</param>
        /// <param name="config">波形配置</param>
        /// <param name="exportConfig">导出配置</param>
        /// <returns>CSS代码</returns>
        private string GenerateCssCode(ColorScheme scheme, WaveformConfig config, ExportConfig exportConfig)
        {
            var sb = new StringBuilder();

            // 文件头注释
            if (exportConfig.IncludeComments)
            {
                sb.AppendLine("/*");
                sb.AppendLine($" * EasyChartX 配色方案：{scheme.Name}");
                sb.AppendLine($" * 描述：{scheme.Description}");
                sb.AppendLine($" * 分类：{scheme.Category}");
                sb.AppendLine($" * 生成时间：{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                sb.AppendLine($" * 通道数量：{config.ChannelCount}");
                sb.AppendLine(" * 由 EasyChartX 配色方案设计器生成");
                sb.AppendLine(" */");
                sb.AppendLine();
            }

            var className = GetValidCssClassName(scheme.Name);

            // CSS 变量
            sb.AppendLine($".{className}-colors {{");
            var colors = scheme.GetColors(config.ChannelCount);
            for (int i = 0; i < colors.Length; i++)
            {
                var color = colors[i];
                var hex = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
                sb.AppendLine($"  --color-{i + 1}: {hex};");
            }
            sb.AppendLine("}");
            sb.AppendLine();

            // 单独的颜色类
            for (int i = 0; i < colors.Length; i++)
            {
                var color = colors[i];
                var hex = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
                
                if (exportConfig.IncludeComments)
                {
                    sb.AppendLine($"/* Channel {i + 1} */");
                }
                
                sb.AppendLine($".{className}-color-{i + 1} {{");
                sb.AppendLine($"  color: {hex};");
                sb.AppendLine("}");
                sb.AppendLine();
                
                sb.AppendLine($".{className}-bg-{i + 1} {{");
                sb.AppendLine($"  background-color: {hex};");
                sb.AppendLine("}");
                sb.AppendLine();
                
                sb.AppendLine($".{className}-border-{i + 1} {{");
                sb.AppendLine($"  border-color: {hex};");
                sb.AppendLine("}");
                sb.AppendLine();
            }

            // 背景设置
            var bgColor = config.GetBackgroundColor();
            var bgHex = $"#{bgColor.R:X2}{bgColor.G:X2}{bgColor.B:X2}";
            
            sb.AppendLine($".{className}-background {{");
            sb.AppendLine($"  background-color: {bgHex};");
            sb.AppendLine("}");
            sb.AppendLine();

            // 使用示例
            if (exportConfig.IncludeExample)
            {
                sb.AppendLine("/*");
                sb.AppendLine(" * 使用示例：");
                sb.AppendLine(" * ");
                sb.AppendLine($" * <div class=\"{className}-colors\">");
                sb.AppendLine($" *   <span class=\"{className}-color-1\">Channel 1</span>");
                sb.AppendLine($" *   <span class=\"{className}-color-2\">Channel 2</span>");
                sb.AppendLine(" *   ...");
                sb.AppendLine(" * </div>");
                sb.AppendLine(" * ");
                sb.AppendLine(" * 或使用 CSS 变量：");
                sb.AppendLine(" * .my-element {");
                sb.AppendLine(" *   color: var(--color-1);");
                sb.AppendLine(" * }");
                sb.AppendLine(" */");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 生成JavaScript代码
        /// </summary>
        /// <param name="scheme">配色方案</param>
        /// <param name="config">波形配置</param>
        /// <param name="exportConfig">导出配置</param>
        /// <returns>JavaScript代码</returns>
        private string GenerateJavaScriptCode(ColorScheme scheme, WaveformConfig config, ExportConfig exportConfig)
        {
            var sb = new StringBuilder();

            // 文件头注释
            if (exportConfig.IncludeComments)
            {
                sb.AppendLine("/*");
                sb.AppendLine($" * EasyChartX 配色方案：{scheme.Name}");
                sb.AppendLine($" * 描述：{scheme.Description}");
                sb.AppendLine($" * 分类：{scheme.Category}");
                sb.AppendLine($" * 生成时间：{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                sb.AppendLine($" * 通道数量：{config.ChannelCount}");
                sb.AppendLine(" * 由 EasyChartX 配色方案设计器生成");
                sb.AppendLine(" */");
                sb.AppendLine();
            }

            var objectName = GetValidJsObjectName(scheme.Name);

            // 配色方案对象
            sb.AppendLine($"const {objectName}ColorScheme = {{");
            sb.AppendLine($"  name: '{scheme.Name}',");
            sb.AppendLine($"  description: '{scheme.Description}',");
            sb.AppendLine($"  category: '{scheme.Category}',");
            sb.AppendLine($"  recommended: {scheme.IsRecommended.ToString().ToLower()},");
            sb.AppendLine();

            // 颜色数组
            sb.AppendLine("  colors: [");
            var colors = scheme.GetColors(config.ChannelCount);
            for (int i = 0; i < colors.Length; i++)
            {
                var color = colors[i];
                var hex = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
                
                if (exportConfig.IncludeComments)
                {
                    sb.AppendLine($"    '{hex}', // Channel {i + 1}");
                }
                else
                {
                    sb.AppendLine($"    '{hex}'{(i < colors.Length - 1 ? "," : "")}");
                }
            }
            sb.AppendLine("  ],");
            sb.AppendLine();

            // RGB 颜色数组
            sb.AppendLine("  rgbColors: [");
            for (int i = 0; i < colors.Length; i++)
            {
                var color = colors[i];
                
                if (exportConfig.IncludeComments)
                {
                    sb.AppendLine($"    {{ r: {color.R}, g: {color.G}, b: {color.B} }}, // Channel {i + 1}");
                }
                else
                {
                    sb.AppendLine($"    {{ r: {color.R}, g: {color.G}, b: {color.B} }}{(i < colors.Length - 1 ? "," : "")}");
                }
            }
            sb.AppendLine("  ],");
            sb.AppendLine();

            // 配置信息
            sb.AppendLine("  config: {");
            sb.AppendLine($"    channelCount: {config.ChannelCount},");
            sb.AppendLine($"    waveformType: '{config.WaveformType}',");
            sb.AppendLine($"    lineWidth: {config.LineWidth},");
            sb.AppendLine($"    backgroundType: '{config.BackgroundType}',");
            
            var bgColor = config.GetBackgroundColor();
            sb.AppendLine($"    backgroundColor: '#{bgColor.R:X2}{bgColor.G:X2}{bgColor.B:X2}',");
            sb.AppendLine($"    showGrid: {config.ShowGrid.ToString().ToLower()},");
            sb.AppendLine($"    gridOpacity: {config.GridOpacity},");
            sb.AppendLine($"    showLegend: {config.ShowLegend.ToString().ToLower()},");
            sb.AppendLine($"    enableAnimation: {config.EnableAnimation.ToString().ToLower()}");
            sb.AppendLine("  },");
            sb.AppendLine();

            // 方法
            sb.AppendLine("  // 获取指定索引的颜色（支持循环）");
            sb.AppendLine("  getColor(index) {");
            sb.AppendLine("    if (this.colors.length === 0) return '#000000';");
            sb.AppendLine("    return this.colors[index % this.colors.length];");
            sb.AppendLine("  },");
            sb.AppendLine();

            sb.AppendLine("  // 获取指定索引的RGB颜色（支持循环）");
            sb.AppendLine("  getRgbColor(index) {");
            sb.AppendLine("    if (this.rgbColors.length === 0) return { r: 0, g: 0, b: 0 };");
            sb.AppendLine("    return this.rgbColors[index % this.rgbColors.length];");
            sb.AppendLine("  },");
            sb.AppendLine();

            sb.AppendLine("  // 获取所有颜色");
            sb.AppendLine("  getAllColors() {");
            sb.AppendLine("    return [...this.colors];");
            sb.AppendLine("  },");
            sb.AppendLine();

            sb.AppendLine("  // 应用到 Chart.js");
            sb.AppendLine("  applyToChartJs(chart) {");
            sb.AppendLine("    if (!chart || !chart.data || !chart.data.datasets) return;");
            sb.AppendLine("    ");
            sb.AppendLine("    chart.data.datasets.forEach((dataset, index) => {");
            sb.AppendLine("      const color = this.getColor(index);");
            sb.AppendLine("      dataset.borderColor = color;");
            sb.AppendLine("      dataset.backgroundColor = color + '20'; // 添加透明度");
            sb.AppendLine("    });");
            sb.AppendLine("    ");
            sb.AppendLine("    chart.update();");
            sb.AppendLine("  }");

            sb.AppendLine("};");
            sb.AppendLine();

            // 导出
            sb.AppendLine("// 导出配色方案");
            sb.AppendLine($"if (typeof module !== 'undefined' && module.exports) {{");
            sb.AppendLine($"  module.exports = {objectName}ColorScheme;");
            sb.AppendLine("}");
            sb.AppendLine();

            // 使用示例
            if (exportConfig.IncludeExample)
            {
                sb.AppendLine("/*");
                sb.AppendLine(" * 使用示例：");
                sb.AppendLine(" * ");
                sb.AppendLine(" * // 获取颜色");
                sb.AppendLine($" * const color1 = {objectName}ColorScheme.getColor(0);");
                sb.AppendLine($" * const rgb1 = {objectName}ColorScheme.getRgbColor(0);");
                sb.AppendLine(" * ");
                sb.AppendLine(" * // 应用到 Chart.js");
                sb.AppendLine($" * {objectName}ColorScheme.applyToChartJs(myChart);");
                sb.AppendLine(" * ");
                sb.AppendLine(" * // 获取所有颜色");
                sb.AppendLine($" * const allColors = {objectName}ColorScheme.getAllColors();");
                sb.AppendLine(" * ");
                sb.AppendLine(" * // 在 D3.js 中使用");
                sb.AppendLine($" * const colorScale = d3.scaleOrdinal({objectName}ColorScheme.colors);");
                sb.AppendLine(" */");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 保存到文件
        /// </summary>
        /// <param name="content">文件内容</param>
        /// <param name="defaultFileName">默认文件名</param>
        /// <param name="exportConfig">导出配置</param>
        /// <returns>保存是否成功</returns>
        private bool SaveToFile(string content, string defaultFileName, ExportConfig exportConfig)
        {
            try
            {
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.FileName = defaultFileName;
                    saveDialog.Filter = GetFileFilter(exportConfig.Format);
                    saveDialog.DefaultExt = GetFileExtension(exportConfig.Format);

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(saveDialog.FileName, content, Encoding.UTF8);
                        
                        if (exportConfig.AutoOpenFile)
                        {
                            try
                            {
                                System.Diagnostics.Process.Start(saveDialog.FileName);
                            }
                            catch
                            {
                                // 忽略打开文件失败的错误
                            }
                        }

                        MessageBox.Show($"导出成功！\n文件保存至：{saveDialog.FileName}", "成功", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存文件失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// 获取文件过滤器
        /// </summary>
        /// <param name="format">导出格式</param>
        /// <returns>文件过滤器</returns>
        private string GetFileFilter(ExportFormat format)
        {
            switch (format)
            {
                case ExportFormat.CSharp:
                    return "C# 文件 (*.cs)|*.cs|所有文件 (*.*)|*.*";
                case ExportFormat.Json:
                    return "JSON 文件 (*.json)|*.json|所有文件 (*.*)|*.*";
                case ExportFormat.Css:
                    return "CSS 文件 (*.css)|*.css|所有文件 (*.*)|*.*";
                case ExportFormat.JavaScript:
                    return "JavaScript 文件 (*.js)|*.js|所有文件 (*.*)|*.*";
                default:
                    return "所有文件 (*.*)|*.*";
            }
        }

        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="format">导出格式</param>
        /// <returns>文件扩展名</returns>
        private string GetFileExtension(ExportFormat format)
        {
            switch (format)
            {
                case ExportFormat.CSharp:
                    return ".cs";
                case ExportFormat.Json:
                    return ".json";
                case ExportFormat.Css:
                    return ".css";
                case ExportFormat.JavaScript:
                    return ".js";
                default:
                    return ".txt";
            }
        }

        /// <summary>
        /// 获取有效的类名
        /// </summary>
        /// <param name="name">原始名称</param>
        /// <returns>有效的类名</returns>
        private string GetValidClassName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return "Default";

            var result = name.Replace(" ", "")
                            .Replace("-", "")
                            .Replace("(", "")
                            .Replace(")", "")
                            .Replace("⭐", "Star");

            // 确保以字母开头
            if (!char.IsLetter(result[0]))
                result = "Color" + result;

            return result;
        }

        /// <summary>
        /// 获取有效的CSS类名
        /// </summary>
        /// <param name="name">原始名称</param>
        /// <returns>有效的CSS类名</returns>
        private string GetValidCssClassName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return "default";

            var result = name.ToLower()
                            .Replace(" ", "-")
                            .Replace("(", "")
                            .Replace(")", "")
                            .Replace("⭐", "star");

            // 移除无效字符
            var validChars = "abcdefghijklmnopqrstuvwxyz0123456789-_";
            var filtered = "";
            foreach (char c in result)
            {
                if (validChars.Contains(c.ToString()))
                    filtered += c;
            }

            // 确保以字母开头
            if (string.IsNullOrEmpty(filtered) || !char.IsLetter(filtered[0]))
                filtered = "color-" + filtered;

            return filtered;
        }

        /// <summary>
        /// 获取有效的JavaScript对象名
        /// </summary>
        /// <param name="name">原始名称</param>
        /// <returns>有效的JavaScript对象名</returns>
        private string GetValidJsObjectName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return "default";

            var result = name.Replace(" ", "")
                            .Replace("-", "")
                            .Replace("(", "")
                            .Replace(")", "")
                            .Replace("⭐", "Star");

            // 首字母小写
            if (result.Length > 0)
                result = char.ToLower(result[0]) + result.Substring(1);

            // 确保以字母开头
            if (string.IsNullOrEmpty(result) || !char.IsLetter(result[0]))
                result = "color" + result;

            return result;
        }

        /// <summary>
        /// 获取EasyChartX线条宽度枚举
        /// </summary>
        /// <param name="width">线条宽度</param>
        /// <returns>EasyChartX线条宽度枚举名称</returns>
        private string GetEasyChartXLineWidth(int width)
        {
            switch (width)
            {
                case 1:
                    return "Thin";
                case 2:
                    return "Normal";
                case 3:
                    return "Thick";
                case 4:
                case 5:
                    return "Wide";
                default:
                    return "Normal";
            }
        }

        #endregion
    }
}
