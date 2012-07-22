using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;    

namespace DictionaryClasses
{
    public class Dictionary
    {
        //The dictionary itself contains the root node of the dictionary nodes tree - this node points to all other nodes
        private DictionaryClasses.MonoDirTreeNode dictionaryRoot = new DictionaryClasses.MonoDirTreeNode();

        //Also create a bidir tree that we will use to index by letters other than the first
        private DictionaryClasses.BiDirTreeNode dictionaryMiddleRoot = new DictionaryClasses.BiDirTreeNode();

        private List<string> Words;

        /// <summary>
        /// Create the dictionary data structure on program load
        /// </summary>
        public bool Initialize(string RelativePath)
        {

            //file input setup
            FileStream input;
            try { input = File.OpenRead(RelativePath); }
            catch (FileNotFoundException) { return false; }
            BinaryReader reader; 
            reader = new BinaryReader(input);
   

            //looping constants
            char currentChar;
            //string currentString;
            bool endWord = false;
            bool endFile = false;

            //dictionary itself
            DictionaryClasses.MonoDirTreeNode currentRootBasedNode;
            DictionaryClasses.MonoDirTreeNode childRootBasedNode;

            currentRootBasedNode = dictionaryRoot;
            childRootBasedNode = new DictionaryClasses.MonoDirTreeNode();

            DictionaryClasses.BiDirTreeNode currentMiddleBasedNode;
            DictionaryClasses.BiDirTreeNode childLeftMiddleBasedNode;
            DictionaryClasses.BiDirTreeNode childRightMiddleBasedNode;

            currentMiddleBasedNode = dictionaryMiddleRoot;
            childLeftMiddleBasedNode = new DictionaryClasses.BiDirTreeNode();
            childRightMiddleBasedNode = new DictionaryClasses.BiDirTreeNode();


            //loop over the words
            do
            {
                ////////////////////////////
                // Word-level processing ///
                ////////////////////////////

                //reset the word-level vars
                endWord = false;                
                
                //mark the current word as valid (add terminator)
                currentRootBasedNode.IsTerminator = true;

                //move the node marker back to the top
                currentRootBasedNode = dictionaryRoot;

                //loop over the chars in the word
                do
                {
                    /////////////////////////////////
                    // Character-level processing ///
                    /////////////////////////////////
                    
                    currentChar = reader.ReadChar();

                    if (currentChar == '\r')
                    {
                        endWord = true;
                    }
                    else if (Utils.IsAlphabetLetter(currentChar))
                    {
                        //add to dictionary, if not already there
                        if (!(currentRootBasedNode.ChildExists(currentChar)))
                        {
                            currentRootBasedNode.AddChild(currentChar);
                        }
                        currentRootBasedNode = currentRootBasedNode.GetChild(currentChar);
                    }



                } while (endWord != true);

                //clear out the \n as well, and see if we've hit the end
                currentChar = reader.ReadChar();
                if (reader.PeekChar() == -1)
                {
                    endFile = true;
                }

            } while (endFile != true);

            //we exited gracefully, so return true
            return true;

        }





        /// <summary>
        /// True if the string is a word in our dictionary
        /// </summary>
        /// <param name="testString"></param>
        /// <returns></returns>
        public bool IsWord(string testString)
        {
            char testChar;
            DictionaryClasses.MonoDirTreeNode currentNode;
            currentNode = dictionaryRoot;

            if (currentNode == null) { return false; }

            for (int i = 0; i < testString.Length; i++)
            {
                testChar = System.Convert.ToChar(testString.Substring(i, 1));
                if (!(Utils.IsAlphabetLetter(testChar)))
                {
                    return false;
                }
                else if (!(currentNode.ChildExists(testChar)))
                {
                    return false;
                }
                else
                {
                    currentNode = currentNode.GetChild(testChar);
                }
            }
            //now that we're at the end, see whether this is a valid terminator
            if (currentNode.IsTerminator)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// True if the string is the stem of a word in our dictionary
        /// </summary>
        /// <param name="testString"></param>
        /// <returns></returns>
        public bool IsWordStem(string testString)
        {
            char testChar;
            DictionaryClasses.MonoDirTreeNode currentNode;
            currentNode = dictionaryRoot;

            if (currentNode == null) { return false; }

            for (int i = 0; i < testString.Length; i++)
            {
                testChar = System.Convert.ToChar(testString.Substring(i, 1));
                if (!(Utils.IsAlphabetLetter(testChar)))
                {
                    return false;
                }
                else if (!(currentNode.ChildExists(testChar)))
                {
                    return false;
                }
                else
                {
                    currentNode = currentNode.GetChild(testChar);
                }
            }
            //return if we got this far - don't care about terminator check
            return true;
        }
    }
}
