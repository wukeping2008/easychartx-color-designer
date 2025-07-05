using System;
using System.Drawing;
using System.Globalization;

namespace EasyChartX.ColorDesigner.Utils
{
    /// <summary>
    /// 颜色工具类
    /// </summary>
    public static class ColorUtils
    {
        #region 颜色转换

        /// <summary>
        /// 将十六进制颜色字符串转换为Color对象
        /// </summary>
        /// <param name="hex">十六进制颜色字符串（如 "#FF0000" 或 "FF0000"）</param>
        /// <returns>Color对象</returns>
        public static Color HexToColor(string hex)
        {
            if (string.IsNullOrEmpty(hex))
                return Color.Black;

            // 移除 # 前缀
            hex = hex.TrimStart('#');

            // 确保是6位十六进制
            if (hex.Length != 6)
                return Color.Black;

            try
            {
                int r = int.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
                int g = int.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
                int b = int.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);

                return Color.FromArgb(r, g, b);
            }
            catch
            {
                return Color.Black;
            }
        }

        /// <summary>
        /// 将Color对象转换为十六进制颜色字符串
        /// </summary>
        /// <param name="color">Color对象</param>
        /// <param name="includeHash">是否包含#前缀</param>
        /// <returns>十六进制颜色字符串</returns>
        public static string ColorToHex(Color color, bool includeHash = true)
        {
            string hex = $"{color.R:X2}{color.G:X2}{color.B:X2}";
            return includeHash ? "#" + hex : hex;
        }

        /// <summary>
        /// 将Color对象转换为RGB字符串
        /// </summary>
        /// <param name="color">Color对象</param>
        /// <returns>RGB字符串（如 "rgb(255, 0, 0)"）</returns>
        public static string ColorToRgb(Color color)
        {
            return $"rgb({color.R}, {color.G}, {color.B})";
        }

        /// <summary>
        /// 将Color对象转换为RGBA字符串
        /// </summary>
        /// <param name="color">Color对象</param>
        /// <param name="alpha">透明度（0.0-1.0）</param>
        /// <returns>RGBA字符串（如 "rgba(255, 0, 0, 0.5)"）</returns>
        public static string ColorToRgba(Color color, double alpha = 1.0)
        {
            alpha = Math.Max(0.0, Math.Min(1.0, alpha));
            return $"rgba({color.R}, {color.G}, {color.B}, {alpha:F2})";
        }

        /// <summary>
        /// 将RGB值转换为HSV值
        /// </summary>
        /// <param name="color">Color对象</param>
        /// <returns>HSV值（H: 0-360, S: 0-1, V: 0-1）</returns>
        public static (double H, double S, double V) ColorToHsv(Color color)
        {
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            double max = Math.Max(r, Math.Max(g, b));
            double min = Math.Min(r, Math.Min(g, b));
            double delta = max - min;

            double h = 0;
            double s = max == 0 ? 0 : delta / max;
            double v = max;

            if (delta != 0)
            {
                if (max == r)
                    h = ((g - b) / delta) % 6;
                else if (max == g)
                    h = (b - r) / delta + 2;
                else
                    h = (r - g) / delta + 4;

                h *= 60;
                if (h < 0) h += 360;
            }

            return (h, s, v);
        }

        /// <summary>
        /// 将HSV值转换为RGB颜色
        /// </summary>
        /// <param name="h">色相（0-360）</param>
        /// <param name="s">饱和度（0-1）</param>
        /// <param name="v">明度（0-1）</param>
        /// <returns>Color对象</returns>
        public static Color HsvToColor(double h, double s, double v)
        {
            h = h % 360;
            if (h < 0) h += 360;

            s = Math.Max(0, Math.Min(1, s));
            v = Math.Max(0, Math.Min(1, v));

            double c = v * s;
            double x = c * (1 - Math.Abs((h / 60) % 2 - 1));
            double m = v - c;

            double r = 0, g = 0, b = 0;

            if (h >= 0 && h < 60)
            {
                r = c; g = x; b = 0;
            }
            else if (h >= 60 && h < 120)
            {
                r = x; g = c; b = 0;
            }
            else if (h >= 120 && h < 180)
            {
                r = 0; g = c; b = x;
            }
            else if (h >= 180 && h < 240)
            {
                r = 0; g = x; b = c;
            }
            else if (h >= 240 && h < 300)
            {
                r = x; g = 0; b = c;
            }
            else if (h >= 300 && h < 360)
            {
                r = c; g = 0; b = x;
            }

            int red = (int)Math.Round((r + m) * 255);
            int green = (int)Math.Round((g + m) * 255);
            int blue = (int)Math.Round((b + m) * 255);

            return Color.FromArgb(red, green, blue);
        }

