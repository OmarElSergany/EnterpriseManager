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
 *  Copyright (c) 2010 - 2018 Simon Carter.  All Rights Reserved.
 *
 *  Product:  Enterprise Manager
 *  
 *  File: Suppliers.cs
 *
 *  Purpose:  
 *
 *  Date        Name                Reason
 *  
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
using System;
using System.Collections;

using SharedBase.BOL.Countries;

namespace SharedBase.BOL.Suppliers
{
	/// <summary>
	/// Collection of Supplier items
	/// 
	/// Automatically generated by FBSPGen (http://www.sieradelta.com/Products/FBSPGen.aspx)
	/// </summary>
	public class Suppliers : CollectionBase
	{
		#region Static Methods

		/// <summary>
		/// Creates a new instance of Supplier
		/// </summary>
		/// <returns>Supplier instance</returns>
		public static Supplier Create(string businessName, string addresslineOne, 
            string addresslineTwo, string addresslineThree, string city, string county, 
            string postcode, Country country, string website, SupplierStatus status, int reliability, 
            double averageTurnaround)
		{
			return (DAL.FirebirdDB.SupplierInsert(businessName, addresslineOne, addresslineTwo, 
                addresslineThree, city, county, postcode, country, website, status, reliability,
                averageTurnaround));
		}

		/// <summary>
		/// Returns all records from table SUPPLIERS
		/// </summary>
		/// <returns>Suppliers collection of Supplier items</returns>
		public static Suppliers All()
		{
			return (DAL.FirebirdDB.SupplierSelectAll());
		}

		/// <summary>
		/// Returns a specific record from table SUPPLIERS
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Supplier item if found, otherwise null</returns>
		public static Supplier Get(Int64 item)
		{
			return (DAL.FirebirdDB.SupplierSelect(item));
		}

		#endregion Static Methods

		#region Generic CollectionBase Code

		#region Properties

		/// <summary>
		/// Indexer Property
		/// </summary>
		/// <param name="Index">Index of object to return</param>
		/// <returns>Supplier object</returns>
		public Supplier this[int Index]
		{
			get
			{
				return ((Supplier)this.InnerList[Index]);
			}

			set
			{
				this.InnerList[Index] = value;
			}
		}

		#endregion Properties

		#region Public Methods

		/// <summary>
		/// Adds an item to the collection
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public int Add(Supplier value)
		{
			return (List.Add(value));
		}

		/// <summary>
		/// Returns the index of an item within the collection
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public int IndexOf(Supplier value)
		{
			return (List.IndexOf(value));
		}

		/// <summary>
		/// Inserts an item into the collection
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		public void Insert(int index, Supplier value)
		{
			List.Insert(index, value);
		}


		/// <summary>
		/// Removes an item from the collection
		/// </summary>
		/// <param name="value"></param>
		public void Remove(Supplier value)
		{
			List.Remove(value);
		}


		/// <summary>
		/// Indicates the existence of an item within the collection
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool Contains(Supplier value)
		{
			// If value is not of type OBJECT_TYPE, this will return false.
			return (List.Contains(value));
		}

		#endregion Public Methods

		#region Private Members

		private const string OBJECT_TYPE = "Library.BOL.Suppliers.Supplier";
		private const string OBJECT_TYPE_ERROR = "Must be of type Supplier";


		#endregion Private Members

		#region Overridden Methods

		/// <summary>
		/// When Inserting an Item
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		protected override void OnInsert(int index, Object value)
		{
			if (value.GetType() != Type.GetType(OBJECT_TYPE))
				throw new ArgumentException(OBJECT_TYPE_ERROR, "value");
		}


		/// <summary>
		/// When removing an item
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		protected override void OnRemove(int index, Object value)
		{
			if (value.GetType() != Type.GetType(OBJECT_TYPE))
				throw new ArgumentException(OBJECT_TYPE_ERROR, "value");
		}


		/// <summary>
		/// When Setting an Item
		/// </summary>
		/// <param name="index"></param>
		/// <param name="oldValue"></param>
		/// <param name="newValue"></param>
		protected override void OnSet(int index, Object oldValue, Object newValue)
		{
			if (newValue.GetType() != Type.GetType(OBJECT_TYPE))
				throw new ArgumentException(OBJECT_TYPE_ERROR, "newValue");
		}


		/// <summary>
		/// Validates an object
		/// </summary>
		/// <param name="value"></param>
		protected override void OnValidate(Object value)
		{
			if (value.GetType() != Type.GetType(OBJECT_TYPE))
				throw new ArgumentException(OBJECT_TYPE_ERROR);
		}

		#endregion Overridden Methods

		#endregion Generic CollectionBase Code
	}
}