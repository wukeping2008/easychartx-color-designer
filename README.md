# EasyChartX Waveform Color Scheme Designer

## 📖 Project Overview

The EasyChartX Waveform Color Scheme Designer is a professional color scheme tool specifically designed for SeeSharp Toolkit by JYTEK. This tool focuses on providing scientific, aesthetic, and practical multi-channel waveform color schemes for EasyChartX controls, with direct code output capabilities for seamless integration.

### 🎯 Tool Positioning

- **Professional Color Design**: Specialized in EasyChartX color scheme design and code generation
- **Preview Functionality**: Simulated waveforms for color scheme preview only, not for real signal processing
- **Data Separation**: Real project waveforms are determined by sampling frequency and actual signal data
- **Code Integration**: Exported color codes can be directly integrated into your EasyChartX projects

## ✨ Core Features

### 🏆 Standard Color Schemes
- **D3 Category10** ⭐: D3.js standard colors, scientifically designed with optimal distinction in Lab color space
- **Tableau Standard**: Based on D3 Category10, widely used in commercial charts
- **D3 Extended 20**: D3.js extended palette with 10 main colors + 10 corresponding light variants, suitable for large data series

### 📊 Classic Color Schemes
- **Red-Blue Classic**: Most commonly used red-blue palette, suitable for 1-2 line applications
- **Professional Dark**: Modern professional colors, suitable for extended viewing

### 🎨 Special Purpose Colors
- **Accessibility Friendly**: Color-blind friendly palette
- **K Style**: High contrast colors, easy to distinguish
- **T Style**: Bright vivid colors, suitable for dark backgrounds
- **R Style**: Modern color scheme balancing professionalism and aesthetics
- **N Style**: Pure color system with high saturation
- **M Style**: Widely used in scientific computing
- **Custom Colors**: Modern gradient style, beautiful and professional

### 🔧 Advanced Features
- **Comparison Mode**: Side-by-side preview of two different color schemes
- **Color Grouping**: Logical grouping with same color family variations for grouped data
- **Background Adaptation**: Support for white, black, dark gray, and custom backgrounds
- **Grid Configuration**: Adjustable transparency and color grid system
- **Waveform Parameters**: Support for 1-12 channels with multiple waveform types

### 📤 Export Functions
- **JSON Format**: Standard color data format
- **CSS Format**: Web-ready stylesheet files
- **JavaScript Format**: Frontend-ready code modules
- **C# Format**: Complete EasyChartX configuration code for direct integration

## 🚀 Quick Start

### Basic Operation Flow

1. **Select Color Scheme**
   - Choose appropriate colors from the left panel
   - Real-time waveform preview

2. **Adjust Parameters**
   - Set channel count (1-12 channels)
   - Choose waveform type (sine, square, triangle, mixed)
   - Adjust background and grid settings

3. **Preview Effects**
   - View real-time waveform animation
   - Enable comparison mode to compare different schemes

4. **Export Code**
   - Click export buttons
   - Confirm configuration
   - Download code files in desired format

## 📋 Detailed Operation Guide

### 🎨 Background Configuration

#### Background Color Options
- **White**: EasyChartX default background, suitable for most applications
- **Black**: Classic oscilloscope style with professional appearance
- **Dark Gray**: Modern interface style with eye-friendly effects
- **Custom**: Choose any color as background

#### Grid Settings
- **Display Control**: Independent control of grid line visibility
- **Transparency Adjustment**: 10%-100% adjustable for different visual needs
- **Color Modes**:
  - Auto Adapt: Intelligent grid color selection based on background
  - Light: #cccccc, suitable for dark backgrounds
  - Dark: #666666, suitable for light backgrounds
  - Custom: Choose any color

### 📊 Waveform Parameters

#### Channel Count vs Line Count
- **Channel Count**: Data channels affecting vertical waveform layout
  - Determines vertical distribution and spacing of waveforms
  - Affects vertical space allocation for each waveform
  
- **Line Count**: Actual drawn lines, -1 = auto-match channel count
  - Can be less than channel count (partial display)
  - Allows showing partial data with reserved layout

#### Waveform Types
- **Sine Wave**: Standard sine wave, smooth and continuous
- **Square Wave**: Standard square wave with sharp high/low transitions
- **Triangle Wave**: Standard triangle wave with linear rise/fall
- **Mixed Waveforms**: Ordered combination of three standard waveforms

