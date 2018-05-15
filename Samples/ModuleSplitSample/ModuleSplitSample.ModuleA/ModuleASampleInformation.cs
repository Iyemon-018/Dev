using ModuleSplitSample.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleSplitSample.ModuleA
{
    public class ModuleASampleInformation : ISampleInformation
    {
        public string Name => $"{TypeName}({ModelNumberName})";

        public string TypeName => "伊右衛門";

        public string ModelNumberName => "@IYEMON";
    }
}
