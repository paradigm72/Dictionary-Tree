using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DictionaryClasses
{
    /// <summary>
    /// <para>This class is holds a dictionary in the form of a 26-node wide tree.
    /// The depth of the tree is equal to the longest word in the dictionary.</para>
    /// <para>The intended use is to determine whether a given string is a valid English word in O(n) time,
    /// where n is the number of characters in the word.</para>
    /// </summary>
    class MonoDirTreeNode
    {
        char thisChar;   //the character at the current node
        MonoDirTreeNode[] myChildren;
        bool isTerminator;    //does this node terminate a valid word?

        /// <summary>
        /// Returns true iff the path terminating on this node represents a valid word in the dictionary.
        /// </summary>
        public bool IsTerminator { get { return isTerminator; } set { isTerminator = value; } }

        /// <summary>
        /// Constructor for any of the nodes in the tree other than the root
        /// </summary>
        /// <param name="nodeChar"></param>
        public MonoDirTreeNode(char nodeChar)
        {
            thisChar = nodeChar;
            myChildren = new MonoDirTreeNode[26];
            isTerminator = false;
        }

        /// <summary>
        /// Constructor for the root node (doesn't have a character itself)
        /// </summary>
        public MonoDirTreeNode()
        {
            myChildren = new MonoDirTreeNode[26];
            isTerminator = false;
        }


        /// <summary>
        /// Add a child to this node with the given integer character code. Signifies that there is a valid word that can be spelled through this char.
        /// </summary>
        /// <param name="address">A number 1 to 26 representing the child char you're adding. Will add 97 to convert to ASCII.</param>
        public void AddChild(int address)
        {
            myChildren[address] = new MonoDirTreeNode(System.Convert.ToChar(address + 97));
        }

        /// <summary>
        /// Add a child to this node with the given char. Signifies that there is a valid word that can be spelled through this char.
        /// </summary>
        /// <param name="nodeChar">The char (single character) to add.</param>
        public void AddChild(char nodeChar)
        {
            myChildren[System.Convert.ToInt16(nodeChar - 97)] = new MonoDirTreeNode(nodeChar);
        }


        /// <summary>
        /// Returns true iff the passed-in character is a valid next character in a real word.
        /// </summary>
        /// <example>If this node is the 'k' in "bak", then ChildExists('e') returns true, but ChildExists('j') returns false.</example>
        public Boolean ChildExists(char nodeChar)
        {
            MonoDirTreeNode testNode;
            testNode = myChildren[System.Convert.ToInt16(nodeChar - 97)];
            if (testNode == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Returns true iff the passed-in character code represents a valid next character in a real word.
        /// </summary>
        /// <example>If this node is the 'k' in "bak", then ChildExists(5) returns true, but ChildExists(10) returns false.</example>
        /// <param name="address">A number 1 to 26 representing the child char you're checking for. Will add 97 to convert to ASCII.</param>
        public Boolean ChildExists(int address)
        {
            MonoDirTreeNode testNode;
            testNode = myChildren[address];
            if (testNode == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Returns the given child node of this node, if it exists.
        /// </summary>
        /// <example>If this node is the 'k' in "bak", then GetChild('e') will return the 'e' in "bake".</example>
        public MonoDirTreeNode GetChild(char nodeChar)
        {
            MonoDirTreeNode childNode;
            childNode = myChildren[System.Convert.ToInt16(nodeChar - 97)];
            return childNode;

        }

        /// <summary>
        /// Returns the given child node of this node, if it exists.
        /// </summary>
        /// <param name="address">A number 1 to 26 representing the child char you're checking for. Will add 97 to convert to ASCII.</param>
        /// <example>If this node is the 'k' in "bak", then GetChild(5) will return the 'e' in "bake".</example>
        public MonoDirTreeNode GetChild(int address)
        {
            MonoDirTreeNode childNode;
            childNode = myChildren[address];
            return childNode;
        }

    }
}
