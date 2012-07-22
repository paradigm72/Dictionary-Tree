using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;  

namespace DictionaryClasses
{
    static class Utils
    {
        /// <summary>
        /// Checks whether the given char is a real alphabet letter or not (ASCII lookup)
        /// </summary>
        /// <param name="testChar">Char to check for containment inside the a-z, A-Z range</param>
        /// <returns>True if this is a legitimate alphabetic character, false otherwise</returns>
        public static bool IsAlphabetLetter(char testChar)
        {
            if (System.Convert.ToInt16(testChar) >= 97 && System.Convert.ToInt16(testChar) <= 122)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns array address for the bidirectional tree for <a,b> pairs. Formula is 
        /// [left*27 + right]
        /// E.g., ab = 1, bb = 28, bc = 29
        /// </summary>
        public static int LetterPairToInt(char leftChar, char rightChar)
        {
            int pairCode;
            pairCode = System.Convert.ToInt32(leftChar - 97) * 27 + System.Convert.ToInt32(rightChar);
            return pairCode;
        }

        public static List<char> IntToLetterPair(int address)
        {
            List<char> letterPair = new List<char>(2);
            int leftAddress;
            int rightAddress;

            leftAddress = address / 27;
            rightAddress = address % 27;

            letterPair[0] = System.Convert.ToChar(leftAddress + 97);
            letterPair[1] = System.Convert.ToChar(rightAddress + 97);

            return letterPair;
        }


    }
}