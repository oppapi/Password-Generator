using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Password_Generator
{
    public partial class Form1 : Form
    {
        private StringBuilder wordPool = new StringBuilder();
        private Random rand = new Random();
        public Form1()
        {
             InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            chkUppercase.Checked = chkLowercase.Checked = chkNumbers.Checked = chkSymbols.Checked = false;
            txtLength.Text = txtKeyword.Text = txtPassword.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtPassword.Text)) { Clipboard.SetText(txtPassword.Text); }
        }

        private void txtLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) && txtLength.Text.Length < 2 || e.KeyChar == (char)Keys.Back)
            {
                return;
            }
            e.Handled = true;
        }

        private void txtKeyword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtKeyword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtKeyword.Text.Length < 12 || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space)
            {
                return;
            }
            e.Handled = true;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (check())
            {
                string finalPassword = "";
                int length = Convert.ToInt32(txtLength.Text);
                if (txtKeyword.Text != "")
                {
                    length -= txtKeyword.Text.Length;
                    finalPassword = keywordScrabble(txtKeyword.Text);
                }
                if (chkLowercase.Checked) { wordPool.Append("abcdefghijklmnopqrstuvwxyz"); }
                if (chkUppercase.Checked) { wordPool.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ"); }
                if (chkNumbers.Checked) { wordPool.Append("0123456789"); };
                if (chkSymbols.Checked) { wordPool.Append("!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"); }
                finalPassword += generatePass(wordPool.ToString(), length);
                txtPassword.Text = finalPassword;
            }
        }
        private bool check()
        {
            if (txtLength.Text == "") { return false; }
            if (txtKeyword.Text != "")
            {
                if (!(Convert.ToInt32(txtLength.Text) >= txtKeyword.Text.Length)) { return false; }
                else { return true; }
            }
            else
            {
                return true;
            }
        }
        private string keywordScrabble(string keyword)
        {
            Dictionary<char, string[]> leetMap = new Dictionary<char, string[]>()
            {
                { 'a', new[] { "@", "4" } },
                { 'b', new[] { "8" } },
                { 'c', new[] { "(", "<" } },
                { 'e', new[] { "3" } },
                { 'g', new[] { "6", "9" } },
                { 'h', new[] { "#" } },
                { 'i', new[] { "1", "!" } },
                { 'l', new[] { "1", "|" } },
                { 'o', new[] { "0" } },
                { 's', new[] { "$", "5" } },
                { 't', new[] { "7", "+" } },
                { 'z', new[] { "2" } },

                { 'A', new[] { "@", "4" } },
                { 'B', new[] { "8" } },
                { 'C', new[] { "(", "<" } },
                { 'E', new[] { "3" } },
                { 'G', new[] { "6", "9" } },
                { 'H', new[] { "#" } },
                { 'I', new[] { "1", "!" } },
                { 'L', new[] { "1", "|" } },
                { 'O', new[] { "0" } },
                { 'S', new[] { "$", "5" } },
                { 'T', new[] { "7", "+" } },
                { 'Z', new[] { "2" } },
            };
            StringBuilder leet = new StringBuilder();
            foreach (char letter in keyword)
            {
                if (leetMap.ContainsKey(letter))
                {
                    string[] replacements = leetMap[letter];
                    leet.Append(replacements[rand.Next(replacements.Length)]);
                }
                else
                {
                    leet.Append(letter);
                }
            }
            string leeted = leet.ToString();
            leet.Clear();
            return leeted;
        }

        public string generatePass(string words, int length)
        {
            StringBuilder generatedPassword = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                generatedPassword.Append(words[rand.Next(words.Length)]);
            }
            wordPool.Clear();
            return generatedPassword.ToString();
        }
    }
}
