using System;
using System.IO;
using Ionic.Zip;

namespace ManagerApplication.Helper
{
    public static class FileArchiver
    {
        public static void CreateZipArchive(string inputFilePath, string password, bool deleteOriginalFile = false, string outputFilePath = null)
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(inputFilePath, ""); // Добавляем файл в архив (пустой путь означает корневую директорию)

                zip.Password = password;
                foreach (ZipEntry entry in zip)
                {
                    entry.Password = password;
                }

                if (outputFilePath == null)
                {
                    // Если не указан путь для сохранения, используем тот же путь, где находится входной файл
                    outputFilePath = Path.Combine(Path.GetDirectoryName(inputFilePath), Path.GetFileNameWithoutExtension(inputFilePath) + ".zip");
                }

                zip.Save(outputFilePath); // Сохраняем архив по указанному пути

                if (deleteOriginalFile)
                {
                    File.Delete(inputFilePath); // Удаляем исходный файл, если указано
                }
            }
        }

    }
}
