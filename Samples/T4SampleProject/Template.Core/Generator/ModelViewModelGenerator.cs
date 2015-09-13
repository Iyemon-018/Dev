using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using Template.Core;
using Template.Core.Info;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace Template.Core.Generator
{
    /// <summary>
    /// T4テンプレートからModel/ViewModelを生成するためのヘルパー
    /// </summary>
    public static class ModelViewModelGenerator
    {
        /// <summary>
        /// 指定した定義ファイルから、Model クラスを生成する。
        /// </summary>
        /// <param name="filePath">定義ファイルパス</param>
        /// <param name="environment"></param>
        /// <param name="proc"></param>
        public static void CreateModels(string filePath, StringBuilder environment, Action<ClassInfo> proc)
        {
            string outputDir = Path.Combine(Directory.GetParent(Path.GetDirectoryName(filePath)).FullName, "Models");
            var exemptSheetNames = new string[] { "雛形", "サンプル", };
            OutputClassTemplate(filePath, exemptSheetNames, outputDir, environment, proc);
        }

        /// <summary>
        /// クラス情報を出力する。
        /// </summary>
        /// <param name="filePath">定義ファイルパス</param>
        /// <param name="exemptSheetNames">読み込み対象外のシート名</param>
        /// <param name="outputDir">出力先フォルダ</param>
        /// <param name="environment"></param>
        /// <param name="proc"></param>
        public static void OutputClassTemplate(string filePath, string[] exemptSheetNames, string outputDir, StringBuilder environment, Action<ClassInfo> proc)
        {
            if (File.Exists(filePath) == false)
            {
                throw new FileNotFoundException("指定した定義ファイルが見つかりません。", filePath);
            }

            // 出力先がなければ作っておく。
            if (Directory.Exists(outputDir) == false)
            {
                Directory.CreateDirectory(outputDir);
            }

            try
            {
                var classes = new List<ClassInfo>();
                var fileInfo = new FileInfo(filePath);

                // 定義ファイル読み込み開始
                using (var excel = new ExcelPackage(fileInfo))
                {
                    // 読み込み対象のシートのみ抽出
                    var targetSheets = excel.Workbook.Worksheets.Where(sheet => { return !exemptSheetNames.Contains(sheet.Name); });
                    if (targetSheets == null || targetSheets.Count() == 0)
                    {
                        // 対象のシート無し
                        return;
                    }

                    // クラス情報を生成する。
                    foreach (var sheet in targetSheets)
                    {
                        classes.Add(CreateClassInfo(sheet));
                    }
                }

                // 読み込んだクラス情報を基に、T4テンプレートを使用してコードファイルを生成する。
                foreach (var info in classes)
                {
                    // ここでT4テンプレートを実施
                    proc(info);

                    // 自動生成ファイル名フォーマットは"*.Auto.cs"とする。
                    string outputFilePath = Path.Combine(outputDir, info.Name) + ".Auto.cs";
                    using (var sw = new StreamWriter(outputFilePath, false))
                    {
                        sw.Write(environment.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 指定したシートからクラス情報を生成する。
        /// </summary>
        /// <param name="sheet">対象シート</param>
        /// <returns></returns>
        private static ClassInfo CreateClassInfo(ExcelWorksheet sheet)
        {
            // 基準となるセルは"C2"
            var cell = sheet.Cells["C2"];
            string name = cell.ValueString();
            string comment = cell.Offset(1, 0).ValueString();

            var result = new ClassInfo(name, comment);

            // 名前空間を取得する。
            string nameSpace = sheet.Cells["G2"].ValueString();
            if (string.IsNullOrEmpty(nameSpace) == false)
            {
                result.NameSpaces.AddRange(nameSpace.Split(new string[] { "\r", "\n", }, StringSplitOptions.None));
            }

            // 定数を取得する。
            result.DefinedList.AddRange(CreateDefinedInfoList(sheet));

            // プロパティを取得する。
            result.Properties.AddRange(CreatePropertyInfoList(sheet));

            return result;
        }

        /// <summary>
        /// 指定したシートから定数情報リストを生成する。
        /// </summary>
        /// <param name="sheet">対象シート</param>
        /// <returns></returns>
        private static IEnumerable<DefinedInfo> CreateDefinedInfoList(ExcelWorksheet sheet)
        {
            // 基準となるセルは"C5"
            var results = new List<DefinedInfo>();
            var cell = sheet.Cells["C5"];
            int column = 0;

            // 定数は横並び
            while (!cell.Offset(0, column).IsEmpty())
            {
                var def = CreateDefinedInfo(cell.Offset(0, column));

                results.Add(def);

                column += 1;
            }

            return results;
        }

        /// <summary>
        /// 指定したセルを基準とした定数情報を生成する。
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static DefinedInfo CreateDefinedInfo(ExcelRangeBase cell)
        {
            var result = new DefinedInfo(cell.Offset(0, 0).ValueString()
                                        , cell.Offset(1, 0).ValueString()
                                        , cell.Offset(3, 0).ValueString()
                                        , cell.Offset(2, 0).ValueString());

            return result;
        }

        /// <summary>
        /// 指定したシートからプロパティ情報リストを生成する。
        /// </summary>
        /// <param name="sheet">対象シート</param>
        /// <returns></returns>
        private static IEnumerable<PropertyInfo> CreatePropertyInfoList(ExcelWorksheet sheet)
        {
            // 基準となるセルは"B12"
            var results = new List<PropertyInfo>();
            var cell = sheet.Cells["B12"];
            int row = 0;

            // プロパティは縦並び
            while (!cell.Offset(row, 0).IsEmpty())
            {
                var property = CreatePropertyInfo(cell.Offset(row, 0));
                results.Add(property);
                row += 1;
            }

            return results;
        }

        /// <summary>
        /// 指定したセルを基準としたプロパティ情報を生成する。
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static PropertyInfo CreatePropertyInfo(ExcelRangeBase cell)
        {
            string typeName = cell.Offset(0, 2).ValueString();
            string defaultValue = string.Empty;
            string elementCount = cell.Offset(0, 3).ValueString();
            int count;

            // 配列やコレクションの場合、メンバで初期化する必要がある。
            // それ以外の場合は、初期化は実施しない。
            // ここでは、デフォルト値の設定有無の判定と、型名の変換を行う。
            if (int.TryParse(elementCount, out count) == true)
            {
                if (count > 1)
                {
                    // 配列で定義されている。
                    defaultValue = string.Format("new {0}[{1}]", typeName, count);
                    typeName = string.Format("{0}[]", typeName);
                }
            }
            else if (elementCount == "*")
            {
                // コレクションで定義されている。
                typeName = string.Format("ObservableCollection<{0}>", typeName);
                defaultValue = string.Format("new {0}()", typeName);
            }

            var result = new PropertyInfo(cell.Offset(0, 0).ValueString()
                                        , typeName
                                        , cell.Offset(0, 4).ValueString()
                                        , cell.Offset(0, 1).ValueString()
                                        , cell.Offset(0, 5).ValueString())
                                        {
                                            DefaultValue = defaultValue,
                                        };

            result.CheckAttributes.AddRange(CreateCheckAttributes(cell.Offset(0, 6)));

            return result;
        }

        /// <summary>
        /// 入力チェック属性情報を生成する。
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static IEnumerable<CheckAttributeBase> CreateCheckAttributes(ExcelRangeBase cell)
        {
            var results = new List<CheckAttributeBase>();

            string isCheckRequired = cell.Offset(0, 0).ValueString();
            if (string.IsNullOrEmpty(isCheckRequired) == false)
            {
                // 未入力チェック実施
                results.Add(new CheckRequiredAttribute());
            }

            string typeName = cell.Offset(0, -4).ValueString();
            string minimum = cell.Offset(0, 1).ValueString();
            string maximum = cell.Offset(0, 2).ValueString();
            if (string.IsNullOrEmpty(minimum) == false
                && string.IsNullOrEmpty(maximum) == false)
            {
                // 範囲入力チェック実施
                results.Add(new CheckRangeAttribute(typeName, minimum, maximum));
            }

            string minLength = cell.Offset(0, 3).ValueString();
            string maxLength = cell.Offset(0, 4).ValueString();
            if (string.IsNullOrEmpty(minLength) == false
                && string.IsNullOrEmpty(maxLength) == false)
            {
                // 文字数制限チェック実施
                results.Add(new CheckStringLengthAttribute(int.Parse(minLength), int.Parse(maxLength)));
            }

            return results;
        }
    }
}
