using System;
using System.Windows.Forms;

namespace EasyChartX.ColorDesigner
{
    /// <summary>
    /// EasyChartX 配色方案设计器
    /// 简仪科技 - SeeSharp工具包配色设计工具
    /// </summary>
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            try
            {
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"应用程序启动失败：\n{ex.Message}", 
                    "EasyChartX 配色设计器", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }
    }
}
