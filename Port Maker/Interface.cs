using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Port_Maker
{
    public partial class Interface : Form
    {
        public Interface()
        {
            InitializeComponent();
            label2.Text = File.ReadAllText("Version.txt");
        }
        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Coming Soon :D");
            // Opening Normal Port Menu And Close This
            Form2 y = new Form2();
            y.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Opening Cross Port Menu And Close This
            Cross_Port y = new Cross_Port();
            y.Show();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Made By Hussein Medhat From 'Pharohs Team' :D", "About Programer", MessageBoxButtons.OK, MessageBoxIcon.Information,MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        }

        private void button4_Click(object sender, EventArgs e)
        {
           // MessageBox.Show("Servers Are Down");  
             WebClient webClient = new WebClient();
            var new_VERSION =  webClient.DownloadString("http://127.0.0.1/Version.txt");
            var ver = File.ReadAllText("Version.txt");
            var Version = Convert.ToInt32(ver);
            var Update = Convert.ToInt32(new_VERSION);
              if(Version < Update)
            {
                
				MessageBox.Show("Wait While Updating");
				webClient.DownloadFile("http://127.0.0.1/Update.zip", @"Update.zip");
				using (ZipFile zip = ZipFile.Read ("Update.zip")) {
					foreach (ZipEntry zipFiles in zip) {
						string Root = AppDomain.CurrentDomain.BaseDirectory;
						zipFiles.Extract (@Root + "/");
					}
				}

                //download new version file
                webClient.DownloadFile("http://127.0.0.1/Version.txt", "Version.txt");

                //Delete Zip File
                File.Delete("Update.zip");

                MessageBox.Show("Update is Done :D, Please Restart The Program");
            }
            else
            {
                MessageBox.Show("Version is Up to Date");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void Interface_Load(object sender, EventArgs e)
        {

        }
    }
}
