namespace LibraryAPI.Interfaces
{
    public interface IStartupConfig
    {
        string ConnectionString { get; set; }
        string PhotosPath { get; set; }     
        string CopysPath { get; set; }
        string DefaultImagePath { get; set; }
    }
}
