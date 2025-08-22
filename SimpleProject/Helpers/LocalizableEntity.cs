using System.Globalization;

namespace SimpleProject.Helpers
{
    public class LocalizableEntity
    {
        public string Localize(string nameAr, string nameEn)
        {
            CultureInfo culture = Thread.CurrentThread.CurrentCulture;
            if (culture.TwoLetterISOLanguageName.ToLower().Equals("ar"))
            {
                return nameAr;
            }
            else if (culture.TwoLetterISOLanguageName.ToLower().Equals("en"))
            {
                return nameEn;
            }
            else
            {
                // Default to English if the culture is not recognized
                return nameEn;
            }
        }
    }
}
