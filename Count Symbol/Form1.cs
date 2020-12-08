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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e) // Инициализация формы
        {    
            if (!(File.Exists("valueLimitWords.txt")) || !(File.Exists("symbolSettings.txt")))
            {              
                File.AppendAllText(@"valueLimitWords.txt", "500");
                File.AppendAllText(@"symbolSettings.txt", "RButton1-CBoxTrue");
            } 
            //
            StatusWordsLabel1.Text = "Символов в текстовом поле: 0"; 
            //
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Символ");
            listView1.Columns.Add("Количество");
            listView1.Columns.Add("%");
            //
            saveFileDialog1.Filter = "Текстовой документ (*.txt)|*.txt|All files(*.*)|*.*";
        }

        private void richTextBox1_KeyUp(object sender, KeyEventArgs e) // Отслеживание нажатия клавиши
        {
            int countWordsInTB;
            countWordsInTB = richTextBox1.Text.Length;
            int Count = countWordsInTB;
            statusStripUpdade(ref countWordsInTB);
        } 

        void statusStripUpdade(ref int Count) // Обновление Title
        {
            int LimitSymbol;
            //
            using (FileStream fstream = File.OpenRead(@"valueLimitWords.txt")) 
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                LimitSymbol = Convert.ToInt32(Encoding.Default.GetString(array));
                fstream.Close();
            }
            //
            StatusWordsLabel1.Text = "Символов в текстовом поле: " + Count;
            //
            if (Count < LimitSymbol)
            {
                StatusLabelLimited1.ForeColor = Color.Red;
                StatusLabelLimited1.Text = "Не хватает для подсчета";
                toolStripSplitButton1.Enabled = false;
            }
            else if (Count >= LimitSymbol)
            {
                StatusLabelLimited1.ForeColor = Color.Green;
                StatusLabelLimited1.Text = "Хватает для подсчета";
                toolStripSplitButton1.Enabled = true;
            }
        } 


        private void очиститьТаблицуToolStripMenuItem_Click(object sender, EventArgs e) // Событие нажатия кнопки очищения таблицы результата
        {
            listView1.Items.Clear();
            очиститьТаблицуToolStripMenuItem.Enabled = false;
            toolStripButton1.Enabled = false;
            toolStripButton2.Enabled = false;
        } 

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e) // Запуск формы настроек
        {
            FormSettings Settings = new FormSettings(); 
            Settings.Show();
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e) // Выход из приложения
        {
            Application.Exit();
        } 

        private void создательToolStripMenuItem_Click(object sender, EventArgs e) // Запуск формы информации об авторе
        {
            Developer dev = new Developer();
            dev.Show(); 
        }

        private void toolStripButton1_Click(object sender, EventArgs e) // Событие нажатия кнопки копирования данных из таблицы результата
        {
            string output = "";
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                for (int j = 0; j < listView1.Items[i].SubItems.Count; j++)
                    output += listView1.Items[i].SubItems[j].Text + "\t";
                output += "\r\n";
            }
            Clipboard.SetText(output);
        }

        private void toolStripButton2_Click(object sender, EventArgs e) // Событие нажатия кнопки вывода данных в текстовый файл из таблицы результата 
        {
            string output = "";
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                for (int j = 0; j < listView1.Items[i].SubItems.Count; j++)
                    output += listView1.Items[i].SubItems[j].Text + "\t";
                output += "\r\n";
            }
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;
            File.WriteAllText(filename, output);
            MessageBox.Show("Файл создан!", "Count Symbol", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }   

        private void toolStripSplitButton1_Click_1(object sender, EventArgs e) // Событие нажатия кнопки подсчета символов
        {
            string settingSymbol;
            string alphabet = "q";
            //
            listView1.Items.Clear();
            //
            using (FileStream fstream = File.OpenRead("symbolSettings.txt"))
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                settingSymbol = Encoding.Default.GetString(array);
                fstream.Close();
            }
            //
            switch (settingSymbol)
            {
                case "RButton1-CBoxTrue":
                    alphabet = "абвгдеёжзийклмнопрстуфхцчшщъьыэюя!?., ";
                    break;
                case "RButton1-CBoxFalse":
                    alphabet = "абвгдеёжзийклмнопрстуфхцчшщъьыэюя ";
                    break;
                //
                case "RButton2-CBoxTrue":
                    alphabet = "abcdefghijklmnopqrstuvwxyz!?., ";
                    break;
                case "RButton2-CBoxFalse":
                    alphabet = "abcdefghijklmnopqrstuvwxyz ";
                    break;
                //
                case "RButton3-CBoxTrue":
                    alphabet = "абвгдеёжзийклмнопрстуфхцчшщъьыэюяabcdefghijklmnopqrstuvwxyz!?., ";
                    break;
                case "RButton3-CBoxFalse":
                    alphabet = "абвгдеёжзийклмнопрстуфхцчшщъьыэюяabcdefghijklmnopqrstuvwxyz ";
                    break;
            }
            string allText = richTextBox1.Text;
            Dictionary<char, int> dic = new Dictionary<char, int>();
            foreach (char ch in alphabet)
                dic.Add(ch, 0);
            string lowerAllText = allText.ToLower();
            foreach (char ch in lowerAllText)
            {
                if (alphabet.Contains(ch.ToString()))
                    dic[ch]++;
            }
            int TWO_COLONN = 0;
            int TREE_COLONN = 1;
            foreach (var pair in dic)
            {
                string symbol = Convert.ToString(pair.Key);
                if (symbol == " ")
                    symbol = "Пробел";
                listView1.Items.Add(symbol);
                listView1.Items[TWO_COLONN].SubItems.Add(Convert.ToString(pair.Value)); 
                int prosent = pair.Value * 100;
                listView1.Items[TWO_COLONN].SubItems.Add((prosent / richTextBox1.Text.Length).ToString());
                TWO_COLONN++;
                TREE_COLONN++;
            }
            listView1.Items.Add(" ");
            очиститьТаблицуToolStripMenuItem.Enabled = true;
            toolStripButton1.Enabled = true;
            toolStripButton2.Enabled = true;
        } 
    }
}