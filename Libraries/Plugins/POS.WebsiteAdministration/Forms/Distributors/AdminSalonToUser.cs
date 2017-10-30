﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Languages;

using Library;
using Library.Utils;
using Library.BOL.Users;
using Library.BOL.Salons;

using POS.Base.Classes;

namespace POS.WebsiteAdministration.Forms.Salons
{
    public partial class AdminSalonToUser : POS.Base.Forms.BaseForm
    {
        #region Constructors

        public AdminSalonToUser()
        {
            InitializeComponent();
            BuildSalonUserList();
            LoadSalons();
            LoadUsers();
        }

        #endregion Constructors

        #region Overridden Methods
        
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            HelpTopic = POS.Base.Classes.HelpTopics.WebSalonToSalonOwner;
        }

        protected override void LanguageChanged(System.Globalization.CultureInfo culture)
        {
            this.Text = LanguageStrings.AppSalonToSalonOwners;

            btnCreate.Text = LanguageStrings.AppMenuButtonCreate;

            lblSalons.Text = LanguageStrings.AppSalons;
            lblUsers.Text = LanguageStrings.AppUsers;

            cbUnassigned.Text = LanguageStrings.AppSalonUnassignedSalons;
        }

        protected override void SetPermissions()
        {
            btnCreate.Enabled = AppController.ActiveUser.MemberLevel > MemberLevel.AdminReadOnly;   
        }

        #endregion Overridde Methods

        #region Private Methods

        private void BuildSalonUserList()
        {
			//clear table and create new header row
			pnlSalonUsers.Controls.Clear();

            Users users = AppController.Administration.SalonOwnersGet(1, 250);

			foreach (User user in users)
			{
                foreach (Salon salon in user.Salons)
                {
                    Controls.SalonUser salonUser = new Controls.SalonUser(AppController.ActiveUser, salon, user);
                    salonUser.OnDelete += new EventHandler(salonUser_OnDelete);
                    pnlSalonUsers.Controls.Add(salonUser);
                }
			}
        }

        private void salonUser_OnDelete(object sender, EventArgs e)
        {
            BuildSalonUserList();
            LoadSalons();
            LoadUsers();
        }

        private void LoadUsers()
        {
            //clear table and create new header row
            cmbUsers.Items.Clear();
            Users users = AppController.Administration.UsersGet(MemberLevel.Distributor);

            foreach (User user in users)
            {
                cmbUsers.Items.Add(user);
            }
        }

        private void LoadSalons()
        {
            //clear table and create new header row
            cmbSalons.Items.Clear();

            Library.BOL.Salons.Salons salons = null;

            if (cbUnassigned.Checked)
                salons = AppController.Administration.SalonsUnassigned();
            else
                salons = AppController.Administration.SalonsGet();

            foreach (Salon salon in salons)
            {
                cmbSalons.Items.Add(salon);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Library.BOL.Salons.Salon salon = (Library.BOL.Salons.Salon)cmbSalons.SelectedItem;
            User user = (User)cmbUsers.SelectedItem;

            if (user != null & salon != null)
            {
                AppController.Administration.SalonOwnerCreate(user, salon);
                BuildSalonUserList();
                LoadSalons();
                LoadUsers();
            }
        }

        private void cmbSalons_Format(object sender, ListControlConvertEventArgs e)
        {
            Library.BOL.Salons.Salon salon = (Library.BOL.Salons.Salon)e.ListItem;
            e.Value = salon.Name;
        }

        private void cmbUsers_Format(object sender, ListControlConvertEventArgs e)
        {
            User user = (User)e.ListItem;
            e.Value = user.UserName;
        }

        private void cbUnassigned_CheckedChanged(object sender, EventArgs e)
        {
            LoadSalons();
        }

        #endregion Private Methods
    }
}
