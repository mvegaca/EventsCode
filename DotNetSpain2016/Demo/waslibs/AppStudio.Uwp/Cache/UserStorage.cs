using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;
using System.Linq;

namespace AppStudio.Uwp.Cache
{
    public static class UserStorage
    {
        public static async Task<string> ReadTextFromFile(string fileName)
        {
            try
            {
                var folder = ApplicationData.Current.LocalFolder;
                var file = await folder.GetFileAsync(fileName);
                if (file != null)
                {
                    return await FileIO.ReadTextAsync(file);
                }
            }
            catch (FileNotFoundException)
            {
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return String.Empty;
        }

        public static async Task<List<string>> GetMatchingFilesByPrefixAsync(string prefix, List<string> excludeFiles)
        {
            try
            {
                List<string> result = new List<string>();
                var folder = ApplicationData.Current.LocalFolder;
                QueryOptions queryOptions = new QueryOptions(CommonFileQuery.DefaultQuery, new List<string>() { "*" });
                queryOptions.UserSearchFilter = $"{prefix}*.*";
                queryOptions.FolderDepth = FolderDepth.Shallow;
                queryOptions.IndexerOption = IndexerOption.UseIndexerWhenAvailable;
                StorageFileQueryResult queryResult = folder.CreateFileQueryWithOptions(queryOptions);

                IReadOnlyList<StorageFile> matchingFiles = await queryResult.GetFilesAsync();

                if (matchingFiles.Count > 0) {
                    result.AddRange(
                        matchingFiles.Where(f => !excludeFiles.Contains(f.Name)).Select(f => f.Name)
                    );
                }
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return null;
        }

        public static async Task WriteText(string fileName, string content)
        {
            try
            {
                var folder = ApplicationData.Current.LocalFolder;
                var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(file, content);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public static async Task DeleteFileIfExists(string fileName)
        {
            try
            {
                var folder = ApplicationData.Current.LocalFolder;
                var file = await folder.GetFileAsync(fileName);
                if (file != null)
                {
                    await file.DeleteAsync(StorageDeleteOption.PermanentDelete);
                }
            }
            catch (FileNotFoundException)
            {
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public static async Task AppendLineToFile(string fileName, string line)
        {
            try
            {
                var folder = ApplicationData.Current.LocalFolder;
                var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
                await FileIO.AppendLinesAsync(file, new List<string>() { line });
            }
            catch { /* Avoid any exception at this point. */ }
        }
    }
}
