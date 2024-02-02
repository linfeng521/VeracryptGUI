using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Input;
using VeracryptGui.Utils;

namespace VeracryptGui
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileInfo diskFileInfo = null;

        public MainWindow()
        {
            InitializeComponent();

            DiskList.ItemsSource = DriveDiskUtils.GetFreeDriveLetters();
        }

        private void tbFilePath_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "磁盘文件 (*.disk)|*.disk";
            if (openFileDialog.ShowDialog() == true)
            {
                diskFileInfo = new FileInfo(openFileDialog.FileName);
                string parentDirectoryName = Directory.GetParent(openFileDialog.FileName).Name;
                tbFilePath.Text = diskFileInfo.Name;
                //调用FileUitls.GetFileSize获取文件合适的大小单位
                statusBarText.Text = $"{parentDirectoryName}\\{diskFileInfo.Name} 文件大小：{FileUtils.GetFileSize(diskFileInfo)}";
            }
        }

        private void CreateDiskBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateDiskWindow createDiskWindow = new CreateDiskWindow();
            createDiskWindow.ShowDialog();
        }

        private void MountBtn_Click(object sender, RoutedEventArgs e)
        {
            string password = vearcryptPassword.Password;
            string diskletter = DiskList.Text;
            if (diskletter.Equals(""))
            {
                MessageBox.Show("磁盘盘符为空");
            }
            else
            {
                VecryptUtil.MountEncryptDisk(diskFileInfo.FullName, password, diskletter);
            }
        }

        private void UnMountBtn_Click(object sender, RoutedEventArgs e)
        {
            string diskletter = DiskList.Text;
            if (diskletter.Equals(""))
            {
                MessageBox.Show("磁盘盘符为空");
            }
            else
            {
                VecryptUtil.UnMountEncryptDisk(diskletter);
            }
        }
    }
}