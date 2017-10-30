﻿using System;
using System.Collections.Generic;
using System.Text;

using Shared.Classes;

namespace Library.BOL.Products
{
    [Serializable]
    public sealed class ProductTypes : BaseCollection
    {
        #region Static Methods

        public static ProductType Create(string description, bool primary)
        {
            return (DAL.FirebirdDB.ProductTypeInsert(description, primary));
        }

        /// <summary>
        /// Returns a collection of product types
        /// </summary>
        /// <param name="forceRefresh">Forces the cache to refresh</param>
        /// <returns>ProductTypes collection</returns>
        public static ProductTypes Get(bool forceRefresh = false)
        {
            CacheItem cachedResult = DAL.DALHelper.InternalCache.Get(Consts.CACHE_NAME_PRODUCT_TYPES);

            if (forceRefresh && cachedResult != null)
            {
                DAL.DALHelper.InternalCache.Remove(cachedResult);
                cachedResult = null;
            }

            if (DAL.DALHelper.AllowCaching && cachedResult == null)
            {
                cachedResult = new CacheItem(Consts.CACHE_NAME_PRODUCT_TYPES, DAL.FirebirdDB.ProductTypesGet());
                DAL.DALHelper.InternalCache.Add(Consts.CACHE_NAME_PRODUCT_TYPES, cachedResult);
            }


            if (cachedResult != null)
                return ((ProductTypes)cachedResult.Value);
            else
                return (DAL.FirebirdDB.ProductTypesGet());
        }

        /// <summary>
        /// Returns a product type based on it's name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ProductType Get(string name)
        {
            foreach (ProductType prodType in Get())
            {
                if (prodType.Description == name)
                    return (prodType);
            }

            return (DAL.FirebirdDB.ProductTypeGet(name));
        }

        #endregion Static Methods

        #region Generic CollectionBase Code

        #region Properties

        /// <summary>
        /// Indexer Property
        /// </summary>
        /// <param name="Index">Index of object to return</param>
        /// <returns>ProductType object</returns>
        public ProductType this[int Index]
        {
            get
            {
                return ((ProductType)this.InnerList[Index]);
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
        public int Add(ProductType value)
        {
            return (List.Add(value));
        }

        /// <summary>
        /// Returns the index of an item within the collection
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int IndexOf(ProductType value)
        {
            return (List.IndexOf(value));
        }

        /// <summary>
        /// Inserts an item into the collection
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void Insert(int index, ProductType value)
        {
            List.Insert(index, value);
        }


        /// <summary>
        /// Removes an item from the collection
        /// </summary>
        /// <param name="value"></param>
        public void Remove(ProductType value)
        {
            List.Remove(value);
        }


        /// <summary>
        /// Indicates the existence of an item within the collection
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(ProductType value)
        {
            // If value is not of type OBJECT_TYPE, this will return false.
            return (List.Contains(value));
        }

        #endregion Public Methods

        #region Private Members

        private const string OBJECT_TYPE = "Library.BOL.Products.ProductType";
        private const string OBJECT_TYPE_ERROR = "Must be of type ProductType";


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