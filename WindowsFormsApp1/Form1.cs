using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string[] recenice;
        string odabranitekst = "";
        Class1 c = new Class1();

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
         
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Niste odabrali tekst!");
                return;
            }
            else
            {
                odabranitekst = listBox1.SelectedItem.ToString() + ".txt";
            }

            textBox1.Enabled = true;

            char[] krajevirecenica = new char[] { '.', '!', '?'}; 
            using (StreamReader sr = File.OpenText(odabranitekst)) 
            {
                string tekst = sr.ReadToEnd();
                recenice = tekst.Split(krajevirecenica);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

          
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        
        private void Oznacavanje(string uzorak)
        {
            int da = 0;
            int indeks=0;
            string t = richTextBox1.Text;
            richTextBox1.Text = "";
            richTextBox1.Text = t;
            while (indeks < richTextBox1.Text.LastIndexOf(uzorak))
            {
                richTextBox1.Find(uzorak, indeks, richTextBox1.TextLength, RichTextBoxFinds.None);               
                richTextBox1.SelectionColor = Color.GreenYellow;
                da += 1;
                indeks = richTextBox1.Text.IndexOf(uzorak, indeks) + 1;
            }
            if (da>=10)
            {
                richTextBox1.AppendText("DOBIVENI PODACI NAKON OBAVLJENOG ALGORITMA SU:" +"\n");
                richTextBox1.AppendText("Broj pojavljivanja upisane riječi je ");
                richTextBox1.AppendText(da.ToString());
                richTextBox1.AppendText(" i ona se često pojavljuje u odabranom tekstu.");            
            }
            else
            {
                richTextBox1.AppendText("DOBIVENI PODACI NAKON OBAVLJENOG ALGORITMA SU:" + "\n");
                richTextBox1.AppendText("Broj pojavljivanja upisane riječi je ");
                richTextBox1.AppendText(da.ToString());
                richTextBox1.AppendText(" i ona se ne pojavljuje toliko često u odabranom tekstu.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            string uzorak = textBox1.Text;
            if (uzorak == "")
            {
                return;
            }
            foreach (string item in recenice)
            {
                if (c.KnutMorrisPratt(uzorak, item))
                {
                    richTextBox1.AppendText(item + "\n\n");
                }
            }

            if (richTextBox1.Text == "")
            {
                richTextBox1.AppendText("Nije pronaden trazeni uzorak!");
            }
            else
            {
                Oznacavanje(uzorak);
            }


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            string uzorak = textBox1.Text;
            if (uzorak == "")
            {
                return;
            }

            foreach (string item in recenice)
            {
                if (c.BruteForce(uzorak, item))
                {
                    richTextBox1.AppendText(item + "\n\n");
                }
            }

            if (richTextBox1.Text == "")
            {
                richTextBox1.AppendText("Nije pronaden trazeni uzorak!");
            }
            else
            {
                Oznacavanje(uzorak);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            string uzorak = textBox1.Text;
            if (uzorak == "")
            {
                return;
            }

            foreach (string item in recenice)
            {
                if (c.BoyerMooreSimpleSearch(uzorak, item))
                {
                    richTextBox1.AppendText(item + "\n\n");
                }
            }

            if (richTextBox1.Text == "")
            {
                richTextBox1.AppendText("Nije pronaden trazeni uzorak!");
            }
            else
            {
                Oznacavanje(uzorak);
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            string uzorak = textBox1.Text;
            if (uzorak == "")
            {
                return;
            }

            foreach (string item in recenice)
            {
                if (c.RabinKarp(uzorak, item))
                {
                    richTextBox1.AppendText(item + "\n\n");
                }
            }

            if (richTextBox1.Text == "")
            {
                richTextBox1.AppendText("Nije pronaden trazeni uzorak!");
            }
            else
            {
                Oznacavanje(uzorak);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            textBox1.Clear();
            if (checkBox1.Checked==true)
            {
                checkBox1.Checked=false;
            }
            if (checkBox2.Checked == true)
            {
                checkBox2.Checked = false;
            }
            if (checkBox3.Checked == true)
            {
                checkBox3.Checked = false;
            }
            if (checkBox4.Checked == true)
            {
                checkBox4.Checked = false;
            }
            
            listBox1.ClearSelected(); 
            
        }

    }
}
