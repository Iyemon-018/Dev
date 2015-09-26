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
using AutoMapper;

namespace AutoMapperSample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // マッピングの初期設定
            // １．デフォルトマッピング
            //Mapper.CreateMap<Source, Destination>();

            // ２．対象外を設定
            // Numberだけマッピング対象外とする。
            //Mapper.CreateMap<Source, Destination>()
            //    .ForMember(d => d.Number, opt => opt.Ignore());

            // ３．異なる名称のデータをマッピング
            // SourceにRemarksを、DestinationにRemarksDummyを追加した。
            //Mapper.CreateMap<Source, Destination>()
            //    .ForMember(d => d.RemarksDummy, opt => opt.MapFrom(s => s.Remarks));

            // ４．異なる型のデータをマッピング
            // Destination.RemarksDummy にSource.DayOfWeek の文字列をマッピング
            //Mapper.CreateMap<Source, Destination>()
            //    .ForMember(d => d.RemarksDummy, opt => opt.MapFrom(s => s.DayOfWeek.ToString()));
            // 複合的なマッピング
            //Mapper.CreateMap<Source, Destination>()
            //    .ForMember(d => d.RemarksDummy, opt => opt.MapFrom(s => s.DayOfWeek.ToString()
            //                                                            + s.Number.ToString()));

            // ５．相互マッピング
            // どちらからもマッピングできるように設定
            //  Source.Remarks ⇒ Destination.RemarksDummy
            //  Destination.DayOfWeek ⇒ Source.Remarks
            // をマッピング
            //Mapper.CreateMap<Source, Destination>()
            //    .ForMember(d => d.RemarksDummy, opt => opt.MapFrom(s => s.Remarks))
            //    .ReverseMap()
            //    .ForMember(s => s.Remarks, opt => opt.MapFrom(d => d.DayOfWeek.ToString()));

            try
            {
                // マッピング定義の初期化
                Mapper.Initialize(config =>
                    {
                        // 複数のプロファイルがあれば、AddProfileで追加していく。
                        config.AddProfile<MappingProfile>();

                        // ここでは、マッピング名称ルールのカスタマイズ設定なども実施できる。
                    });

                // マッピング設定を検証する。
                // マッピングするデータの過不足があれば、ここで例外が発生する。
                // ただし、キャストの失敗などは検知できないので注意すること。
                Mapper.AssertConfigurationIsValid();
            }
            catch (Exception ex)
            {
                // マッピング失敗時のエラー処理を実施
                Console.WriteLine(ex.Message);
            }

            var dt = DateTime.Now;

            // 元となるデータを用意
            var src = new Source()
            {
                Number = dt.Millisecond,
                Text = dt.ToString("yyyyMMddHHmmssfff"),
                DayOfWeek = dt.DayOfWeek,
                Remarks = "Source Remarks",
            };
            Console.WriteLine(src.ToString());

            // マッピング実施
            var dst = Mapper.Map<Destination>(src);

            Console.WriteLine(dst.ToString());

            dst.Text = "Changed dst.";

            // 逆方向へマッピング
            var newSrc = Mapper.Map<Source>(dst);

            Console.WriteLine(newSrc.ToString());
        }
    }
}
