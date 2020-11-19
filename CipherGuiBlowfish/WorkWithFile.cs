using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherGuiBlowfish
{
    /// <summary>
    /// Статичный класс для работы с файлом
    /// </summary>
    static class WorkWithFile
    {
        /// <summary>
        /// Считывает файл, указанный пользователем через FileDialog
        /// </summary>
        /// <param name="filePath"> Выходной параметр - абсолютный путь до файла (null если не считан) </param>
        /// <returns> Возвращает байты файла или null, если по какой-либо причине не получилось считать файл.</returns>
        public static byte[] ReadAllBytesWithOpenFileDialog(out string filePath)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            if (fileDialog.ShowDialog() != true)
            {
                filePath = null;
                return null;
            }
            filePath = fileDialog.FileName;
            return ReadALLBytes(fileDialog.FileName);
        }

        /// <summary>
        /// Записывает байты recordedBytes в файл, указанный пользователем через FileDialog.
        /// </summary>
        /// <param name="recordedBytes"> Записываемые байты </param>
        /// <returns> Возвращает путь до файла при успешной записи или null, если не получилось записать в файл </returns>
        public static string WriteAllBytesWithOpenFileDialog(byte[] recordedBytes)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            if (fileDialog.ShowDialog() != true)
            {
                return null;
            }

            if (WriteALLBytes(fileDialog.FileName, recordedBytes) == true)
            {
                return fileDialog.FileName;
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// Считывает файл с абсолютным путём filePath
        /// </summary>
        /// <param name="filePath"> Путь к файлу </param>
        /// <returns> Возвращает байты файла или null, если по какой-либо причине не получилось считать файл. </returns>
        public static byte[] ReadALLBytes(string filePath)
        {
            try
            {
                FileInfo infoFile = new FileInfo(filePath);
                byte[] thisFile = new byte[infoFile.Length];

                using (BinaryReader strReader = new BinaryReader(File.Open(filePath, FileMode.Open), Encoding.UTF8))
                {
                    strReader.Read(thisFile, 0, thisFile.Length);
                }

                return thisFile;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Записывает байты recordedBytes в файл с абсолютным путём filePath.
        /// </summary>
        /// <param name="filePath"> Путь к файлу </param>
        /// <param name="recordedBytes"> Записываемые байты </param>
        /// <returns> Возвращает true при успешной записи или false, если не получилось записать в файл </returns>
        public static bool WriteALLBytes(string filePath, byte[] recordedBytes)
        {
            try
            {
                using (BinaryWriter strWr = new BinaryWriter(File.Open(filePath, FileMode.Create), Encoding.UTF8))
                {
                    strWr.Write(recordedBytes, 0, recordedBytes.Length);
                    strWr.Flush();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }       

    }
}
