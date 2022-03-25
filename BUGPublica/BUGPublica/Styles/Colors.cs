using Xamarin.Forms;

namespace BUGPublica.Styles
{
    public static class Colors
    {
        // Common
        public static readonly Color PrimaryColor = Color.FromHex("#4CAF50");
        public static readonly Color PrimaryDarkColor = Color.FromHex("#388E3C");
        public static readonly Color CommonBlackColor = Color.FromHex("#000000");
        public static readonly Color CommonWhiteColor = Color.FromHex("#FFFFFF");
        public static readonly Color CommonGrayLightColor = Color.FromHex("#C1C2C3");
        public static readonly Color CommonGrayColor = Color.FromHex("#232323");
        public static readonly Color CommonGreenColor = Color.FromHex("#86DC9D");
        public static readonly Color OKColor = Color.Green;
        public static readonly Color PrecautionColor = Color.FromHex("#FD9726");
        public static readonly Color DangerColor = Color.FromHex("#B00020");
        public static readonly Color DisabledColor = Color.LightGray;
        public static readonly Color DividerColor = Color.FromHex("#B3B3B3");
        public static readonly Color EntryBorderColor = Color.Gray;
        public static readonly Color EntryBackgroundColor = Color.White;
        public static readonly Color EntryError = Color.FromHex("#FF0000");
        public static readonly Color IconTappedBackColor = Color.FromHex("#33FFFFFF");
        public static readonly Color ButtonTextColor = Color.White;
        public static readonly Color IconColor = Color.White;
        public static readonly Color NavBarColor = Color.FromHex("#FFFFFF");
        public static readonly Color ToolbarColor = Color.FromHex("#121212");

        // Backgrounds
        public static readonly Color PrimaryLightBackground = Color.FromHex("#F2F2F2");
        public static readonly Color SecondaryLightBackground = Color.FromHex("#FFFFFF");
        public static readonly Color CellLightBackground = Color.FromHex("#FFFFFF");
        public static readonly Color PopupLightBackground = Color.FromRgba(254, 254, 254, 0.7);
        public static readonly Color PrimaryDarkBackground = Color.FromHex("#212121");
        public static readonly Color SecondaryDarkBackground = Color.FromHex("#535353");
        public static readonly Color CellDarkBackground = Color.FromHex("#121212");
        public static readonly Color PopupDarkBackground = Color.FromRgba(18, 18, 18, 0.7);

        // Texts
        public static readonly Color TextDarkPrimary = new Color(CommonBlackColor.R, CommonBlackColor.G, CommonBlackColor.B, 1);
        public static readonly Color TextDarkSecondary = new Color(CommonBlackColor.R, CommonBlackColor.G, CommonBlackColor.B, 0.87);
        public static readonly Color TextDarkHint = new Color(CommonBlackColor.R, CommonBlackColor.G, CommonBlackColor.B, 0.6);
        public static readonly Color TextDarkDisabled = new Color(CommonBlackColor.R, CommonBlackColor.G, CommonBlackColor.B, 0.38);
        public static readonly Color TextLightPrimary = new Color(CommonWhiteColor.R, CommonWhiteColor.G, CommonWhiteColor.B, 1);
        public static readonly Color TextLightSecondary = new Color(CommonWhiteColor.R, CommonWhiteColor.G, CommonWhiteColor.B, 0.87);
        public static readonly Color TextLightHint = new Color(CommonWhiteColor.R, CommonWhiteColor.G, CommonWhiteColor.B, 0.6);
        public static readonly Color TextLightDisabled = new Color(CommonWhiteColor.R, CommonWhiteColor.G, CommonWhiteColor.B, 0.38);
    }
}
