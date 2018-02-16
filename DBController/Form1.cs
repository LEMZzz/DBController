using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Data.Sql;
using System.Data.OleDb;

namespace DBController
{
    public partial class Form1 : Form
    {
        public string folder, file, myDoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {/*
            logo f = new logo();
            f.Show();
            Wait(2);
            f.Close();*/
        }

        public void Wait(int seconds)
        {
            int ticks = System.Environment.TickCount + (int)Math.Round(seconds * 1000.0);
            while (System.Environment.TickCount < ticks)
            {
                Application.DoEvents();
            }
        }

        private void открытьВПроводникеToolStripMenuItem_Click(object sender, EventArgs e)                     //Открыть в проводнике
        {
            if (file != "")
            {
                Process PrFolder = new Process();
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.CreateNoWindow = true;
                psi.WindowStyle = ProcessWindowStyle.Normal;
                psi.FileName = "explorer";
                psi.Arguments = @"/n, /select, " + file;
                PrFolder.StartInfo = psi;
                PrFolder.Start();
            }
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)                                //Файл - Создать
        {
            DirectoryInfo di = new DirectoryInfo(myDoc + "\\DBControllerDataBases");
            if(!di.Exists)
            {
                di.Create();
                folder = myDoc + "\\DBControllerDataBases";
            }
            CreateDB cr = new CreateDB(this);
            cr.Show();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)                                //Файл - Открыть
        {
            OpenFileDialog ofd1 = new OpenFileDialog();
            ofd1.Filter = "SQL Files (*.mdf)|*.mdf|Access (*.accdb)|*.accdb";
            ofd1.RestoreDirectory = true;
            if (ofd1.ShowDialog() == DialogResult.OK)
            {
                file = ofd1.FileName;
                this.Text = file;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)                                  //Закрытие
        {
            DialogResult r = MessageBox.Show("Сохранить?", "Выход", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.Yes)
            {
                FolderBrowserDialog folder = new FolderBrowserDialog();
                if (folder.ShowDialog() == DialogResult.OK)
                {
                    string str = folder.SelectedPath;
                    int i = file.LastIndexOf("\\");
                    str = str + file.Substring(i);
                    FileInfo fileInf = new FileInfo(file);
                    if (fileInf.Exists)
                    {
                        FileInfo fileInf2 = new FileInfo(str);
                        if (!fileInf2.Exists)
                        {
                            fileInf.MoveTo(str);
                            MessageBox.Show("Complete");
                        }
                        else e.Cancel = true;
                    }
                    else e.Cancel = true;
                }
            }
            else if(r == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }
    }
}
