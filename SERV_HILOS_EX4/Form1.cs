using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SERV_HILOS_EX4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string BuscaPalabra(string path, string wordToSearch) // hugo
        {
            int contador = 0;
            using (StreamReader sreader = new StreamReader(path))
            {
                string allText = sreader.ReadToEnd(); //hola a hola asd dasdafa asdfasdfasdf sadfas asdf asdfasdfa sdf asdfasdfa
                for (int i = 0; i < allText.Length - wordToSearch.Length; i++)
                {
                    if (allText.Substring(i, wordToSearch.Length) == wordToSearch)
                    {
                        contador++;
                    }
                }
            }
            
            return $"{Path.GetFileName(path)},{wordToSearch},{contador}";
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(txtUrl.Text); // Comprobar vacíos, nulls (Funcion comprobar campos de todos txb)
            FileInfo[] archivos = directoryInfo.GetFiles();
            foreach (FileInfo archivo in archivos)
            {
                if (archivo.Extension == ".txt")
                {
                    Task<string> tarea = Task.Run(() => BuscaPalabra(archivo.FullName, txbChars.Text));
                }
            }
        }
    }
}
