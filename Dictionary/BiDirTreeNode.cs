using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DictionaryClasses;

namespace DictionaryClasses
{
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
