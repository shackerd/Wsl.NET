using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wsl.NET;
using Wsl.NET.IPC;

namespace WslGUI
{
    public partial class Form1 : Form
    {
        private readonly CancellationTokenSource _source =
                new CancellationTokenSource();

        private CancellationToken Token => _source.Token;
        private VmmemAgent _agent;
        private readonly IWslDriver _driver;

        private WslDistro SelectedDistro =>
            (WslDistro)dataGridView1.SelectedRows[0].DataBoundItem;

        public Form1()
        {
            InitializeComponent();
            _driver = WslBootstrap.Setup();            
            dataGridView1.DataSource = bindingSource1;
            // https://docs.microsoft.com/en-us/windows/terminal/command-line-arguments?tabs=windows
        }

        private async void terminateBtn_Click(object sender, EventArgs e)
        {            
            await _driver.TerminateAsync(SelectedDistro, Token);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            refreshTimer.Start();
            var distros = await _driver.FetchDistroAsync(Token);
            bindingSource1.DataSource = distros;
            _agent = new VmmemAgent();
        }

        private async void RefreshTimer_Tick(object sender, EventArgs e)
        {
            refreshLbl.Visible = true;            
            var distros = await _driver.FetchDistroAsync(Token);
            bindingSource1.DataSource = distros;
            vmmemUsageLbl.Text = _agent.GetMemoryUsage(out int percent);
            toolStripProgressBar1.Value = percent;
            refreshLbl.Visible = false;
        }

        private async void shutdownBtn_Click(object sender, EventArgs e)
        {
            var result =
                MessageBox.Show(
                    "This action will terminate all distro, are you sure?",
                    "Warning",
                    MessageBoxButtons.YesNoCancel
                );

            if (result == DialogResult.Yes)
            {
                await _driver.ShutdownAsync(Token);
            }
        }

        private void Log(string text)
        {
            richTextBox1.Text += $"{DateTime.Now} - {text}";
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await _driver.StartAsync(SelectedDistro, "sudo apt update", true, Token);
            await _driver.StartAsync(SelectedDistro, Token);
        }

        private async void ExportBtnClick(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog =
                new SaveFileDialog();

            saveFileDialog.FileName = SelectedDistro.Name;
            saveFileDialog.Filter = $"Tarball archive | .tar.gz";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await _driver.ExportAsync(
                        SelectedDistro,
                        saveFileDialog.FileName,
                        Token
                    );

                    MessageBox.Show(
                        $"Successfully exported distribution: {SelectedDistro.Name}", 
                        "Success", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Information
                    );
                }
                catch (WslException ex)
                {
                    MessageBox.Show(
                        ex.Message, 
                        "Error", 
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );                    
                }
            }            
        }



        private async void MovebtnClick(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog =
                new SaveFileDialog();

            saveFileDialog.FileName = SelectedDistro.Name;
            saveFileDialog.Filter = $"Tarball archive | .tar.gz";

            FolderBrowserDialog folderBrowserDialog = 
                new FolderBrowserDialog();

            WslDistro distro = SelectedDistro;

            if (saveFileDialog.ShowDialog() == DialogResult.OK && folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (distro.State == WslDistroState.Running)
                    {
                        Log($"Distribution is running, terminating : {distro.Name}");
                        await _driver.TerminateAsync(distro, Token);
                    }
                    Log($"Moving distribution : {distro.Name}");
                    await _driver.MoveAsync(
                        distro,
                        saveFileDialog.FileName,
                        folderBrowserDialog.SelectedPath,
                        Token
                    );
                    Log($"Successfully moved distribution: {distro.Name}");                    
                }
                catch (WslException ex)
                {
                    Log($"Failed to move distribution: {distro.Name}\n{ex.Message}");
                }
            }
        }

        private async void ImportBtnClick(object sender, EventArgs e)
        {
            //OpenFileDialog openFileDialog =
            //    new OpenFileDialog();

            //openFileDialog.Filter = $"Tarball archive | *.tar.gz";

            //if (openFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    try
            //    {
            //        Log("Importing distribution: Ubuntu");

            //        await _driver.ImportAsync(
            //            openFileDialog.FileName,
            //            "Ubuntu",
            //            WslDistroVersion.V1,
            //            Token
            //        );


            //        MessageBox.Show(
            //            $"Successfully imported distribution: {SelectedDistro.Name}",
            //            "Success",
            //            MessageBoxButtons.OK,
            //            MessageBoxIcon.Information
            //        );
            //    }
            //    catch (WslException ex)
            //    {
            //        MessageBox.Show(
            //            ex.Message,
            //            "Error",
            //            MessageBoxButtons.OK,
            //            MessageBoxIcon.Error
            //        );
            //    }
            //}
        }

        private async void SetDefaultBtnClick(object sender, EventArgs e)
        {
            await _driver.SetDefaultDistroAsync(SelectedDistro, Token);
        }
    }
}
