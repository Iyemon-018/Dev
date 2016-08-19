namespace Mc.Common.Views
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;

    public enum DialogButtonType
    {
        None,
        Ok,
        OkAndCancel,
        YesAndNo,
    }

    public enum DialogResultKind
    {
        Ok,
        Cancel,
        Yes,
        No,
    }
}