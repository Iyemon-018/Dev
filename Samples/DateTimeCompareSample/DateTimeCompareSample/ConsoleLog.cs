using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeCompareSample
{
    public static class ConsoleLog
    {
        public static void Write(string message
                                , [CallerFilePath]string filePath = ""
                                , [CallerMemberName]string memberName = ""
                                , [CallerLineNumber]int lineNumber = 0)
        {
            Console.WriteLine("> {0:yyyy/MM/dd(ddd) HH:mm:ss.fff} {1} ({2}) Line {3} : {4}"
                              , DateTime.Now
                              , Path.GetFileName(filePath)
                              , memberName
                              , lineNumber
                              , message);
        }
    }
}
