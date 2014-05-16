using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace program
{
    public partial class Form1 : Form
    {
        private delegate void Delegacja();

        public Form1()
        {
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ulong wynik = 1;
                ulong bufor = 1;
                ulong temp;
                var wyrazy = long.Parse(textBox1.Text);
                if (wyrazy < 0)
                {
                    throw new FormatException();
                }
                if (wyrazy == 0)
                {
                    wynik = 0;
                    label4.Invoke((Delegacja)delegate()
                    {
                        this.label4.Text = wynik.ToString();
                    });
                }
                if (wyrazy == 1)
                {
                    label4.Invoke((Delegacja)delegate()
                    {
                        this.label4.Text = "1";
                    });
                }
                else
                {
                    var backgroundWorker1 = sender as BackgroundWorker;
                    for (int i = 2; i < wyrazy; i++)
                    {
                        temp = wynik;
                        wynik += bufor;
                        bufor = temp;
                        var procent = ((double)(i) / wyrazy) * 100 + 1;
                        if (wyrazy < 101)
                        {
                            backgroundWorker1.ReportProgress(100);
                        }
                        else
                        {
                            backgroundWorker1.ReportProgress((int)(procent));
                        }
                    
                        label4.Invoke((Delegacja)delegate()
                        {
                            this.label4.Text = wynik.ToString();
                        });
                    }
                }
            }
            catch (FormatException)
            {
                const string message = "Proszę wprowadzić liczbę całkowita, nieujemną.";
                MessageBox.Show(message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {           
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
                backgroundWorker1.CancelAsync();
                progressBar1.Value = 0;
     
            const string message = "Obliczanie zostało anulowane.";
            MessageBox.Show(message, "STOP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }
    }
}
