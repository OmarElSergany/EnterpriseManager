﻿using System;
using System.Collections.Generic;
using System.Text;

using Shared.Classes;
using Languages;
using Library.BOL.Appointments;
using Library.BOL.Therapists;

namespace SalonDiary.Classes
{
    /// <summary>
    /// Looks through the waiting list and discovers wether an item on the waiting list an fit, runs every hour
    /// 
    /// or, if an appointment is cancelled, looks through the waiting list and see's if something can fit in it's place
    /// </summary>
    internal class WaitingListThread : ThreadManager
    {
        #region Private Members

        private Appointment _cancelledAppointment = null;

        private WaitingLists _availableToFill;

        #endregion Private Members

        #region Constructor

        internal WaitingListThread(Appointment cancelledAppointment)
            : base(null, new TimeSpan(0, 0, 0))
        {
            HangTimeout = 3;
            _cancelledAppointment = cancelledAppointment;
        }

        internal WaitingListThread()
            : base (null, new TimeSpan(1, 0, 0))
        {
            HangTimeout = 80;
        }

        #endregion Constructor

        #region Overridden Methods

        protected override bool Run(object parameters)
        {
            WaitingLists allLists = WaitingLists.All();

            if (_cancelledAppointment != null)
            {
                _availableToFill = new WaitingLists();

                Therapist therapist = Therapist.Get(_cancelledAppointment.EmployeeID);

                foreach (WaitingList list in allLists)
                {
                    // is the staff member allowed
                    if ((list.StaffID == -1 || list.StaffID == therapist.EmployeeID) &&
                        (therapist.CompareTreatments(list.Treatments)))
                    {
                        // this therapist can do the treatments required
                        if (list.Treatments.TotalTreatmentTime() <= _cancelledAppointment.TotalTime())
                        {
                            // it can fit the appointment schedule
                            _availableToFill.Add(list);
                            continue;
                        }
                    }
                }
            }

            return base.Run(_cancelledAppointment == null);
        }

        #endregion Overridden Methods

        #region Properties

        /// <summary>
        /// Returns a list of waiting lists, if any, that can fill a cancelled appointment slot
        /// </summary>
        public WaitingLists AvailableList
        {
            get
            {
                return (_availableToFill);
            }
        }

        #endregion Properties
    }
}
