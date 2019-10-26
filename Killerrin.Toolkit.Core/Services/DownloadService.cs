using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Killerrin.Toolkit.Core.Services
{
    public class DownloadService
    {
        public Uri FolderPathToSave { get; }
        public bool CreateFolderIfNotExists { get; }

        public DownloadService() : this(null, false) { }
        public DownloadService(Uri folderPathToSave, bool createFolderIfNotExists)
        {
            FolderPathToSave = folderPathToSave;
            CreateFolderIfNotExists = createFolderIfNotExists;

            if (CreateFolderIfNotExists)
            {
                CreateFolder();
            }
        }

        /// <summary>
        /// Creates a folder if it does not exist
        /// </summary>
        public void CreateFolder()
        {
            if (!Directory.Exists(FolderPathToSave.OriginalString))
            {
                Directory.CreateDirectory(FolderPathToSave.OriginalString);
            }
        }

        /// <summary>
        /// Downloads the given resource and saves it to the Folder Path in the Download Service
        /// </summary>
        /// <param name="resourceUrl">The Resource URL</param>
        /// <returns>Whether the download completed sucessfully</returns>
        public async Task<bool> DownloadFileAsync(Uri resourceUrl)
        {
            return await DownloadFileAsync(FolderPathToSave, resourceUrl);
        }

        /// <summary>
        /// Downloads the given resource and saves it to the given folder path
        /// </summary>
        /// <param name="folderPathToSave">The path to save the resouce to</param>
        /// <param name="resourceUrl">The Resource URL</param>
        /// <returns>Whether the download completed sucessfully</returns>
        public static async Task<bool> DownloadFileAsync(Uri folderPathToSave, Uri resourceUrl)
        {
            //if (!resourceUrl.IsFile) return false;

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(resourceUrl);
                if (!response.IsSuccessStatusCode) return false;

                var responseBytes = await response.Content.ReadAsByteArrayAsync();
                if (responseBytes.Length <= 0) return false;

                // Get the File Path to save it to
                var fileName = Path.GetFileName(resourceUrl.LocalPath);
                var filePath = Path.Combine(folderPathToSave.OriginalString, fileName);

                // Save the File
                File.WriteAllBytes(filePath, responseBytes);
            }

            return true;
        }
    }
}
