using My_Final_Project.FileManagers;

namespace My_Final_Project.FileManager
{
    public interface IFileManager
    {
        Task<FileResponseModel> UploadFileToSystem(IFormFile formFile);
        Task<FilesResponseModel> ListOfFilesToSystem(IList<IFormFile> formFiles);
    }
}
