using System;
using System.Drawing;
using EasyChartX.ColorDesigner.Models;
using SeeSharpTools.JY.DSP.Fundamental;
using SeeSharpTools.JY.ArrayUtility;
using SeeSharpTools.JY.GUI;

namespace EasyChartX.ColorDesigner.Managers
{
    /// <summary>
    /// 波形生成器
    /// </summary>
    public class WaveformGenerator
    {
        private Random _random;
        private double _timeOffset;

        /// <summary>
        /// 构造函数
        /// </summary>
        public WaveformGenerator()
        {
            _random = new Random();
            _timeOffset = 0;
        }

        /// <summary>
        /// 生成多通道波形数据
        /// </summary>
        /// <param name="config">波形配置</param>
        /// <returns>波形数据 [samples, channels]</returns>
        public double[,] GenerateWaveforms(WaveformConfig config)
        {
            if (config == null || !config.IsValid())
                throw new ArgumentException("无效的波形配置");

            var waveforms = new double[config.SampleCount, config.ChannelCount];
            var oneChannelWaveform = new double[config.SampleCount];

            for (int channel = 0; channel < config.ChannelCount; channel++)
            {
                GenerateSingleChannelWaveform(oneChannelWaveform, config, channel);
                
                // 复制到多通道数组
                for (int sample = 0; sample < config.SampleCount; sample++)
                {
                    waveforms[sample, channel] = oneChannelWaveform[sample];
                }
            }

            return waveforms;
        }

        /// <summary>
        /// 生成单通道波形
        /// </summary>
        /// <param name="waveform">输出波形数组</param>
        /// <param name="config">波形配置</param>
        /// <param name="channelIndex">通道索引</param>
        private void GenerateSingleChannelWaveform(double[] waveform, WaveformConfig config, int channelIndex)
        {
            // 固定频率：1Hz 基础频率，每个通道增加 0.2Hz
            var frequency = 1.0 + channelIndex * 0.2;
            
            // 固定幅度：1.0V，范围在 -1V 到 +1V
            var amplitude = 1.0;
            
            // 相位偏移：每个通道相差 30 度
            var phaseOffset = channelIndex * Math.PI / 6;

            // 只生成正弦波
            GenerateSineWave(waveform, frequency, amplitude, phaseOffset);
        }

        /// <summary>
        /// 生成正弦波
        /// </summary>
        /// <param name="waveform">输出数组</param>
        /// <param name="frequency">频率</param>
        /// <param name="amplitude">幅度</param>
        /// <param name="phaseOffset">相位偏移</param>
        private void GenerateSineWave(double[] waveform, double frequency, double amplitude, double phaseOffset)
        {
            // 使用自定义实现
            for (int i = 0; i < waveform.Length; i++)
            {
                var t = (double)i / waveform.Length * 2 * Math.PI * frequency;
                waveform[i] = amplitude * Math.Sin(t + phaseOffset + _timeOffset);
            }
        }

        /// <summary>
        /// 生成方波
        /// </summary>
        /// <param name="waveform">输出数组</param>
        /// <param name="frequency">频率</param>
        /// <param name="amplitude">幅度</param>
        /// <param name="phaseOffset">相位偏移</param>
        private void GenerateSquareWave(double[] waveform, double frequency, double amplitude, double phaseOffset)
        {
            try
            {
                // 使用SeeSharpTools生成方波
                Generation.SquareWave(ref waveform, amplitude, frequency, 0.5, phaseOffset + _timeOffset);
            }
            catch
            {
                // 如果SeeSharpTools方法失败，使用自定义实现
                for (int i = 0; i < waveform.Length; i++)
                {
                    var t = (double)i / waveform.Length * 2 * Math.PI * frequency;
                    var sineValue = Math.Sin(t + phaseOffset + _timeOffset);
                    waveform[i] = amplitude * Math.Sign(sineValue);
                }
            }
        }

        /// <summary>
        /// 生成三角波
        /// </summary>
        /// <param name="waveform">输出数组</param>
        /// <param name="frequency">频率</param>
        /// <param name="amplitude">幅度</param>
        /// <param name="phaseOffset">相位偏移</param>
        private void GenerateTriangleWave(double[] waveform, double frequency, double amplitude, double phaseOffset)
        {
            // 使用自定义实现
            for (int i = 0; i < waveform.Length; i++)
            {
                var t = (double)i / waveform.Length * frequency + (phaseOffset + _timeOffset) / (2 * Math.PI);
                var phase = t - Math.Floor(t); // 0 to 1
                
                if (phase < 0.5)
                    waveform[i] = amplitude * (4 * phase - 1);
                else
                    waveform[i] = amplitude * (3 - 4 * phase);
            }
        }

