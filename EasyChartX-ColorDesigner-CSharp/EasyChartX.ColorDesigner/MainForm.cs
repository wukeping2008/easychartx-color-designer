using System;
using System.Drawing;
using System.Windows.Forms;
using EasyChartX.ColorDesigner.Managers;
using EasyChartX.ColorDesigner.Models;
using EasyChartX.ColorDesigner.Utils;
using SeeSharpTools.JY.GUI;

namespace EasyChartX.ColorDesigner
{
    /// <summary>
    /// 主窗体
    /// </summary>
    public partial class MainForm : Form
    {
        #region 私有字段

        private ColorSchemeManager _colorSchemeManager;
        private WaveformGenerator _waveformGenerator;
        private ExportManager _exportManager;
        private WaveformConfig _currentConfig;
        private Timer _animationTimer;
        private double[,] _currentWaveforms;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            InitializeManagers();
            SetupEventHandlers();
            InitializeUI();
            StartAnimation();
        }

        #endregion

        #region 初始化方法

        /// <summary>
        /// 初始化管理器
        /// </summary>
        private void InitializeManagers()
        {
            try
            {
                _currentConfig = new WaveformConfig();
                _colorSchemeManager = new ColorSchemeManager();
                _waveformGenerator = new WaveformGenerator();
                _exportManager = new ExportManager();
                
                System.Diagnostics.Debug.WriteLine("管理器初始化完成");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"初始化管理器失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 设置事件处理器
        /// </summary>
        private void SetupEventHandlers()
        {
            // 配色方案变更事件
            if (_colorSchemeManager != null)
            {
                _colorSchemeManager.OnCurrentSchemeChanged += OnColorSchemeChanged;
                _colorSchemeManager.OnSchemesChanged += OnSchemesListChanged;
            }

            // 窗体事件
            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;
            this.Resize += MainForm_Resize;
        }

        /// <summary>
        /// 初始化UI
        /// </summary>
        private void InitializeUI()
        {
            try
            {
                // 设置窗体属性
                this.Text = "EasyChartX 配色方案设计器 - 简仪科技";
                this.WindowState = FormWindowState.Maximized;
                this.MinimumSize = new Size(1200, 800);

                // 初始化配色方案列表
                UpdateColorSchemeList();

                // 初始化配置控件
                UpdateConfigControls();

                // 应用默认配色方案
                if (_colorSchemeManager?.CurrentScheme != null)
                {
                    ApplyCurrentColorScheme();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"初始化UI失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 启动动画
        /// </summary>
        private void StartAnimation()
        {
            try
            {
                _animationTimer = new Timer();
                _animationTimer.Interval = _currentConfig.AnimationSpeed;
                _animationTimer.Tick += AnimationTimer_Tick;
                
                if (_currentConfig.EnableAnimation)
                {
                    _animationTimer.Start();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"启动动画失败：{ex.Message}");
            }
        }

        #endregion

        #region 事件处理器

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                // 生成初始波形
                UpdateWaveforms();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载窗体失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 波形类型选择变更事件
        /// </summary>
        private void ComboBoxWaveformType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // 只有正弦波选项，直接设置为正弦波
                _currentConfig.WaveformType = WaveformType.Sine;
                UpdateWaveforms();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"波形类型选择失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 动画定时器事件
        /// </summary>
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_currentConfig.EnableAnimation && _waveformGenerator != null)
                {
                    // 更新时间偏移
                    _waveformGenerator.UpdateTimeOffset(0.1);
                    
                    // 重新生成波形
                    UpdateWaveforms();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"动画更新失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 配色方案变更事件
        /// </summary>
        private void OnColorSchemeChanged(ColorScheme scheme)
        {
            try
            {
                ApplyCurrentColorScheme();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"应用配色方案失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 配色方案列表变更事件
        /// </summary>
        private void OnSchemesListChanged(System.Collections.Generic.List<ColorScheme> schemes)
        {
            try
            {
                UpdateColorSchemeList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"更新配色方案列表失败：{ex.Message}");
            }
        }

        #endregion

        #region UI更新方法

        /// <summary>
        /// 更新配色方案列表
        /// </summary>
        private void UpdateColorSchemeList()
        {
            try
            {
                if (_colorSchemeManager == null)
                {
                    System.Diagnostics.Debug.WriteLine("ColorSchemeManager 为空");
                    return;
                }

                _listBoxColorSchemes.Items.Clear();

                var allSchemes = _colorSchemeManager.ColorSchemes;
                System.Diagnostics.Debug.WriteLine($"配色方案总数：{allSchemes.Count}");

                if (allSchemes.Count == 0)
                {
                    _listBoxColorSchemes.Items.Add("没有找到配色方案");
                    return;
                }

                // 直接添加所有方案，不分类
                foreach (var scheme in allSchemes)
                {
                    _listBoxColorSchemes.Items.Add(scheme);
                }

                // 选中第一个方案作为默认
                if (_listBoxColorSchemes.Items.Count > 0)
                {
                    _listBoxColorSchemes.SelectedIndex = 0;
                    if (_colorSchemeManager.CurrentScheme == null && allSchemes.Count > 0)
                    {
                        _colorSchemeManager.CurrentScheme = allSchemes[0];
                    }
                }

                System.Diagnostics.Debug.WriteLine($"列表框项目数：{_listBoxColorSchemes.Items.Count}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"更新配色方案列表失败：{ex.Message}");
                _listBoxColorSchemes.Items.Clear();
                _listBoxColorSchemes.Items.Add($"加载失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 获取分类显示名称
        /// </summary>
        private string GetCategoryDisplayName(string category)
        {
            switch (category?.ToLower())
            {
                case "standard":
                    return "🏆 标准配色方案";
                case "classic":
                    return "📊 经典配色方案";
                case "special":
                    return "🎨 特殊用途";
                case "custom":
                    return "🔧 自定义配色";
                default:
                    return category ?? "其他";
            }
        }

        /// <summary>
        /// 更新配置控件
        /// </summary>
        private void UpdateConfigControls()
        {
            try
            {
                if (_currentConfig == null) return;

                // 更新通道数量
                _numericUpDownChannels.Value = _currentConfig.ChannelCount;

                // 更新波形类型
                _comboBoxWaveformType.SelectedIndex = (int)_currentConfig.WaveformType;

                // 更新线条宽度
                _trackBarLineWidth.Value = _currentConfig.LineWidth;
                _labelLineWidthValue.Text = _currentConfig.LineWidth.ToString();

                // 更新背景类型
                _comboBoxBackground.SelectedIndex = (int)_currentConfig.BackgroundType;

                // 更新显示选项
                _checkBoxShowGrid.Checked = _currentConfig.ShowGrid;
                _checkBoxShowLegend.Checked = _currentConfig.ShowLegend;
                _checkBoxEnableAnimation.Checked = _currentConfig.EnableAnimation;

                // 更新网格透明度
                _trackBarGridOpacity.Value = (int)(_currentConfig.GridOpacity * 100);
                _labelGridOpacityValue.Text = $"{(int)(_currentConfig.GridOpacity * 100)}%";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"更新配置控件失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 应用当前配色方案
        /// </summary>
        private void ApplyCurrentColorScheme()
        {
            try
            {
                if (_colorSchemeManager?.CurrentScheme == null || _easyChartX == null)
                {
                    System.Diagnostics.Debug.WriteLine("配色方案或图表控件为空");
                    return;
                }

                System.Diagnostics.Debug.WriteLine($"应用配色方案：{_colorSchemeManager.CurrentScheme.Name}");

                // 应用配色方案到图表
                _colorSchemeManager.ApplySchemeToChart(
                    _easyChartX, 
                    _colorSchemeManager.CurrentScheme, 
                    _currentConfig.ChannelCount, 
                    _currentConfig);

                // 更新颜色信息显示
                UpdateColorInfo();

                // 重新生成波形
                UpdateWaveforms();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"应用配色方案失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 更新颜色信息显示
        /// </summary>
        private void UpdateColorInfo()
        {
            try
            {
                if (_colorSchemeManager?.CurrentScheme == null)
                {
                    _richTextBoxColorInfo.Text = "未选择配色方案";
                    return;
                }

                var scheme = _colorSchemeManager.CurrentScheme;
                var info = new System.Text.StringBuilder();

                info.AppendLine($"配色方案：{scheme.Name}");
                info.AppendLine($"描述：{scheme.Description}");
                info.AppendLine($"分类：{scheme.Category}");
                info.AppendLine($"颜色数量：{scheme.ColorHexValues?.Length ?? 0}");
                info.AppendLine($"通道数量：{_currentConfig.ChannelCount}");
                info.AppendLine();
                info.AppendLine("颜色值：");

                var colors = scheme.GetColors(_currentConfig.ChannelCount);
                for (int i = 0; i < colors.Length; i++)
                {
                    var color = colors[i];
                    var hex = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
                    info.AppendLine($"Channel{i + 1}: {hex}");
                }

                _richTextBoxColorInfo.Text = info.ToString();
            }
            catch (Exception ex)
            {
                _richTextBoxColorInfo.Text = $"显示颜色信息失败：{ex.Message}";
            }
        }

        /// <summary>
        /// 更新波形
        /// </summary>
        private void UpdateWaveforms()
        {
            try
            {
                if (_waveformGenerator == null || _easyChartX == null)
                    return;

                // 生成波形数据
                _currentWaveforms = _waveformGenerator.GenerateWaveforms(_currentConfig);

                // 更新图表
                _waveformGenerator.UpdateChart(_easyChartX, _currentWaveforms, _currentConfig);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"更新波形失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 重新布局控件
        /// </summary>
        private void LayoutControls()
        {
            try
            {
                // 这里可以添加动态布局逻辑
                // 当前使用设计器布局，后续可以优化
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"布局控件失败：{ex.Message}");
            }
        }

        #endregion

        #region 控件事件处理器

        /// <summary>
        /// 配色方案选择变更
        /// </summary>
        private void ListBoxColorSchemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = _listBoxColorSchemes.SelectedItem;
                if (selectedItem is ColorScheme scheme)
                {
                    _colorSchemeManager.CurrentScheme = scheme;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"选择配色方案失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 通道数量变更
        /// </summary>
        private void NumericUpDownChannels_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                _currentConfig.ChannelCount = (int)_numericUpDownChannels.Value;
                ApplyCurrentColorScheme();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"更改通道数量失败：{ex.Message}");
            }
        }


        /// <summary>
        /// 线条宽度变更
        /// </summary>
        private void TrackBarLineWidth_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                _currentConfig.LineWidth = _trackBarLineWidth.Value;
                _labelLineWidthValue.Text = _currentConfig.LineWidth.ToString();
                ApplyCurrentColorScheme();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"更改线条宽度失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 背景类型变更
        /// </summary>
        private void ComboBoxBackground_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _currentConfig.BackgroundType = (BackgroundType)_comboBoxBackground.SelectedIndex;
                ApplyCurrentColorScheme();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"更改背景类型失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 显示网格变更
        /// </summary>
        private void CheckBoxShowGrid_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                _currentConfig.ShowGrid = _checkBoxShowGrid.Checked;
                ApplyCurrentColorScheme();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"更改网格显示失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 显示图例变更
        /// </summary>
        private void CheckBoxShowLegend_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                _currentConfig.ShowLegend = _checkBoxShowLegend.Checked;
                ApplyCurrentColorScheme();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"更改图例显示失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 启用动画变更
        /// </summary>
        private void CheckBoxEnableAnimation_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                _currentConfig.EnableAnimation = _checkBoxEnableAnimation.Checked;
                
                if (_currentConfig.EnableAnimation)
                {
                    _animationTimer?.Start();
                }
                else
                {
                    _animationTimer?.Stop();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"更改动画设置失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 网格透明度变更
        /// </summary>
        private void TrackBarGridOpacity_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                _currentConfig.GridOpacity = _trackBarGridOpacity.Value / 100.0;
                _labelGridOpacityValue.Text = $"{_trackBarGridOpacity.Value}%";
                ApplyCurrentColorScheme();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"更改网格透明度失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 导出按钮点击
        /// </summary>
        private void ButtonExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (_colorSchemeManager?.CurrentScheme == null)
                {
                    MessageBox.Show("请先选择一个配色方案", "提示", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 确定导出格式
                var format = ExportFormat.CSharp;
                if (sender == _buttonExportJson)
                    format = ExportFormat.Json;
                else if (sender == _buttonExportCss)
                    format = ExportFormat.Css;
                else if (sender == _buttonExportJs)
                    format = ExportFormat.JavaScript;

                // 创建导出配置
                var exportConfig = new ExportConfig
                {
                    Format = format,
                    IncludeComments = true,
                    IncludeExample = true,
                    AutoOpenFile = true
                };

                // 执行导出
                _exportManager.ExportColorScheme(
                    _colorSchemeManager.CurrentScheme, 
                    _currentConfig, 
                    exportConfig);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"导出失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // 停止动画定时器
                _animationTimer?.Stop();
                _animationTimer?.Dispose();
                
                // 保存配色方案
                _colorSchemeManager?.SaveColorSchemes();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"窗体关闭处理失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 窗体大小变更事件
        /// </summary>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            try
            {
                // 重新布局控件
                LayoutControls();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"窗体大小变更处理失败：{ex.Message}");
            }
        }

        #endregion
    }
}
