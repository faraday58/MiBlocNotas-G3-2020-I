using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MiBlocNotas_G3_2020_I
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void GuardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileStream fs = null;
            StreamWriter sw = null;

            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Texto (*.txt)|*.txt ";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fs = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write);
                    sw = new StreamWriter(fs);
                    sw.WriteLine(txtbAutor.Text + "," + txtbNombre.Text + "," + txtbFecha.Text);
                    for (int i = 0; i < richtxtbBloc.Lines.Length; i++)
                    {
                        sw.WriteLine(richtxtbBloc.Lines[i]);
                    }
                }else
                {
                    fs = new FileStream("error_log.txt", FileMode.Append, FileAccess.Write);
                    sw = new StreamWriter(fs);
                    sw.WriteLine("Intento de crear un archivo "+File.GetLastAccessTime("error_log.txt"));
                }

            }
            catch (IOException error)
            {
                MessageBox.Show(error.Message, "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            finally
            {
                sw.Close();
                fs.Close();
            }
        }

        private void AbrirToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FileStream fs = null,fs1 = null;
            StreamReader sr = null;
            StreamWriter sw = null; 
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Texto (*.txt)|*.txt|Binario(*.bin)|*.bin";
                
                if( openFileDialog.ShowDialog() == DialogResult.OK )
                {


                    if(openFileDialog.FilterIndex == 1    )
                    {
                        0



                    }else
                    {
                        fs = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                        sr = new StreamReader(fs);

                        string cadena = sr.ReadLine();
                        string[] cabeceras = cadena.Split(',');
                        //txtbAutor.Text = cabeceras[0];
                        //  txtbNombre.Text = cabeceras[1];
                        //txtbFecha.Text = cabeceras[2];
                        txtbAutor.Text = File.GetAccessControl(openFileDialog.FileName).GetOwner(typeof(System.Security.Principal.NTAccount)).ToString();

                        DirectoryInfo directoryInfo = new DirectoryInfo(openFileDialog.FileName);
                        txtbNombre.Text = directoryInfo.Name;

                        txtbFecha.Text = File.GetCreationTime(openFileDialog.FileName).Date.ToString(); ;
                        string cont = "";

                        while (cadena != null)
                        {
                            cadena = sr.ReadLine();
                            cont = cont + cadena + "\n";
                        }
                        richtxtbBloc.Text = cont;
                    }
                }
                else
                {
                    
                    fs1 = new FileStream("error_log_open.txt", FileMode.Append, FileAccess.Write);
                    sw = new StreamWriter(fs1);
                    sw.WriteLine("Se intentó abrir un archivo " + File.GetLastAccessTime("error_log_open.txt"));
                    sw.Close();
                    fs1.Close();
                    fs = new FileStream("error_log_open.txt", FileMode.Open, FileAccess.Read);
                    sr = new StreamReader(fs);
                }



            }
            catch(IOException error)
            {

                MessageBox.Show(error.Message, "Error al Abrir", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                sr.Close();
                fs.Close();
            }




        }

        private void GuardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileStream fs = null;
            BinaryWriter bw = null;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Texto(*.txt)|*.txt|Binario(*.bin)|*.bin";
            try
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fecha = txtbFecha.Text;
                    fs = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write);
                    bw = new BinaryWriter(fs);
                    double dfecha = double.Parse(fecha);
                    string autor = txtbAutor.Text;

                    bw.Write(dfecha);
                    bw.Write(autor);
                }
            }            
            
            catch(IOException error)
            {
                MessageBox.Show(error.Message, "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                bw.Close();
                fs.Close();

            }

            

        }
    }
}
