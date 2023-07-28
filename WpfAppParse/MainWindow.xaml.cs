using System;
using System.IO.Compression;
using System.IO;
using System.Windows;
using Path = System.IO.Path;
using System.Windows.Forms;
using System.Text.Json;
using System.Text;

namespace WpfAppParse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 解压压缩包某个文件
        /// </summary>
        /// <param name="zipFilePath"></param>
        /// <param name="desiredFile"></param>
        /// <param name="outputDirectory"></param>
        private void UnzipSpecificFile(string zipFilePath, string desiredFile, string outputDirectory)
        {
            if (!Directory.Exists(outputDirectory))
                Directory.CreateDirectory(outputDirectory);

            using ZipArchive zipArchive = ZipFile.OpenRead(zipFilePath);
            foreach (ZipArchiveEntry entry in zipArchive.Entries)
            {
                if (entry.Name.Equals(desiredFile, StringComparison.OrdinalIgnoreCase))
                {
                    string extractedFilePath = Path.Combine(outputDirectory, entry.Name);
                    if(File.Exists(extractedFilePath))
                        break;
                    entry.ExtractToFile(extractedFilePath);
                    break;
                }
            }
        }

        /// <summary>
        /// 读取压缩包中json文件
        /// </summary>
        /// <param name="zipFilePath"></param>
        /// <param name="jsonFileName"></param>
        /// <returns></returns>
        private JsonDocument? ReadJsonFromZipFileUsingJsonDocument(string zipFilePath, string jsonFileName)
        {
            using ZipArchive zipArchive = ZipFile.OpenRead(zipFilePath);
            foreach (ZipArchiveEntry entry in zipArchive.Entries)
            {
                if (entry.Name.Equals(jsonFileName, StringComparison.OrdinalIgnoreCase))
                {
                    using var streamReader = new StreamReader(entry.Open());
                    string jsonContent = streamReader.ReadToEnd();
                    JsonDocument jsonDocument = JsonDocument.Parse(jsonContent);
                    return jsonDocument;
                }
            }
            return null;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "请选择文件夹";
            dialog.Filter = "所有文件(*.*)|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = dialog.FileName;
                if (string.IsNullOrEmpty(path))
                {
                    System.Windows.MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                PathTextBox.Text = path;
                string? directoryPath = Path.GetDirectoryName(path);
                if (directoryPath != null && dialog.SafeFileName != null)
                {
                    var savePath = dialog.SafeFileName.Remove(dialog.SafeFileName.LastIndexOf('.'));
                    string sDirPath = Path.Combine(directoryPath, savePath);
                    UnzipSpecificFile(path, "thumb.jpg", sDirPath);

                    var tt = ReadJsonFromZipFileUsingJsonDocument(path, "design.cfg");
                    if (tt != null)
                    {
                        var root = tt.RootElement;
                        var name = root.GetProperty("sName").GetString();

                        //string sName = "";
                        //if (!string.IsNullOrWhiteSpace(name))
                        //{
                        //    byte[] byteArray = Encoding.UTF8.GetBytes(name);
                        //    Encoding targetEncoding = Encoding.GetEncoding("GB2312");
                        //    sName = targetEncoding.GetString(byteArray);
                        //}

                        var thumbFile = root.GetProperty("sThumbFile").GetString();
                        string sThumbFile = @"http://own.colkwp.com:59527/upload/userDesign";
                        if (!string.IsNullOrWhiteSpace(thumbFile))
                            sThumbFile = Path.Combine(sThumbFile, thumbFile);
                        var design = new Design()
                        {
                            Name = name,
                            ThumbFile = sThumbFile,
                        };
                        var json = JsonSerializer.Serialize(design);
                        var jsonPath = Path.Combine(sDirPath, "design.json");
                        if (!File.Exists(jsonPath))
                            File.WriteAllText(jsonPath, json);
                    }
                }
            }
        }
    }
}
