# EasyChartX 配色方案设计器 (C# 版本)

## 📖 项目简介

EasyChartX 配色方案设计器是专为简仪科技 SeeSharp 工具包设计的专业配色工具。本工具提供直观的界面来设计、预览和导出多通道波形的配色方案，完美集成 EasyChartX 控件。

### 🎯 核心特性

- **🎨 11种专业配色方案**：包含 Tableau、D3、经典红蓝等业界标准配色
- **📊 实时波形预览**：使用真实的 EasyChartX 控件进行预览
- **⚙️ 丰富的配置选项**：支持多种波形类型、背景色、网格设置
- **📤 多格式导出**：支持 C#、JSON、CSS、JavaScript 等格式
- **🔄 对比模式**：同时预览两种配色方案的效果
- **🎨 色系分组**：高级配色功能，支持逻辑分组配色
- **💾 配置持久化**：自动保存用户配置和自定义方案

## 🏗️ 项目结构

```
EasyChartX-ColorDesigner-CSharp/
├── EasyChartX.ColorDesigner.sln          # 解决方案文件
├── EasyChartX.ColorDesigner/              # 主项目
│   ├── Models/                            # 数据模型
│   │   ├── ColorScheme.cs                 # 配色方案模型
│   │   ├── WaveformConfig.cs              # 波形配置模型
│   │   └── ExportConfig.cs                # 导出配置模型
│   ├── Managers/                          # 管理器类
│   │   ├── ColorSchemeManager.cs          # 配色方案管理器
│   │   ├── WaveformGenerator.cs           # 波形生成器
│   │   └── ExportManager.cs               # 导出管理器
│   ├── Controls/                          # 自定义控件
│   │   ├── ColorSchemePanel.cs            # 配色方案选择面板
│   │   └── ConfigPanel.cs                 # 配置面板
│   ├── Utils/                             # 工具类
│   │   ├── ColorUtils.cs                  # 颜色工具类
│   │   └── FileUtils.cs                   # 文件工具类
│   ├── Resources/                         # 资源文件
│   │   ├── ColorSchemes.json              # 预设配色方案
│   │   └── Templates/                     # 导出模板
│   └── MainForm.cs                        # 主窗体
├── Dependencies/                          # 依赖库
│   ├── SeeSharpTools.JY.GUI.dll          # EasyChartX 控件库
│   ├── SeeSharpTools.JY.DSP.Fundamental.dll
│   └── SeeSharpTools.JY.ArrayUtility.dll
└── Documentation/                         # 文档
    ├── README.md                          # 项目说明
    ├── UserGuide.md                       # 用户指南
    └── DeveloperGuide.md                  # 开发者指南
```

## 🚀 快速开始

### 系统要求

- **操作系统**：Windows 7 SP1 或更高版本
- **.NET Framework**：4.8 或更高版本
- **Visual Studio**：2017 或更高版本（开发时）
- **内存**：至少 512MB 可用内存
- **磁盘空间**：至少 50MB 可用空间

### 安装步骤

1. **克隆或下载项目**
   ```bash
   git clone [项目地址]
   cd EasyChartX-ColorDesigner-CSharp
   ```

2. **打开解决方案**
   - 使用 Visual Studio 打开 `EasyChartX.ColorDesigner.sln`
   - 或直接运行编译好的 exe 文件

3. **构建项目**
   - 在 Visual Studio 中按 `Ctrl+Shift+B` 构建解决方案
   - 或使用命令行：`msbuild EasyChartX.ColorDesigner.sln`

4. **运行程序**
   - 按 `F5` 运行调试版本
   - 或运行 `Build/Release/EasyChartX.ColorDesigner.exe`

## 🎨 功能特性

### 配色方案管理

#### 预设方案
- **Tableau标准** ⭐：商业图表黄金标准
- **D3标准**：Web数据可视化行业标准
- **D3扩展20色**：支持更多数据系列
- **红蓝经典**：最常用的双色配色
- **专业深色**：适合长时间观看
- **无障碍友好**：考虑色盲用户
- **K/T/R/N/M风格**：特殊用途配色
- **自定义配色**：现代渐变风格

#### 自定义方案
- 创建个人配色方案
- 导入/导出配色配置
- 方案分类管理
- 实时预览效果

### 波形配置

#### 基础设置
- **通道数量**：1-32通道可选
- **波形类型**：正弦波、方波、三角波、混合
- **线条宽度**：1-5像素可调
- **采样点数**：100-10000点可配

#### 背景配置
- **背景色**：白色、黑色、深灰、自定义
- **网格线**：显示/隐藏、透明度、颜色
- **图例**：显示/隐藏、位置、样式

#### 高级功能
- **对比模式**：同时预览两种方案
- **色系分组**：逻辑分组配色
- **动画效果**：实时波形动画
- **智能配色**：根据背景自动调整

