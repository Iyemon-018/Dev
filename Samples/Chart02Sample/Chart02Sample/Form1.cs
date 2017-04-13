using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chart02Sample
{
    using System.Windows.Forms.DataVisualization.Charting;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateChartData();
        }
        
        private void UpdateChartData()
        {
            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisY.StripLines.Clear();

            // グラフ用のデータを構築する。
            Random r = new Random();
            PlotData[] plotData = Enumerable.Range(0, 100).Select(x => new PlotData { X = x, Y = r.Next(10, 90) }).ToArray();
            Series series = chart1.Series.Add("サンプル");
            series.ChartType = SeriesChartType.Line;
            foreach (var data in plotData)
            {
                series.Points.AddXY(data.X, data.Y);
            }

            // 平均値の水平線を表示する。
            double mean = chart1.DataManipulator.Statistics.Mean("サンプル");
            StripLine stlipLine = new StripLine
                                      {
                                          Text              = $"平均値:{mean}",
                                          TextAlignment     = StringAlignment.Near,     // テキストの水平位置（Near:左, Center:中央, Far:右）
                                          TextLineAlignment = StringAlignment.Far,      // テキストの垂直位置（Near:下, Center:中央, Far:上）
                                          Interval          = 0,                        // 線分の表示間隔 値を設定すると指定した間隔で表示される。
                                          IntervalOffset    = mean,                     // 線分の表示オフセット
                                          BorderWidth       = 1,
                                          BorderDashStyle   = ChartDashStyle.DashDot,
                                          BorderColor       = Color.Blue,
                                      };
            chart1.ChartAreas[0].AxisY.StripLines.Add(stlipLine);
        }

        private void ReloadButton_Click(object sender, EventArgs e)
        {
            UpdateChartData();
        }
    }

    /// <summary>
    /// グラフデータ
    /// </summary>
    public class PlotData
    {
        public int X { get; set; }

        public double Y { get; set; }
    }
}
