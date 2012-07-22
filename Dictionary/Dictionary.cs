using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;    

namespace DictionaryClasses
{
    /// <summary>
    /// <para>This class holds a dictionary in the form of a 26-node wide tree.
    /// The depth of the tree is equal to the longest word in the dictionary.</para>
    /// <para>The intended use is to determine whether a given string is a valid English word in O(n) time,
    /// where n is the number of characters in the word.</para>
    /// </summary>
    public class Dictionary
    {
        //The dictionary itself contains the root node of the dictionary nodes tree - this node points to all other nodes
        private DictionaryClasses.MonoDirTreeNode dictionaryRoot = new DictionaryClasses.MonoDirTreeNode();

        //Also create a bidir tree that we will use to index by letters other than the first (not yet fully implemented)
        private DictionaryClasses.BiDirTreeNode dictionaryMiddleRoot = new DictionaryClasses.BiDirTreeNode();

        private List<string> Words;

        /// <summary>
        /// Create the dictionary object. To be run once sometime during load. Loads words and builds the
        /// tree at a rate of something like 120 words per millisecond (highly dependent on disk I/O and CPU).
        /// </summary>
        /// <param name="RelativePath">
        /// File path to the dictionary.txt file. This must be a plain text file with one word per line.
        /// </param>
        /// <returns>True if initialization is successful. False otherwise (typically because the file can't be found).</returns>
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

            //dictionary itself - top node is a navigational marker, all children represent letters
            DictionaryClasses.MonoDirTreeNode currentRootBasedNode;
            DictionaryClasses.MonoDirTreeNode childRootBasedNode;

            //the mono-directional tree is used for cases where we know either
            //the entire potential word, or the first characters of the word
            currentRootBasedNode = dictionaryRoot;
            childRootBasedNode = new DictionaryClasses.MonoDirTreeNode();

            //the bi-directional tree is used for cases where we know some letters in
            //the middle of the word (not yet fully implemented as of 7/21/12)
            DictionaryClasses.BiDirTreeNode currentMiddleBasedNode;
            DictionaryClasses.BiDirTreeNode childLeftMiddleBasedNode;
            DictionaryClasses.BiDirTreeNode childRightMiddleBasedNode;
            currentMiddleBasedNode = dictionaryMiddleRoot;
            childLeftMiddleBasedNode = new DictionaryClasses.BiDirTreeNode();
            childRightMiddleBasedNode = new DictionaryClasses.BiDirTreeNode();


            //loop over the lines in the text file (each represents one word)
            do
            {
                ////////////////////////////////
                // Word-level processing     ///
                //  jump back up to root node //
                ////////////////////////////////
                //reset the word-level vars
                endWord = false;                
                
                //mark the current word as valid (add terminator)
                currentRootBasedNode.IsTerminator = true;

                //move the node marker back to the top
                currentRootBasedNode = dictionaryRoot;

                //loop over the characters on the line (each represents a letter in the word)
                do
                {
                    /////////////////////////////////
                    // Character-level processing ///
                    //  fill out the tree         ///
                    /////////////////////////////////                    
                    currentChar = reader.ReadChar();

                    if (currentChar == '\r')  //end of line
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
                        //traverse down to the child we just added and repeat the loop (read the next char on the line)
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
        /// Determine whether the passed-in string represents a complete word in our dictionary
        /// </summary>
        /// <param name="testString">The word to check validity of</param>
        /// <returns>True if this is a valid, complete word; False otherwise</returns>
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
                return false;   //if not, this was a word stem but not a complete word
            }

        }


        /// <summary>
        /// Determine whether the passed-in string represents either a complete word, or a valid
        /// beginning of a complete word, in our dictionary
        /// </summary>
        /// <param name="testString">The word to check validity of</param>
        /// <returns>True if this is a valid beginning of a word; False otherwise</returns>
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
