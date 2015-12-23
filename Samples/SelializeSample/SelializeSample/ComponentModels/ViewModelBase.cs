using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelializeSample.ComponentModels
{
    /// <summary>
    /// すべてのViewModelの基底クラスです。
    /// </summary>
    [Serializable]
    public abstract class ViewModelBase : BindableBase
    {
        /// <summary>
        /// オブジェクトの状態を保存するためのスナップショットデータです。
        /// </summary>
        private ViewModelBase _snapshot;

        /// <summary>
        /// スナップショットデータを保存します。
        /// </summary>
        public void SaveSnapshotData()
        {
            using (new TimeTracer("オブジェクトのコピー"))
            {
                _snapshot = this.DeepCopy();
            }
        }

        /// <summary>
        /// オブジェクトが更新されたかどうかを判定します。
        /// </summary>
        /// <returns>
        /// オブジェクトが更新されている場合は、true を
        /// 更新されていない場合は、false を返します。
        /// </returns>
        public bool IsUpdate()
        {
            using (new TimeTracer("更新チェック"))
            {
                if (_snapshot == null)
                {
                    return true;
                }

                return !this.DeepCopy().Equals(_snapshot);
            }
        }

        /// <summary>
        /// オブジェクトの状態を元に戻します。
        /// </summary>
        public void Reset()
        {
            using (new TimeTracer("オブジェクトにリセット"))
            {
                if (_snapshot != null)
                {
                    UpdateObject(_snapshot);
                }
            }
        }

        /// <summary>
        /// オブジェクトを更新します。
        /// </summary>
        /// <param name="obj">オブジェクトデータ</param>
        protected virtual void UpdateObject(ViewModelBase obj)
        {

        }
    }
}
