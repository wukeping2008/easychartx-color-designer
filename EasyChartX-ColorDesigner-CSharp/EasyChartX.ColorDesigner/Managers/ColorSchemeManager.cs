using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EasyChartX.ColorDesigner.Models;
using SeeSharpTools.JY.GUI;

namespace EasyChartX.ColorDesigner.Managers
{
    /// <summary>
    /// 配色方案管理器
    /// </summary>
    public class ColorSchemeManager
    {
        private List<ColorScheme> _colorSchemes;
        private ColorScheme _currentScheme;
        private readonly string _configFilePath;

        /// <summary>
        /// 当前选中的配色方案
        /// </summary>
        public ColorScheme CurrentScheme
        {
            get { return _currentScheme; }
            set
            {
                _currentScheme = value;
                OnCurrentSchemeChanged?.Invoke(_currentScheme);
            }
        }

        /// <summary>
        /// 所有配色方案
        /// </summary>
        public List<ColorScheme> ColorSchemes => _colorSchemes ?? new List<ColorScheme>();

        /// <summary>
        /// 当前方案变更事件
        /// </summary>
        public event Action<ColorScheme> OnCurrentSchemeChanged;

        /// <summary>
        /// 方案列表变更事件
        /// </summary>
        public event Action<List<ColorScheme>> OnSchemesChanged;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ColorSchemeManager()
        {
            _colorSchemes = new List<ColorScheme>();
            _configFilePath = Path.Combine(Application.StartupPath, "Resources", "ColorSchemes.json");
            LoadColorSchemes();
        }

