﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using SalonDiary.Classes;
using Languages;
using Library.BOL.Appointments;
using Library.BOL.Treatments;
using Library.BOL.Users;
using SharedControls.WizardBase;

namespace SalonDiary.Controls.Wizards.WaitingListWizard
{
    public partial class Step2 : BaseWizardPage
    {
        #region Private Members

        private WaitingListWizardOptions _options;

        private User _selectedUser;

        #endregion Private Members

        #region Constructors

        public Step2()
        {
            InitializeComponent();
        }
        
        public Step2(WaitingListWizardOptions options)
            : this()
        {
            _options = options;
        }

        #endregion Constructors

        #region Overridden Methods

        public override void LanguageChanged(System.Globalization.CultureInfo culture)
        {

        }

        public override bool NextClicked()
        {
            if (_selectedUser == null)
            {
                ShowError(LanguageStrings.AppSelectCustomer, LanguageStrings.AppSelectCustomerDescription);
                return (false);
            }

            if (clbTreatments.CheckedItems.Count == 0)
            {
                ShowError(LanguageStrings.AppDiaryTreatment, LanguageStrings.AppDiaryTreatmentSelect);
                return (false);
            }

            _options.WaitingListItem.Customer = _selectedUser;
            _options.WaitingListItem.Treatments.Clear();

            foreach (AppointmentTreatment item in clbTreatments.CheckedItems)
            {
                _options.WaitingListItem.Treatments.Add(item);
            }

            return base.NextClicked();
        }

        public override void PageShown()
        {
            if (_options.WaitingListItem.UserID == -1)
            {
                _selectedUser = null;
                txtUserName.Text = String.Empty;
            }
            else
            {
                _selectedUser = _options.WaitingListItem.Customer;
                txtUserName.Text = _selectedUser.UserName;
            }

            LoadTreatments();

        }

        public override bool BeforeFinish()
        {
            return (true);
        }

        #endregion Overridden Methods

        #region Private Methods

        private void LoadTreatments()
        {
            clbTreatments.Items.Clear();
            int newWidth = clbTreatments.ColumnWidth;

            foreach (AppointmentTreatment treat in AppointmentTreatments.Get())
            {
                int idx = clbTreatments.Items.Add(treat);
                Size textSize = Shared.Utilities.MeasureText(treat.Name, clbTreatments.Font);

                if (textSize.Width > newWidth)
                    newWidth = textSize.Width + 5;

                clbTreatments.SetItemChecked(idx, _options.WaitingListItem.Treatments.Contains(treat));    
            }

            clbTreatments.ColumnWidth = newWidth;
        }

        private void clbTreatments_Format(object sender, ListControlConvertEventArgs e)
        {
            AppointmentTreatment treat = (AppointmentTreatment)e.ListItem;
            e.Value = treat.Name;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            _selectedUser = _options.Diary.RaiseSelectUser();

            if (_selectedUser != null)
                txtUserName.Text = _selectedUser.UserName;
        }

        #endregion Private Methods
    }
}