/* CSVReader - a simple open source C# class library to read CSV data
 * by Andrew Stellman - http://www.stellman-greene.com/CSVReader
 * 
 * CSVReader.cs - Class to read CSV data from a string, file or stream
 * 
 * download the latest version: http://svn.stellman-greene.com/CSVReader
 * 
 * (c) 2008, Stellman & Greene Consulting
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of Stellman & Greene Consulting nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY STELLMAN & GREENE CONSULTING ''AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL STELLMAN & GREENE CONSULTING BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 * 
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Text;

namespace Com.StellmanGreene.CSVReader
{
    public class CSVReader
    {
        private static char _csvSeparator = ',';
        private static bool _trimColumns = false;
        public static string GetCSVFormat(string str)
        {
            string tempStr = str;
            if (str.Contains(","))
            {
                if (str.Contains("\""))
                {
                    tempStr = str.Replace("\"", "\"\"");
                }
                tempStr = "\"" + tempStr + "\"";
            }
            return tempStr;
        }

        public static string GetCSVFormatLine(List<string> strList)
        {
            string tempStr = "";
            for (int i = 0; i < strList.Count - 1; i++)
            {
                string str = strList[i];
                tempStr = tempStr + GetCSVFormat(str) + ",";
            }
            tempStr = tempStr + GetCSVFormat(strList[strList.Count - 1]) + "\r\n";
            return tempStr;
        }

        public static List<string> ParseLine(string line)
        {
            StringBuilder _columnBuilder = new StringBuilder();
            List<string> Fields = new List<string>();
            bool inColum = false;
            bool inQuotes = false;
            bool isNotEnd = false;
            _columnBuilder.Remove(0, _columnBuilder.Length);

            if (line == "")
            {
                Fields.Add("");
            }
            for (int i = 0; i < line.Length; i++)
            {
                char character = line[i];

                if (!inColum)
                {
                    inColum = true;
                    if (character == '"')
                    {
                        inQuotes = true;
                        continue;
                    }
                }
                if (inQuotes)
                {
                    if ((i + 1) == line.Length)
                    {
                        if (character == '"')
                        {
                            inQuotes = false;
                            continue;
                        }
                        else
                        {
                            isNotEnd = true;
                        }
                    }
                    else if (character == '"' && line[i + 1] == _csvSeparator)
                    {
                        inQuotes = false;
                        inColum = false;
                        i++;
                    }
                    else if (character == '"' && line[i + 1] == '"')
                    {
                        i++;
                    }
                    else if (character == '"')
                    {
                        throw new System.Exception("格式错误，错误的双引号转义");
                    }
                }
                else if (character == _csvSeparator)
                {
                    inColum = false;
                }
                if (!inColum)
                {
                    Fields.Add(_trimColumns ? _columnBuilder.ToString().Trim() : _columnBuilder.ToString());
                    _columnBuilder.Remove(0, _columnBuilder.Length);
                }
                else
                {
                    _columnBuilder.Append(character);
                }
            }

            if (inColum)
            {
                if (isNotEnd)
                {
                    _columnBuilder.Append("\r\n");
                }
                Fields.Add(_trimColumns ? _columnBuilder.ToString().Trim() : _columnBuilder.ToString());
            }
            else
            {
                Fields.Add("");
            }
            return Fields;
        }

        public static List<List<string>> Read(string filePath, Encoding encoding)
        {
            List<List<string>> result = new List<List<string>>();
            string content = File.ReadAllText(filePath, encoding);
            string[] lines = content.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < lines.Length; i++)
            {
                List<string> line = ParseLine(lines[i]);
                result.Add(line);
            }
            return result;
        }
    }
}