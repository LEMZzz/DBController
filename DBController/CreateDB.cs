using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.IO;

namespace DBController
{
    public partial class CreateDB : Form
    {
        Form1 _mainform;
        public CreateDB(Form1 mainform)
        {
            _mainform = mainform;
            InitializeComponent();
        }

        public static void CreateSqlDatabase(string filename)
        {
            string databaseName = System.IO.Path.GetFileNameWithoutExtension(filename);
            using (var connection = new System.Data.SqlClient.SqlConnection(
                "Data Source=.\\sqlexpress;Initial Catalog=tempdb; Integrated Security=true;User Instance=True;"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                        String.Format("CREATE DATABASE {0} ON PRIMARY (NAME={0}, FILENAME='{1}')", databaseName, filename);
                    command.ExecuteNonQuery();

                    command.CommandText =
                        String.Format("EXEC sp_detach_db '{0}', 'true'", databaseName);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Join(" ", textBox1.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                try
                {
                    string nameFile = textBox1.Text;
                    nameFile = Regex.Replace(nameFile, "[^0-9a-zA-Zа-яА-Я ]", "");
                    nameFile = string.Join(" ", nameFile.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                    if (radioButton1.Checked)
                    {
                        var filename = System.IO.Path.Combine(_mainform.myDoc + "\\DBControllerDataBases", nameFile + ".mdf");
                        if (!System.IO.File.Exists(_mainform.myDoc + "\\DBControllerDataBases\\" + nameFile + ".mdf"))
                        {
                            CreateSqlDatabase(filename);
                            _mainform.file = _mainform.myDoc + "\\DBControllerDataBases\\" + nameFile + ".mdf";
                            _mainform.Text = _mainform.file;
                            this.Close();
                        }
                        else
                        {
                            label3.Text = "Файл с таким именем уже существует!";
                            label3.Visible = true;
                            panel1.BackColor = Color.Red;
                        }
                    }
                    else
                    {/*
                        ADOX.CatalogClass MDB = new ADOX.CatalogClass();
                        String StrConnMDB = String.Format(""
                            + "Provider=Microsoft.Jet.OLEDB.4.0; "
                            + "Data Source={0}; "
                            + "Jet OLEDB:Engine Type=5"
                            + "", nameFile);
                        MDB.Create(StrConnMDB);
                        MessageBox.Show("Complete");*/

                        /*if (!System.IO.File.Exists(filename))
                        {
                            CreateSqlDatabase(filename);
                            _mainform.file = nameFile + ".mdf";
                            this.Close();
                        }
                        else
                        {
                            label1.Text = "Файл с таким именем уже существует!";
                            label1.Visible = true;
                            groupBox1.BackColor = Color.Red;
                        }*/
                    }
                }
                catch
                {
                    MessageBox.Show("ERROR!", "Попробуйте перезагрузить свой ПК");
                }
            }
            else
            {
                label3.Text = "*Введите имя!";
                label3.Visible = true;
                panel1.BackColor = Color.Red;
            }
        }
    }
}
