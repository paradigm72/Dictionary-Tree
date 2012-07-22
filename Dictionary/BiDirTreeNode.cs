using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DictionaryClasses;

namespace DictionaryClasses
{
    /// <summary>
    /// This is a dictionary tree node that can be used to go both backwards and forwards in words to check validity.
    /// This would be useful if, for example, you know some of the letters in the middle of a word, and want to
    /// check whether those letters form a valid word 'stem'.
    /// 
    /// 7/21/12:
    /// The bidirectional tree is currently not actually built by Dictionary.Initialize and the API to use it effectively
    /// doesn't exist yet (you'd need to be able to pass in some kind of pattern code to use this for something like
    /// Cryptoquote solving).
    /// </summary>
    class BiDirTreeNode
    {
        char myLeftChar;
        char myRightChar;
        BiDirTreeNode[] myChildren;
        bool isTerminator;

        /// <summary>
        /// Returns true iff the path terminating on this node represents a valid word in the dictionary.
        /// </summary>
        public bool IsTerminator { get { return isTerminator; } set { IsTerminator = value; } }

        public BiDirTreeNode(char leftChar, char rightChar)
        {
            myLeftChar = leftChar;
            myRightChar = rightChar;
            myChildren = new BiDirTreeNode[729];
            isTerminator = false;
        }

        public BiDirTreeNode(List<char> leftRightChars)
        {
            myLeftChar = leftRightChars[0];
            myRightChar = leftRightChars[1];
            myChildren = new BiDirTreeNode[729];
            isTerminator = false;
        }

        public BiDirTreeNode()
        {
            myChildren = new BiDirTreeNode[729];
            isTerminator = false;
        }

        public void AddChild(char leftChar, char rightChar)
        {
            myChildren[Utils.LetterPairToInt(leftChar, rightChar)] = new BiDirTreeNode(leftChar, rightChar);
        }

        public void AddChild(int address)
        {
            myChildren[address] = new BiDirTreeNode(Utils.IntToLetterPair(address));
        }

    }
}
