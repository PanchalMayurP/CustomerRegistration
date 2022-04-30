namespace CustomerRegistration.Core.Utilities;
public static class CommonExtendedUilities
{
    /// <summary>
    /// Remdon numver generare
    /// </summary>
    /// <param name="ramdom"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int RendomNumber(this Random ramdom, int min, int max)
                => ramdom.Next(min, max);


    /// <summary>
    /// Unique number generator 
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string GetUniqueFileName(this string fileName, int length = 4)
    {
        return fileName
                  + "_" + Guid.NewGuid().ToString().Substring(0, length);
    }

    public static bool IsNumeric(this string text) => double.TryParse(text, out _);
    public static bool IsLetter(this string text) => text.All(Char.IsLetter);
    public static bool IsLatterWithSpecialChar(this string text) => text.All(c => Char.IsLetter(c) || c == '_' || c == '/' || c == '+' || c == '@' || c == ',' || c == '-');
    public static DateTime NumberToDate(this string date) => DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None);
    public static string UniqueFileName(this int patientId, string ext) => string.Join("_", DateTime.Now.ToString(string.Format("{0:yyyyMMdd_HHmmssfff}", DateTime.Now)), patientId) + ext;
    public static string UniqueFileName(this long patientId, string ext) => string.Join("_", DateTime.Now.ToString(string.Format("{0:yyyyMMdd_HHmmssfff}", DateTime.Now)), patientId) + ext;

    public static void FileUpload(this IFormFile FormFile, string dirPath, string filename)
    {
        //if (!Directory.Exists(@"C:/test/" + foldername)
        //            Directory.CreateDirectory(@"C:/test/" + foldername);
        string path = Path.Combine(Directory.GetCurrentDirectory(), dirPath, filename);
        using Stream stream = new FileStream(path, FileMode.Create);
        FormFile.CopyTo(stream);
    }

    public static string AppendTimeStamp(this string fileName, string appandName = "")
    {
        string strName = Path.GetFileNameWithoutExtension(fileName) + "-";
        strName = appandName != "" ? strName + "_" + appandName : strName;

        return string.Concat(
            //Path.GetFileNameWithoutExtension(fileName),
            strName,
            DateTime.Now.ToString(string.Format("{0:yyyy-MM-dd_HH-mm-ss-fff}", DateTime.Now)),
            Path.GetExtension(fileName)
            );
    }

    /// <summary>
    /// Base64 to image
    /// </summary>
    /// <param name="base64String"></param>
    /// <returns></returns>
    public static Image Base64ToImage(this string base64String)
    {
        byte[] imageBytes = Convert.FromBase64String(base64String);
        MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
        ms.Write(imageBytes, 0, imageBytes.Length);
        Image image = System.Drawing.Image.FromStream(ms, true);
        return image;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static string ImageToBase64(this string path)
    {
        // string path = "D:\\SampleImage.jpg";
        string base64String = string.Empty;
        using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
        {
            using (MemoryStream m = new MemoryStream())
            {
                image.Save(m, image.RawFormat);
                byte[] imageBytes = m.ToArray();
                base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
    }
}
