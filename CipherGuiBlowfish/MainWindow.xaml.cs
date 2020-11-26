using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;
using BlowfishAlgorithm;
using Me;

namespace CipherGuiBlowfish
{
    public partial class MainWindow : Window
    {
        // байты считанного файла
        byte[] _userFile;

        public MainWindow()
        {
            InitializeComponent();
            _userFile = new byte[0];
        }

        private void Button_FindFile_Click(object sender, RoutedEventArgs e)
        {
            string filePath;
            _userFile = WorkWithFile.ReadAllBytesWithOpenFileDialog(out filePath);

            if (_userFile == null)
            {
                AppendInConsoleLine("Не считал файл");
            }
            else
            {
                Label_FilePath.Content = filePath;
                AppendInConsoleLine("Считал файл: " + filePath);
            }

        }

        private async void CipherFile_Click(object sender, RoutedEventArgs e)
        {
            if ((_userFile == null) || (_userFile.Length == 0))
            {
                AppendInConsoleLine("Ошибка! Файл пуст или вы забыли его считать");
                return;
            }
            if (UserKey.Password.Length == 0)
            {
                AppendInConsoleLine("Ошибка! Не ввели пароль (он не может быть 0 символов)");
                return;
            }

            Blowfish blowfi = new Blowfish(123456789);
            switch (ComboBox_ModeDes.SelectedIndex)
            {
                case 0: // ECB
                    _userFile = blowfi.ECB_Encrypt(_userFile, Encoding.UTF8.GetBytes(UserKey.Password.ToString()));
                    break;
                case 1: // CBC
                    _userFile = blowfi.CBC_Encrypt(_userFile, Encoding.UTF8.GetBytes(UserKey.Password.ToString()));
                    break;
                case 2: // OFB
                    _userFile = blowfi.OFB_Encrypt(_userFile, Encoding.UTF8.GetBytes(UserKey.Password.ToString()));
                    break;
                case 3: // CFB
                    _userFile = blowfi.CFB_Encrypt(_userFile, Encoding.UTF8.GetBytes(UserKey.Password.ToString()));
                    break;
                case 4: // ECB - многопоточный 
                    _userFile = await blowfi.ECB_MultithreadedEncrypt(_userFile, Encoding.UTF8.GetBytes(UserKey.Password.ToString()));
                    break;
                default: // default будет ECB
                    _userFile = await blowfi.ECB_MultithreadedEncrypt(_userFile, Encoding.UTF8.GetBytes(UserKey.Password.ToString()));
                    break;
            }
            AppendInConsoleLine("Зашифровал файл: " + Label_FilePath.Content);

            if (WorkWithFile.WriteALLBytes((string)Label_FilePath.Content, _userFile) == true)
            {
                AppendInConsoleLine("== Сохранил результат ==");
            }
            else
            {
                AppendInConsoleLine("== Не удалось сохранить результат ==");
            }

            _userFile = new byte[0];
            Label_FilePath.Content = "-";
        }

        private async void DecipherFile_Click(object sender, RoutedEventArgs e)
        {
            if ((_userFile == null) || (_userFile.Length == 0))
            {
                AppendInConsoleLine("Ошибка! Файл пуст или вы забыли его считать");
                return;
            }
            if (UserKey.Password.Length == 0)
            {
                AppendInConsoleLine("Ошибка! Не ввели пароль (он не может быть 0 символов)");
                return;
            }

            Blowfish blowfi = new Blowfish(123456789);
            switch (ComboBox_ModeDes.SelectedIndex)
            {
                case 0: // ECB
                    _userFile = blowfi.ECB_Decrypt(_userFile, Encoding.UTF8.GetBytes(UserKey.Password.ToString()));
                    break;
                case 1: // CBC
                    _userFile = blowfi.CBC_Decrypt(_userFile, Encoding.UTF8.GetBytes(UserKey.Password.ToString()));
                    break;
                case 2: // OFB
                    _userFile = blowfi.OFB_Decrypt(_userFile, Encoding.UTF8.GetBytes(UserKey.Password.ToString()));
                    break;
                case 3: // CFB
                    _userFile = blowfi.CFB_Decrypt(_userFile, Encoding.UTF8.GetBytes(UserKey.Password.ToString()));
                    break;
                case 4: // ECB - многопоточный 
                    _userFile = await blowfi.ECB_MultithreadedDecrypt(_userFile, Encoding.UTF8.GetBytes(UserKey.Password.ToString()));
                    break;
                default: // default будет ECB
                    _userFile = await blowfi.ECB_MultithreadedDecrypt(_userFile, Encoding.UTF8.GetBytes(UserKey.Password.ToString()));
                    break;
            }
            AppendInConsoleLine("Расшифровал файл: " + Label_FilePath.Content);

            if (WorkWithFile.WriteALLBytes((string)Label_FilePath.Content, _userFile) == true)
            {
                AppendInConsoleLine("== Сохранил результат ==");
            }
            else
            {
                AppendInConsoleLine("== Не удалось сохранить результат ==");
            }

            _userFile = new byte[0];
            Label_FilePath.Content = "-";
        }

        private void MenuItem_Author_Click(object sender, RoutedEventArgs e)
        {
            AboutMe abme = new AboutMe();
            abme.Show();
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AppendInConsoleLine(string text)
        {
            MyConsole.AppendText(text + "\n");
        }

    }
}
