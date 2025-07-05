using System;
using System.Drawing;
using System.Linq;

namespace EasyChartX.ColorDesigner.Models
{
    /// <summary>
    /// 配色方案模型
    /// </summary>
    public class ColorScheme
    {
        /// <summary>
        /// 方案名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 方案描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 颜色数组（十六进制字符串）
        /// </summary>
        public string[] ColorHexValues { get; set; }

        /// <summary>
        /// 方案分类
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 是否为推荐方案
        /// </summary>
        public bool IsRecommended { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 是否为用户自定义方案
        /// </summary>
        public bool IsCustom { get; set; }

        /// <summary>
        /// 获取Color对象数组
        /// </summary>
        public Color[] Colors
        {
            get
            {
                if (ColorHexValues == null) return new Color[0];
                
                return ColorHexValues.Select(hex => 
                {
                    try
                    {
                        return ColorTranslator.FromHtml(hex);
                    }
                    catch
                    {
                        return Color.Gray; // 默认颜色
                    }
                }).ToArray();
            }
        }

        /// <summary>
        /// 获取指定索引的颜色，支持循环使用
        /// </summary>
        /// <param name="index">颜色索引</param>
        /// <returns>对应的颜色</returns>
        public Color GetColor(int index)
        {
            if (Colors.Length == 0) return Color.Gray;
            return Colors[index % Colors.Length];
        }

        /// <summary>
        /// 获取指定数量的颜色数组
        /// </summary>
        /// <param name="count">需要的颜色数量</param>
        /// <returns>颜色数组</returns>
        public Color[] GetColors(int count)
        {
            var result = new Color[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = GetColor(i);
            }
            return result;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ColorScheme()
        {
            CreatedTime = DateTime.Now;
            IsCustom = false;
            Category = "Custom";
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">方案名称</param>
        /// <param name="description">方案描述</param>
        /// <param name="colorHexValues">颜色十六进制值数组</param>
        /// <param name="category">分类</param>
        /// <param name="isRecommended">是否推荐</param>
        public ColorScheme(string name, string description, string[] colorHexValues, 
            string category = "Custom", bool isRecommended = false)
        {
            Name = name;
            Description = description;
            ColorHexValues = colorHexValues;
            Category = category;
            IsRecommended = isRecommended;
            CreatedTime = DateTime.Now;
            IsCustom = category == "Custom";
        }

        /// <summary>
        /// 克隆配色方案
        /// </summary>
        /// <returns>新的配色方案实例</returns>
        public ColorScheme Clone()
        {
            return new ColorScheme
            {
                Name = this.Name,
                Description = this.Description,
                ColorHexValues = (string[])this.ColorHexValues?.Clone(),
                Category = this.Category,
                IsRecommended = this.IsRecommended,
                CreatedTime = this.CreatedTime,
                IsCustom = this.IsCustom
            };
        }

        /// <summary>
        /// 转换为字符串表示
        /// </summary>
        /// <returns>字符串表示</returns>
        public override string ToString()
        {
            var colorCount = ColorHexValues?.Length ?? 0;
            var recommended = IsRecommended ? " ⭐" : "";
            return $"{Name} ({colorCount}色){recommended}";
        }
    }
}
