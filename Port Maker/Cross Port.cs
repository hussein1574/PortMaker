//using Ionic.Zip;
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

namespace Port_Maker
{
    public partial class Cross_Port : Form
    {
        public Cross_Port()
        {
            InitializeComponent();
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

        private void button1_Click(object sender, EventArgs e)
        {
            #region Rom's Pathes
            var Stock_Path = textBox1.Text;
            var Port_Path = textBox2.Text;
            #endregion

            // Rom Porting
            try
            {
                #region Deleting Stock Folders

                Directory.Delete(Stock_Path + "\\system\\app", true);
                Directory.Delete(Stock_Path + "\\system\\fonts", true);
                Directory.Delete(Stock_Path + "\\system\\framework", true);
                Directory.Delete(Stock_Path + "\\system\\media", true);
                Directory.Delete(Stock_Path + "\\system\\priv-app", true);
                File.Delete(Stock_Path + "\\system\\build.prop");

                #endregion

                #region Moving Port Folders

                Directory.Move(Port_Path + "\\system\\app", Stock_Path + "\\system\\app");
                Directory.Move(Port_Path + "\\system\\fonts", Stock_Path + "\\system\\fonts");
                Directory.Move(Port_Path + "\\system\\framework", Stock_Path + "\\system\\framework");
                Directory.Move(Port_Path + "\\system\\media", Stock_Path + "\\system\\media");
                Directory.Move(Port_Path + "\\system\\priv-app", Stock_Path + "\\system\\priv-app");
                Directory.Move(Port_Path + "\\system\\build.prop", Stock_Path + "\\system\\build.prop");

                #endregion

                #region Moving Bin Folder
                foreach (var file in Directory.GetFiles(Port_Path + "\\system\\bin"))
                {
                    // بيفصل كل كلمة من المكان لوحدها علشان اجيب اسم الفايل
                    var spliter = file.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                    // length starts from 0 so we need to remove 1 to get the last entry
                    var fileName = spliter[spliter.Length - 1];
                    //* loop to copy all non exist files *\\
                    if (!File.Exists(Stock_Path + "\\system\\bin\\" + fileName))
                    {
                        var bytes = File.ReadAllBytes(file);
                        File.WriteAllBytes(Stock_Path + "\\system\\bin\\" + fileName, bytes);
                    }
                }
                #endregion

                #region Moving lib Folder
                foreach (var file2 in Directory.GetFiles(Port_Path + "\\system\\lib"))
                {
                    var spliter2 = file2.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                    var fileName2 = spliter2[spliter2.Length - 1];
                    //* loop to copy all non exist files *\\
                    if (!File.Exists(Stock_Path + "\\system\\lib\\" + fileName2))
                    {
                        var bytes = File.ReadAllBytes(file2);
                        File.WriteAllBytes(Stock_Path + "\\system\\lib\\" + fileName2, bytes);
                    }

                }
                #endregion
                
                #region Moving ETC Folder

                foreach (var file3 in Directory.GetFiles(Port_Path + "\\system\\etc"))
                {
                    var spliter3 = file3.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                    var fileName3 = spliter3[spliter3.Length - 1];
                    //* loop to copy all non exist files *\\
                    if (!File.Exists(Stock_Path + "\\system\\etc\\" + fileName3))
                    {
                        var bytes = File.ReadAllBytes(file3);
                        File.WriteAllBytes(Stock_Path + "\\system\\etc\\" + fileName3, bytes);
                    }
                }
                #endregion

                #region Moving Permissions Folder
                foreach (var file4 in Directory.GetFiles(Port_Path + "\\system\\etc\\permissions"))
                {
                    var spliter4 = file4.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                    var fileName4 = spliter4[spliter4.Length - 1];
                    //* loop to copy all non exist files *\\
                    if (!File.Exists(Stock_Path + "\\system\\etc\\permissions\\" + fileName4))
                    {
                        var bytes = File.ReadAllBytes(file4);
                        File.WriteAllBytes(Stock_Path + "\\system\\etc\\permissions\\" + fileName4, bytes);
                    }
                }
                #endregion

                #region Replacing Some lib Files

                if(File.Exists(Port_Path + "system\\lib\\libandroid.so") && File.Exists(Stock_Path + "system\\lib\\libandroid.so"))
                {
                    var bytes1 = File.ReadAllBytes(Port_Path + "system\\lib\\libandroid.so");
                    File.WriteAllBytes(Stock_Path + "system\\lib\\libandroid.so", bytes1);
                }

                if (File.Exists(Port_Path + "system\\lib\\libandroid_runtime.so") && File.Exists(Stock_Path + "system\\lib\\libandroid_runtime.so" ))
                {
                    var bytes2 = File.ReadAllBytes(Port_Path + "system\\lib\\libandroid_runtime.so");
                    File.WriteAllBytes(Stock_Path + "system\\lib\\libandroid_runtime.so", bytes2);
                }

                if (File.Exists(Port_Path + "system\\lib\\libandroid_servers.so") && File.Exists(Stock_Path + "system\\lib\\libandroid_servers.so"))
                {
                    var bytes3 = File.ReadAllBytes(Port_Path + "system\\lib\\libandroid_servers.so");
                    File.WriteAllBytes(Stock_Path + "system\\lib\\libandroid_servers.so", bytes3);
                }

                if (File.Exists(Port_Path + "system\\lib\\libandroidfw.so") && File.Exists(Stock_Path + "system\\lib\\libandroidfw.so"))
                {
                    var bytes4 = File.ReadAllBytes(Port_Path + "system\\lib\\libandroidfw.so");
                    File.WriteAllBytes(Stock_Path + "system\\lib\\libandroidfw.so", bytes4);
                }

                if (File.Exists(Port_Path + "system\\lib\\libmedia_jni.so") && File.Exists(Stock_Path + "system\\lib\\libmedia_jni.so"))
                {
                    var bytes5 = File.ReadAllBytes(Port_Path + "system\\lib\\libmedia_jni.so");
                    File.WriteAllBytes(Stock_Path + "system\\lib\\libmedia_jni.so", bytes5);
                }

                if (File.Exists(Port_Path + "system\\lib\\libwebviewchromium.so") && File.Exists(Stock_Path + "system\\lib\\libwebviewchromium.so"))
                {
                    var bytes6 = File.ReadAllBytes(Port_Path + "system\\lib\\libwebviewchromium.so");
                    File.WriteAllBytes(Stock_Path + "system\\lib\\libwebviewchromium.so", bytes6);
                }

                if (File.Exists(Port_Path + "system\\lib\\libwebviewchromium_loader.so") && File.Exists(Stock_Path + "system\\lib\\libwebviewchromium_loader.so"))
                {
                    var bytes7 = File.ReadAllBytes(Port_Path + "system\\lib\\libwebviewchromium_loader.so");
                    File.WriteAllBytes(Stock_Path + "system\\lib\\libwebviewchromium_loader.so", bytes7);
                }

                if (File.Exists(Port_Path + "system\\lib\\libwebviewchromium_plat_support.so") && File.Exists(Stock_Path + "system\\lib\\libwebviewchromium_plat_support.so"))
                {
                    var bytes8 = File.ReadAllBytes(Port_Path + "system\\lib\\libwebviewchromium_plat_support.so");
                    File.WriteAllBytes(Stock_Path + "system\\lib\\libwebviewchromium_plat_support.so", bytes8);
                }
                
                #endregion

                #region edit build.prop

                var text = File.ReadAllText(Stock_Path + "\\system\\build.prop");

                if (text.Contains("mt6582"))
                {
                    text = text.Replace("mt6582", "mt6580");
                }

                if (text.Contains("mt6592"))
                {
                    text = text.Replace("mt6592", "mt6580");
                }

                if (text.Contains("mt6572"))
                {
                    text = text.Replace("mt6572", "mt6580");
                }

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
                    File.AppendAllText(Stock_Path + "\\system\\build.prop",
                       "qemu.hw.mainkeys=0" + Environment.NewLine);
                }

                if (text.Contains("ro.sf.lcd_density=240"))
                {
                    text = text.Replace("ro.sf.lcd_density=240", "ro.sf.lcd_density=320");
                }


                File.WriteAllText(Stock_Path + "\\system\\build.prop", text);

                #endregion
                

                MessageBox.Show("Your Port Has Been Succeed :D                                                                    All Copy Rights Reserved To Hussein Medhat", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception Exc)
            {
                MessageBox.Show(Exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
    }
}
