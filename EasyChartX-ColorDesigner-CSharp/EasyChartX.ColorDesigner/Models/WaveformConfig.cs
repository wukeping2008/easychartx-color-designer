using System;
using System.Drawing;

namespace EasyChartX.ColorDesigner.Models
{
    /// <summary>
    /// 波形类型枚举
    /// </summary>
    public enum WaveformType
    {
        /// <summary>
        /// 正弦波
        /// </summary>
        Sine,
        
        /// <summary>
        /// 方波
        /// </summary>
        Square,
        
        /// <summary>
        /// 三角波
        /// </summary>
        Triangle,
        
        /// <summary>
        /// 混合波形
        /// </summary>
        Mixed
    }

    /// <summary>
    /// 背景类型枚举
    /// </summary>
    public enum BackgroundType
    {
        /// <summary>
        /// 白色背景
        /// </summary>
        White,
        
        /// <summary>
        /// 黑色背景
        /// </summary>
        Black,
        
        /// <summary>
        /// 深灰背景
        /// </summary>
        Dark,
        
        /// <summary>
        /// 自定义背景
        /// </summary>
        Custom
    }

    /// <summary>
    /// 波形配置模型
    /// </summary>
    public class WaveformConfig
    {
        /// <summary>
        /// 通道数量
        /// </summary>
        public int ChannelCount { get; set; }

        /// <summary>
        /// 波形类型
        /// </summary>
        public WaveformType WaveformType { get; set; }

        /// <summary>
        /// 线条宽度
        /// </summary>
        public int LineWidth { get; set; }

        /// <summary>
        /// 背景类型
        /// </summary>
        public BackgroundType BackgroundType { get; set; }

        /// <summary>
        /// 自定义背景颜色
        /// </summary>
        public Color CustomBackgroundColor { get; set; }

        /// <summary>
        /// 是否显示网格
        /// </summary>
        public bool ShowGrid { get; set; }

        /// <summary>
        /// 网格透明度 (0.0 - 1.0)
        /// </summary>
        public double GridOpacity { get; set; }

        /// <summary>
        /// 网格颜色
        /// </summary>
        public Color GridColor { get; set; }

        /// <summary>
        /// 是否显示图例
        /// </summary>
        public bool ShowLegend { get; set; }

        /// <summary>
        /// 是否启用对比模式
        /// </summary>
        public bool ComparisonMode { get; set; }

        /// <summary>
        /// 对比方案名称
        /// </summary>
        public string ComparisonSchemeName { get; set; }

        /// <summary>
        /// 是否启用色系分组
        /// </summary>
        public bool EnableColorGroups { get; set; }

        /// <summary>
        /// 色系数量
        /// </summary>
        public int ColorGroupCount { get; set; }

        /// <summary>
        /// 每组颜色数
        /// </summary>
        public int ColorsPerGroup { get; set; }

        /// <summary>
        /// 采样点数
        /// </summary>
        public int SampleCount { get; set; }

        /// <summary>
        /// 动画速度 (毫秒)
        /// </summary>
        public int AnimationSpeed { get; set; }

        /// <summary>
        /// 是否启用动画
        /// </summary>
        public bool EnableAnimation { get; set; }

        /// <summary>
        /// 构造函数 - 设置默认值
        /// </summary>
        public WaveformConfig()
        {
            ChannelCount = 6;
            WaveformType = WaveformType.Sine; // 默认使用正弦波
            LineWidth = 2;
            BackgroundType = BackgroundType.Black;
            CustomBackgroundColor = Color.Black;
            ShowGrid = true;
            GridOpacity = 0.3;
            GridColor = Color.Gray;
            ShowLegend = true;
            ComparisonMode = false;
            ComparisonSchemeName = "d3category10";
            EnableColorGroups = false;
            ColorGroupCount = 2;
            ColorsPerGroup = 3;
            SampleCount = 2000;
            AnimationSpeed = 50; // 50ms刷新间隔
            EnableAnimation = true;
        }

        /// <summary>
        /// 获取实际背景颜色
        /// </summary>
        /// <returns>背景颜色</returns>
        public Color GetBackgroundColor()
        {
            switch (BackgroundType)
            {
                case BackgroundType.White:
                    return Color.White;
                case BackgroundType.Black:
                    return Color.Black;
                case BackgroundType.Dark:
                    return Color.FromArgb(44, 62, 80); // #2c3e50
                case BackgroundType.Custom:
                    return CustomBackgroundColor;
                default:
                    return Color.Black;
            }
        }

        /// <summary>
        /// 获取智能网格颜色（根据背景自动调整）
        /// </summary>
        /// <returns>网格颜色</returns>
        public Color GetSmartGridColor()
        {
            var bgColor = GetBackgroundColor();
            var brightness = (bgColor.R * 299 + bgColor.G * 587 + bgColor.B * 114) / 1000;
            
            if (brightness > 128)
            {
                // 浅色背景使用深色网格
                return Color.FromArgb(204, 204, 204); // #cccccc
            }
            else
            {
                // 深色背景使用浅色网格
                return Color.FromArgb(102, 102, 102); // #666666
            }
        }

        /// <summary>
        /// 获取智能文字颜色（根据背景自动调整）
        /// </summary>
        /// <returns>文字颜色</returns>
        public Color GetSmartTextColor()
        {
            var bgColor = GetBackgroundColor();
            var brightness = (bgColor.R * 299 + bgColor.G * 587 + bgColor.B * 114) / 1000;
            
            return brightness > 128 ? Color.Black : Color.White;
        }

        /// <summary>
        /// 克隆配置
        /// </summary>
        /// <returns>新的配置实例</returns>
        public WaveformConfig Clone()
        {
            return new WaveformConfig
            {
                ChannelCount = this.ChannelCount,
                WaveformType = this.WaveformType,
                LineWidth = this.LineWidth,
                BackgroundType = this.BackgroundType,
                CustomBackgroundColor = this.CustomBackgroundColor,
                ShowGrid = this.ShowGrid,
                GridOpacity = this.GridOpacity,
                GridColor = this.GridColor,
                ShowLegend = this.ShowLegend,
                ComparisonMode = this.ComparisonMode,
                ComparisonSchemeName = this.ComparisonSchemeName,
                EnableColorGroups = this.EnableColorGroups,
                ColorGroupCount = this.ColorGroupCount,
                ColorsPerGroup = this.ColorsPerGroup,
                SampleCount = this.SampleCount,
                AnimationSpeed = this.AnimationSpeed,
                EnableAnimation = this.EnableAnimation
            };
        }

        /// <summary>
        /// 验证配置有效性
        /// </summary>
        /// <returns>是否有效</returns>
        public bool IsValid()
        {
            return ChannelCount > 0 && ChannelCount <= 32 &&
                   LineWidth > 0 && LineWidth <= 10 &&
                   GridOpacity >= 0.0 && GridOpacity <= 1.0 &&
                   ColorGroupCount > 0 && ColorGroupCount <= 10 &&
                   ColorsPerGroup > 0 && ColorsPerGroup <= 20 &&
                   SampleCount > 0 && SampleCount <= 10000 &&
                   AnimationSpeed > 0 && AnimationSpeed <= 1000;
        }
    }
}
