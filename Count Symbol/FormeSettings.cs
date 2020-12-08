using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Count_Symbol
{
    public partial class FormSettings : Form
    {
        string pathApp;
        int valueLimitWords;
        string symbolSettings;

        public FormSettings()
        {
            InitializeComponent();
            using (FileStream fstream = File.OpenRead("valueLimitWords.txt")) //Чтение из файла 
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                valueLimitWords = Convert.ToInt32(Encoding.Default.GetString(array));
                label2.Text = valueLimitWords.ToString();
                trackBar1.Value = valueLimitWords;
                fstream.Close();
            }
            using (FileStream fstream = File.OpenRead("path.txt")) //Чтение из файла 
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                pathApp = Encoding.Default.GetString(array);
                fstream.Close();
            }
            using (FileStream fstream = File.OpenRead("symbolSettings.txt")) //Чтение из файла 
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                symbolSettings = Encoding.Default.GetString(array);
                fstream.Close();
            }
            switch (symbolSettings)
            {
                case "RButton1-CBoxTrue":
                    radioButton1.Checked = true;
                    checkBox1.Checked = true;
                    break;
                case "RButton1-CBoxFalse":
                    radioButton1.Checked = true;
                    checkBox1.Checked = false;
                    break;

                case "RButton2-CBoxTrue":
                    radioButton2.Checked = true;
                    checkBox1.Checked = true;
                    break;
                case "RButton2-CBoxFalse":
                    radioButton2.Checked = true;
                    checkBox1.Checked = false;
                    break;

                case "RButton3-CBoxTrue":
                    radioButton3.Checked = true;
                    checkBox1.Checked = true;
                    break;
                case "RButton3-CBoxFalse":
                    radioButton3.Checked = true;
                    checkBox1.Checked = false;
                    break;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if(checkBox1.Checked)
                {
                    File.Delete(pathApp + @"symbolSettings.txt");
                    File.AppendAllText(pathApp + @"symbolSettings.txt", "RButton1-CBoxTrue");
                }
                else
                {
                    File.Delete(pathApp + @"symbolSettings.txt");
                    File.AppendAllText(pathApp + @"symbolSettings.txt", "RButton1-CBoxFalse");
                }
            }
            else if (radioButton2.Checked)
            {
                if (checkBox1.Checked)
                {
                    File.Delete(pathApp + @"symbolSettings.txt");
                    File.AppendAllText(pathApp + @"\symbolSettings.txt", "RButton2-CBoxTrue");
                }
                else
                {
                    File.Delete(pathApp + @"symbolSettings.txt");
                    File.AppendAllText(pathApp + @"symbolSettings.txt", "RButton2-CBoxFalse");
                }
            }
            else if (radioButton3.Checked)
            {
                if (checkBox1.Checked)
                {
                    File.Delete(pathApp + @"symbolSettings.txt");
                    File.AppendAllText(pathApp + @"symbolSettings.txt", "RButton3-CBoxTrue");
                }
                else
                {
                    File.Delete(pathApp + @"symbolSettings.txt");
                    File.AppendAllText(pathApp + @"symbolSettings.txt", "RButton3-CBoxFalse"); 
                }
            }
            label4.Visible = true;
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = Convert.ToString(trackBar1.Value);
            button2.Enabled = true;
            symbolSettings = Convert.ToString(trackBar1.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            File.Delete(pathApp + @"valueLimitWords.txt");
            File.AppendAllText(pathApp + @"valueLimitWords.txt", symbolSettings);
            button2.Enabled = false;
            label3.Visible = true;
        }

    }
}
