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

namespace Password_Generator
{
    public partial class Form1 : Form
    {
        private StringBuilder wordPool = new StringBuilder();
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
            if (chkLowercase.Checked) { wordPool.Append("abcdefghijklmnopqrstuvwxyz"); }
            if (chkUppercase.Checked) { wordPool.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ"); }
            if (chkNumbers.Checked) { wordPool.Append("0123456789"); };
            if (chkSymbols.Checked) { wordPool.Append("!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"); }
            if (txtLength.Text != "")
            {
                txtPassword.Text = generatePass(wordPool.ToString());
            }
        }

        public String generatePass(string words)
        {
            Random rand = new Random();
            StringBuilder generatedPassword = new StringBuilder();

            int length = Convert.ToInt32(txtLength.Text);
            for (int i = 0; i < length; i++)
            {
                generatedPassword.Append(words[rand.Next(words.Length)]);
            }
            wordPool.Clear();
            return generatedPassword.ToString();
        }
    }
}
