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
    }
}
