using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Trackr
{
    public static class Helper
    {
        // PascalCase to sentence
        public static string CaseToSentence(this string str)
        {
            return Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1]));
        }

        // Trim FooBarSuffix to Foo Bar
        public static string TrimToSentence(this string str)
        {
            String text = str.CaseToSentence();
            String[] textArr = text.Split(' ');
            textArr = textArr.Take(textArr.Count() - 1).ToArray();

            text = "";
            foreach (String s in textArr)
                text += s + " ";

            return text.Trim();
        }
    }
}
