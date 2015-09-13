using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace T4SampleProject.DataAnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RangeCustomAttribute : RangeAttribute
    {
        public RangeCustomAttribute(double minimum, double maximum)
            : base(minimum, maximum)
        {
            ErrorMessage = "{0}は{1:0.0}～{2:0.0}の値を設定してください。";
        }

        public RangeCustomAttribute(int minimum, int maximum)
            : base(minimum, maximum)
        {
            ErrorMessage = "{0}は{1}～{2}の値を設定してください。";   
        }

        public RangeCustomAttribute(Type type, string minimum, string maximu)
            : base(type, minimum, maximu)
        {
            ErrorMessage = "{0}は{1}～{2}の値を設定してください。";
        }
    }
}
