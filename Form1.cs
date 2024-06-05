using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        bool insert = false;
        private readonly MDIParent1 MainForm;

        public Form1(MDIParent1 form)
        {
            InitializeComponent();
            MainForm = form;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = MainForm.представление_Торговые_МаркиBindingSource;
            bindingNavigator1.BindingSource = MainForm.представление_Торговые_МаркиBindingSource;
            textBox1.DataBindings.Add("Text", MainForm.представление_Торговые_МаркиBindingSource, "Наименование", true, DataSourceUpdateMode.OnPropertyChanged);
            // Выравние колонок по содержимому
            dataGridView1.AutoResizeColumns();
            // Скрытие поля айдишника
            dataGridView1.Columns[0].Visible = false;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            try
            {
                insert = true;
                MainForm.представление_Торговые_МаркиBindingSource.AddNew();
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected = true;
            }
            catch (Exception)
            {
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (insert)
                {
                    if (textBox1.Text != "")
                    {
                        //добавление товара
                        MainForm.queriesTableAdapter2.Марки_добавить(textBox1.Text);
                        insert = false;
                    }
                }
                else
                {
                    //обновление данных
                    if (textBox1.Text != "")
                    {
                        //добавление товара
                        int id;
                        DataRowView dt;

                        dt = (DataRowView)MainForm.представление_Торговые_МаркиBindingSource.Current;
                        id = (int)dt["Код_марки"];
                        MainForm.queriesTableAdapter2.Марки_изменить(id, textBox1.Text);
                    }
                }
                MainForm.представление_Торговые_МаркиTableAdapter.Fill(MainForm.examBDDataSet.Представление_Торговые_Марки);
                dataGridView1.AutoResizeColumns();
            }
            catch (Exception)
            {

                MessageBox.Show("Ошибка сохранения данных");
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            string name = "";
            //try
            //{
                int id;
                DataRowView dt;

                dt = (DataRowView)MainForm.представление_Торговые_МаркиBindingSource.Current;
                id = (int)dt["Код_марки"];
                name = (string)dt["Наименование"];

                MainForm.queriesTableAdapter2.Марки_удалить(id);
                MainForm.представление_Торговые_МаркиTableAdapter.Fill(MainForm.examBDDataSet.Представление_Торговые_Марки);
           // }
            //catch (Exception)
            //{

               // MessageBox.Show(String.Format(" Ошибка удаления"));
            //}
        }
    }
}
