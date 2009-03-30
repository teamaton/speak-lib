﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SpeakFriend.Utilities
{
    public static class StringExtension
    {
        public static string NEW_LINE { get { return Environment.NewLine; } }

        /// <summary>
        /// Truncates the string to a specified length and replaces the truncated to a ...
        /// </summary>
        /// <param name="text">string that will be truncated</param>
        /// <param name="maxLength">total length of characters to maintain before the truncate happens</param>
        /// <param name="suffix"></param>
        /// <returns>truncated string</returns>
        public static string Truncate(this string text, int maxLength, string suffix)
        {
            string truncatedString = text;

            if (maxLength <= 0)
                return truncatedString;

            int strLength = maxLength - suffix.Length;

            if (strLength <= 0)
                return truncatedString;

            if (text == null || text.Length <= maxLength)
                return truncatedString;

            truncatedString = text.Substring(0, strLength);
            truncatedString = truncatedString.TrimEnd();
            truncatedString += suffix;
            return truncatedString;
        }

        /// <summary>
        /// Truncates the string to a specified length and replace the truncated to a ...
        /// </summary>
        /// <param name="text">string that will be truncated</param>
        /// <param name="maxLength">total length of characters to maintain before the truncate happens</param>
        /// <returns>truncated string</returns>
        public static string Truncate(this string text, int maxLength)
        {
            return text.Truncate(maxLength, "...");
        }

        /// <summary>
        /// Return a list of wrapped lines
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxLineLength"></param>
        /// <returns></returns>
        public static List<string> WordWrap(this string text, int maxLineLength)
        {
            text = text.Replace(NEW_LINE, "\n");

            var lines = text.Split('\n').ToList();

            bool lengthsTrimmed = false;
            while (!lengthsTrimmed)
            {
                var longLines = lines.Where(line => line.Length > maxLineLength).ToList();
                if (longLines.Count() == 0) lengthsTrimmed = true;
                foreach (var longLine in longLines)
                {
                    // get the largest index in the line for which the char is a space
                    // and the index is smaller than the max line length
                    var length = longLine.Select((chr, i) => (chr == ' ' && i <= maxLineLength) ? i : -1).Max();
                    
                    // If no space found, force wrap in the middle of a word.
                    if (length == -1)
                        length = maxLineLength;

                    var newLine1 = longLine.Substring(0, length);
                    var newLine2 = longLine.Substring(length);

                    // Remove space at beginning of wrapped line. Looks better.
                    if (newLine2[0] == ' ')
                        newLine2 = newLine2.Substring(1);

                    lines.InsertRange(lines.IndexOf(longLine),
                                      new[] { newLine1, newLine2 });
                    lines.Remove(longLine);
                }
            }
            return lines;
        }

        /// <summary>
        /// Returns the given text with lines separated by <see cref="Environment.NewLine"/>
        /// and a maximum length of <paramref name="maxLineLength"/>.
        /// Uses <see cref="WordWrap"/> internally.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxLineLength"></param>
        /// <returns></returns>
        public static string Wrap(this string text, int maxLineLength)
        {
            var lines = text.WordWrap(maxLineLength);

            var output = new StringBuilder();

            for (int i = 0; i < lines.Count; i++)
            {
                output.Append(lines[i]);
                if (i < lines.Count - 1)
                    output.AppendLine();
            }

            return output.ToString();
        }

        public static bool IsEmail(this string text)
        {
            if (String.IsNullOrEmpty(text))
                return false;

            const string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                    @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                    @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            var re = new Regex(strRegex);

            if (re.IsMatch(text))
                return true;

            return false;
        }

        public static bool IsUri(this string uri)
        {
            if (String.IsNullOrEmpty(uri))
                return false;

            const string strRegex = @"^[a-z]+([a-z0-9-]*[a-z0-9]+)?(\.([a-z]+([a-z0-9-]*[a-z0-9]+)?)+)*$";
            var re = new Regex(strRegex);

            if (re.IsMatch(uri))
                return true;

            return false;
        }
		
        public static bool IsNumeric(this string value)
        {
            var regex = new Regex(@"^\-?[0-9]+$");
            return regex.IsMatch(value);
        }
    }
}