        /// <summary>
        /// 加载配色方案
        /// </summary>
        public void LoadColorSchemes()
        {
            try
            {
                if (File.Exists(_configFilePath))
                {
                    LoadFromFile(_configFilePath);
                }
                else
                {
                    LoadDefaultSchemes();
                }

                // 设置默认选中方案
                if (_colorSchemes.Count > 0)
                {
                    CurrentScheme = _colorSchemes.FirstOrDefault(s => s.IsRecommended) ?? _colorSchemes[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载配色方案失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadDefaultSchemes();
            }
        }

        /// <summary>
        /// 从文件加载配色方案（简化版JSON解析）
        /// </summary>
        /// <param name="filePath">文件路径</param>
        private void LoadFromFile(string filePath)
        {
            try
            {
                var jsonContent = File.ReadAllText(filePath);
                _colorSchemes.Clear();
                
                // 简单的JSON解析 - 查找colorSchemes数组
                var startIndex = jsonContent.IndexOf("\"colorSchemes\"");
                if (startIndex == -1)
                {
                    LoadDefaultSchemes();
                    return;
                }

                // 找到数组开始位置
                var arrayStart = jsonContent.IndexOf('[', startIndex);
                if (arrayStart == -1)
                {
                    LoadDefaultSchemes();
                    return;
                }

                // 简单解析每个配色方案对象
                var currentPos = arrayStart + 1;
                var braceCount = 0;
                var inString = false;
                var escapeNext = false;
                var objectStart = -1;

                for (int i = currentPos; i < jsonContent.Length; i++)
                {
                    char c = jsonContent[i];

                    if (escapeNext)
                    {
                        escapeNext = false;
                        continue;
                    }

                    if (c == '\\')
                    {
                        escapeNext = true;
                        continue;
                    }

                    if (c == '"')
                    {
                        inString = !inString;
                        continue;
                    }

                    if (inString) continue;

                    if (c == '{')
                    {
                        if (braceCount == 0)
                            objectStart = i;
                        braceCount++;
                    }
                    else if (c == '}')
                    {
                        braceCount--;
                        if (braceCount == 0 && objectStart != -1)
                        {
                            // 提取对象JSON
                            var objectJson = jsonContent.Substring(objectStart, i - objectStart + 1);
                            var scheme = ParseColorScheme(objectJson);
                            if (scheme != null)
                            {
                                _colorSchemes.Add(scheme);
                            }
                            objectStart = -1;
                        }
                    }
                    else if (c == ']' && braceCount == 0)
                    {
                        break;
                    }
                }

                OnSchemesChanged?.Invoke(_colorSchemes);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"解析JSON失败：{ex.Message}");
                LoadDefaultSchemes();
            }
        }

        /// <summary>
        /// 解析单个配色方案对象
        /// </summary>
        /// <param name="json">JSON字符串</param>
        /// <returns>配色方案对象</returns>
        private ColorScheme ParseColorScheme(string json)
        {
            try
            {
                var name = ExtractStringValue(json, "name");
                var description = ExtractStringValue(json, "description");
                var category = ExtractStringValue(json, "category");
                var recommended = ExtractBoolValue(json, "isRecommended");
                var colors = ExtractStringArray(json, "colorHexValues");

                if (!string.IsNullOrEmpty(name) && colors != null && colors.Length > 0)
                {
                    return new ColorScheme(name, description, colors, category, recommended);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"解析配色方案失败：{ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// 从JSON中提取字符串值
        /// </summary>
        private string ExtractStringValue(string json, string key)
        {
            var pattern = $"\"{key}\"\\s*:\\s*\"";
            var startIndex = json.IndexOf(pattern);
            if (startIndex == -1) return "";

            startIndex += pattern.Length - 1; // 定位到开始引号
            var endIndex = json.IndexOf('"', startIndex + 1);
            if (endIndex == -1) return "";

            return json.Substring(startIndex + 1, endIndex - startIndex - 1);
        }

        /// <summary>
        /// 从JSON中提取布尔值
        /// </summary>
        private bool ExtractBoolValue(string json, string key)
        {
            var pattern = $"\"{key}\"\\s*:\\s*";
            var startIndex = json.IndexOf(pattern);
            if (startIndex == -1) return false;

            startIndex += pattern.Length;
            var remaining = json.Substring(startIndex);
            return remaining.StartsWith("true");
        }

        /// <summary>
        /// 从JSON中提取字符串数组
        /// </summary>
        private string[] ExtractStringArray(string json, string key)
        {
            var pattern = $"\"{key}\"\\s*:\\s*\\[";
            var startIndex = json.IndexOf(pattern);
            if (startIndex == -1) return null;

            var arrayStart = json.IndexOf('[', startIndex);
            var arrayEnd = json.IndexOf(']', arrayStart);
            if (arrayStart == -1 || arrayEnd == -1) return null;

            var arrayContent = json.Substring(arrayStart + 1, arrayEnd - arrayStart - 1);
            var items = new List<string>();
            
            var inString = false;
            var escapeNext = false;
            var currentItem = new StringBuilder();

            for (int i = 0; i < arrayContent.Length; i++)
            {
                char c = arrayContent[i];

                if (escapeNext)
                {
                    currentItem.Append(c);
                    escapeNext = false;
                    continue;
                }

                if (c == '\\')
                {
                    escapeNext = true;
                    continue;
                }

                if (c == '"')
                {
                    if (inString)
                    {
                        items.Add(currentItem.ToString());
                        currentItem.Clear();
                    }
                    inString = !inString;
                }
                else if (inString)
                {
                    currentItem.Append(c);
                }
            }

            return items.ToArray();
        }

        /// <summary>
        /// 加载默认配色方案（当文件不存在时）
        /// </summary>
        private void LoadDefaultSchemes()
        {
            _colorSchemes.Clear();

            // Tableau标准配色
            _colorSchemes.Add(new ColorScheme(
                "Tableau标准",
                "Matplotlib Tableau标准10色，商业图表首选配色方案",
                new[] { "#1f77b4", "#ff7f0e", "#2ca02c", "#d62728", "#9467bd", "#8c564b", "#e377c2", "#7f7f7f", "#bcbd22", "#17becf" },
                "Standard",
                true
            ));

            // 红蓝经典配色
            _colorSchemes.Add(new ColorScheme(
                "红蓝经典",
                "最常用的红蓝配色，适合1-2根线的应用场景",
                new[] { "#FF0000", "#0000FF", "#FF6600", "#0066FF", "#FF3333", "#3333FF", "#FF9900", "#0099FF" },
                "Classic"
            ));

            // 专业深色配色
            _colorSchemes.Add(new ColorScheme(
                "专业深色",
                "现代专业配色，适合长时间观看",
                new[] { "#E74C3C", "#3498DB", "#2ECC71", "#F39C12", "#9B59B6", "#1ABC9C", "#E67E22", "#34495E" },
                "Classic"
            ));

            OnSchemesChanged?.Invoke(_colorSchemes);
        }

        /// <summary>
        /// 根据名称获取配色方案
        /// </summary>
        /// <param name="name">方案名称</param>
        /// <returns>配色方案</returns>
        public ColorScheme GetSchemeByName(string name)
        {
            return _colorSchemes.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 根据分类获取配色方案
        /// </summary>
        /// <param name="category">分类名称</param>
        /// <returns>配色方案列表</returns>
        public List<ColorScheme> GetSchemesByCategory(string category)
        {
            return _colorSchemes.Where(s => s.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        /// <summary>
        /// 获取所有分类
        /// </summary>
        /// <returns>分类列表</returns>
        public List<string> GetCategories()
        {
            return _colorSchemes.Select(s => s.Category).Distinct().OrderBy(c => c).ToList();
        }

        /// <summary>
        /// 应用配色方案到EasyChartX控件
        /// </summary>
        /// <param name="chart">EasyChartX控件</param>
        /// <param name="scheme">配色方案</param>
        /// <param name="channelCount">通道数量</param>
        /// <param name="config">波形配置</param>
        public void ApplySchemeToChart(SeeSharpTools.JY.GUI.EasyChartX chart, ColorScheme scheme, int channelCount, WaveformConfig config = null)
        {
            try
            {
                if (chart == null || scheme == null)
                    return;

                // 设置背景
                if (config != null)
                {
                    var backgroundColor = config.GetBackgroundColor();
                    chart.BackColor = backgroundColor;
                    chart.ChartAreaBackColor = backgroundColor;

                    // 设置网格和坐标轴颜色
                    var gridColor = config.ShowGrid ? config.GetSmartGridColor() : Color.Transparent;
                    chart.AxisX.Color = gridColor;
                    chart.AxisX.MajorGridColor = gridColor;
                    chart.AxisX.MinorGridColor = gridColor;
                    chart.AxisY.Color = gridColor;
                    chart.AxisY.MajorGridColor = gridColor;
                    chart.AxisY.MinorGridColor = gridColor;

                    // 设置图例
                    chart.LegendVisible = config.ShowLegend;
                    chart.LegendBackColor = backgroundColor;
                    chart.LegendForeColor = config.GetSmartTextColor();
                }

                // 设置通道数量
                chart.Series.AdaptSeriesCount(channelCount);

                // 应用配色方案
                var colors = scheme.GetColors(channelCount);
                for (int i = 0; i < channelCount; i++)
                {
                    if (i < chart.Series.Count)
                    {
                        chart.Series[i].Color = colors[i];
                        chart.Series[i].Name = $"Channel{i + 1}";
                        
                        // 设置线条宽度
                        if (config != null)
                        {
                            switch (config.LineWidth)
                            {
                                case 1:
                                    chart.Series[i].Width = EasyChartXSeries.LineWidth.Thin;
                                    break;
                                case 2:
                                    chart.Series[i].Width = EasyChartXSeries.LineWidth.Thick;
                                    break;
                                case 3:
                                case 4:
                                    chart.Series[i].Width = EasyChartXSeries.LineWidth.Thick;
                                    break;
                                default:
                                    chart.Series[i].Width = EasyChartXSeries.LineWidth.Thick;
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"应用配色方案失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 保存配色方案到文件（简化版）
        /// </summary>
        public void SaveColorSchemes()
        {
            try
            {
                var sb = new StringBuilder();
                sb.AppendLine("{");
                sb.AppendLine("  \"version\": \"1.0\",");
                sb.AppendLine("  \"description\": \"EasyChartX 配色方案设计器 - 配色方案配置\",");
                sb.AppendLine("  \"author\": \"简仪科技 (JYTEK)\",");
                sb.AppendLine($"  \"lastUpdated\": \"{DateTime.Now:yyyy-MM-dd}\",");
                sb.AppendLine("  \"colorSchemes\": [");

                for (int i = 0; i < _colorSchemes.Count; i++)
                {
                    var scheme = _colorSchemes[i];
                    sb.AppendLine("    {");
                    sb.AppendLine($"      \"name\": \"{EscapeJsonString(scheme.Name)}\",");
                    sb.AppendLine($"      \"description\": \"{EscapeJsonString(scheme.Description)}\",");
                    sb.AppendLine($"      \"category\": \"{EscapeJsonString(scheme.Category)}\",");
                    sb.AppendLine($"      \"isRecommended\": {scheme.IsRecommended.ToString().ToLower()},");
                    sb.AppendLine("      \"colorHexValues\": [");
                    
                    for (int j = 0; j < scheme.ColorHexValues.Length; j++)
                    {
                        sb.Append($"        \"{scheme.ColorHexValues[j]}\"");
                        if (j < scheme.ColorHexValues.Length - 1)
                            sb.AppendLine(",");
                        else
                            sb.AppendLine();
                    }
                    
                    sb.AppendLine("      ]");
                    sb.Append("    }");
                    if (i < _colorSchemes.Count - 1)
                        sb.AppendLine(",");
                    else
                        sb.AppendLine();
                }

                sb.AppendLine("  ]");
                sb.AppendLine("}");

                // 确保目录存在
                var directory = Path.GetDirectoryName(_configFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                File.WriteAllText(_configFilePath, sb.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存配色方案失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}
