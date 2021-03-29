using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HashLib
{
    public class WordStreamer  : IDisposable
    {
        public WordStreamer(string filename, 
            string delimiters = " \t.?!;:!#@,$%^&*()_+~-`\'\"\\")
        {
            Delimiters = delimiters.ToCharArray();
            reader = new StreamReader(filename);
        }

        public string getNextWord(out bool endOfFile)
        {
            endOfFile = false;
            while(currentLine == null || currentWordIndex >= currentLine.Length 
                && !endOfFile)
            {
                if (reader.Peek() > 0)
                {
                    string lineRead = reader.ReadLine();
                    currentLine = lineRead.Split(Delimiters,
                        StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    endOfFile = true;
                }
                currentWordIndex = 0;
            }
            string returnString = "";
            if (!endOfFile) returnString = currentLine[currentWordIndex++];
            returnString = returnString.ToLower();
            return returnString;
        }

        public void Dispose()
        {
            reader.Close();
        }

        char[] Delimiters;
        string[] currentLine;
        int currentWordIndex = 0;
        StreamReader reader;
    }
}
