using System.Windows;
using VeracryptGui.Utils;

namespace VeracryptGui
{
    /// <summary>
    /// CreateDiskWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CreateDiskWindow : Window
    {
        public CreateDiskWindow()
        {
            InitializeComponent();
        }

        private void CreateDiskBtn_Click(object sender, RoutedEventArgs e)
        {
            string createDisk = createDiskName.Text;
            string createDiskPwd = createDiskPassword.Password;
            string createDiskSize = diskSize.Text;
            VecryptUtil.CreateEncryptedDisk(createDisk, createDiskPwd, createDiskSize);
        }
    }
}