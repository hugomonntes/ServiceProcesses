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

        public string BuscaPalabra(string path, string wordToSearch)
        {
            int contador = 0;
            using (StreamReader sreader = new StreamReader(path))
            {
                string allText = sreader.ReadToEnd();
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

        public string primeraAparicionPalabra(string path, string wordToSearch)
        {
            using (StreamReader sreader = new StreamReader(path))
            {
                string allText = sreader.ReadToEnd();
                for (int i = 0; i < allText.Length - wordToSearch.Length; i++)
                {
                    if (allText.Substring(i, wordToSearch.Length) == wordToSearch)
                    {
                        return $"{Path.GetFileName(path)},{wordToSearch},{i}";
                    }
                }
            }
            return $"{Path.GetFileName(path)},{wordToSearch},{-1}";
        }
        public FileInfo[] getArchivos(string path) // Controlar excepciones
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            return directoryInfo.GetFiles();
        }

        List<Task<string>> listaTareas = new List<Task<string>>();
        private async void btnBusqueda_Click(object sender, EventArgs e) // Control de excepciones
        {
            foreach (FileInfo archivo in getArchivos(txtUrl.Text))
            {
                if (archivo.Extension == ".txt")
                {
                    listaTareas.Add(Task.Run(() => BuscaPalabra(archivo.FullName, txbChars.Text)));
                }
            }

            while (listaTareas.Count > 0)
            {
                Task<string> tareaRealizada = await Task.WhenAny(listaTareas);
                listaTareas.Remove(tareaRealizada);
                listBox1.Items.Add(tareaRealizada.Result);
            }
        }

        List<Task<string>> tareasPosicion = new List<Task<string>>();
        private async void btnPosicion_Click(object sender, EventArgs e)
        {
            foreach (FileInfo archivo in getArchivos(txtUrl.Text))
            {
                if (archivo.Extension == ".txt")
                {
                    tareasPosicion.Add(Task.Run(() => primeraAparicionPalabra(archivo.FullName, textBox1.Text)));
                }
            }

            while (tareasPosicion.Count > 0)
            {
                string[] tareasRealizadas = await Task.WhenAll(tareasPosicion);
                foreach (string tarea in tareasRealizadas)
                {
                    listBox1.Items.Add(tarea);
                }
                tareasPosicion.Clear();
            }
        }
    }
}
