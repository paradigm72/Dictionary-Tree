using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DictionaryTester
{
    public partial class DictionaryForm : Form
    {
        string filePath;
        DictionaryClasses.Dictionary myDictionary;
        
        
        public DictionaryForm()
        {
            InitializeComponent();
            myDictionary = new DictionaryClasses.Dictionary();
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            //start in the same directory as this project
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text  = openFileDialog1.FileName;
            }
            
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            filePath = txtFilePath.Text;
            System.Diagnostics.Stopwatch mySW = new System.Diagnostics.Stopwatch();
            mySW.Start();

            if (myDictionary.Initialize(filePath))
            {
                mySW.Stop();

                txtOutput.AppendText("Dictionary loaded successfully in " +  mySW.ElapsedMilliseconds + "ms\r\n");
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch mySW = new System.Diagnostics.Stopwatch();
            mySW.Start();
            string testString = txtInput.Text;

            //Is this a word?
            if (myDictionary.IsWord(testString))
            {
                txtOutput.AppendText(testString + " is a word!");
            }
            else if (myDictionary.IsWordStem(testString))
            {
                txtOutput.AppendText(testString + " is a word stem");
            }
            else
            {
                txtOutput.AppendText(testString + " is not a word");
            }

            mySW.Stop();
            //The execution time should always be less than 1 ms, so we need to track ticks and convert back to ms
            double elapsedMS = (double)mySW.ElapsedTicks / (double)(System.Diagnostics.Stopwatch.Frequency * 1000);
            txtOutput.AppendText(" (" + elapsedMS + "ms)\r\n");

        }

        private void DictionaryForm_Load(object sender, EventArgs e)
        {

        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtInput.Text.Length > 0) { btnCalculate_Click(this, e); }
            }
        }

    }
}
