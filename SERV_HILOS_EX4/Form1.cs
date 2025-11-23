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

        List<Task<string>> listaTareas = new List<Task<string>>();
        private async void btnBusqueda_Click(object sender, EventArgs e) // Control de excepciones
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(txtUrl.Text); // Comprobar vacíos, nulls (Funcion comprobar campos de todos txb)
            FileInfo[] archivos = directoryInfo.GetFiles();
            foreach (FileInfo archivo in archivos)
            {
                if (archivo.Extension == ".txt")
                {
                   listaTareas.Add(Task.Run(() => BuscaPalabra(archivo.FullName, txbChars.Text)));
                }
            }

            while(listaTareas.Count > 0)
            {
                Task<string> tareaRealizada = await Task.WhenAny(listaTareas);
                listaTareas.Remove(tareaRealizada);
                listBox1.Items.Add(tareaRealizada.Result);
            }
        }
    }
}