        /// <summary>
        /// 生成混合波形
        /// </summary>
        /// <param name="waveform">输出数组</param>
        /// <param name="config">配置</param>
        /// <param name="channelIndex">通道索引</param>
        /// <param name="frequency">频率</param>
        /// <param name="amplitude">幅度</param>
        /// <param name="phaseOffset">相位偏移</param>
        private void GenerateMixedWaveform(double[] waveform, WaveformConfig config, int channelIndex, 
            double frequency, double amplitude, double phaseOffset)
        {
            var waveType = channelIndex % 3;
            
            switch (waveType)
            {
                case 0:
                    GenerateSineWave(waveform, frequency, amplitude, phaseOffset);
                    break;
                case 1:
                    GenerateSquareWave(waveform, frequency * 0.8, amplitude * 0.9, phaseOffset);
                    break;
                case 2:
                    GenerateTriangleWave(waveform, frequency * 1.2, amplitude * 0.85, phaseOffset);
                    break;
            }
        }

        /// <summary>
        /// 添加噪声
        /// </summary>
        /// <param name="waveform">波形数组</param>
        /// <param name="noiseLevel">噪声级别</param>
        private void AddNoise(double[] waveform, double noiseLevel)
        {
            for (int i = 0; i < waveform.Length; i++)
            {
                var noise = (_random.NextDouble() - 0.5) * 2 * noiseLevel;
                waveform[i] += noise;
            }
        }

        /// <summary>
        /// 更新时间偏移（用于动画效果）
        /// </summary>
        /// <param name="deltaTime">时间增量</param>
        public void UpdateTimeOffset(double deltaTime)
        {
            _timeOffset += deltaTime;
            
            // 防止时间偏移过大
            if (_timeOffset > 2 * Math.PI * 100)
            {
                _timeOffset -= 2 * Math.PI * 100;
            }
        }

        /// <summary>
        /// 重置时间偏移
        /// </summary>
        public void ResetTimeOffset()
        {
            _timeOffset = 0;
        }

        /// <summary>
        /// 更新EasyChartX控件的波形数据
        /// </summary>
        /// <param name="chart">EasyChartX控件</param>
        /// <param name="waveforms">波形数据</param>
        /// <param name="config">配置</param>
        public void UpdateChart(SeeSharpTools.JY.GUI.EasyChartX chart, double[,] waveforms, WaveformConfig config)
        {
            try
            {
                if (chart == null || waveforms == null)
                    return;

                // 确保通道数量匹配
                var channelCount = waveforms.GetLength(1);
                if (chart.Series.Count != channelCount)
                {
                    chart.Series.AdaptSeriesCount(channelCount);
                }

                // 绘制波形
                chart.Plot(waveforms, 0, 1.0 / config.SampleCount, SeeSharpTools.JY.GUI.MajorOrder.Column);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"更新图表失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 生成测试数据（用于调试）
        /// </summary>
        /// <param name="channelCount">通道数量</param>
        /// <param name="sampleCount">采样点数</param>
        /// <returns>测试波形数据</returns>
        public double[,] GenerateTestData(int channelCount, int sampleCount)
        {
            var config = new WaveformConfig
            {
                ChannelCount = channelCount,
                SampleCount = sampleCount,
                WaveformType = WaveformType.Mixed
            };

            return GenerateWaveforms(config);
        }

        /// <summary>
        /// 获取波形统计信息
        /// </summary>
        /// <param name="waveforms">波形数据</param>
        /// <param name="channelIndex">通道索引</param>
        /// <returns>统计信息字符串</returns>
        public string GetWaveformStats(double[,] waveforms, int channelIndex)
        {
            if (waveforms == null || channelIndex >= waveforms.GetLength(1))
                return "无效数据";

            var sampleCount = waveforms.GetLength(0);
            double min = double.MaxValue;
            double max = double.MinValue;
            double sum = 0;

            for (int i = 0; i < sampleCount; i++)
            {
                var value = waveforms[i, channelIndex];
                min = Math.Min(min, value);
                max = Math.Max(max, value);
                sum += value;
            }

            var average = sum / sampleCount;
            var range = max - min;

            return $"通道{channelIndex + 1}: 最小值={min:F3}, 最大值={max:F3}, 平均值={average:F3}, 范围={range:F3}";
        }
    }
}