        #endregion

        #region 颜色分析

        /// <summary>
        /// 计算颜色的亮度
        /// </summary>
        /// <param name="color">Color对象</param>
        /// <returns>亮度值（0-1）</returns>
        public static double GetLuminance(Color color)
        {
            // 使用相对亮度公式
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            // 应用gamma校正
            r = r <= 0.03928 ? r / 12.92 : Math.Pow((r + 0.055) / 1.055, 2.4);
            g = g <= 0.03928 ? g / 12.92 : Math.Pow((g + 0.055) / 1.055, 2.4);
            b = b <= 0.03928 ? b / 12.92 : Math.Pow((b + 0.055) / 1.055, 2.4);

            return 0.2126 * r + 0.7152 * g + 0.0722 * b;
        }

        /// <summary>
        /// 判断颜色是否为深色
        /// </summary>
        /// <param name="color">Color对象</param>
        /// <returns>是否为深色</returns>
        public static bool IsDarkColor(Color color)
        {
            return GetLuminance(color) < 0.5;
        }

        /// <summary>
        /// 判断颜色是否为浅色
        /// </summary>
        /// <param name="color">Color对象</param>
        /// <returns>是否为浅色</returns>
        public static bool IsLightColor(Color color)
        {
            return !IsDarkColor(color);
        }

        /// <summary>
        /// 计算两个颜色的对比度
        /// </summary>
        /// <param name="color1">颜色1</param>
        /// <param name="color2">颜色2</param>
        /// <returns>对比度（1-21）</returns>
        public static double GetContrast(Color color1, Color color2)
        {
            double lum1 = GetLuminance(color1);
            double lum2 = GetLuminance(color2);

            double lighter = Math.Max(lum1, lum2);
            double darker = Math.Min(lum1, lum2);

            return (lighter + 0.05) / (darker + 0.05);
        }

        /// <summary>
        /// 获取与指定颜色对比度最佳的文字颜色
        /// </summary>
        /// <param name="backgroundColor">背景颜色</param>
        /// <returns>文字颜色（黑色或白色）</returns>
        public static Color GetContrastTextColor(Color backgroundColor)
        {
            return IsDarkColor(backgroundColor) ? Color.White : Color.Black;
        }

        #endregion

        #region 颜色调整

        /// <summary>
        /// 调整颜色的亮度
        /// </summary>
        /// <param name="color">原始颜色</param>
        /// <param name="factor">调整因子（-1.0到1.0，负值变暗，正值变亮）</param>
        /// <returns>调整后的颜色</returns>
        public static Color AdjustBrightness(Color color, double factor)
        {
            factor = Math.Max(-1.0, Math.Min(1.0, factor));

            int r, g, b;

            if (factor >= 0)
            {
                // 变亮
                r = (int)(color.R + (255 - color.R) * factor);
                g = (int)(color.G + (255 - color.G) * factor);
                b = (int)(color.B + (255 - color.B) * factor);
            }
            else
            {
                // 变暗
                factor = Math.Abs(factor);
                r = (int)(color.R * (1 - factor));
                g = (int)(color.G * (1 - factor));
                b = (int)(color.B * (1 - factor));
            }

            return Color.FromArgb(
                Math.Max(0, Math.Min(255, r)),
                Math.Max(0, Math.Min(255, g)),
                Math.Max(0, Math.Min(255, b))
            );
        }

        /// <summary>
        /// 调整颜色的饱和度
        /// </summary>
        /// <param name="color">原始颜色</param>
        /// <param name="factor">调整因子（-1.0到1.0）</param>
        /// <returns>调整后的颜色</returns>
        public static Color AdjustSaturation(Color color, double factor)
        {
            var (h, s, v) = ColorToHsv(color);
            s = Math.Max(0, Math.Min(1, s + factor));
            return HsvToColor(h, s, v);
        }

        /// <summary>
        /// 调整颜色的色相
        /// </summary>
        /// <param name="color">原始颜色</param>
        /// <param name="degrees">色相偏移角度（-360到360）</param>
        /// <returns>调整后的颜色</returns>
        public static Color AdjustHue(Color color, double degrees)
        {
            var (h, s, v) = ColorToHsv(color);
            h = (h + degrees) % 360;
            if (h < 0) h += 360;
            return HsvToColor(h, s, v);
        }

