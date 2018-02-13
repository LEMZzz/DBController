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

namespace DBController
{
    public partial class Form1 : Form
    {
        string returnFile;
        string openFile = "";
        private string myDoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); public string Doc() { return myDoc; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            logo f = new logo();
            f.Show();
            Wait(2);
            f.Close();
        }

        private void Exit()
        {
            DialogResult r;
            r = MessageBox.Show("Сохранить?", "Выход", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if(r == DialogResult.Yes)
            {
                string str = "";
                FolderBrowserDialog folder = new FolderBrowserDialog();
                if (folder.ShowDialog() == DialogResult.OK)
                {
                    int ind1 = openFile.LastIndexOf("\\");
                    string item = openFile.Substring(ind1);
                    str = folder.SelectedPath + item;
                    FileInfo fileInf = new FileInfo(openFile);
                    if (fileInf.Exists)
                    {
                        FileInfo fileInf2 = new FileInfo(str);
                        if (!fileInf2.Exists)
                        {
                            fileInf.MoveTo(str);
                        }
                    }
                }
            }
            //Делать
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
            if (openFile != "")
            {
                Process PrFolder = new Process();
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.CreateNoWindow = true;
                psi.WindowStyle = ProcessWindowStyle.Normal;
                psi.FileName = "explorer";
                psi.Arguments = @"/n, /select, " + openFile;
                PrFolder.StartInfo = psi;
                PrFolder.Start();
            }
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)                                //Файл - Создать
        {
            DirectoryInfo di = new DirectoryInfo(Doc() + "\\DBController");
            if(!di.Exists)
            {
                di.Create();
                returnFile = Doc() + "\\DBController";
            }
            //Создание файла
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)                                //Файл - Открыть
        {
            OpenFileDialog ofd1 = new OpenFileDialog();
            ofd1.Filter = "SQL Files (*.mdf)|*.mdf|Access (*.accdb)|*.accdb";
            ofd1.RestoreDirectory = true;
            if (ofd1.ShowDialog() == DialogResult.OK)
            {
                openFile = ofd1.FileName;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Exit();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit();
        }
    }
}
