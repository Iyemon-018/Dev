using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace T4SampleProject.DataAnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class StringLengthCustomAttribute : StringLengthAttribute
    {
        public StringLengthCustomAttribute(int maxLength)
            : this(0, maxLength)
        {
            this.ErrorMessage = "{0}は{2}文字以内で入力してください。";
        }

        public StringLengthCustomAttribute(int minLength, int maxLength)
            : base(maxLength)
        {
            this.MinimumLength = minLength;
            this.ErrorMessage = "{0}は{1}文字以上、{2}文字以内で入力してください。";
        }
    }
}
