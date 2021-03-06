/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 *  Enterprise Manager is distributed under the GNU General Public License version 3 and  
 *  is also available under alternative licenses negotiated directly with Simon Carter.  
 *  If you obtained Enterprise Manager under the GPL, then the GPL applies to all loadable 
 *  Enterprise Manager modules used on your system as well. The GPL (version 3) is 
 *  available at https://opensource.org/licenses/GPL-3.0
 *
 *  This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 *  without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 *  See the GNU General Public License for more details.
 *
 *  The Original Code was created by Simon Carter (s1cart3r@gmail.com)
 *
 *  Copyright (c) 2010 - 2019 Simon Carter.  All Rights Reserved.
 *
 *  Product:  Enterprise Manager
 *  
 *  File: AdminSettingsWrapper.cs
 *
 *  Purpose:  
 *
 *  Date        Name                Reason
 *  
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
using System;
using System.Windows.Forms;

using PointOfSale.Controls.Settings.Admin;

using Languages;
using SharedBase;
using SharedControls.Forms;
using POS.Base.Classes;

namespace PointOfSale.Classes
{
    internal class AdminSettingsWrapper
    {
        internal static bool LoadAdminSettings(Form parent)
        {
            bool Result = false;

            if (AppController.ActiveUser.MemberLevel < MemberLevel.AdminUpdateDelete)
                throw new Exception(LanguageStrings.AppNoPermissionsAdminSettings);

            if (parent != null)
            {
                parent.Cursor = Cursors.WaitCursor;
            }

            FormSettings adminSettings = new FormSettings(LanguageStrings.AppPointOfSaleAdministration, String.Empty);
            try
            {
                adminSettings.AddSettings += adminSettings_AddSettings;

                if (adminSettings.ShowDialog(parent) == DialogResult.OK)
                {
                    Result = true;
                    AppController.SaveSettings();
                }
            }
            finally
            {
                adminSettings.Dispose();
                adminSettings = null;

                if (parent != null)
                {
                    parent.Cursor = Cursors.Arrow;
                }
            }
            return (Result);
        }
      
        private static void adminSettings_AddSettings(object sender, Shared.SettingsLoadArgs e)
        {
            FormSettings settingsform = (FormSettings)sender;

            TreeNode parent = null;

            parent = settingsform.LoadControlOption(LanguageStrings.AppLoginLogout, 
                LanguageStrings.AppLoginLogoutSettings, 
                null, new LogInOut());

            settingsform.LoadControlOption(LanguageStrings.AppAutomaticLogin, 
                LanguageStrings.AppAutomaticLoginSettings, 
                parent, new AutoLogin());

            parent = settingsform.LoadControlOption(LanguageStrings.AppPluginSettings,
                LanguageStrings.AppPluginAdministrationSettings,
                null, new Controls.Settings.Admin.Plugins());

            settingsform.LoadControlOption(LanguageStrings.AppPluginStatusBar,
                LanguageStrings.AppPluginStatusBarSettings,
                parent, new Controls.Settings.Admin.PluginStatusBar());

            // are there any plugins which need to add items to the settings
            foreach (POS.Base.Plugins.BasePlugin pluginModule in PluginManager.PluginsGet())
            {
                pluginModule.LoadAdministrationSettings(settingsform);
            }

        }
    }
}
