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
        #region Variables
        int countWordsInTB = 0; // Объявляем переменную для хранения слов в текстовом поле
        string pathApp;
        string alphabet;
        string settingSymbol;
        int settingLimitSymbol;
        int Count;
        string path = AppDomain.CurrentDomain.BaseDirectory; // Получаем путь откуда открыт файл
        #endregion
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {    
            if (!(File.Exists("path.txt")) || !(File.Exists("valueLimitWords.txt")) || !(File.Exists("symbolSettings.txt")))
            {              
                // запись в файл
                using (FileStream fstream = new FileStream(path + @"\path.txt", FileMode.OpenOrCreate))
                {
                    // преобразуем строку в байты
                    byte[] array = System.Text.Encoding.Default.GetBytes(path);
                    // запись массива байтов в файл
                    fstream.Write(array, 0, array.Length);
                    Console.WriteLine("Текст записан в файл");
                }
                File.AppendAllText(path + @"valueLimitWords.txt", "500");
                File.AppendAllText(path + @"symbolSettings.txt", "RButton1-CBoxTrue");
            }
            else
            {
                using (FileStream fstream = File.OpenRead("path.txt")) //Чтение из файла 
                {
                    byte[] array = new byte[fstream.Length];
                    fstream.Read(array, 0, array.Length);
                    pathApp = Encoding.Default.GetString(array);
                    fstream.Close();
                }
            }          
            StatusWordsLabel1.Text = "Символов в текстовом поле: " + countWordsInTB; // Пишем начальный лайбл с текстом "Символов в текстовом поле: 0"
            listView1.GridLines = true;
            listView1.FullRowSelect = true; // Возвращает или задает значение, указывающее, выбираются ли при щелчке элемента все его подэлементы
            listView1.Columns.Add("Символ", 40); // Создаем колонну "Name"
            listView1.Columns.Add("Количество", 85); // Создаем колонну "Date update"
            listView1.Columns.Add("%", 50); // Создаем колонну "Date update"
            saveFileDialog1.Filter = "Текстовой документ (*.txt)|*.txt|All files(*.*)|*.*";
        }
        void statusStripUpdade(ref int Count)
        {
            //Чтение из файла лимита символов
            using (FileStream fstream = File.OpenRead(path + @"valueLimitWords.txt")) //Чтение из файла 
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                settingLimitSymbol = Convert.ToInt32(Encoding.Default.GetString(array));
                fstream.Close();
            }

            StatusWordsLabel1.Text = "Символов в текстовом поле: " + Count;
            if (Count < settingLimitSymbol)
            {
                StatusLabelLimited1.ForeColor = Color.Red;
                StatusLabelLimited1.Text = "Не хватает для подсчета";
            }
            else if (Count >= settingLimitSymbol)
            {
                StatusLabelLimited1.ForeColor = Color.Green;
                StatusLabelLimited1.Text = "Хватает для подсчета";
                toolStripSplitButton1.Enabled = true;
            }
        } // Обновление Title
        private void richTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            countWordsInTB = richTextBox1.Text.Length; // Увеличиваем нашу переменную на количество символов в самом текстовом поле
            Count = countWordsInTB; // Объявляем новую переменную для передачи параметра в метод 
            statusStripUpdade(ref countWordsInTB); // Вызываем метод 'statusStripUpdade' с передачей в него параметра
        } // Отслеживание нажатия клавиши
        private void очиститьТаблицуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            очиститьТаблицуToolStripMenuItem.Enabled = false;
            toolStripButton1.Enabled = false;
            toolStripButton2.Enabled = false;
        } // Кнопка очищение таблици результата
        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettings Settings = new FormSettings(); // Инициализируем
            Settings.Show(); // Показываем
        } // Открытие формы настроек
        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        } // Выход из приложение
        private void создательToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Developer dev = new Developer(); // Инициализируем
            dev.Show(); // Показываем
        } // Информация об авторе
        private void toolStripButton1_Click(object sender, EventArgs e) // Кнопка копирования данных из таблицы результата
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
        private void toolStripButton2_Click(object sender, EventArgs e)
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
            // получаем выбранный файл
            string filename = saveFileDialog1.FileName;
            // сохраняем текст в файл
            System.IO.File.WriteAllText(filename, output);
            MessageBox.Show("Файл сохранен", "Count Symbol", MessageBoxButtons.OK, MessageBoxIcon.Information);
        } // Кнопка вывода данных в текстовойфайл из таблицы результата    
        private void toolStripSplitButton1_Click_1(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            using (FileStream fstream = File.OpenRead("symbolSettings.txt")) //Чтение из файла 
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                settingSymbol = Encoding.Default.GetString(array);
                fstream.Close();
            }
            switch (settingSymbol)
            {
                case "RButton1-CBoxTrue":
                    alphabet = "абвгдеёжзийклмнопрстуфхцчшщъьыэюя!?., ";
                    break;
                case "RButton1-CBoxFalse":
                    alphabet = "абвгдеёжзийклмнопрстуфхцчшщъьыэюя ";
                    break;

                case "RButton2-CBoxTrue":
                    alphabet = "abcdefghijklmnopqrstuvwxyz!?., ";
                    break;
                case "RButton2-CBoxFalse":
                    alphabet = "abcdefghijklmnopqrstuvwxyz ";
                    break;

                case "RButton3-CBoxTrue":
                    alphabet = "абвгдеёжзийклмнопрстуфхцчшщъьыэюяabcdefghijklmnopqrstuvwxyz!?., ";
                    break;
                case "RButton3-CBoxFalse":
                    alphabet = "абвгдеёжзийклмнопрстуфхцчшщъьыэюяabcdefghijklmnopqrstuvwxyz ";
                    break;
            }
            string str = richTextBox1.Text;
            Dictionary<char, int> dic = new Dictionary<char, int>();
            foreach (char ch in alphabet)
                dic.Add(ch, 0);
            string lowerStr = str.ToLower();
            foreach (char ch in lowerStr)
            {
                if (alphabet.Contains(ch.ToString()))
                    dic[ch]++;
            }
            int twoColonn = 0;
            int treeColonn = 1;
            foreach (var pair in dic)
            {
                string symbol = Convert.ToString(pair.Key);
                if (symbol == " ")
                    symbol = "Пробел";
                listView1.Items.Add(symbol); // Добавляем в ListView имя аккаунта
                listView1.Items[twoColonn].SubItems.Add(Convert.ToString(pair.Value)); // Добавляем в ListView дату обновления 
                int prosent = pair.Value * 100;
                listView1.Items[twoColonn].SubItems.Add((prosent / Count).ToString() + " %"); // Добавляем в ListView дату обновления  
                twoColonn++; // Увиличиваем переменную на еденицу
                treeColonn++;
            }
            listView1.Items.Add(" ");
            очиститьТаблицуToolStripMenuItem.Enabled = true;
            toolStripButton1.Enabled = true;
            toolStripButton2.Enabled = true;
        } // Кнопка подсчета символов
    }
}