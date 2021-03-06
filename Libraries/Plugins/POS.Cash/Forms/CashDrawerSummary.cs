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
 *  File: CashDrawerSummary.cs
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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Languages;
using SharedBase;
using SharedBase.BOL.Appointments;
using POS.Base.Classes;

namespace POS.CashManager.Forms
{
    public partial class CashDrawerSummary : POS.Base.Forms.BaseForm
    {
        #region Private Members

        private CashDrawerType _cashDrawerType = CashDrawerType.Till;

        #endregion Private Members

        #region Constructors

        public CashDrawerSummary(CashDrawerType drawerType, string title)
        {
            InitializeComponent();

            _cashDrawerType = drawerType;
            Text = title;

            dtpDate.Value = DateTime.Now.Date;
            LoadStores();

            dtpDate.MaxDate = DateTime.Now.Date;
            dtpDate.ValueChanged += dtpDate_ValueChanged;
        }

        #endregion Constructors

        #region Static Methods

        public static void Show(Form parent, CashDrawerType drawerType, string title)
        {
            CashDrawerSummary form = new CashDrawerSummary(drawerType, title);
            try
            {
                form.ShowDialog(parent);
            }
            finally
            {
                form.Dispose();
                form = null;
            }
        }

        #endregion Static Methods

        #region Overridden Methods
        
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            switch (_cashDrawerType)
            {
                case CashDrawerType.PettyCash:
                    HelpTopic = POS.Base.Classes.HelpTopics.CashManagerPettyCashVerify;
                    break;
                case CashDrawerType.Safe:
                    HelpTopic = POS.Base.Classes.HelpTopics.CashManagerSafeVerify;
                    break;
                case CashDrawerType.Till:
                    HelpTopic = POS.Base.Classes.HelpTopics.CashManagerTillVerify;
                    break;
            }
        }

        protected override void LanguageChanged(System.Globalization.CultureInfo culture)
        {
            btnOK.Text = LanguageStrings.AppOK;
            lblLocation.Text = LanguageStrings.AppLocation;
            chDescription.Text = LanguageStrings.AppDescription;
            chValue.Text = LanguageStrings.AppValue;
        }

        #endregion Overridden Methods

        #region Private Methods

        private void LoadStores()
        {
            cmbStoreLocation.Items.Clear();
            AppointmentGroups groups = AppointmentGroups.Get();

            foreach (AppointmentGroup group in groups)
            {
                if (group.Description != LanguageStrings.AppAllUsers)
                {
                    int idx = cmbStoreLocation.Items.Add(group);

                    if (group.ID == SharedBase.DAL.DALHelper.StoreID)
                        cmbStoreLocation.SelectedIndex = idx;
                }
            }
        }

        private void cmbStoreLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCashDrawData();
        }

        private void LoadCashDrawData()
        {
            AppointmentGroup grp = (AppointmentGroup)cmbStoreLocation.SelectedItem;
            lvCashDrawSummary.Items.Clear();
            string[] lines = SharedBase.BOL.CashDrawer.CashDrawers.CurrentStatus(grp.ID,
                AppController.LocalCountry, AppController.LocalCurrency,  
                dtpDate.Value, _cashDrawerType).Split(StringConstants.SYMBOL_HASH_CHAR);

            foreach (string line in lines)
            {
                if (!String.IsNullOrEmpty(line))
                {
                    string[] data = line.Split(StringConstants.SYMBOL_SQUIGGLE_CHAR);
                    ListViewItem item = new ListViewItem(data[0].Trim());
                    item.SubItems.Add(data[1].Trim());
                    lvCashDrawSummary.Items.Add(item);
                }
            }

            lvCashDrawSummary.Focus();
            lvCashDrawSummary.Items[0].Selected = true;
        }

        private void cmbStoreLocation_Format(object sender, ListControlConvertEventArgs e)
        {
            AppointmentGroup grp = (AppointmentGroup)e.ListItem;
            e.Value = grp.Description;
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            LoadCashDrawData();
        }

        #endregion Private Methods
    }
}
