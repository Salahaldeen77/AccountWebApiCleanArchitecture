using System.Globalization;

namespace AccountWeb.Data.CommonsLocalizationEntity
{
    public class GeneralLocalizableEntity
    {
        public string Localize(string textAr, string textEn)
        {
            CultureInfo CultureInfo = Thread.CurrentThread.CurrentCulture;
            if (CultureInfo.TwoLetterISOLanguageName.ToLower().Equals("ar"))
                return textAr;

            return textEn;
        }
    }
}