### 导出功能

#### 支持格式
- **C# 代码**：可直接集成到 EasyChartX 项目
- **JSON 配置**：通用配置格式
- **CSS 样式**：Web 前端样式
- **JavaScript**：Web 前端脚本

#### 导出选项
- 包含配置信息
- 包含使用示例
- 包含详细注释
- 自定义文件名和路径
- 自动打开导出文件

## 🔧 开发指南

### 核心架构

#### 设计模式
- **MVC 模式**：Model-View-Controller 分离
- **观察者模式**：事件驱动的界面更新
- **工厂模式**：配色方案和波形生成
- **策略模式**：不同的导出格式处理

#### 关键类说明

**ColorScheme**：配色方案数据模型
```csharp
public class ColorScheme
{
    public string Name { get; set; }
    public string[] ColorHexValues { get; set; }
    public Color[] Colors { get; }
    public Color GetColor(int index) { }
}
```

**WaveformConfig**：波形配置模型
```csharp
public class WaveformConfig
{
    public int ChannelCount { get; set; }
    public WaveformType WaveformType { get; set; }
    public BackgroundType BackgroundType { get; set; }
    public Color GetBackgroundColor() { }
}
```

**ColorSchemeManager**：配色方案管理器
```csharp
public class ColorSchemeManager
{
    public ColorScheme CurrentScheme { get; set; }
    public void ApplySchemeToChart(EasyChartX chart, ColorScheme scheme) { }
    public bool AddCustomScheme(ColorScheme scheme) { }
}
```

### 扩展开发

#### 添加新的配色方案
1. 在 `Resources/ColorSchemes.json` 中添加配色数据
2. 或通过 `ColorSchemeManager.AddCustomScheme()` 动态添加

#### 添加新的波形类型
1. 在 `WaveformType` 枚举中添加新类型
2. 在 `WaveformGenerator` 中实现生成逻辑

#### 添加新的导出格式
1. 在 `ExportFormat` 枚举中添加新格式
2. 在 `ExportManager` 中实现导出逻辑
3. 在 `Resources/Templates/` 中添加模板文件

## 📚 使用指南

### 基本操作

1. **选择配色方案**
   - 在左侧面板选择预设方案
   - 点击方案名称即可应用
   - 推荐方案标有 ⭐ 标记

2. **调整波形参数**
   - 设置通道数量（1-32）
   - 选择波形类型
   - 调整线条宽度
   - 配置背景和网格

3. **实时预览**
   - 中央区域显示实时波形预览
   - 参数变化立即反映到预览
   - 支持动画效果

4. **导出配色方案**
   - 点击导出按钮
   - 选择导出格式
   - 确认配置后下载文件

### 高级功能

#### 对比模式
1. 勾选"对比模式"
2. 选择对比的配色方案
3. 同时查看两种方案效果
4. 便于选择最佳配色

#### 色系分组
1. 启用"色系分组"
2. 设置色系数量和每组颜色数
3. 系统自动生成分组配色
4. 适合逻辑分组的数据显示

#### 自定义配色
1. 创建新的配色方案
2. 手动选择颜色值
3. 保存为自定义方案
4. 可导出分享给其他用户

## 🔍 故障排除

### 常见问题

**Q: 程序启动时提示缺少 DLL 文件**
A: 确保 Dependencies 文件夹中的 SeeSharpTools 库文件存在，并且 .NET Framework 4.8 已安装。

**Q: 波形显示异常或不更新**
A: 检查通道数量设置，确保在 1-32 范围内。重启程序或重置配置。

**Q: 导出的 C# 代码无法编译**
A: 确保目标项目已引用 SeeSharpTools 库，并且命名空间正确。

**Q: 配色方案丢失**
A: 检查 Resources/ColorSchemes.json 文件是否存在。如果损坏，删除该文件重启程序会重新生成默认配置。

**Q: 自定义配色方案无法保存**
A: 确保程序有写入权限，检查磁盘空间是否充足。

### 性能优化

- **减少通道数量**：对于预览，使用较少的通道数量
- **降低采样率**：减少采样点数可提高响应速度
- **关闭动画**：在低性能设备上关闭动画效果
- **定期清理**：删除不需要的自定义配色方案

## 📄 许可证

本项目为简仪科技内部工具，仅供内部使用。

## 🤝 贡献

欢迎提交问题报告和功能建议。

## 📞 技术支持

- **开发团队**：简仪科技 SeeSharp 工具包团队
- **技术支持**：[support@jytek.com]
- **官方网站**：[https://www.jytek.com]

---

**版本信息**：v1.0.0  
**最后更新**：2025年1月5日  
**兼容性**：EasyChartX v2.0+, SeeSharpTools v2.0+
