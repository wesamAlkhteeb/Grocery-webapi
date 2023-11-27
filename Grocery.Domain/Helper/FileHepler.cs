
using Microsoft.AspNetCore.Http;

namespace Helper;

public class FileHelper
{
    public static async Task<bool> DeleteImage(string path)
    {

        path = Directory.GetCurrentDirectory() + path;
        //Console.WriteLine(path);
        if (File.Exists(path))
        {
            await Task.Run(()=>File.Delete(path));
            return true;
        }
        else
        {
            return false;
        }
    }

    public static async Task UpdateFile(string path, IFormFile file)
    {
        if (! await DeleteImage(path))
        {
            throw new Exception("not found any file");
        }
        string name = "\\" + path.Split("\\").Last();
        await saveImageInDeterminePath(file, name);
    }
    
    public static async Task<String> saveFile( IFormFile file)
    {
        string name = GenerateName(file);
        return await saveImageInDeterminePath(file, name);
    }

    private static async Task<string> saveImageInDeterminePath(IFormFile file, string pathFile)
    {
        if (file.Length >= 0)
        {
            try
            {
                string pathFolder = "Uploads";
                if (!Directory.Exists(pathFolder))
                {
                    Directory.CreateDirectory(pathFolder);
                }
                string path = Path.Combine(pathFolder , pathFile);
                
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return "/Uploads/" + pathFile;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);                    
            }
        }
        return string.Empty;
    }

    public static string GenerateName(IFormFile file)
    {
        string[] np = file.FileName.Split(".");
        return getNameFile(np[0]) + DateTime.Now.GetHashCode() + new Random().Next(100) + "." + np.Last();
    }

    private static string getNameFile(string namefile)
    {
        string name = string.Empty;
        string[] a = namefile.Split(" ");
        foreach (var b in a)
        {
            name += b;
        }
        return name;
    }
}
