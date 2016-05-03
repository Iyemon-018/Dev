using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExcelDataReaderSample
{
    using System.Data;
    using System.IO;
    using Excel;

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string _filePath = @"./Book1.xlsx";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            DataSet ds;
            using (var f = File.OpenRead(_filePath))
            {
                // OpenXml はこれで読み込むことができる。
                var reader = ExcelReaderFactory.CreateOpenXmlReader(f);

                // *.xls ファイルはこれで読み込むことができる。
                //var reader = ExcelReaderFactory.CreateBinaryReader(f);

                // 以下の方法でDataSet へ変換する。
                ds = reader.AsDataSet();
            }

            int rowCount = 0;
            int columnCount = 0;

            // DataSet.Tables[X] が１シートの情報と一致する。
            foreach (DataTable table in ds.Tables)
            {
                // DataTable.TableName = シート名
                if (table.TableName == "Sheet1")
                {
                    foreach (DataRow row in table.Rows)
                    {
                        foreach (var item in row.ItemArray)
                        {
                            Console.WriteLine("行:{0}/列:{1}, 値:{2}", rowCount, columnCount, item);
                        }
                    }
                }
            }
        }
    }
}
