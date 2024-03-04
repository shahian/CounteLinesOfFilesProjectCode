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

namespace CountFiles
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int counter = 0;
        int filecounter = 1;
        int TotallLines = 0;
        //int TotallAreaLines = 0;
        int fileCount = 0;
        int javaCount = 0;
        int javaLineCount = 0;
        int csCount = 0;
        int csLineCount = 0;
        int jsCount = 0;
        int jsLineCount = 0;
        int swiftLineCount = 0;
        int swiftCount = 0; 
        int cssCount = 0;
        int cssLineCount = 0;
        int cshtmlCount = 0;
        int cshtmlLineCount = 0;

        string line = string.Empty;
        string fileName = string.Empty;
        string result = string.Empty;
        //string resultArea = string.Empty;
        string csfiles = string.Empty;
        string cssfiles = string.Empty;
        string jsfiles = string.Empty;
        string swiftfiles = string.Empty;
        string javafiles = string.Empty;
        string cshtmlfiles = string.Empty;
        List<String> TempFiles = new List<string>();
        //List<String> TempAreas = new List<string>();

        private void button2_Click_1(object sender, EventArgs e)
        {
            button1_Click_1(sender, e);
            button2_Click(sender, e);
            FillOperation();
            FillTextBoxResult();
            ShowFinallMessage();
            ClearVariables();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Select folder to save report files";

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string directoryPath = folderBrowserDialog.SelectedPath;
                GenerateFiles(directoryPath);
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {

            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "Select source of folder";
                DialogResult dialogResult = fbd.ShowDialog();
                textBox1.Text = "";
                string[] dirs = Directory.GetDirectories(fbd.SelectedPath);
                DirSearch(fbd.SelectedPath);
                
            }
        }

        private void ShowFinallMessage()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("گزارش جزئیات فایل های پروژه در مسیر");
            //stringBuilder.AppendLine("D:\\DetailsReportFile");
            stringBuilder.Append("تولید گردید.");
            MessageBox.Show(stringBuilder.ToString());
        }

        private void ClearVariables()
        {
            counter = 0;
            filecounter = 0;
            TotallLines = 0;
            //TotallAreaLines = 0;
            fileCount = 0;
            javaCount = 0;
            javaLineCount = 0;
            csCount = 0;
            csLineCount = 0;
            jsCount = 0;
            jsLineCount = 0;
            swiftCount = 0;
            swiftLineCount = 0;
            cshtmlCount = 0;
            cshtmlLineCount = 0;
            cssCount = 0;
            cssLineCount = 0;
            line = string.Empty;
            fileName = string.Empty;
            result = string.Empty;
            csfiles = string.Empty;
            cssfiles = string.Empty;
            jsfiles = string.Empty;
            javafiles = string.Empty;
            cshtmlfiles = string.Empty;
            swiftfiles = string.Empty;
            TempFiles.Clear();
        }

        private void FillTextBoxResult()
        {
            this.textBox1.Multiline = true;
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.RightToLeft = RightToLeft.Yes;
            textBox1.Height = 200;
            textBox1.Text += "تعداد کل فایل های  .java  :  " + javaCount.ToString();
            textBox1.Text += System.Environment.NewLine;
            textBox1.Text += "تعداد کل لاین های  .java  :  " + javaLineCount.ToString();
            textBox1.Text += System.Environment.NewLine;
            textBox1.Text += "**************************";
            textBox1.Text += System.Environment.NewLine;
            textBox1.Text += "تعداد کل فایل های  .cs  :  " + csCount.ToString();
            textBox1.Text += System.Environment.NewLine;
            textBox1.Text += "تعداد کل لاین های  .cs  :  " + csLineCount.ToString();
            textBox1.Text += System.Environment.NewLine;
            textBox1.Text += "**************************";
            textBox1.Text += System.Environment.NewLine;
            textBox1.Text += "تعداد کل فایل های  .css  :  " + cssCount.ToString();
            textBox1.Text += System.Environment.NewLine;
            textBox1.Text += "تعداد کل لاین های  .css  :  " + cssLineCount.ToString();
            textBox1.Text += System.Environment.NewLine;
            textBox1.Text += "**************************";
            textBox1.Text += System.Environment.NewLine;
            textBox1.Text += "تعداد کل فایل های  .js  :  " + jsCount.ToString();
            textBox1.Text += System.Environment.NewLine;
            textBox1.Text += "تعداد کل لاین های  .js  :  " + jsLineCount.ToString();
            textBox1.Text += System.Environment.NewLine;
            textBox1.Text += "**************************";
            textBox1.Text += System.Environment.NewLine;
            textBox1.Text += "تعداد کل فایل های  .cshtml  :  " + cshtmlCount.ToString();
            textBox1.Text += System.Environment.NewLine;
            textBox1.Text += "تعداد کل لاین های  .cshtml  :  " + cshtmlLineCount.ToString();
            textBox1.Text += System.Environment.NewLine;
            textBox1.Text += "**************************";
            textBox1.Text += System.Environment.NewLine;
            textBox1.Text += "تعداد کل فایل های  .swift  :  " + swiftCount.ToString();
            textBox1.Text += System.Environment.NewLine;
            textBox1.Text += "تعداد کل لاین های  .swift  :  " + swiftLineCount.ToString();
            textBox1.Text += System.Environment.NewLine;
            textBox1.Text += "**************************";
            textBox1.Text += System.Environment.NewLine;
            textBox1.Text += "تعداد کل فایل ها  :  " + fileCount.ToString();
            textBox1.Text += System.Environment.NewLine;
            textBox1.Text += "تعداد کل لاین ها  :  " + TotallLines;
        }

        private void GenerateFiles(string directoryPath)
        {
            try
            {
                // Create the directory if it doesn't exist
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Create a subfolder named "Report"
                string reportFolderPath = Path.Combine(directoryPath, "Report");
                if (!Directory.Exists(reportFolderPath))
                {
                    Directory.CreateDirectory(reportFolderPath);
                }

                // Write files to the "Report" subfolder
                File.WriteAllText(Path.Combine(reportFolderPath, "DetailsReportFile.txt"), result);

                if (javafiles.Length > 0)
                    File.WriteAllText(Path.Combine(reportFolderPath, "javafiles.txt"), javafiles);

                if (cssfiles.Length > 0)
                    File.WriteAllText(Path.Combine(reportFolderPath, "cssfiles.txt"), cssfiles);

                if (csfiles.Length > 0)
                    File.WriteAllText(Path.Combine(reportFolderPath, "csfiles.txt"), csfiles);

                if (jsfiles.Length > 0)
                    File.WriteAllText(Path.Combine(reportFolderPath, "jsfiles.txt"), jsfiles);

                if (cshtmlfiles.Length > 0)
                    File.WriteAllText(Path.Combine(reportFolderPath, "cshtmlfiles.txt"), cshtmlfiles);

                if (swiftfiles.Length > 0)
                    File.WriteAllText(Path.Combine(reportFolderPath, "swiftfiles.txt"), swiftfiles);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillOperation()
        {
            
            result += System.Environment.NewLine;
            result += "تعداد کل فایل های  .java  :  " + javaCount;
            result += System.Environment.NewLine;
            result += "تعداد کل لاین های  .java  :  " + javaLineCount;
            result += System.Environment.NewLine;
            result += "**************************";
            result += System.Environment.NewLine;
            result += "تعداد کل فایل های  .cs  :  " + csCount;
            result += System.Environment.NewLine;
            result += "تعداد کل لاین های  .cs  :  " + csLineCount;
            result += System.Environment.NewLine;
            result += "**************************";
            result += System.Environment.NewLine;
            result += "تعداد کل فایل های  .css :  " + cssCount;
            result += System.Environment.NewLine;
            result += "تعداد کل لاین های  .css :  " + cssLineCount;
            result += System.Environment.NewLine;
            result += "**************************";
            result += System.Environment.NewLine;
            result += "تعداد کل فایل های  .js :  " + jsCount;
            result += System.Environment.NewLine;
            result += "تعداد کل لاین های  .js :  " + jsLineCount;
            result += System.Environment.NewLine;
            result += "**************************";
            result += System.Environment.NewLine;
            result += "تعداد کل فایل های  .cshtml :  " + cshtmlCount;
            result += System.Environment.NewLine;
            result += "تعداد کل لاین های  .cshtml :  " + cshtmlLineCount;
            result += System.Environment.NewLine;
            result += "**************************";
            result += System.Environment.NewLine;
            result += "تعداد کل فایل های  .swift :  " + swiftCount;
            result += System.Environment.NewLine;
            result += "تعداد کل لاین های  .swift :  " + swiftLineCount;
            result += System.Environment.NewLine;
            result += "**************************";
            result += System.Environment.NewLine;
            result += "تعداد کل فایل ها  :  " + fileCount;
            result += System.Environment.NewLine;
            result += "**************************";
            result += System.Environment.NewLine;
            result += "تعداد کل لاین ها  :  " + TotallLines;
        }

        private void DirSearch(string sDir)
        {

            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    List<string> myFiles = Directory.GetFiles(d, "*.*", SearchOption.AllDirectories)
                      .Where(file => new string[] { ".java", ".css", ".cs", ".js",".cshtml", ".swift" }
                      .Contains(Path.GetExtension(file)))
                      .ToList();
                    //fileCount += myFiles.Count();

                    if (myFiles.Count() > 0)

                    {
                        foreach (string f in myFiles)
                        {
                            if (!TempFiles.Contains(f))
                            {
                                TempFiles.Add(f);
                                fileCount++;

                                if (!string.IsNullOrEmpty(result)) result += System.Environment.NewLine;

                                fileName = Path.GetFileName(f);
                                var getExtensionfile = Path.GetExtension(f);
                                switch (getExtensionfile)
                                {
                                    #region .java
                                   
                                    case ".java":
                                        counter = 0;
                                        result += filecounter + " - " + "دایرکتوری  :  " + f;
                                        result += System.Environment.NewLine;
                                        result += "نام فایل  :  " + fileName;
                                        System.IO.StreamReader javafile =
                                                new System.IO.StreamReader(f);
                                        while ((line = javafile.ReadLine()) != null)
                                        {
                                            counter++;

                                        }

                                        javafile.Close();
                                        result += System.Environment.NewLine;
                                        result += "تعداد لاین های فایل :" + counter.ToString();
                                        result += System.Environment.NewLine;
                                        result += "--------------------------------";
                                        filecounter++;
                                        javaCount++;
                                        TotallLines += counter;
                                        javaLineCount += counter;
                                        javafiles += f;
                                        javafiles += System.Environment.NewLine;
                                        break;
                                    #endregion
                                    #region .css
                                    case ".css":
                                        counter = 0;
                                        result += filecounter + " - " + "دایرکتوری  :  " + f;
                                        result += System.Environment.NewLine;
                                        result += "نام فایل  :  " + fileName;
                                        System.IO.StreamReader cssfile =
                                                new System.IO.StreamReader(f);
                                        while ((line = cssfile.ReadLine()) != null)
                                        {
                                            counter++;
                                        }

                                        cssfile.Close();
                                        result += System.Environment.NewLine;
                                        result += "تعداد لاین های فایل :" + counter.ToString();
                                        result += System.Environment.NewLine;
                                        result += "--------------------------------";
                                        filecounter++;
                                        cssCount++;
                                        TotallLines += counter;
                                        cssLineCount += counter;
                                        cssfiles += f;
                                        cssfiles += System.Environment.NewLine;
                                        break;
                                    #endregion
                                    #region .cs
                                    case ".cs":
                                        
                                        //if (f.Contains(@"\Areas\"))
                                        //{
                                        //    int areafilecounter = 0;
                                        //    int startindex = f.IndexOf(@"\Areas\");
                                        //    string outputstring1 = f.Substring(startindex + 7);
                                        //    int startindex1 = outputstring1.IndexOf(@"\");
                                        //    string outputstring2 = outputstring1.Substring(0,startindex1);
                                        //    if (!TempAreas.Contains(outputstring2))
                                        //    {
                                        //        TempAreas.Add(outputstring2);
                                        //    }
                                        //    foreach (var item in TempAreas)
                                        //    {


                                        //        int areacounter = 0;
                                        //        result += System.Environment.NewLine;
                                        //        result +="نام Area : " + item;
                                        //        result += System.Environment.NewLine;
                                        //        System.IO.StreamReader Item11 =
                                        //                new System.IO.StreamReader(f);
                                        //        while ((line = Item11.ReadLine()) != null)
                                        //        {
                                        //            areacounter++;
                                        //        }

                                        //        Item11.Close();
                                        //        TotallAreaLines +=  areacounter;
                                               
                                        //        filecounter++;
                                        //        areafilecounter++;
                                        //        csCount++;
                                        //        TotallLines += counter;
                                        //        csLineCount += counter;

                                        //        csfiles += f;
                                        //        csfiles += System.Environment.NewLine;
                                        //    }
                                        //}
                                        counter = 0;
                                        result += filecounter + " - " + "دایرکتوری  :  " + f;
                                        result += System.Environment.NewLine;
                                        result += "نام فایل  :  " + fileName;
                                        System.IO.StreamReader csfile =
                                                new System.IO.StreamReader(f);
                                        while ((line = csfile.ReadLine()) != null)
                                        {
                                            counter++;

                                        }

                                        csfile.Close();
                                        result += System.Environment.NewLine;
                                        result += "تعداد لاین های فایل :" + counter.ToString();
                                        result += System.Environment.NewLine;
                                        result += "--------------------------------";
                                        filecounter++;
                                        csCount++;
                                        TotallLines += counter;
                                        csLineCount += counter;

                                        csfiles += f;
                                        csfiles += System.Environment.NewLine;
                                        break;
                                    #endregion
                                    #region .cshtml
                                    case ".cshtml":
                                        counter = 0;
                                        result += filecounter + " - " + "دایرکتوری  :  " + f;
                                        result += System.Environment.NewLine;
                                        result += "نام فایل  :  " + fileName;
                                        System.IO.StreamReader cshtmlfile =
                                                new System.IO.StreamReader(f);
                                        while ((line = cshtmlfile.ReadLine()) != null)
                                        {
                                            counter++;

                                        }

                                        cshtmlfile.Close();
                                        result += System.Environment.NewLine;
                                        result += "تعداد لاین های فایل :" + counter.ToString();
                                        result += System.Environment.NewLine;
                                        result += "--------------------------------";
                                        filecounter++;
                                        cshtmlCount++;
                                        TotallLines += counter;
                                        cshtmlLineCount += counter;

                                        cshtmlfiles += f;
                                        cshtmlfiles += System.Environment.NewLine;
                                        break;
                                    #endregion
                                    #region .js
                                    case ".js":
                                        counter = 0;
                                        result += filecounter + " - " + "دایرکتوری  :  " + f;
                                        result += System.Environment.NewLine;
                                        result += "نام فایل  :  " + fileName;
                                        System.IO.StreamReader jsfile =
                                                new System.IO.StreamReader(f);
                                        while ((line = jsfile.ReadLine()) != null)
                                        {
                                            counter++;
                                        }

                                        jsfile.Close();
                                        result += System.Environment.NewLine;
                                        result += "تعداد لاین های فایل :" + counter.ToString();
                                        result += System.Environment.NewLine;
                                        result += "--------------------------------";
                                        filecounter++;
                                        jsCount++;
                                        TotallLines += counter;
                                        jsLineCount += counter;
                                        jsfiles += f;
                                        jsfiles += System.Environment.NewLine;
                                        break;
                                    #endregion
                                    #region .swift
                                    case ".swift":
                                        counter = 0;
                                        result += filecounter + " - " + "دایرکتوری  :  " + f;
                                        result += System.Environment.NewLine;
                                        result += "نام فایل  :  " + fileName;
                                        System.IO.StreamReader swiftfile =
                                                new System.IO.StreamReader(f);
                                        while ((line = swiftfile.ReadLine()) != null)
                                        {
                                            counter++;
                                        }

                                        swiftfile.Close();
                                        result += System.Environment.NewLine;
                                        result += "تعداد لاین های فایل :" + counter.ToString();
                                        result += System.Environment.NewLine;
                                        result += "--------------------------------";
                                        filecounter++;
                                        swiftCount++;
                                        TotallLines += counter;
                                        swiftLineCount += counter;
                                        swiftfiles += f;
                                        swiftfiles += System.Environment.NewLine;
                                        break;
                                        #endregion


                                }


                            }
                        }
                    }

                    DirSearch(d);
                }

                //}

            }



            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }

        }

       

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

       

       
    }
}
