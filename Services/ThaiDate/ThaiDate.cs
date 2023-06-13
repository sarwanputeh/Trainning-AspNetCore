using System.Globalization;

namespace OrixNetCoreApp.Services.ThaiDate
{
    public class ThaiDate : IThaiDate
    {
        public string ShowThaiDate()
        {
            return DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("th-TH"));
        }
    }
}
