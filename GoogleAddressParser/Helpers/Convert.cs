using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GoogleAddressParser.Helpers
{

    public static class Convert
    {
        public static string TruncateAtSentence(this string text, int length, int lengthMin = -1,
                                       bool addEllipsis = true)
        {
            if (string.IsNullOrEmpty(text) || text.Length <= length) return text;


            const string ellChar = "…";
            const int defLengthMin = 20;
            char[] goodChars = { ',', ';' };
            char[] badChars = { ':', '-', '—', '–', ' ' };

            var r = text;
            if (lengthMin < 0 || lengthMin > length)
            {

                lengthMin = length * .8 > defLengthMin ? (length * 0.8).ToInt() : defLengthMin;

            }



            Regex rx = new Regex(@"(\S.+?[.!?])(?=\s+|$)");
            var ret = new StringBuilder();
            foreach (Match match in rx.Matches(text))
            {
                if (ret.ToString().Length + match.Value.Trim().Length + 1 <= length)
                {
                    if (ret.Length > 0)
                        ret.Append(" ");

                    ret.Append(match.Value.Trim());

                }
                else
                {
                    if (ret.Length >= lengthMin)
                        return ret.ToString();
                    else
                    {
                        if (ret.Length > 0)
                            ret.Append(" ");

                        ret.Append(match.Value.Trim());
                        break;
                    }
                }
            }

            r = ret.ToString().Trim();
            if (r.Length <= length) return r;

            if (length - lengthMin < 0)
            {
                return r;
            }
            int index;
            index = r.IndexOfAny(goodChars, lengthMin, length - lengthMin);
            if (index > 0)
                return addEllipsis ? r.Substring(0, index).Trim() + ellChar : r.Substring(0, index).Trim();

            index = r.IndexOfAny(badChars, lengthMin, length - lengthMin);
            if (index > 0)
                return addEllipsis ? r.Substring(0, index).Trim() + ellChar : r.Substring(0, index).Trim();

            return addEllipsis ? r.Substring(0, length).Trim() + ellChar : r.Substring(0, length).Trim();
        }
        public static string StripHtml(this string html)
        {
            html = html.ToStr();
            char[] array = new char[html.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < html.Length; i++)
            {
                char let = html[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
        public static int ToInt(this object arg, int def = 0)
        {
            int ret = def;

            Int32.TryParse(arg.ToStr(), out ret);

            return ret;
        }
        public static double ToDouble(this object arg, double def = 0)
        {
            double ret = def;

            double.TryParse(arg.ToStr(), out ret);

            return ret;
        }
        public static decimal ToDecimal(this object arg, decimal def = 0)
        {
            decimal ret = def;

            decimal.TryParse(arg.ToStr(), out ret);

            return ret;
        }

        public static float ToFloat(this object arg, float def = 0)
        {
            float ret = def;

            float.TryParse(arg.ToStr(), out ret);

            return ret;
        }

        public static string ToStr(this object arg)
        {
            string ret = String.Empty;
            if (arg != null)
            {
                ret = arg.ToString();
            }
            return ret;
        }

        public static bool ToBool(this object arg, bool defaultValue = false)
        {
            bool ret = defaultValue;
            var m = arg.ToStr();
            if (!String.IsNullOrEmpty(m) && !bool.TryParse(m, out ret))
            {
                if (arg.ToStr().ToLower().Contains((!defaultValue).ToString().ToLower()))
                {
                    ret = !defaultValue;
                }
            }


            return ret;
        }
        public static string Right(this string str, int length)
        {
            return str.Substring(str.Length - length, length);
        }

        public static string MyLast(this string str, int length)
        {
            if (str == null)
                return null;
            else if (str.Length >= length)
                return str.Substring(str.Length - length, length);
            else
                return str;
        }
        public static string ToStr(this object arg, int length)
        {
            string ret = string.Empty;
            if (arg != null)
            {
                ret = arg.ToString();
            }
            if (ret.Length > length)
            {
                return ret.Substring(0, length);
            }
            else
            {
                return ret;
            }
        }

        public static string ToStr(this string text, int minLen, int maxLen)
        {
            string s = text != null ? text : "";
            if (s.Length > maxLen) s = s.Substring(0, maxLen).Trim();

            int ix = 0;
            ix = s.LastIndexOf(".");
            if (ix > minLen)
            {
                s = s.Substring(0, ix + 1).Trim();
            }
            else if ((ix = s.LastIndexOf(",")) > minLen)
            {
                s = s.Substring(0, ix).Trim();

            }
            else if ((ix = s.LastIndexOf(" ")) > minLen)
            {
                s = s.Substring(0, ix).Trim();
            }

            return s;
        }

        static Regex _dateRegex = new Regex(@"^(19|20)(\d\d)[- /.]?(0[1-9]|1[012])[- /.]?(0[1-9]|[12][0-9]|3[01])$", RegexOptions.Compiled);




        public static DateTime? ToNullableDateTime(this object arg)
        {
            DateTime ret = DateTime.MinValue;

            if (!DateTime.TryParse(arg.ToStr(), out ret))
            {
                Match md = _dateRegex.Match(arg.ToStr());

                if (md != null && md.Groups.Count == 5)
                {
                    int year = (md.Groups[1].Value + md.Groups[2].Value).ToInt();
                    int month = (md.Groups[3].Value).ToInt();
                    int day = (md.Groups[4].Value).ToInt();
                    try
                    {
                        ret = new DateTime(year, month, day);
                    }
                    catch { }
                }


            }



            if (ret != DateTime.MinValue)
            {
                return ret;
            }
            else
            {
                return null;
            }
        }


        public static DateTime ToDateTime(this object arg)
        {
            DateTime ret = DateTime.MinValue;

            if (!DateTime.TryParse(arg.ToStr(), out ret))
            {
                Match md = _dateRegex.Match(arg.ToStr());

                if (md != null && md.Groups.Count == 5)
                {
                    int year = (md.Groups[1].Value + md.Groups[2].Value).ToInt();
                    int month = (md.Groups[3].Value).ToInt();
                    int day = (md.Groups[4].Value).ToInt();
                    try
                    {
                        ret = new DateTime(year, month, day);
                    }
                    catch { }
                }

            }


            return ret;
        }


        public static string ToFormatedTodayDateTime(this DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return ToFormatedTodayDateTime(dateTime.Value);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ToFormatedTodayDateTime(this DateTime dateTime)
        {
            if (dateTime.Date == DateTime.Now.Date)
            {
                return dateTime.ToString("hh:mm tt");
            }
            else
            {
                return dateTime.ToString("MMM d, yyyy");
            }
        }

    }
}
