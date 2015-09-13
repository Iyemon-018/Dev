using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace T4SampleProject.DataAnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredCustomAttribute : RequiredAttribute
    {
        public RequiredCustomAttribute()
            : base()
        {
            ErrorMessage = "{0}を入力してください。";
        }
    }
}