### 🎨 Color Grouping (Advanced Feature)

#### Functionality
Color grouping categorizes colors by hue, with each group containing variations of the same hue in different brightness and saturation levels.

#### Parameter Configuration
- **Formula**: Group Count × Colors per Group = Total Colors
- **Principle**: Total colors should ≥ Channel count, otherwise cycling occurs
- **Example**: 2 groups × 3 colors = 6 colors, suitable for up to 6 channels

#### Application Scenarios
- **Device Grouping**: Device A (blue family) vs Device B (orange family)
- **Quality Classification**: Pass group (green family) vs Fail group (red family)
- **A/B Testing**: Group A uses one color family, Group B uses another

### 🔄 Comparison Mode

#### Functionality
Comparison mode allows simultaneous preview of two different color schemes for quick comparison and optimal selection.

#### Usage
1. Check "Comparison Mode" checkbox
2. Select comparison color scheme
3. Left panel shows current scheme, right panel shows comparison scheme
4. Real-time visual difference comparison

## 📤 Export Function Details

### Export Confirmation
Each export displays detailed confirmation information:
- Export format type
- Current color scheme details
- Configuration parameters (channels, background, waveform type, etc.)
- Comparison mode status (if enabled)
- Color grouping information (if enabled)
- Specific color value list

### Export Formats

#### JSON Format
```json
{
  "name": "Tableau Standard",
  "description": "Based on D3 Category10, widely used in commercial charts",
  "colors": ["#1f77b4", "#ff7f0e", "#2ca02c", "#d62728"],
  "channelCount": 4,
  "exportDate": "2025-01-05T08:30:00.000Z"
}
```

#### CSS Format
```css
/* EasyChartX Tableau Standard Color Scheme */
:root {
  --channel-1-color: #1f77b4;
  --channel-2-color: #ff7f0e;
  --channel-3-color: #2ca02c;
  --channel-4-color: #d62728;
}

.waveform-channel-1 { stroke: var(--channel-1-color); }
.waveform-channel-2 { stroke: var(--channel-2-color); }
```

#### JavaScript Format
```javascript
// EasyChartX Tableau Standard Color Scheme
const channelColors = [
    "#1f77b4", // Channel 1
    "#ff7f0e", // Channel 2
    "#2ca02c", // Channel 3
    "#d62728"  // Channel 4
];

// Usage example:
// chart.setChannelColor(channelIndex, channelColors[channelIndex]);
```

#### C# Format
```csharp
// EasyChartX Tableau Standard Color Scheme
private void GuiRefresh_Click(object sender, EventArgs e)
{
    // Set chart background and style
    easyChartX1.BackColor = Color.Black;
    easyChartX1.ChartAreaBackColor = Color.Black;
    
    // Get curve color table
    var colorTable = new Color[4];
    colorTable[0] = Color.FromArgb(31, 119, 180);  // #1f77b4
    colorTable[1] = Color.FromArgb(255, 127, 14);  // #ff7f0e
    colorTable[2] = Color.FromArgb(44, 160, 44);   // #2ca02c
    colorTable[3] = Color.FromArgb(214, 39, 40);   // #d62728
    
    // Set curve colors
    int numOfWaveforms = 4;
    easyChartX1.Series.AdaptSeriesCount(numOfWaveforms);
    for (int i = 0; i < numOfWaveforms; i++)
    {
        easyChartX1.Series[i].Color = colorTable[i];
    }
    
    // Plot waveform data
    easyChartX1.Plot(allWaveforms, 0, 1);
}
```

## 🎯 Best Practices

### Color Scheme Selection Guide

#### Channel Count Guidelines
- **1-2 channels**: Red-Blue Classic, any standard scheme
- **3-6 channels**: Tableau Standard, D3 Standard
- **7-12 channels**: D3 Extended 20
- **Many channels**: Enable color grouping feature

#### Application Scenario Matching
- **Business Reports**: Tableau Standard
- **Web Applications**: D3 Standard
- **Scientific Computing**: M Style
- **Industrial Monitoring**: Professional Dark, Accessibility Friendly
- **Presentations**: K Style, T Style

#### Background Color Selection
- **White Background**: Suitable for printing, documents, reports
- **Black Background**: Suitable for control rooms, dark environments
- **Dark Gray Background**: Suitable for extended viewing, modern interfaces
- **Custom Background**: Match corporate VI, special requirements

