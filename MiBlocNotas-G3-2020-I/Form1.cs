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
    }
}
