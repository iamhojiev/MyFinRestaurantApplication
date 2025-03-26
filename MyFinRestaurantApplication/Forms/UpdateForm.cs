using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;


namespace ManagerApplication.Forms
{
    public partial class UpdateForm : Form
    {
        private readonly WebClient _webClient = new WebClient();
        private string _tempExtractPath;
        private string _detectedCassaPath;

        public UpdateForm()
        {
            InitializeComponent();
            DetectInstalledCassa();
            _webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
            _webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
        }

        private void DetectInstalledCassa()
        {
            string[] possiblePaths =
            {
                @"C:\Program Files (x86)\MyFin\MyFinCassa (Restaurant)",
                @"C:\Program Files (x86)\MyFin\MyFinCassa (Cafe)"
            };

            foreach (var path in possiblePaths)
            {
                if (Directory.Exists(path))
                {
                    _detectedCassaPath = path;
                    break;
                }
            }
        }

        public bool CheckForUpdates()
        {
            try
            {
                var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
                var versionFileUrl = "https://drive.google.com/uc?export=download&id=1HZE3YZTato6U3nXJXnRlXcbMIMIEuo2w";
                var latestVersion = new Version(_webClient.DownloadString(versionFileUrl).Trim());

                return latestVersion > currentVersion;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка проверки обновлений: {ex.Message}");
                return false;
            }
        }

        public void UpdateApplication()
        {
            try
            {
                var fileIdUrl = "https://drive.google.com/uc?export=download&id=1H8qeYqLcNa-cFo68ZtVRTe0SeRtOXw-N";
                var fileId = _webClient.DownloadString(fileIdUrl).Trim();
                var updateFileUrl = $"https://drive.usercontent.google.com/download?id={fileId}&confirm=t&uuid=c087dfae-7043-4c0f-b7db-7f480801b674&at=AIrpjvMwGWVc19rTk2GRe_CSri1h%3A1739826447288";

                _tempExtractPath = Path.Combine(Path.GetTempPath(), "MyFinUpdate");
                var zipPath = Path.Combine(Path.GetTempPath(), "MyFinSetup.zip");

                Directory.CreateDirectory(_tempExtractPath);
                _webClient.DownloadFileAsync(new Uri(updateFileUrl), zipPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка запуска обновления: {ex.Message}");
            }
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            pbDownload.Value = e.ProgressPercentage;
            lblDownload.Text = $"{e.BytesReceived / 1024 / 1024}MB / {e.TotalBytesToReceive / 1024 / 1024}MB";
        }

        private void WebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show($"Ошибка загрузки: {e.Error.Message}");
                return;
            }

            try
            {
                var zipPath = Path.Combine(Path.GetTempPath(), "MyFinSetup.zip");
                ZipFile.ExtractToDirectory(zipPath, _tempExtractPath);
                File.Delete(zipPath);

                CreateAndRunBatchScript();
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обработки обновления: {ex.Message}");
            }
        }

        private void CreateAndRunBatchScript()
        {
            var batPath = Path.Combine(Path.GetTempPath(), "MyFinUpdate.bat");
            var appPath = Path.GetDirectoryName(Application.ExecutablePath);

            var batchCommands = new StringBuilder();
            batchCommands.AppendLine("@echo off");
            batchCommands.AppendLine("echo Обновление MyFin...");
            batchCommands.AppendLine("echo Пожалуйста не закрывайте это окно...");
            batchCommands.AppendLine("timeout /t 3 /nobreak > nul");

            // Закрываем все связанные приложения
            batchCommands.AppendLine("taskkill /IM MyFinManager.exe /F > nul 2>&1");
            batchCommands.AppendLine("taskkill /IM MyFinCassa*.exe /F > nul 2>&1");

            // Копирование для MyFinManager
            batchCommands.AppendLine($"xcopy /Y /E \"{_tempExtractPath}\\MyFinManager\\*\" \"{appPath}\" > nul");

            // Копирование для MyFinCassa (если обнаружена)
            if (!string.IsNullOrEmpty(_detectedCassaPath))
            {
                var cassaFolder = Path.GetFileName(_detectedCassaPath);
                batchCommands.AppendLine($"xcopy /Y /E \"{_tempExtractPath}\\{cassaFolder}\\*\" \"{_detectedCassaPath}\" > nul");
            }

            // Копирование для Android
            batchCommands.AppendLine($"xcopy /Y /E \"{_tempExtractPath}\\android\\*\" \"C:\\xampp\\htdocs\" > nul");

            // Очистка и перезапуск
            batchCommands.AppendLine($"rmdir /S /Q \"{_tempExtractPath}\"");
            batchCommands.AppendLine($"start \"\" \"{Application.ExecutablePath}\"");
            batchCommands.AppendLine("del \"%~f0\"");

            File.WriteAllText(batPath, batchCommands.ToString());

            Process.Start(new ProcessStartInfo
            {
                FileName = batPath,
                WindowStyle = ProcessWindowStyle.Normal
            });
        }

        private void btnClose_Click_1(object sender, EventArgs e) => Close();
    }
}