### Color Grouping Usage Guide

#### Suitable Scenarios
- Multi-device monitoring (grouped by device)
- Quality analysis (grouped by grade)
- A/B testing comparisons
- Multi-stage process monitoring
- Categorical data display

#### Configuration Recommendations
```
Recommended Settings:
• Ensure Group Count × Colors per Group ≥ Channel Count
• Avoid excessive cycling that causes color confusion
• Choose appropriate group count based on actual application

Common Configurations:
• 2 groups × 3 colors = 6 colors (suitable for up to 6 channels)
• 3 groups × 4 colors = 12 colors (suitable for up to 12 channels)
• 4 groups × 3 colors = 12 colors (suitable for complex categorization)
```

## 🔧 Technical Specifications

### Supported Browsers
- Chrome 80+
- Firefox 75+
- Safari 13+
- Edge 80+

### Performance Features
- Responsive design supporting desktop, tablet, mobile
- 60fps smooth animation
- High-resolution screen optimization
- Low CPU usage

### File Formats
- Input: No input files required
- Output: JSON, CSS, JavaScript, C#
- Encoding: UTF-8

## 🆘 FAQ

### Q: Why do my colors look different in actual projects?
A: This tool provides color schemes. Actual display effects are influenced by:
- Monitor color calibration
- EasyChartX control rendering settings
- Operating system color management
- Actual data characteristics

### Q: When should I use color grouping?
A: Use when your data has logical grouping relationships, such as:
- Multiple devices with similar parameters
- Different quality grade data
- Comparative experiment grouped data

### Q: How do I use the exported C# code?
A: Copy the exported code into your WinForms project, typically in button click events or initialization methods. The code includes complete EasyChartX configuration and can run directly.

### Q: Which scheme is exported in comparison mode?
A: In comparison mode, the currently selected main scheme (left panel) is always exported, not the comparison scheme.

### Q: How do I choose the appropriate channel count?
A: Channel count should equal the number of data series you need to display. If uncertain, choose a slightly larger value and control actual display through the "Line Count" parameter.

## 📝 Changelog

### v1.1 (July 5, 2025)
**🔬 Color Science Optimization Update**

#### Core Adjustments
- **Default Color Scheme**: Set D3 Category10 as default recommended scheme with ⭐ indicator
- **Scheme Reordering**: Moved D3 Category10 to top of standard color schemes
- **Category20 Optimization**: Reorganized D3 Extended 20 colors with 10 main + 10 light variants

#### Description Enhancements
- **D3 Category10**: Emphasized "scientifically designed with optimal distinction in Lab color space"
- **Tableau Standard**: Updated to "Based on D3 Category10, widely used in commercial charts"
- **Scientific Emphasis**: Highlighted Lab color space distance and color distinction properties

#### UI Experience Improvements
- **Custom Color Pickers**: Optimized display effects for custom background and grid color selectors
- **Color Display Enhancement**: Selected colors now display directly in picker controls for cleaner interface
- **Style Consistency**: Unified color picker styling for improved visual effects

#### Technical Improvements
- Optimized color scheme selection logic using most scientific color standards
- Maintained backward compatibility for familiar color schemes
- Enhanced tool professionalism and scientific foundation
- Improved user interface interaction experience

### v1.0 (January 5, 2025)
**🎉 Initial Release**

#### Core Features
- 11 professional color schemes (standard, classic, special purpose)
- Real-time waveform preview and animation
- Multi-format export (JSON, CSS, JavaScript, C#)
- Comparison mode and color grouping advanced features

#### Technical Features
- Responsive design supporting multiple devices
- High-performance 60fps animation
- Complete EasyChartX integration code generation

## 📞 Technical Support

If you encounter issues or have suggestions for improvement, please contact the JYTEK SeeSharp Toolkit technical support team.

## 🌐 Language Versions

- **English**: README.md (this file)
- **中文**: README_CN.md

---

**Version**: v1.1  
**Update Date**: July 5, 2025  
**Development Team**: JYTEK SeeSharp Toolkit Team  
**Compatible Product**: EasyChartX Control

## 📄 License

This project is open source. Please refer to the license file for specific terms.

## 🤝 Contributing

We welcome contributions! Please feel free to submit issues and pull requests.

## ⭐ Star History

If this project helps you, please consider giving it a star!
