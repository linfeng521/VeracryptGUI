using System.Configuration;

namespace VeracryptGui.Utils
{
    public class ConfigUtil
    {
        public static class SettingsManager
        {

            // 通过在应用程序配置文件中添加设置节来指定参数和默认值
            public static string MySetting1
            {
                get { return ConfigurationManager.AppSettings["MySetting1"]; }
                set
                {
                    SetConfig("MySetting1", value);
                }
            }

            public static int MySetting2
            {
                get
                {
                    var value = ConfigurationManager.AppSettings["MySetting2"];
                    return string.IsNullOrEmpty(value) ? -1 : int.Parse(value);
                }
                set
                {
                    SetConfig("MySetting2", value.ToString());
                }
            }

            public static bool MySetting3
            {
                get
                {
                    var value = ConfigurationManager.AppSettings["MySetting3"];
                    return string.IsNullOrEmpty(value) ? false : bool.Parse(value);
                }
                set
                {
                    SetConfig("MySetting3", value.ToString());
                }
            }

            // 返回应用程序的配置对象
            private static Configuration GetConfig()
            {
                return ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }

            // 因为当前config为只读属性，需要使用此方法来写入
            private static void SetConfig(string key, string value)
            {
                var config = GetConfig();
                config.AppSettings.Settings[key].Value = value;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }
    }
}