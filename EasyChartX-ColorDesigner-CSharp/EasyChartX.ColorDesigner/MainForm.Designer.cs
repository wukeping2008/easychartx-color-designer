namespace EasyChartX.ColorDesigner
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            SeeSharpTools.JY.GUI.EasyChartXSeries easyChartXSeries1 = new SeeSharpTools.JY.GUI.EasyChartXSeries();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.splitContainerLeft = new System.Windows.Forms.SplitContainer();
            this.groupBoxColorSchemes = new System.Windows.Forms.GroupBox();
            this._listBoxColorSchemes = new System.Windows.Forms.ListBox();
            this.groupBoxColorInfo = new System.Windows.Forms.GroupBox();
            this._richTextBoxColorInfo = new System.Windows.Forms.RichTextBox();
            this.splitContainerRight = new System.Windows.Forms.SplitContainer();
            this.groupBoxPreview = new System.Windows.Forms.GroupBox();
            this._easyChartX = new SeeSharpTools.JY.GUI.EasyChartX();
            this.groupBoxConfig = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelConfig = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxWaveform = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelWaveform = new System.Windows.Forms.TableLayoutPanel();
            this.labelChannels = new System.Windows.Forms.Label();
            this._numericUpDownChannels = new System.Windows.Forms.NumericUpDown();
            this.labelWaveformType = new System.Windows.Forms.Label();
            this._comboBoxWaveformType = new System.Windows.Forms.ComboBox();
            this.labelLineWidth = new System.Windows.Forms.Label();
            this._trackBarLineWidth = new System.Windows.Forms.TrackBar();
            this._labelLineWidthValue = new System.Windows.Forms.Label();
            this.groupBoxBackground = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelBackground = new System.Windows.Forms.TableLayoutPanel();
            this.labelBackground = new System.Windows.Forms.Label();
            this._comboBoxBackground = new System.Windows.Forms.ComboBox();
            this._checkBoxShowGrid = new System.Windows.Forms.CheckBox();
            this.labelGridOpacity = new System.Windows.Forms.Label();
            this._trackBarGridOpacity = new System.Windows.Forms.TrackBar();
            this._labelGridOpacityValue = new System.Windows.Forms.Label();
            this.groupBoxDisplay = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelDisplay = new System.Windows.Forms.TableLayoutPanel();
            this._checkBoxShowLegend = new System.Windows.Forms.CheckBox();
            this._checkBoxEnableAnimation = new System.Windows.Forms.CheckBox();
            this.groupBoxExport = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelExport = new System.Windows.Forms.TableLayoutPanel();
            this._buttonExportCSharp = new System.Windows.Forms.Button();
            this._buttonExportJson = new System.Windows.Forms.Button();
            this._buttonExportCss = new System.Windows.Forms.Button();
            this._buttonExportJs = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeft)).BeginInit();
            this.splitContainerLeft.Panel1.SuspendLayout();
            this.splitContainerLeft.Panel2.SuspendLayout();
            this.splitContainerLeft.SuspendLayout();
            this.groupBoxColorSchemes.SuspendLayout();
            this.groupBoxColorInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRight)).BeginInit();
            this.splitContainerRight.Panel1.SuspendLayout();
            this.splitContainerRight.Panel2.SuspendLayout();
            this.splitContainerRight.SuspendLayout();
            this.groupBoxPreview.SuspendLayout();
            this.groupBoxConfig.SuspendLayout();
            this.tableLayoutPanelConfig.SuspendLayout();
            this.groupBoxWaveform.SuspendLayout();
            this.tableLayoutPanelWaveform.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numericUpDownChannels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._trackBarLineWidth)).BeginInit();
            this.groupBoxBackground.SuspendLayout();
            this.tableLayoutPanelBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._trackBarGridOpacity)).BeginInit();
            this.groupBoxDisplay.SuspendLayout();
            this.tableLayoutPanelDisplay.SuspendLayout();
            this.groupBoxExport.SuspendLayout();
            this.tableLayoutPanelExport.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.splitContainerLeft);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainerRight);
            this.splitContainerMain.Size = new System.Drawing.Size(1200, 800);
            this.splitContainerMain.SplitterDistance = 300;
            this.splitContainerMain.TabIndex = 0;
            // 
            // splitContainerLeft
            // 
            this.splitContainerLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerLeft.Location = new System.Drawing.Point(0, 0);
            this.splitContainerLeft.Name = "splitContainerLeft";
            this.splitContainerLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerLeft.Panel1
            // 
            this.splitContainerLeft.Panel1.Controls.Add(this.groupBoxColorSchemes);
            // 
            // splitContainerLeft.Panel2
            // 
            this.splitContainerLeft.Panel2.Controls.Add(this.groupBoxColorInfo);
            this.splitContainerLeft.Size = new System.Drawing.Size(300, 800);
            this.splitContainerLeft.SplitterDistance = 500;
            this.splitContainerLeft.TabIndex = 0;
            // 
            // groupBoxColorSchemes
            // 
            this.groupBoxColorSchemes.Controls.Add(this._listBoxColorSchemes);
            this.groupBoxColorSchemes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxColorSchemes.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxColorSchemes.Location = new System.Drawing.Point(0, 0);
            this.groupBoxColorSchemes.Name = "groupBoxColorSchemes";
            this.groupBoxColorSchemes.Size = new System.Drawing.Size(300, 500);
            this.groupBoxColorSchemes.TabIndex = 0;
            this.groupBoxColorSchemes.TabStop = false;
            this.groupBoxColorSchemes.Text = "🎨 配色方案";
            // 
            // _listBoxColorSchemes
            // 
            this._listBoxColorSchemes.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listBoxColorSchemes.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._listBoxColorSchemes.FormattingEnabled = true;
            this._listBoxColorSchemes.ItemHeight = 17;
            this._listBoxColorSchemes.Location = new System.Drawing.Point(3, 19);
            this._listBoxColorSchemes.Name = "_listBoxColorSchemes";
            this._listBoxColorSchemes.Size = new System.Drawing.Size(294, 478);
            this._listBoxColorSchemes.TabIndex = 0;
            this._listBoxColorSchemes.SelectedIndexChanged += new System.EventHandler(this.ListBoxColorSchemes_SelectedIndexChanged);
            // 
            // groupBoxColorInfo
            // 
            this.groupBoxColorInfo.Controls.Add(this._richTextBoxColorInfo);
            this.groupBoxColorInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxColorInfo.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxColorInfo.Location = new System.Drawing.Point(0, 0);
            this.groupBoxColorInfo.Name = "groupBoxColorInfo";
            this.groupBoxColorInfo.Size = new System.Drawing.Size(300, 296);
            this.groupBoxColorInfo.TabIndex = 0;
            this.groupBoxColorInfo.TabStop = false;
            this.groupBoxColorInfo.Text = "📊 颜色信息";
            // 
            // _richTextBoxColorInfo
            // 
            this._richTextBoxColorInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this._richTextBoxColorInfo.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._richTextBoxColorInfo.Location = new System.Drawing.Point(3, 19);
            this._richTextBoxColorInfo.Name = "_richTextBoxColorInfo";
            this._richTextBoxColorInfo.ReadOnly = true;
            this._richTextBoxColorInfo.Size = new System.Drawing.Size(294, 274);
            this._richTextBoxColorInfo.TabIndex = 0;
            this._richTextBoxColorInfo.Text = "";
            // 
            // splitContainerRight
            // 
            this.splitContainerRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerRight.Location = new System.Drawing.Point(0, 0);
            this.splitContainerRight.Name = "splitContainerRight";
            // 
            // splitContainerRight.Panel1
            // 
            this.splitContainerRight.Panel1.Controls.Add(this.groupBoxPreview);
            // 
            // splitContainerRight.Panel2
            // 
            this.splitContainerRight.Panel2.Controls.Add(this.groupBoxConfig);
            this.splitContainerRight.Size = new System.Drawing.Size(896, 800);
            this.splitContainerRight.SplitterDistance = 596;
            this.splitContainerRight.TabIndex = 0;
            // 
            // groupBoxPreview
            // 
            this.groupBoxPreview.Controls.Add(this._easyChartX);
            this.groupBoxPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPreview.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxPreview.Location = new System.Drawing.Point(0, 0);
            this.groupBoxPreview.Name = "groupBoxPreview";
            this.groupBoxPreview.Size = new System.Drawing.Size(596, 800);
            this.groupBoxPreview.TabIndex = 0;
            this.groupBoxPreview.TabStop = false;
            this.groupBoxPreview.Text = "📈 实时预览";
            // 
            // _easyChartX
            // 
            this._easyChartX.AutoClear = true;
            this._easyChartX.AxisX.AutoScale = true;
            this._easyChartX.AxisX.AutoScalingMode = SeeSharpTools.JY.GUI.EasyChartXAxis.AutoScaleMode.ByWholeNumbers;
            this._easyChartX.AxisX.AutoZoomReset = false;
            this._easyChartX.AxisX.Color = System.Drawing.Color.Black;
            this._easyChartX.AxisX.InitWithScaleView = false;
            this._easyChartX.AxisX.IsLogarithmic = false;
            this._easyChartX.AxisX.LabelAngle = 0;
            this._easyChartX.AxisX.LabelEnabled = true;
            this._easyChartX.AxisX.LabelFormat = null;
            this._easyChartX.AxisX.LogarithmBase = 10D;
            this._easyChartX.AxisX.LogLabelStyle = SeeSharpTools.JY.GUI.EasyChartXAxis.LogarithmicLabelStyle.E2;
            this._easyChartX.AxisX.MajorGridColor = System.Drawing.Color.Black;
            this._easyChartX.AxisX.MajorGridCount = -1;
            this._easyChartX.AxisX.MajorGridEnabled = true;
            this._easyChartX.AxisX.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this._easyChartX.AxisX.MaxGridCountPerPixel = 0.012D;
            this._easyChartX.AxisX.Maximum = 1000D;
            this._easyChartX.AxisX.MinGridCountPerPixel = 0.004D;
            this._easyChartX.AxisX.Minimum = 0D;
            this._easyChartX.AxisX.MinorGridColor = System.Drawing.Color.DimGray;
            this._easyChartX.AxisX.MinorGridEnabled = false;
            this._easyChartX.AxisX.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this._easyChartX.AxisX.ShowLogarithmicLines = false;
            this._easyChartX.AxisX.TickLineColor = System.Drawing.Color.Black;
            this._easyChartX.AxisX.TickWidth = 1F;
            this._easyChartX.AxisX.Title = "";
            this._easyChartX.AxisX.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this._easyChartX.AxisX.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this._easyChartX.AxisX.ViewMaximum = 1000D;
            this._easyChartX.AxisX.ViewMinimum = 0D;
            this._easyChartX.AxisX2.AutoScale = true;
            this._easyChartX.AxisX2.AutoScalingMode = SeeSharpTools.JY.GUI.EasyChartXAxis.AutoScaleMode.ByWholeNumbers;
            this._easyChartX.AxisX2.AutoZoomReset = false;
            this._easyChartX.AxisX2.Color = System.Drawing.Color.Black;
            this._easyChartX.AxisX2.InitWithScaleView = false;
            this._easyChartX.AxisX2.IsLogarithmic = false;
            this._easyChartX.AxisX2.LabelAngle = 0;
            this._easyChartX.AxisX2.LabelEnabled = true;
            this._easyChartX.AxisX2.LabelFormat = null;
            this._easyChartX.AxisX2.LogarithmBase = 10D;
            this._easyChartX.AxisX2.LogLabelStyle = SeeSharpTools.JY.GUI.EasyChartXAxis.LogarithmicLabelStyle.E2;
            this._easyChartX.AxisX2.MajorGridColor = System.Drawing.Color.Black;
            this._easyChartX.AxisX2.MajorGridCount = -1;
            this._easyChartX.AxisX2.MajorGridEnabled = true;
            this._easyChartX.AxisX2.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this._easyChartX.AxisX2.MaxGridCountPerPixel = 0.012D;
            this._easyChartX.AxisX2.Maximum = 1000D;
            this._easyChartX.AxisX2.MinGridCountPerPixel = 0.004D;
            this._easyChartX.AxisX2.Minimum = 0D;
            this._easyChartX.AxisX2.MinorGridColor = System.Drawing.Color.DimGray;
            this._easyChartX.AxisX2.MinorGridEnabled = false;
            this._easyChartX.AxisX2.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this._easyChartX.AxisX2.ShowLogarithmicLines = false;
            this._easyChartX.AxisX2.TickLineColor = System.Drawing.Color.Black;
            this._easyChartX.AxisX2.TickWidth = 1F;
            this._easyChartX.AxisX2.Title = "";
            this._easyChartX.AxisX2.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this._easyChartX.AxisX2.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this._easyChartX.AxisX2.ViewMaximum = 1000D;
            this._easyChartX.AxisX2.ViewMinimum = 0D;
            this._easyChartX.AxisY.AutoScale = true;
            this._easyChartX.AxisY.AutoScalingMode = SeeSharpTools.JY.GUI.EasyChartXAxis.AutoScaleMode.ByWholeNumbers;
            this._easyChartX.AxisY.AutoZoomReset = false;
            this._easyChartX.AxisY.Color = System.Drawing.Color.Black;
            this._easyChartX.AxisY.InitWithScaleView = false;
            this._easyChartX.AxisY.IsLogarithmic = false;
            this._easyChartX.AxisY.LabelAngle = 0;
            this._easyChartX.AxisY.LabelEnabled = true;
            this._easyChartX.AxisY.LabelFormat = null;
            this._easyChartX.AxisY.LogarithmBase = 10D;
            this._easyChartX.AxisY.LogLabelStyle = SeeSharpTools.JY.GUI.EasyChartXAxis.LogarithmicLabelStyle.E2;
            this._easyChartX.AxisY.MajorGridColor = System.Drawing.Color.Black;
            this._easyChartX.AxisY.MajorGridCount = 10;
            this._easyChartX.AxisY.MajorGridEnabled = true;
            this._easyChartX.AxisY.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this._easyChartX.AxisY.MaxGridCountPerPixel = 0.012D;
            this._easyChartX.AxisY.Maximum = 3.5D;
            this._easyChartX.AxisY.MinGridCountPerPixel = 0.004D;
            this._easyChartX.AxisY.Minimum = 0.5D;
            this._easyChartX.AxisY.MinorGridColor = System.Drawing.Color.DimGray;
            this._easyChartX.AxisY.MinorGridEnabled = false;
            this._easyChartX.AxisY.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this._easyChartX.AxisY.ShowLogarithmicLines = false;
            this._easyChartX.AxisY.TickLineColor = System.Drawing.Color.Black;
            this._easyChartX.AxisY.TickWidth = 1F;
            this._easyChartX.AxisY.Title = "";
            this._easyChartX.AxisY.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this._easyChartX.AxisY.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this._easyChartX.AxisY.ViewMaximum = 3.5D;
            this._easyChartX.AxisY.ViewMinimum = 0.5D;
            this._easyChartX.AxisY2.AutoScale = true;
            this._easyChartX.AxisY2.AutoScalingMode = SeeSharpTools.JY.GUI.EasyChartXAxis.AutoScaleMode.ByWholeNumbers;
            this._easyChartX.AxisY2.AutoZoomReset = false;
            this._easyChartX.AxisY2.Color = System.Drawing.Color.Black;
            this._easyChartX.AxisY2.InitWithScaleView = false;
            this._easyChartX.AxisY2.IsLogarithmic = false;
            this._easyChartX.AxisY2.LabelAngle = 0;
            this._easyChartX.AxisY2.LabelEnabled = true;
            this._easyChartX.AxisY2.LabelFormat = null;
            this._easyChartX.AxisY2.LogarithmBase = 10D;
            this._easyChartX.AxisY2.LogLabelStyle = SeeSharpTools.JY.GUI.EasyChartXAxis.LogarithmicLabelStyle.E2;
            this._easyChartX.AxisY2.MajorGridColor = System.Drawing.Color.Black;
            this._easyChartX.AxisY2.MajorGridCount = 10;
            this._easyChartX.AxisY2.MajorGridEnabled = true;
            this._easyChartX.AxisY2.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this._easyChartX.AxisY2.MaxGridCountPerPixel = 0.012D;
            this._easyChartX.AxisY2.Maximum = 3.5D;
            this._easyChartX.AxisY2.MinGridCountPerPixel = 0.004D;
            this._easyChartX.AxisY2.Minimum = 0.5D;
            this._easyChartX.AxisY2.MinorGridColor = System.Drawing.Color.DimGray;
            this._easyChartX.AxisY2.MinorGridEnabled = false;
            this._easyChartX.AxisY2.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this._easyChartX.AxisY2.ShowLogarithmicLines = false;
            this._easyChartX.AxisY2.TickLineColor = System.Drawing.Color.Black;
            this._easyChartX.AxisY2.TickWidth = 1F;
            this._easyChartX.AxisY2.Title = "";
            this._easyChartX.AxisY2.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this._easyChartX.AxisY2.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this._easyChartX.AxisY2.ViewMaximum = 3.5D;
            this._easyChartX.AxisY2.ViewMinimum = 0.5D;
            this._easyChartX.BackColor = System.Drawing.Color.White;
            this._easyChartX.ChartAreaBackColor = System.Drawing.Color.Empty;
            // this._easyChartX.Cumulitive = false; // 已过时的属性，移除
            this._easyChartX.Dock = System.Windows.Forms.DockStyle.Fill;
            this._easyChartX.GradientStyle = SeeSharpTools.JY.GUI.EasyChartX.ChartGradientStyle.None;
            this._easyChartX.LegendBackColor = System.Drawing.Color.Transparent;
            this._easyChartX.LegendFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this._easyChartX.LegendForeColor = System.Drawing.Color.Black;
            this._easyChartX.LegendVisible = true;
            easyChartXSeries1.Color = System.Drawing.Color.Red;
            easyChartXSeries1.Marker = SeeSharpTools.JY.GUI.EasyChartXSeries.MarkerType.None;
            easyChartXSeries1.Name = "Series1";
            easyChartXSeries1.Type = SeeSharpTools.JY.GUI.EasyChartXSeries.LineType.FastLine;
            easyChartXSeries1.Visible = true;
            easyChartXSeries1.Width = SeeSharpTools.JY.GUI.EasyChartXSeries.LineWidth.Thin;
            easyChartXSeries1.XPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries1.YPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            this._easyChartX.LineSeries.Add(easyChartXSeries1);
            this._easyChartX.Location = new System.Drawing.Point(3, 19);
            this._easyChartX.Miscellaneous.CheckInfinity = false;
            this._easyChartX.Miscellaneous.CheckNaN = false;
            this._easyChartX.Miscellaneous.CheckNegtiveOrZero = false;
            this._easyChartX.Miscellaneous.DataStorage = SeeSharpTools.JY.GUI.DataStorageType.Clone;
            this._easyChartX.Miscellaneous.DirectionChartCount = 3;
            this._easyChartX.Miscellaneous.Fitting = SeeSharpTools.JY.GUI.EasyChartX.FitType.Range;
            this._easyChartX.Miscellaneous.MarkerSize = 7;
            this._easyChartX.Miscellaneous.MaxSeriesCount = 32;
            this._easyChartX.Miscellaneous.MaxSeriesPointCount = 4000;
            this._easyChartX.Miscellaneous.ShowFunctionMenu = true;
            this._easyChartX.Miscellaneous.SplitLayoutColumnInterval = 0F;
            this._easyChartX.Miscellaneous.SplitLayoutDirection = SeeSharpTools.JY.GUI.EasyChartXUtility.LayoutDirection.LeftToRight;
            this._easyChartX.Miscellaneous.SplitLayoutRowInterval = 0F;
            this._easyChartX.Miscellaneous.SplitViewAutoLayout = true;
            this._easyChartX.Name = "_easyChartX";
            this._easyChartX.SeriesCount = 0;
            this._easyChartX.Size = new System.Drawing.Size(590, 778);
            this._easyChartX.SplitView = false;
            this._easyChartX.TabIndex = 0;
            this._easyChartX.XCursor.AutoInterval = true;
            this._easyChartX.XCursor.Color = System.Drawing.Color.DeepSkyBlue;
            this._easyChartX.XCursor.Interval = 0.001D;
            this._easyChartX.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartXCursor.CursorMode.Zoom;
            this._easyChartX.XCursor.SelectionColor = System.Drawing.Color.LightGray;
            this._easyChartX.XCursor.Value = double.NaN;
            this._easyChartX.YCursor.AutoInterval = true;
            this._easyChartX.YCursor.Color = System.Drawing.Color.DeepSkyBlue;
            this._easyChartX.YCursor.Interval = 0.001D;
            this._easyChartX.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartXCursor.CursorMode.Disabled;
            this._easyChartX.YCursor.SelectionColor = System.Drawing.Color.LightGray;
            this._easyChartX.YCursor.Value = double.NaN;
            // 
            // groupBoxConfig
            // 
            this.groupBoxConfig.Controls.Add(this.tableLayoutPanelConfig);
            this.groupBoxConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxConfig.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxConfig.Location = new System.Drawing.Point(0, 0);
            this.groupBoxConfig.Name = "groupBoxConfig";
            this.groupBoxConfig.Size = new System.Drawing.Size(296, 800);
            this.groupBoxConfig.TabIndex = 0;
            this.groupBoxConfig.TabStop = false;
            this.groupBoxConfig.Text = "⚙️ 配置选项";
            // 
            // tableLayoutPanelConfig
            // 
            this.tableLayoutPanelConfig.ColumnCount = 1;
            this.tableLayoutPanelConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelConfig.Controls.Add(this.groupBoxWaveform, 0, 0);
            this.tableLayoutPanelConfig.Controls.Add(this.groupBoxBackground, 0, 1);
            this.tableLayoutPanelConfig.Controls.Add(this.groupBoxDisplay, 0, 2);
            this.tableLayoutPanelConfig.Controls.Add(this.groupBoxExport, 0, 3);
            this.tableLayoutPanelConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelConfig.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanelConfig.Name = "tableLayoutPanelConfig";
            this.tableLayoutPanelConfig.RowCount = 4;
            this.tableLayoutPanelConfig.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanelConfig.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanelConfig.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelConfig.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelConfig.Size = new System.Drawing.Size(290, 778);
            this.tableLayoutPanelConfig.TabIndex = 0;
            // 
            // groupBoxWaveform
            // 
            this.groupBoxWaveform.Controls.Add(this.tableLayoutPanelWaveform);
            this.groupBoxWaveform.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxWaveform.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxWaveform.Location = new System.Drawing.Point(3, 3);
            this.groupBoxWaveform.Name = "groupBoxWaveform";
            this.groupBoxWaveform.Size = new System.Drawing.Size(284, 227);
            this.groupBoxWaveform.TabIndex = 0;
            this.groupBoxWaveform.TabStop = false;
            this.groupBoxWaveform.Text = "📊 波形设置";
            // 
            // tableLayoutPanelWaveform
            // 
            this.tableLayoutPanelWaveform.ColumnCount = 3;
            this.tableLayoutPanelWaveform.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanelWaveform.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanelWaveform.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelWaveform.Controls.Add(this.labelChannels, 0, 0);
            this.tableLayoutPanelWaveform.Controls.Add(this._numericUpDownChannels, 1, 0);
            this.tableLayoutPanelWaveform.Controls.Add(this.labelWaveformType, 0, 1);
            this.tableLayoutPanelWaveform.Controls.Add(this._comboBoxWaveformType, 1, 1);
            this.tableLayoutPanelWaveform.Controls.Add(this.labelLineWidth, 0, 2);
            this.tableLayoutPanelWaveform.Controls.Add(this._trackBarLineWidth, 1, 2);
            this.tableLayoutPanelWaveform.Controls.Add(this._labelLineWidthValue, 2, 2);
            this.tableLayoutPanelWaveform.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelWaveform.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanelWaveform.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanelWaveform.Name = "tableLayoutPanelWaveform";
            this.tableLayoutPanelWaveform.RowCount = 3;
            this.tableLayoutPanelWaveform.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelWaveform.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelWaveform.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelWaveform.Size = new System.Drawing.Size(278, 207);
            this.tableLayoutPanelWaveform.TabIndex = 0;
            // 
            // labelChannels
            // 
            this.labelChannels.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelChannels.AutoSize = true;
            this.labelChannels.Location = new System.Drawing.Point(3, 27);
            this.labelChannels.Name = "labelChannels";
            this.labelChannels.Size = new System.Drawing.Size(55, 13);
            this.labelChannels.TabIndex = 0;
            this.labelChannels.Text = "通道数量:";
            // 
            // _numericUpDownChannels
            // 
            this._numericUpDownChannels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._numericUpDownChannels.Location = new System.Drawing.Point(114, 24);
            this._numericUpDownChannels.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this._numericUpDownChannels.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._numericUpDownChannels.Name = "_numericUpDownChannels";
            this._numericUpDownChannels.Size = new System.Drawing.Size(105, 20);
            this._numericUpDownChannels.TabIndex = 1;
            this._numericUpDownChannels.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this._numericUpDownChannels.ValueChanged += new System.EventHandler(this.NumericUpDownChannels_ValueChanged);
            // 
            // labelWaveformType
            // 
            this.labelWaveformType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelWaveformType.AutoSize = true;
            this.labelWaveformType.Location = new System.Drawing.Point(3, 96);
            this.labelWaveformType.Name = "labelWaveformType";
            this.labelWaveformType.Size = new System.Drawing.Size(55, 13);
            this.labelWaveformType.TabIndex = 2;
            this.labelWaveformType.Text = "波形类型:";
            // 
            // _comboBoxWaveformType
            // 
            this._comboBoxWaveformType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._comboBoxWaveformType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxWaveformType.FormattingEnabled = true;
            this._comboBoxWaveformType.Items.AddRange(new object[] {
            "正弦波"});
            this._comboBoxWaveformType.Location = new System.Drawing.Point(114, 92);
            this._comboBoxWaveformType.Name = "_comboBoxWaveformType";
            this._comboBoxWaveformType.Size = new System.Drawing.Size(105, 21);
            this._comboBoxWaveformType.TabIndex = 3;
            this._comboBoxWaveformType.SelectedIndexChanged += new System.EventHandler(this.ComboBoxWaveformType_SelectedIndexChanged);
            // 
            // labelLineWidth
            // 
            this.labelLineWidth.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelLineWidth.AutoSize = true;
            this.labelLineWidth.Location = new System.Drawing.Point(3, 166);
            this.labelLineWidth.Name = "labelLineWidth";
            this.labelLineWidth.Size = new System.Drawing.Size(55, 13);
            this.labelLineWidth.TabIndex = 4;
            this.labelLineWidth.Text = "线条宽度:";
            // 
            // _trackBarLineWidth
            // 
            this._trackBarLineWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._trackBarLineWidth.Location = new System.Drawing.Point(114, 157);
            this._trackBarLineWidth.Maximum = 5;
            this._trackBarLineWidth.Minimum = 1;
            this._trackBarLineWidth.Name = "_trackBarLineWidth";
            this._trackBarLineWidth.Size = new System.Drawing.Size(105, 45);
            this._trackBarLineWidth.TabIndex = 5;
            this._trackBarLineWidth.Value = 2;
            this._trackBarLineWidth.ValueChanged += new System.EventHandler(this.TrackBarLineWidth_ValueChanged);
            // 
            // _labelLineWidthValue
            // 
            this._labelLineWidthValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._labelLineWidthValue.AutoSize = true;
            this._labelLineWidthValue.Location = new System.Drawing.Point(225, 166);
            this._labelLineWidthValue.Name = "_labelLineWidthValue";
            this._labelLineWidthValue.Size = new System.Drawing.Size(13, 13);
            this._labelLineWidthValue.TabIndex = 6;
            this._labelLineWidthValue.Text = "2";
            // 
            // groupBoxBackground
            // 
            this.groupBoxBackground.Controls.Add(this.tableLayoutPanelBackground);
            this.groupBoxBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxBackground.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxBackground.Location = new System.Drawing.Point(3, 236);
            this.groupBoxBackground.Name = "groupBoxBackground";
            this.groupBoxBackground.Size = new System.Drawing.Size(284, 227);
            this.groupBoxBackground.TabIndex = 1;
            this.groupBoxBackground.TabStop = false;
            this.groupBoxBackground.Text = "🎨 背景设置";
            // 
            // tableLayoutPanelBackground
            // 
            this.tableLayoutPanelBackground.ColumnCount = 3;
            this.tableLayoutPanelBackground.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanelBackground.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanelBackground.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelBackground.Controls.Add(this.labelBackground, 0, 0);
            this.tableLayoutPanelBackground.Controls.Add(this._comboBoxBackground, 1, 0);
            this.tableLayoutPanelBackground.Controls.Add(this._checkBoxShowGrid, 0, 1);
            this.tableLayoutPanelBackground.Controls.Add(this.labelGridOpacity, 0, 2);
            this.tableLayoutPanelBackground.Controls.Add(this._trackBarGridOpacity, 1, 2);
            this.tableLayoutPanelBackground.Controls.Add(this._labelGridOpacityValue, 2, 2);
            this.tableLayoutPanelBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelBackground.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanelBackground.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanelBackground.Name = "tableLayoutPanelBackground";
            this.tableLayoutPanelBackground.RowCount = 3;
            this.tableLayoutPanelBackground.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelBackground.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelBackground.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelBackground.Size = new System.Drawing.Size(278, 207);
            this.tableLayoutPanelBackground.TabIndex = 0;
            // 
            // labelBackground
            // 
            this.labelBackground.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelBackground.AutoSize = true;
            this.labelBackground.Location = new System.Drawing.Point(3, 27);
            this.labelBackground.Name = "labelBackground";
            this.labelBackground.Size = new System.Drawing.Size(43, 13);
            this.labelBackground.TabIndex = 0;
            this.labelBackground.Text = "背景色:";
            // 
            // _comboBoxBackground
            // 
            this._comboBoxBackground.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._comboBoxBackground.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxBackground.FormattingEnabled = true;
            this._comboBoxBackground.Items.AddRange(new object[] {
            "白色",
            "黑色",
            "深灰",
            "自定义"});
            this._comboBoxBackground.Location = new System.Drawing.Point(114, 24);
            this._comboBoxBackground.Name = "_comboBoxBackground";
            this._comboBoxBackground.Size = new System.Drawing.Size(105, 21);
            this._comboBoxBackground.TabIndex = 1;
            this._comboBoxBackground.SelectedIndexChanged += new System.EventHandler(this.ComboBoxBackground_SelectedIndexChanged);
            // 
            // _checkBoxShowGrid
            // 
            this._checkBoxShowGrid.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._checkBoxShowGrid.AutoSize = true;
            this._checkBoxShowGrid.Checked = true;
            this._checkBoxShowGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this._checkBoxShowGrid.Location = new System.Drawing.Point(3, 95);
            this._checkBoxShowGrid.Name = "_checkBoxShowGrid";
            this._checkBoxShowGrid.Size = new System.Drawing.Size(74, 17);
            this._checkBoxShowGrid.TabIndex = 2;
            this._checkBoxShowGrid.Text = "显示网格线";
            this._checkBoxShowGrid.UseVisualStyleBackColor = true;
            this._checkBoxShowGrid.CheckedChanged += new System.EventHandler(this.CheckBoxShowGrid_CheckedChanged);
            // 
            // labelGridOpacity
            // 
            this.labelGridOpacity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelGridOpacity.AutoSize = true;
            this.labelGridOpacity.Location = new System.Drawing.Point(3, 166);
            this.labelGridOpacity.Name = "labelGridOpacity";
            this.labelGridOpacity.Size = new System.Drawing.Size(67, 13);
            this.labelGridOpacity.TabIndex = 3;
            this.labelGridOpacity.Text = "网格透明度:";
            // 
            // _trackBarGridOpacity
            // 
            this._trackBarGridOpacity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._trackBarGridOpacity.Location = new System.Drawing.Point(114, 157);
            this._trackBarGridOpacity.Maximum = 100;
            this._trackBarGridOpacity.Name = "_trackBarGridOpacity";
            this._trackBarGridOpacity.Size = new System.Drawing.Size(105, 45);
            this._trackBarGridOpacity.TabIndex = 4;
            this._trackBarGridOpacity.TickFrequency = 10;
            this._trackBarGridOpacity.Value = 30;
            this._trackBarGridOpacity.ValueChanged += new System.EventHandler(this.TrackBarGridOpacity_ValueChanged);
            // 
            // _labelGridOpacityValue
            // 
            this._labelGridOpacityValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._labelGridOpacityValue.AutoSize = true;
            this._labelGridOpacityValue.Location = new System.Drawing.Point(225, 166);
            this._labelGridOpacityValue.Name = "_labelGridOpacityValue";
            this._labelGridOpacityValue.Size = new System.Drawing.Size(25, 13);
            this._labelGridOpacityValue.TabIndex = 5;
            this._labelGridOpacityValue.Text = "30%";
            // 
            // groupBoxDisplay
            // 
            this.groupBoxDisplay.Controls.Add(this.tableLayoutPanelDisplay);
            this.groupBoxDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDisplay.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxDisplay.Location = new System.Drawing.Point(3, 469);
            this.groupBoxDisplay.Name = "groupBoxDisplay";
            this.groupBoxDisplay.Size = new System.Drawing.Size(284, 149);
            this.groupBoxDisplay.TabIndex = 2;
            this.groupBoxDisplay.TabStop = false;
            this.groupBoxDisplay.Text = "🖥️ 显示选项";
            // 
            // tableLayoutPanelDisplay
            // 
            this.tableLayoutPanelDisplay.ColumnCount = 1;
            this.tableLayoutPanelDisplay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelDisplay.Controls.Add(this._checkBoxShowLegend, 0, 0);
            this.tableLayoutPanelDisplay.Controls.Add(this._checkBoxEnableAnimation, 0, 1);
            this.tableLayoutPanelDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelDisplay.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanelDisplay.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanelDisplay.Name = "tableLayoutPanelDisplay";
            this.tableLayoutPanelDisplay.RowCount = 2;
            this.tableLayoutPanelDisplay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelDisplay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelDisplay.Size = new System.Drawing.Size(278, 129);
            this.tableLayoutPanelDisplay.TabIndex = 0;
            // 
            // _checkBoxShowLegend
            // 
            this._checkBoxShowLegend.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._checkBoxShowLegend.AutoSize = true;
            this._checkBoxShowLegend.Checked = true;
            this._checkBoxShowLegend.CheckState = System.Windows.Forms.CheckState.Checked;
            this._checkBoxShowLegend.Location = new System.Drawing.Point(3, 24);
            this._checkBoxShowLegend.Name = "_checkBoxShowLegend";
            this._checkBoxShowLegend.Size = new System.Drawing.Size(62, 17);
            this._checkBoxShowLegend.TabIndex = 0;
            this._checkBoxShowLegend.Text = "显示图例";
            this._checkBoxShowLegend.UseVisualStyleBackColor = true;
            this._checkBoxShowLegend.CheckedChanged += new System.EventHandler(this.CheckBoxShowLegend_CheckedChanged);
            // 
            // _checkBoxEnableAnimation
            // 
            this._checkBoxEnableAnimation.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._checkBoxEnableAnimation.AutoSize = true;
            this._checkBoxEnableAnimation.Checked = true;
            this._checkBoxEnableAnimation.CheckState = System.Windows.Forms.CheckState.Checked;
            this._checkBoxEnableAnimation.Location = new System.Drawing.Point(3, 88);
            this._checkBoxEnableAnimation.Name = "_checkBoxEnableAnimation";
            this._checkBoxEnableAnimation.Size = new System.Drawing.Size(74, 17);
            this._checkBoxEnableAnimation.TabIndex = 1;
            this._checkBoxEnableAnimation.Text = "启用动画效果";
            this._checkBoxEnableAnimation.UseVisualStyleBackColor = true;
            this._checkBoxEnableAnimation.CheckedChanged += new System.EventHandler(this.CheckBoxEnableAnimation_CheckedChanged);
            // 
            // groupBoxExport
            // 
            this.groupBoxExport.Controls.Add(this.tableLayoutPanelExport);
            this.groupBoxExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxExport.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxExport.Location = new System.Drawing.Point(3, 624);
            this.groupBoxExport.Name = "groupBoxExport";
            this.groupBoxExport.Size = new System.Drawing.Size(284, 151);
            this.groupBoxExport.TabIndex = 3;
            this.groupBoxExport.TabStop = false;
            this.groupBoxExport.Text = "📤 导出功能";
            // 
            // tableLayoutPanelExport
            // 
            this.tableLayoutPanelExport.ColumnCount = 2;
            this.tableLayoutPanelExport.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelExport.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelExport.Controls.Add(this._buttonExportCSharp, 0, 0);
            this.tableLayoutPanelExport.Controls.Add(this._buttonExportJson, 1, 0);
            this.tableLayoutPanelExport.Controls.Add(this._buttonExportCss, 0, 1);
            this.tableLayoutPanelExport.Controls.Add(this._buttonExportJs, 1, 1);
            this.tableLayoutPanelExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelExport.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanelExport.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanelExport.Name = "tableLayoutPanelExport";
            this.tableLayoutPanelExport.RowCount = 2;
            this.tableLayoutPanelExport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelExport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelExport.Size = new System.Drawing.Size(278, 131);
            this.tableLayoutPanelExport.TabIndex = 0;
            // 
            // _buttonExportCSharp
            // 
            this._buttonExportCSharp.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buttonExportCSharp.Location = new System.Drawing.Point(3, 3);
            this._buttonExportCSharp.Name = "_buttonExportCSharp";
            this._buttonExportCSharp.Size = new System.Drawing.Size(133, 59);
            this._buttonExportCSharp.TabIndex = 0;
            this._buttonExportCSharp.Text = "导出 C# 代码";
            this._buttonExportCSharp.UseVisualStyleBackColor = true;
            this._buttonExportCSharp.Click += new System.EventHandler(this.ButtonExport_Click);
            // 
            // _buttonExportJson
            // 
            this._buttonExportJson.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buttonExportJson.Location = new System.Drawing.Point(142, 3);
            this._buttonExportJson.Name = "_buttonExportJson";
            this._buttonExportJson.Size = new System.Drawing.Size(133, 59);
            this._buttonExportJson.TabIndex = 1;
            this._buttonExportJson.Text = "导出 JSON";
            this._buttonExportJson.UseVisualStyleBackColor = true;
            this._buttonExportJson.Click += new System.EventHandler(this.ButtonExport_Click);
            // 
            // _buttonExportCss
            // 
            this._buttonExportCss.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buttonExportCss.Location = new System.Drawing.Point(3, 68);
            this._buttonExportCss.Name = "_buttonExportCss";
            this._buttonExportCss.Size = new System.Drawing.Size(133, 60);
            this._buttonExportCss.TabIndex = 2;
            this._buttonExportCss.Text = "导出 CSS";
            this._buttonExportCss.UseVisualStyleBackColor = true;
            this._buttonExportCss.Click += new System.EventHandler(this.ButtonExport_Click);
            // 
            // _buttonExportJs
            // 
            this._buttonExportJs.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buttonExportJs.Location = new System.Drawing.Point(142, 68);
            this._buttonExportJs.Name = "_buttonExportJs";
            this._buttonExportJs.Size = new System.Drawing.Size(133, 60);
            this._buttonExportJs.TabIndex = 3;
            this._buttonExportJs.Text = "导出 JavaScript";
            this._buttonExportJs.UseVisualStyleBackColor = true;
            this._buttonExportJs.Click += new System.EventHandler(this.ButtonExport_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.splitContainerMain);
            this.Name = "MainForm";
            this.Text = "EasyChartX 配色方案设计器";
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainerLeft.Panel1.ResumeLayout(false);
            this.splitContainerLeft.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeft)).EndInit();
            this.splitContainerLeft.ResumeLayout(false);
            this.groupBoxColorSchemes.ResumeLayout(false);
            this.groupBoxColorInfo.ResumeLayout(false);
            this.splitContainerRight.Panel1.ResumeLayout(false);
            this.splitContainerRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRight)).EndInit();
            this.splitContainerRight.ResumeLayout(false);
            this.groupBoxPreview.ResumeLayout(false);
            this.groupBoxConfig.ResumeLayout(false);
            this.tableLayoutPanelConfig.ResumeLayout(false);
            this.groupBoxWaveform.ResumeLayout(false);
            this.tableLayoutPanelWaveform.ResumeLayout(false);
            this.tableLayoutPanelWaveform.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numericUpDownChannels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._trackBarLineWidth)).EndInit();
            this.groupBoxBackground.ResumeLayout(false);
            this.tableLayoutPanelBackground.ResumeLayout(false);
            this.tableLayoutPanelBackground.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._trackBarGridOpacity)).EndInit();
            this.groupBoxDisplay.ResumeLayout(false);
            this.tableLayoutPanelDisplay.ResumeLayout(false);
            this.tableLayoutPanelDisplay.PerformLayout();
            this.groupBoxExport.ResumeLayout(false);
            this.tableLayoutPanelExport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.SplitContainer splitContainerLeft;
        private System.Windows.Forms.GroupBox groupBoxColorSchemes;
        private System.Windows.Forms.ListBox _listBoxColorSchemes;
        private System.Windows.Forms.GroupBox groupBoxColorInfo;
        private System.Windows.Forms.RichTextBox _richTextBoxColorInfo;
        private System.Windows.Forms.SplitContainer splitContainerRight;
        private System.Windows.Forms.GroupBox groupBoxPreview;
        private SeeSharpTools.JY.GUI.EasyChartX _easyChartX;
        private System.Windows.Forms.GroupBox groupBoxConfig;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelConfig;
        private System.Windows.Forms.GroupBox groupBoxWaveform;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelWaveform;
        private System.Windows.Forms.Label labelChannels;
        private System.Windows.Forms.NumericUpDown _numericUpDownChannels;
        private System.Windows.Forms.Label labelWaveformType;
        private System.Windows.Forms.ComboBox _comboBoxWaveformType;
        private System.Windows.Forms.Label labelLineWidth;
        private System.Windows.Forms.TrackBar _trackBarLineWidth;
        private System.Windows.Forms.Label _labelLineWidthValue;
        private System.Windows.Forms.GroupBox groupBoxBackground;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelBackground;
        private System.Windows.Forms.Label labelBackground;
        private System.Windows.Forms.ComboBox _comboBoxBackground;
        private System.Windows.Forms.CheckBox _checkBoxShowGrid;
        private System.Windows.Forms.Label labelGridOpacity;
        private System.Windows.Forms.TrackBar _trackBarGridOpacity;
        private System.Windows.Forms.Label _labelGridOpacityValue;
        private System.Windows.Forms.GroupBox groupBoxDisplay;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDisplay;
        private System.Windows.Forms.CheckBox _checkBoxShowLegend;
        private System.Windows.Forms.CheckBox _checkBoxEnableAnimation;
        private System.Windows.Forms.GroupBox groupBoxExport;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelExport;
        private System.Windows.Forms.Button _buttonExportCSharp;
        private System.Windows.Forms.Button _buttonExportJson;
        private System.Windows.Forms.Button _buttonExportCss;
        private System.Windows.Forms.Button _buttonExportJs;
    }
}
