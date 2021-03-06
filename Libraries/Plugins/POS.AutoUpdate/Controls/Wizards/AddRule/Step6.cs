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
 *  File: Step6.cs
 *
 *  Purpose:  
 *
 *  Date        Name                Reason
 *  
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Languages;
using SharedBase.BOL.DatabaseUpdates;
using POS.AutoUpdate.Classes;

namespace POS.AutoUpdate.Controls.Wizards.AddRule
{
    public partial class Step6 : SharedControls.WizardBase.BaseWizardPage
    {
        #region Private Members

        private CreateAutoRuleSettings _settings;

        #endregion Private Members

        #region Constructors

        public Step6(CreateAutoRuleSettings settings)
        {
            _settings = settings;
            InitializeComponent();

            dtpRunTime.Value = DateTime.Now.AddDays(1);
            dtpRunTime.MinDate = DateTime.Now.AddDays(-1);
        }

        #endregion Constructors

        #region Overridden Methods

        public override void LanguageChanged(System.Globalization.CultureInfo culture)
        {
            lblDescription.Text = LanguageStrings.AppAutoRuleCreateRule;
            lblName.Text = LanguageStrings.AppName;
            lblRunDate.Text = LanguageStrings.AppAutoRuleRunDate;

            dtpRunTime.CustomFormat = culture.DateTimeFormat.FullDateTimePattern;
        }

        public override bool NextClicked()
        {
            return (base.NextClicked());
        }

        public override void PageShown()
        {

        }

        public override bool BeforeFinish()
        {
            try
            {
                if (String.IsNullOrEmpty(txtName.Text))
                    throw new Exception(LanguageStrings.AppErrorAutoUpdateNameRequired);

                _settings.AutoUpdate = new SharedBase.BOL.DatabaseUpdates.AutoUpdate(-1,
                    txtName.Text, _settings.Rule.SQL, dtpRunTime.Value, false,
                    POS.Base.Classes.AppController.ActiveUser, DateTime.Now);

                AutoUpdates.Create(_settings.AutoUpdate);

                return (base.BeforeFinish());
            }
            catch (Exception err)
            {
                ShowError(LanguageStrings.AppError, String.Format(LanguageStrings.AppAutoRuleCreateError, err.Message));
                return (false);
            }
        }

        #endregion Overridden Methods

        #region Private Methods


        #endregion Private Methods
    }
}
