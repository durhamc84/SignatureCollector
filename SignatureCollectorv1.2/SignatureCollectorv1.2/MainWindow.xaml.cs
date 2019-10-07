using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Data.SqlClient;
using System.Drawing;
using Xceed.Wpf.Toolkit;
using System.Timers;
using System.Diagnostics;



namespace SignatureCollectorv1._2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtClientCode.Select(txtClientCode.Text.Length, 0);
            txtStaffCode.Select(txtStaffCode.Text.Length, 0);
            txtStartTime.Select(0, 0);
            txtEndTime.Select(0, 0);
            txtDate.Select(0, 0);
        }             
               

        private void TextClear_Click(object sender, RoutedEventArgs e)
        {
            Signature.Strokes.Clear();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Directory.CreateDirectory(@"C:\ProgramData\SignatureCollector");            

            DateTime dateTime = new DateTime();

            int margin = (int)this.Signature.Margin.Left;
            int width = (int)this.Signature.ActualWidth - margin;
            int height = (int)this.Signature.ActualHeight - margin;
            RenderTargetBitmap rtb = new RenderTargetBitmap(width, height, 96d, 96d, PixelFormats.Default);
            rtb.Render(Signature);
            BmpBitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));            

            String clientCode = txtClientCode.Text;
            String staffCode = txtStaffCode.Text;
            dateTime = DateTime.Now;
            String oldStartTime = txtStartTime.Text;
            String oldEndTime = txtEndTime.Text;
            String oldDate = txtDate.Text;
            String path = @"C:\ProgramData\SignatureCollector\Signature.bmp";
            String pathTwo = @"C:\ProgramData\SignatureCollector\Signature";
            String infoPath = @"C:\ProgramData\SignatureCollector\SignatureInfo.txt";
            String infoPathTwo = @"C:\ProgramData\SignatureCollector\SignatureInfo";            
            int count = 2;
            string startTime = oldStartTime.Replace("_","");
            string endTime = oldEndTime.Replace("_", "");
            string date = oldDate.Replace("_", "");
            String saveText = clientCode + staffCode + startTime + endTime + date + dateTime;

            try
            {
                if (clientCode.Length < 6)
                {
                    MessageBoxResult messageBox = Xceed.Wpf.Toolkit.MessageBox.Show("The Client Code must be 6 characters in length!");
                    return;
                }
                if (staffCode.Length < 4)
                {
                    MessageBoxResult messageBox = Xceed.Wpf.Toolkit.MessageBox.Show("The Staff Code must be 4 characters in length!");
                    return;
                }
                if (startTime.Length < 7)
                {
                    MessageBoxResult messageBox = Xceed.Wpf.Toolkit.MessageBox.Show("The Start Time must be in the following format:" + "\n" + "HH:MMam/pm");
                    return;
                }
                if (endTime.Length < 7)
                {
                    MessageBoxResult messageBox = Xceed.Wpf.Toolkit.MessageBox.Show("The End Time must be in the following format:" + "\n" + "HH:MMam/pm");
                    return;
                }
                if (date.Length < 10)
                {
                    MessageBoxResult messageBox = Xceed.Wpf.Toolkit.MessageBox.Show("The Date must be in the following format:" + "\n" + "MM/DD/YYYY");
                    return;
                }
            }
            catch (Exception)
            {
                return;               
            }


            try
            {
                if (!File.Exists(path))
                {

                    using (FileStream fileStream = File.Open(path, FileMode.Create))
                    {
                        encoder.Save(fileStream);
                        fileStream.Close();
                    }

                    using (StreamWriter sw = File.CreateText(infoPath))
                    {
                        sw.WriteLine(saveText);
                        sw.Close();
                    }

                }

                else 
                {
                    while (File.Exists(path))
                    {
                        path = System.IO.Path.Combine(pathTwo + count + ".bmp");
                        count++;
                    }

                    using (FileStream fileStream = File.Open(path, FileMode.Create))
                    {
                        encoder.Save(fileStream);
                        fileStream.Close();
                    }

                    using (StreamWriter sw = File.CreateText(infoPathTwo + (count-1) + ".txt"))
                    {
                        sw.WriteLine(saveText);
                        sw.Close();
                    }

                }

                Signature.Strokes.Clear();
                txtClientCode.Clear();
                txtEndTime.Clear();
                txtStaffCode.Clear();
                txtStartTime.Clear();
                txtDate.Clear();
                
            }

            catch (Exception ex)
            {
                MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show(ex.ToString());
            }

        }

        private byte[] ImageToStream(string fileName)
        {
            using (MemoryStream stream = new MemoryStream())
            {
            tryagain:
                try
                {
                    Bitmap image = new Bitmap(fileName);
                    image.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                }
                catch (Exception ex)
                {
                    MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show(ex.ToString());
                    goto tryagain;
                }

                return stream.ToArray();
            }
            
        }

        static async void DeleteAllFiles(string[] filePaths)
        {
            await Task.Delay(10000);
            foreach (string filePath in filePaths)
            {
                File.Delete(filePath);
            }           
        }
        
        private void Sync_Click(object sender, RoutedEventArgs e)
        {
            DateTime syncTime = new DateTime();

            syncTime = DateTime.Now;
            String clientcode;
            String staffCode;
            String startTime;
            String endTime;
            String staffDate;
            String activityDate;
            String infoPath = @"C:\ProgramData\SignatureCollector\SignatureInfo.txt";
            String infoPathTwo = @"C:\ProgramData\SignatureCollector\SignatureInfo";
            String path = @"C:\ProgramData\SignatureCollector\Signature.bmp";
            String pathTwo = @"C:\ProgramData\SignatureCollector\Signature";
            String fileInfo;
            Int32 tableID = 0;
            int updated = 0;
           
            
         

            try
            {
                if (File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(infoPath))
                    {
                    
                        fileInfo = sr.ReadToEnd();

                        clientcode = fileInfo.Substring(0, 6);
                        staffCode = fileInfo.Substring(6, 4);
                        startTime = fileInfo.Substring(10, 7);
                        endTime = fileInfo.Substring(17, 7);
                        staffDate = fileInfo.Substring(24, 10);
                        activityDate = fileInfo.Substring(34, 20);
                        

                        sr.Close();
                    }
                     
                    
                    

                    using (SqlConnection con = new SqlConnection("Data Source=ECHOSQL\\ECHOSQL;Initial Catalog=ChrisTestDatabase;User ID = sa; Password = "))
                    {

                        using (SqlCommand com = new SqlCommand("INSERT INTO dbo.signatures (clientcode_c, staffcode_c, starttime_t, endtime_t, staffdate_c, activitydate_c, syncdate_d) VALUES(@clientC, @staffC, @startT, @endT, @staffD, @activityD, @syncD); SELECT SCOPE_IDENTITY();", con))
                        {
                            
                            con.Open();
                            com.Parameters.AddWithValue("@clientC", clientcode);
                            com.Parameters.AddWithValue("@staffC", staffCode);
                            com.Parameters.AddWithValue("@startT", startTime);
                            com.Parameters.AddWithValue("@endT", endTime);
                            com.Parameters.AddWithValue("@staffD", staffDate);
                            com.Parameters.AddWithValue("@activityD", activityDate);
                            com.Parameters.AddWithValue("@syncD", syncTime);
 
                            tableID = Convert.ToInt32(com.ExecuteScalar());

                            con.Close();

                        }
                                                
                        using (SqlCommand cmd = new SqlCommand("UPDATE dbo.signatures SET signature_vb = @signed WHERE uniqueid_i = @id", con))
                        {
                            byte[] image = Array.Empty<byte>();
                            image = ImageToStream(path);
                            con.Open();                         
                            cmd.Parameters.AddWithValue("@id", tableID);
                            cmd.Parameters.AddWithValue("@signed", image);

                            updated = cmd.ExecuteNonQuery();

                        }

                    }

                }
                                           

            
                int dirCount = Directory.GetFiles(@"C:\ProgramData\SignatureCollector").Length / 2;

                if (dirCount != 0)
                {
                    int fileCount = (Directory.GetFiles(@"C:\ProgramData\SignatureCollector").Length/2) + 1;
                    int count = 2;

                    while (count < fileCount)
                    {
                        path = System.IO.Path.Combine(pathTwo + count + ".bmp");
                        infoPath = System.IO.Path.Combine(infoPathTwo + count + ".txt");

                        using (StreamReader sr = new StreamReader(infoPath))
                        {
                            
                            fileInfo = sr.ReadToEnd();

                            clientcode = fileInfo.Substring(0, 6);
                            staffCode = fileInfo.Substring(6, 4);
                            startTime = fileInfo.Substring(10, 7);
                            endTime = fileInfo.Substring(17, 7);
                            staffDate = fileInfo.Substring(24, 10);
                            activityDate = fileInfo.Substring(34, 20);

                            sr.Close();
                        }



                        using (SqlConnection con = new SqlConnection("Data Source=ECHOSQL\\ECHOSQL;Initial Catalog=ChrisTestDatabase;User ID = sa; Password = "))
                        {

                            using (SqlCommand com = new SqlCommand("INSERT INTO dbo.signatures (clientcode_c, staffcode_c, starttime_t, endtime_t, staffdate_c, activitydate_c, syncdate_d) VALUES(@clientC, @staffC, @startT, @endT, @staffD, @activityD, @syncD);SELECT SCOPE_IDENTITY();", con))
                            {                                
                                con.Open();
                                com.Parameters.AddWithValue("@clientC", clientcode);
                                com.Parameters.AddWithValue("@staffC", staffCode);
                                com.Parameters.AddWithValue("@startT", startTime);
                                com.Parameters.AddWithValue("@endT", endTime);
                                com.Parameters.AddWithValue("@staffD", staffDate);
                                com.Parameters.AddWithValue("@activityD", activityDate);
                                com.Parameters.AddWithValue("@syncD", syncTime);

                                tableID = Convert.ToInt32(com.ExecuteScalar());
                                con.Close();

                            }

                            using (SqlCommand cmd = new SqlCommand("UPDATE dbo.signatures SET signature_vb = @signed WHERE uniqueid_i = @id", con))
                            {                                
                                byte[] image = Array.Empty<byte>();
                                image = ImageToStream(path);
                                con.Open();                                
                                cmd.Parameters.AddWithValue("@id", tableID);
                                cmd.Parameters.AddWithValue("@signed", image);

                                updated = cmd.ExecuteNonQuery();

                            }

                        }
                        
                        count++;
                    }
                    
                }                   

            }

            catch (Exception ex)
            {
                MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show(ex.ToString());
            }

            try
            {
                

                if (Directory.GetFiles(@"C:\ProgramData\SignatureCollector").Length != 0 && updated != 0)
                {
                    string[] filePaths = Directory.GetFiles(@"C:\ProgramData\SignatureCollector");
                    DeleteAllFiles(filePaths);
                    MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show("Sync Successful!");
                }
                                

            }

            catch (Exception ex)
            {
                MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show(ex.ToString());
            }                        

        }

    }

}
