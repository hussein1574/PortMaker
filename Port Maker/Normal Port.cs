using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Port_Maker
{
    public partial class Form2 : Form
    {
        
        public Form2()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            #region comment about frimware
            //foreach (var file in Directory.GetFiles(Port_Path + "system\\etc\\firmware"))
            //{
            //    File.Delete(file);
            //}
            //foreach (var file in Directory.GetFiles(Port_Path + "system\\etc\\firmware\\mt6580"))
            //{
            //    File.Delete(file);
            //}
            //foreach (var file in Directory.GetFiles(Stock_Path + "system\\etc\\firmware"))
            //{
            //    var bytes = File.ReadAllBytes(file);
            //    var spliter = file.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            //    var fileName = spliter[spliter.Length - 1];
            //    File.WriteAllBytes(Port_Path + "system\\etc\\firmware\\" + fileName, bytes);
            //}
            //foreach (var file in Directory.GetFiles(Stock_Path + "system\\etc\\firmware\\mt6580"))
            //{
            //    var bytes = File.ReadAllBytes(file);
            //    var spliter = file.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            //    var fileName = spliter[spliter.Length - 1];
            //    File.WriteAllBytes(Port_Path + "system\\etc\\firmware\\mt6580\\" + fileName, bytes);
            //}
            #endregion

            #region Rom's Pathes
            var Stock_Path = textBox1.Text;
            var Port_Path = textBox2.Text;
            #endregion

            // Rom Porting

            try {

                #region Cam Files
               if (File.Exists(Stock_Path + "system\\lib\\libcamalgo.so"))
                    {
                    var bytes1 = File.ReadAllBytes(Stock_Path + "system\\lib\\libcamalgo.so");
                    File.WriteAllBytes(Port_Path + "system\\lib\\libcamalgo.so", bytes1);
                }
               if (File.Exists(Stock_Path + "system\\lib\\libcameracustom.so"))
                {
                    var bytes2 = File.ReadAllBytes(Stock_Path + "system\\lib\\libcameracustom.so");
                    File.WriteAllBytes(Port_Path + "system\\lib\\libcameracustom.so", bytes2);
                }
                
                #endregion

                #region firmware Replace
                Directory.Delete(Port_Path + "\\system\\etc\\firmware", true);
                Directory.Move(Stock_Path + "\\system\\etc\\firmware", Port_Path + "\\system\\etc\\firmware");               
                #endregion

                #region edit build.prop

                var text = File.ReadAllText(Port_Path + "\\system\\build.prop");

                if (text.Contains("qemu.hw.mainkeys=1"))
                {
                    text = text.Replace("qemu.hw.mainkeys=1", "qemu.hw.mainkeys=0");
                }

                else if (text.Contains("qemu.hw.mainkeys=0"))
                {

                }

                else if (text.Contains("qemu.hw.mainkeys=2"))
                {
                    text = text.Replace("qemu.hw.mainkeys=2", "qemu.hw.mainkeys=0");
                }

                else
                {
                    File.AppendAllText(Port_Path + "\\system\\build.prop",
                       "qemu.hw.mainkeys=0" + Environment.NewLine);
                }

                if (text.Contains("ro.sf.lcd_density=240"))
                {
                    text = text.Replace("ro.sf.lcd_density=240", "ro.sf.lcd_density=320");
                }


                File.WriteAllText(Port_Path + "\\system\\build.prop", text);

                #endregion

                #region Unpacking Boot.img

                var StockBoot = File.ReadAllBytes(Stock_Path + "boot.img");
                File.WriteAllBytes("Stock//boot.img", StockBoot);
                Process.Start("Stock//unpack.bat");
                var PortBoot = File.ReadAllBytes(Port_Path + "boot.img");               
                File.WriteAllBytes("Port//boot.img", PortBoot);               
                Process.Start("Port//unpack.bat");

                #endregion

                #region edit Boot.img

                File.Delete("Port//kernal");
                var Data = File.ReadAllBytes("Stock//kernal");
                File.WriteAllBytes("Port//kernal", Data);

                #endregion

                #region Repack Boot.img

                Process.Start("Stock//repack.bat");
                Process.Start("Port//repack.bat");
                var Data2 = File.ReadAllBytes("Port//boot-new.img");
                File.Delete(Port_Path + "boot.img");
                File.WriteAllBytes(Port_Path + "boot.img", Data2);

                #endregion

                #region Restore
                File.Delete("Stock//boot-old.img");
                File.Delete("Port//boot-old.img");
                File.Delete("Stock//boot-new.img");
                File.Delete("Port//boot-new.img");
                #endregion

                MessageBox.Show("Your Port Has Been Succeed :D ,                All Copy Rights Reserved To Hussein Medhat", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception Exc)
            {               
                MessageBox.Show(Exc.Message, "Error", MessageBoxButtons.OK , MessageBoxIcon.Error);
                
            }

           

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            var dia = new OpenFileDialog();
            dia.ShowDialog();
            var Stock_boot = dia.FileName;
            var spliter = Stock_boot.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            var Stock = string.Empty;
            for (var i = 0; i < spliter.Length - 1; i++)
                Stock += spliter[i] + "\\";
            textBox1.Text = Stock;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var dia = new OpenFileDialog();
            dia.ShowDialog();
            var Port_boot = dia.FileName;
            var spliter = Port_boot.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            var Port = string.Empty;
            for (var i = 0; i < spliter.Length - 1; i++)
                Port += spliter[i] + "\\";
            textBox2.Text = Port;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
