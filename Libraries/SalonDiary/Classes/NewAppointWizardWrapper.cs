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
 *  File: NewAppointWizardWrapper.cs
 *
 *  Purpose:  
 *
 *  Date        Name                Reason
 *  
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
using System;
using System.Collections.Generic;
using System.Text;

using SalonDiary.Controls;
using Languages;
using SharedBase.BOL.Therapists;
using SharedBase.BOL.Appointments;
using SharedBase.BOL.Users;
using SharedControls.WizardBase;

namespace SalonDiary.Classes
{
    public static class NewAppointWizardWrapper
    {
        public static void ShowNewAppointmentWizard(Controls.SalonDiary salonDiary)
        {
            NewAppointmentOptionsDiary options = new NewAppointmentOptionsDiary();
            options.Diary = salonDiary;

            WizardForm.ShowWizard(LanguageStrings.AppSalonNewAppointmentWizard,
                new Controls.Wizards.NewAppointmentWizard.Step1(options),
                new Controls.Wizards.NewAppointmentWizard.Step2(options),
                new Controls.Wizards.NewAppointmentWizard.Step3(options));
        }
    }

    public sealed class NewAppointmentOptionsDiary : NewAppointmentOptions
    {
        #region Constructors

        public NewAppointmentOptionsDiary()
        {
        }

        #endregion Constructors

        #region Properties

        public Controls.SalonDiary Diary { get; set; }

        #endregion Properties
    }
}
