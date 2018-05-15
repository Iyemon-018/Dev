namespace Chart01Sample
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Windows.Forms.DataVisualization.Charting;

    public partial class Form1 : Form
    {
        //
        // 選択できるSeriesChartTypeを分けているのは、SeriesChartTypeを切り替えた場合にInvalidOperationExceptionがスローされるため。
        // あるグラフ系列種別から別の系列種別へ切り替えると上記の例外がスローされる。
        // 以下の２つのリストはその例外がスローされない種類で分けている。
        //

        private readonly List<SeriesChartType> _chartTypes1
                = new List<SeriesChartType>
                      {
                          SeriesChartType.Point,
                          SeriesChartType.FastPoint,
                          SeriesChartType.Bubble,
                          SeriesChartType.Line,
                          SeriesChartType.Spline,
                          SeriesChartType.StepLine,
                          SeriesChartType.FastLine,
                          SeriesChartType.Column,
                          SeriesChartType.StackedColumn,
                          SeriesChartType.StackedColumn100,
                          SeriesChartType.Area,
                          SeriesChartType.SplineArea,
                          SeriesChartType.StackedArea,
                          SeriesChartType.StackedArea100,
                          SeriesChartType.Stock,
                          SeriesChartType.Candlestick,
                          SeriesChartType.ErrorBar,
                          SeriesChartType.BoxPlot
                      };

        private readonly List<SeriesChartType> _chartTypes2
                = new List<SeriesChartType>
                      {
                          SeriesChartType.Bar,
                          SeriesChartType.StackedBar,
                          SeriesChartType.StackedBar100,
                          SeriesChartType.Pie,
                          SeriesChartType.Range,
                          SeriesChartType.SplineRange,
                          SeriesChartType.RangeBar,
                          SeriesChartType.RangeColumn,
                          SeriesChartType.Doughnut,
                          SeriesChartType.Radar,
                          SeriesChartType.Polar,
                          SeriesChartType.ThreeLineBreak,
                          SeriesChartType.Kagi,
                          SeriesChartType.Renko,
                          SeriesChartType.PointAndFigure,
                          SeriesChartType.Funnel,
                          SeriesChartType.Pyramid
                      };
        
        public Form1()
        {
            InitializeComponent();

            // Chartには、最初からSeriesが設定されているので削除しておく。
            chart1.Series.Clear();
            chart2.Series.Clear();

            List<ChartData> data = new List<ChartData>
                                       {
                                           new ChartData {X = 0, Y = 3},
                                           new ChartData {X = 1, Y = 4},
                                           new ChartData {X = 2, Y = 8},
                                           new ChartData {X = 3, Y = 6},
                                           new ChartData {X = 4, Y = 7},
                                           new ChartData {X = 5, Y = 3},
                                           new ChartData {X = 6, Y = 1},
                                           new ChartData {X = 7, Y = 0},
                                           new ChartData {X = 8, Y = 5},
                                           new ChartData {X = 9, Y = 2}
                                       };

            chart1.DataSource = data;
            Series series = chart1.Series.Add("Sample");
            series.XValueMember = "X";
            series.YValueMembers = "Y";
            series.Color = Color.Blue;

            chart2.DataSource = data;
            Series series2 = chart2.Series.Add("Sample");
            series2.XValueMember = "X";
            series2.YValueMembers = "Y";
            series2.Color = Color.Blue;
        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                chart1.Series["Sample"].ChartType = (SeriesChartType) comboBox1.SelectedItem;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                chart2.Series["Sample"].ChartType = (SeriesChartType) comboBox2.SelectedItem;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = _chartTypes1;
            comboBox2.DataSource = _chartTypes2;
        }
    }
}