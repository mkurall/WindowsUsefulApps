using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BtkTodo.Utils
{
    public static class SettingsManager
    {
        public static Color HeaderColor { get; set; }
        public static bool AutoHide { get; set; }
        public static bool RunAtStartup { get; set; }

        public static void Refresh()
        {
            ReadSettings();
        }

        public static void Save()
        {
            WriteSettings();
        }

        private static void ReadSettings()
        {
            RegistryKey appKey = Registry.CurrentUser.OpenSubKey("Software\\BtkTodoApp");

            object keyValueHeaderColor = appKey.GetValue("HeaderColor");
            object keyValueAutoHide = appKey.GetValue("AutoHide");

            if (keyValueHeaderColor != null)
            {
                HeaderColor = Color.FromArgb((int)keyValueHeaderColor);
            }
            else
            {
                HeaderColor = Color.Tomato;
            }

            if (keyValueAutoHide != null)
            {
                AutoHide = Convert.ToBoolean(keyValueAutoHide);
            }
            else
            {
                AutoHide = false;
            }

            RegistryKey regRunAtStartup = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            object keyValueRunAtStartup = regRunAtStartup.GetValue("BtkTodoApp");

            if (keyValueRunAtStartup != null && keyValueRunAtStartup.Equals(Application.ExecutablePath))
            {
                RunAtStartup = true;
            }
            else
            {
                RunAtStartup = false;
            }

            regRunAtStartup.Close();

            appKey.Close();

            
        }

        private static void WriteSettings()
        {
            RegistryKey regSoftware = Registry.CurrentUser.OpenSubKey("Software", true);

            RegistryKey regApp = regSoftware.CreateSubKey("BtkTodoApp");

            regApp.SetValue("HeaderColor", HeaderColor.ToArgb(), RegistryValueKind.DWord);
            regApp.SetValue("AutoHide", AutoHide, RegistryValueKind.DWord);
            
            regApp.Close();

            RegistryKey regRunAtStartup = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (RunAtStartup)
            {
                regRunAtStartup.SetValue("BtkTodoApp", Application.ExecutablePath);
            }
            else
            {
                if (regRunAtStartup.GetValue("BtkTodoApp") != null)
                    regRunAtStartup.DeleteValue("BtkTodoApp");
            }

            regRunAtStartup.Close();
            regSoftware.Close();
        }

        static SettingsManager()
        {
            Refresh();
        }
    }
}
