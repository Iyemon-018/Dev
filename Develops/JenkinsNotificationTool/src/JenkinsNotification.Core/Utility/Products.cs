namespace JenkinsNotification.Core.Utility
{
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// アプリケーションの製品情報クラスです。<para/>
    /// エントリポイントを基準としたアセンブリの情報を保持しています。
    /// </summary>
    /// <example>
    /// アプリケーションの製品情報を取得する際は、<see cref="Current"/> プロパティからアクセスして下さい。<para/>
    /// <code><![CDATA[
    /// // アプリケーションのタイトルを取得する。
    /// var title = Products.Current.Title;
    /// ]]></code>
    /// </example>
    /// <remarks>
    /// 各プロパティはアプリケーションのエントリポイント アセンブリ情報を保持しています。<para/>
    /// プロパティに対するアセンブリ情報は次の通り。<para/><para/>
    /// ・<see cref="AssemblyName"/> … アセンブリ名（XXXX.exe などの拡張子を除いた文字列）<para/>
    /// ・<see cref="Company"/> … アセンブリ情報の会社<para/>
    /// ・<see cref="Description"/> … アセンブリ情報の説明<para/>
    /// ・<see cref="Product"/> … アセンブリ情報の製品<para/>
    /// ・<see cref="Title"/> … アセンブリ情報のタイトル<para/>
    /// ・<see cref="Version"/> … アセンブリ情報のアセンブリ バージョン<para/>
    /// ・<see cref="Location"/> … アプリケーション アセンブリの実行パス（ファイル名を含む）<para/>
    /// ・<see cref="LocationDirectory"/> … アプリケーション アセンブリの実行ディレクトリパス<para/>
    /// </remarks>
    public class Products
    {
        #region Const

        /// <summary>
        /// アセンブリ情報
        /// </summary>
        private static readonly Assembly _assembly = Assembly.GetEntryAssembly();

        /// <summary>
        /// カレント情報
        /// </summary>
        private static readonly Products _current = new Products();

        #endregion

        #region Fields

        /// <summary>
        /// 製作企業名
        /// </summary>
        private string _company;

        /// <summary>
        /// アプリケーションの説明
        /// </summary>
        private string _description;

        /// <summary>
        /// アセンブリのロケーションパス
        /// </summary>
        private string _location;

        /// <summary>
        /// アセンブリのロケーションディレクトリパス
        /// </summary>
        private string _locationDirectory;

        /// <summary>
        /// アプリケーション名称
        /// </summary>
        private string _product;

        /// <summary>
        /// アプリケーションタイトル
        /// </summary>
        private string _title;

        /// <summary>
        /// バージョン番号
        /// </summary>
        private Version _version;

        /// <summary>
        /// アセンブリのコピーライト
        /// </summary>
        private string _copyright;

        #endregion

        #region Properties

        /// <summary>
        /// カレント情報を取得します。
        /// </summary>
        public static Products Current => _current;

        /// <summary>
        /// アプリケーションのアセンブリ名を取得します。
        /// </summary>
        public string AssemblyName => Path.GetFileNameWithoutExtension(_assembly.Location);

        /// <summary>
        /// アセンブリのコピーライトを取得します。
        /// </summary>
        public string Copyright
                =>
                _copyright
                ?? (_copyright =
                            ((AssemblyCopyrightAttribute)
                                Attribute.GetCustomAttribute(_assembly, typeof(AssemblyCopyrightAttribute))).Copyright);

        /// <summary>
        /// 製作企業名を取得します。
        /// </summary>
        public string Company
                =>
                _company
                ?? (_company =
                            ((AssemblyCompanyAttribute)
                                Attribute.GetCustomAttribute(_assembly, typeof(AssemblyCompanyAttribute))).Company);

        /// <summary>
        /// アプリケーションの説明を取得します。
        /// </summary>
        public string Description
                =>
                _description
                ?? (_description =
                            ((AssemblyDescriptionAttribute)
                                        Attribute.GetCustomAttribute(_assembly, typeof(AssemblyDescriptionAttribute)))
                                    .Description);

        /// <summary>
        /// アセンブリのロケーションパスを取得します。
        /// </summary>
        public string Location => _location ?? (_location = _assembly.Location);

        /// <summary>
        /// アセンブリのロケーションディレクトリパスを取得します。
        /// </summary>
        public string LocationDirectory
                => _locationDirectory ?? (_locationDirectory = Path.GetDirectoryName(_assembly.Location));

        /// <summary>
        /// アプリケーション名称を取得します。
        /// </summary>
        public string Product
                =>
                _product
                ?? (_product =
                            ((AssemblyProductAttribute)
                                Attribute.GetCustomAttribute(_assembly, typeof(AssemblyProductAttribute))).Product);

        /// <summary>
        /// アプリケーションタイトルを取得します。
        /// </summary>
        public string Title
                =>
                _title
                ?? (_title =
                            ((AssemblyTitleAttribute)
                                Attribute.GetCustomAttribute(_assembly, typeof(AssemblyTitleAttribute))).Title);

        /// <summary>
        /// バージョン番号を取得します。
        /// </summary>
        public Version Version => _version ?? (_version = _assembly.GetName().Version);

        #endregion

        #region Methods

        /// <summary>
        /// このオブジェクトを文字列化します。
        /// </summary>
        /// <returns>このオブジェクトの文字列データ</returns>
        public override string ToString()
        {
            return
                    $"製品名:{Product}, 企業名:{Company}, 説明:{Description}, アセンブリ名:{AssemblyName}, バージョン:{Version}, ロケーション:{Location}, ロケーションディレクトリ:{LocationDirectory}";
        }

        #endregion
    }
}