namespace Mc.Common.Wpf.Core.ComponentModels
{
    using System;
    using System.Collections.Concurrent;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// プロパティの変更通知を発行することのできるオブジェクトの基底クラスです。
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public abstract class BindableBase : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// プロパティ変更通知の遅延実行機能
        /// </summary>
        private PropertyDeferExecuter _deferExecuter;

        #endregion

        #region Events

        /// <summary>
        /// プロパティ値が変更するときに発生します。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        /// <summary>
        /// プロパティ変更通知の遅延実行<see cref="IDisposable"/> を呼び出します。
        /// </summary>
        /// <returns>解放可能な<see cref="IDisposable"/> オブジェクト</returns>
        public IDisposable DeferPropertyChanged()
        {
            _deferExecuter = new PropertyDeferExecuter(this);
            return _deferExecuter;
        }

        /// <summary>
        /// <see cref="PropertyChanged"/> イベントを呼び出します。
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// プロパティの値が変更される前に呼び出されます。
        /// </summary>
        /// <typeparam name="T">プロパティの型</typeparam>
        /// <param name="field">フィールドの値</param>
        /// <param name="value">変更する値</param>
        /// <param name="propertyName">プロパティ名</param>
        protected virtual void OnSetPropertyBefore<T>(T field, T value, string propertyName)
        {
            _deferExecuter?.AddProperyName(propertyName);
        }

        /// <summary>
        /// メンバーの値を変更し、プロパティの変更通知を発行します。
        /// </summary>
        /// <typeparam name="T">プロパティの型</typeparam>
        /// <param name="field">フィールドの値</param>
        /// <param name="value">変更する値</param>
        /// <param name="propertyName">プロパティ名</param>
        /// <returns>変更結果(true:プロパティを変更した。, false:プロパティは変更されていない。)</returns>
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field,value))
            {
                // フィールドと変更する値が一致しているので変更されていない扱いとする。
                return false;
            }

            // 変更前の処理
            // 派生クラス側で実行できるようにする。
            OnSetPropertyBefore(field, value, propertyName);

            // フィールドを更新して、変更通知を呼び出す。
            field = value;
            OnPropertyChanged(propertyName);

            // フィールドの値が更新されました。
            return true;
        }

        #endregion

        #region Nested Classes

        /// <summary>
        /// <see cref="BindableBase.PropertyChanged" /> イベントの遅延実行機能クラスです。
        /// </summary>
        /// <seealso cref="System.IDisposable" />
        internal class PropertyDeferExecuter : IDisposable
        {
            #region Fields

            /// <summary>
            /// プロパティ名キュー
            /// </summary>
            private readonly ConcurrentQueue<string> _queue = new ConcurrentQueue<string>();

            /// <summary>
            /// 変更通知を実行する対象の<see cref="BindableBase"/> オブジェクト
            /// </summary>
            private readonly BindableBase _target;

            #endregion

            #region Ctor

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="target">プロパティ名の遅延変更通知実行対象オブジェクト</param>
            /// <exception cref="System.ArgumentNullException"><paramref name="target"/> がnull の場合に発行されます。</exception>
            public PropertyDeferExecuter(BindableBase target)
            {
                if (target == null) throw new ArgumentNullException(nameof(target));
                _target = target;
            }

            #endregion

            #region Methods

            /// <summary>
            /// 遅延変更通知を呼び出すプロパティ名を追加します。
            /// </summary>
            /// <param name="propertyName">遅延変更通知の対象プロパティ名</param>
            public void AddProperyName(string propertyName)
            {
                _queue.Enqueue(propertyName);
            }

            #endregion

            #region IDisposable Support

            /// <summary>
            /// 当オブジェクトを解放したかどうか
            /// </summary>
            private bool _disposedValue;

            /// <summary>
            /// このオブジェクトを解放します。
            /// </summary>
            /// <param name="disposing">
            /// true:マネージドリソースとアンマネージドリソースの両方を解放します。,
            /// false:アンマネージドリソースのみ解放します。
            /// </param>
            protected virtual void Dispose(bool disposing)
            {
                if (!_disposedValue)
                {
                    if (disposing)
                    {
                        // マネージドリソースの解放
                    }
                    
                    // アンマネージドリソースの解放
                    // キューにためているプロパティに変更通知を呼び出す。
                    string propertyName;
                    while (_queue.TryDequeue(out propertyName))
                    {
                        _target.OnPropertyChanged(propertyName);
                    }

                    // 解放済み
                    _disposedValue = true;
                }
            }

            /// <summary>
            /// ファイナライズ
            /// </summary>
            ~PropertyDeferExecuter()
            {
                Dispose(false);
            }

            /// <summary>
            /// アンマネージ リソースの解放およびリセットに関連付けられているアプリケーション定義のタスクを実行します。
            /// </summary>
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            #endregion
        }

        #endregion
    }
}