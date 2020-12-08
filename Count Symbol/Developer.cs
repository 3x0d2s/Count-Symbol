using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Count_Symbol
{
    public partial class Developer : Form
    {
        public Developer()
        {
            InitializeComponent();
        }

        private void Developer_Load(object sender, EventArgs e)
        {
            label1.Text = "Программа разработана начинающим программистом \nпо имени, указанному ниже.\nКроме того, программа не имеет авторских прав \nи создана исключительно для образовательных целей. \nЕсли вы найдете ошибки в программе - сообщите об этом."; // Заполняем Label
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            Process.Start("https://vk.com/deadbackyarwood"); // Заходим на страницу ВК
        }
    }
}
