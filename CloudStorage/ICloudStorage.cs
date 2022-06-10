using System.Drawing;
using System.Drawing.Imaging;


namespace WorkingWithGoogleCloudStorage.CloudStorage
{
    internal interface ICloudStorage
    {
        Task<string> UploadFileAsync(string localPath, string? objectName);
        Task DeleteFileAsync(string objectName);
    }
}
