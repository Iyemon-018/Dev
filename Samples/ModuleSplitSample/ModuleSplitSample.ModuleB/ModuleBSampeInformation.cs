using ModuleSplitSample.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleSplitSample.ModuleB
{
    public class ModuleBSampeInformation : ISampleInformation
    {
        public string Name => "ModulB Name";

        public string TypeName => "モジュールB";

        public string ModelNumberName => "Module B";
    }
}
