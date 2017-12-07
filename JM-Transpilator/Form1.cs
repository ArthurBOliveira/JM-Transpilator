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

namespace JM_Transpilator
{
    public partial class TranspilatorForm : Form
    {
        public TranspilatorForm()
        {
            InitializeComponent();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in dialog.FileNames)
                {
                    string result = "";

                    StreamReader sr = new StreamReader(file);
                    result = sr.ReadToEnd();
                    sr.Close();

                    result = Transpilator.Transpilate(result);                    


                    //RefreshModels();
                }
            }
        }
    }
}
