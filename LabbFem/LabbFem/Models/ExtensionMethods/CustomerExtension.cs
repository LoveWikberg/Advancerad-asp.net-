using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabbFem.Models.ExtensionMethods
{
    public static class CustomerExtension
    {

        /// <summary>
        /// Convert all objects in collection to a single string. Each object will represent a new line in the string.
        /// </summary>
        public static string ConvertContentToTextFileString(this CustomerVM[] collection)
        {
            string collectionConvertedToString = "";
            for (int i = 0; i < collection.Length; i++)
            {
                if (i != 0)
                    collectionConvertedToString += $"{Environment.NewLine}{collection[i].Id.ToString()},{collection[i].FirstName},{collection[i].LastName},{collection[i].Email},{collection[i].Gender},{collection[i].Age},{collection[i].ImageFileName}";
                else
                    collectionConvertedToString += $"{collection[i].Id.ToString()},{collection[i].FirstName},{collection[i].LastName},{collection[i].Email},{collection[i].Gender},{collection[i].Age},{collection[i].Age}";
            }
            return collectionConvertedToString;
        }
    }
}