        /// <summary>
        /// 创建颜色的透明版本
        /// </summary>
        /// <param name="color">原始颜色</param>
        /// <param name="alpha">透明度（0-255）</param>
        /// <returns>带透明度的颜色</returns>
        public static Color WithAlpha(Color color, int alpha)
        {
            alpha = Math.Max(0, Math.Min(255, alpha));
            return Color.FromArgb(alpha, color.R, color.G, color.B);
        }

        #endregion

        #region 颜色生成

        /// <summary>
        /// 生成互补色
        /// </summary>
        /// <param name="color">原始颜色</param>
        /// <returns>互补色</returns>
        public static Color GetComplementaryColor(Color color)
        {
            return AdjustHue(color, 180);
        }

        /// <summary>
        /// 生成类似色
        /// </summary>
        /// <param name="color">原始颜色</param>
        /// <param name="count">生成数量</param>
        /// <param name="range">色相范围（度）</param>
        /// <returns>类似色数组</returns>
        public static Color[] GetAnalogousColors(Color color, int count, double range = 30)
        {
            if (count <= 0) return new Color[0];

            var colors = new Color[count];
            var (h, s, v) = ColorToHsv(color);

            double step = range * 2 / (count + 1);
            double startHue = h - range;

            for (int i = 0; i < count; i++)
            {
                double newHue = startHue + step * (i + 1);
                colors[i] = HsvToColor(newHue, s, v);
            }

            return colors;
        }

        /// <summary>
        /// 生成三元色
        /// </summary>
        /// <param name="color">原始颜色</param>
        /// <returns>三元色数组</returns>
        public static Color[] GetTriadicColors(Color color)
        {
            return new Color[]
            {
                color,
                AdjustHue(color, 120),
                AdjustHue(color, 240)
            };
        }

        /// <summary>
        /// 生成渐变色
        /// </summary>
        /// <param name="startColor">起始颜色</param>
        /// <param name="endColor">结束颜色</param>
        /// <param name="steps">渐变步数</param>
        /// <returns>渐变色数组</returns>
        public static Color[] GenerateGradient(Color startColor, Color endColor, int steps)
        {
            if (steps <= 0) return new Color[0];
            if (steps == 1) return new Color[] { startColor };

            var colors = new Color[steps];
            colors[0] = startColor;
            colors[steps - 1] = endColor;

            for (int i = 1; i < steps - 1; i++)
            {
                double ratio = (double)i / (steps - 1);
                
                int r = (int)(startColor.R + (endColor.R - startColor.R) * ratio);
                int g = (int)(startColor.G + (endColor.G - startColor.G) * ratio);
                int b = (int)(startColor.B + (endColor.B - startColor.B) * ratio);

                colors[i] = Color.FromArgb(r, g, b);
            }

            return colors;
        }

        #endregion

        #region 颜色验证

        /// <summary>
        /// 验证十六进制颜色字符串是否有效
        /// </summary>
        /// <param name="hex">十六进制颜色字符串</param>
        /// <returns>是否有效</returns>
        public static bool IsValidHexColor(string hex)
        {
            if (string.IsNullOrEmpty(hex))
                return false;

            hex = hex.TrimStart('#');

            if (hex.Length != 6)
                return false;

            return int.TryParse(hex, NumberStyles.HexNumber, null, out _);
        }

        /// <summary>
        /// 验证RGB值是否有效
        /// </summary>
        /// <param name="r">红色分量</param>
        /// <param name="g">绿色分量</param>
        /// <param name="b">蓝色分量</param>
        /// <returns>是否有效</returns>
        public static bool IsValidRgb(int r, int g, int b)
        {
            return r >= 0 && r <= 255 && g >= 0 && g <= 255 && b >= 0 && b <= 255;
        }

        #endregion

        #region 预定义颜色

        /// <summary>
        /// 获取网格颜色（根据背景自动选择）
        /// </summary>
        /// <param name="backgroundColor">背景颜色</param>
        /// <returns>网格颜色</returns>
        public static Color GetGridColor(Color backgroundColor)
        {
            if (IsDarkColor(backgroundColor))
            {
                return Color.FromArgb(64, 64, 64); // 深色背景用深灰色网格
            }
            else
            {
                return Color.FromArgb(192, 192, 192); // 浅色背景用浅灰色网格
            }
        }

        /// <summary>
        /// 获取轴线颜色（根据背景自动选择）
        /// </summary>
        /// <param name="backgroundColor">背景颜色</param>
        /// <returns>轴线颜色</returns>
        public static Color GetAxisColor(Color backgroundColor)
        {
            return GetContrastTextColor(backgroundColor);
        }

        #endregion
    }
}
