using System.Configuration;
using System.Drawing;

namespace Frontend.Web.UICustomization
{
    public static class UICustomization
    {
        private static readonly string DefaultBackgroundColor = ConfigurationManager.AppSettings["DefaultBackgroundColor"];
        private static readonly string DefaultContrastColor   = ConfigurationManager.AppSettings["DefaultContrastColor"];
        private static readonly string DefaultBaseColor       = ConfigurationManager.AppSettings["DefaultBaseColor"];
        private static          string CurrentThemeDefaultFont       => CurrentThemeDefaultFontSize + " " + CurrentThemeDefaultFontFamily;
        private static          string CurrentThemeDefaultFontFamily => "'Segoe UI','Helvetica Neue','Droid Sans',Arial,Tahoma,Geneva,Sans-serif";
        private static          string CurrentThemeDefaultFontSize   => "14px";

        public static string GetBackgroundColor()
        {
            return DefaultBackgroundColor;
        }

        public static string GetContrastColor()
        {
            if (bool.TryParse(ConfigurationManager.AppSettings["CalculateContrastColor"], out var calculated) && !calculated)
                return DefaultContrastColor;

            var baseColor     = ColorTranslator.FromHtml(DefaultBaseColor);
            var contrastColor = Color.FromArgb(baseColor.ToArgb() ^ 0xffffff);
            return ColorTranslator.ToHtml(contrastColor);
        }

        public static string GetBaseColor()
        {
            return DefaultBaseColor;
        }

        public static string GetFont()
        {
            return CurrentThemeDefaultFont;
        }
    }
}