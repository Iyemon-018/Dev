namespace JenkinsNotification.Core.Extensions
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using JenkinsNotification.Core.Utility;

    /// <summary>
    /// Xml シリアライズ拡張メソッドです。
    /// </summary>
    public static class XmlSerializer
    {
        #region Methods

        /// <summary>
        /// 指定したファイルパスへシリアライズします。
        /// </summary>
        /// <typeparam name="T">自分自身の型</typeparam>
        /// <param name="self">自分自身</param>
        /// <param name="filePath">出力先ファイルパス</param>
        public static void Serialize<T>(this T self, string filePath)
        {
            string directory = Path.GetDirectoryName(filePath);
            FileUtility.CreateDirectory(directory);

            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                serializer.Serialize(fs, self);
            }
        }

        /// <summary>
        /// 指定したファイルパスへシリアライズします。
        /// </summary>
        /// <typeparam name="T">自分自身の型</typeparam>
        /// <param name="self">自分自身</param>
        /// <param name="extraTypes">
        /// シリアル化する追加のオブジェクト型の Type 配列。<para/>
        /// シリアル化したい型を設定すると、<see cref="object"/> 型のデータをシリアライズします。
        /// </param>
        /// <param name="filePath">出力先ファイルパス</param>
        public static void Serialize<T>(this T self, string filePath, params Type[] extraTypes)
        {
            var directory = Path.GetDirectoryName(filePath);
            FileUtility.CreateDirectory(directory);

            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T), extraTypes);
                serializer.Serialize(fs, self);
            }
        }

        /// <summary>
        /// このオブジェクトをXMLへシリアライズします。
        /// </summary>
        /// <typeparam name="T">オブジェクトの型</typeparam>
        /// <param name="self">自分自身</param>
        /// <param name="extraTypes">
        /// シリアル化する追加のオブジェクト型の Type 配列。<para/>
        /// シリアル化したい型を設定すると、<see cref="object"/> 型のデータをシリアライズします。
        /// </param>
        /// <returns>シリアライズしたXMLドキュメント文字列</returns>
        public static string Serialize<T>(this T self, params Type[] extraTypes)
        {
            if (self == null)
            {
                return string.Empty;
            }

            //
            // 名前空間 xsi, xsd は不要なので空にする。
            //
            var ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            //
            // 出力するXMLの設定
            // エンコードはUTF8のBOMなし、リトルエンディアン
            // 標準インデントを自動挿入し、XML名前空間の宣言は不要
            // とする。
            //
            var xmlSettings = new XmlWriterSettings
            {
                Encoding = new UnicodeEncoding(false, false),
                Indent = true,
                OmitXmlDeclaration = false
            };

            string result;

            using (var textWriter = new Utf8StringWriter())
            using (var xmlWriter = XmlWriter.Create(textWriter, xmlSettings))
            {
                // シリアライズ実施
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T), extraTypes);
                serializer.Serialize(xmlWriter, self, ns);
                result = textWriter.ToString();
            }

            return result;
        }

        #endregion

        #region Nested Classes

        /// <summary>
        /// UTF8エンコードの<see cref="StringWriter"/> クラスです。
        /// </summary>
        /// <seealso cref="System.IO.StringWriter" />
        private sealed class Utf8StringWriter : StringWriter
        {
            #region Properties

            /// <summary>Gets the <see cref="T:System.Text.Encoding" /> in which the output is written.</summary>
            /// <returns>The Encoding in which the output is written.</returns>
            public override Encoding Encoding => Encoding.UTF8;

            #endregion
        }

        #endregion
    }
}