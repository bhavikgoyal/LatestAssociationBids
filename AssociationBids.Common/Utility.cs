using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Common
{
    public static class Utility
    {
        public enum StoredProcedures
        {
            #region Login

            [Description("[dbo].[USP_User_Login]")]
            GetUserByUserNamePassword,

            #endregion


        }
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

            return attributes != null && attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
        public static string FormatPhoneNumberHelper(dynamic number)
        {
            try
            {
                if (number != null && number.ToString() != "")
                {
                    if (number.ToString().Length == 10)
                    {
                        return string.Format("{0:(###) ###-####}", Convert.ToInt64(number));
                    }
                    else
                    {
                        return number.ToString();
                    }
                }
                return "";
            }
            catch
            {
                return number;
            }
        }
        public static string FormatNumberHelper(dynamic number,bool isCurrency)
        {
            try
            {
                if(number != null && number.ToString() != "")
                {
                    if(isCurrency)
                        return double.Parse(number.ToString()).ToString("C2", new CultureInfo("en-US"));
                    else
                        return int.Parse(number.ToString()).ToString("N0", new CultureInfo("en-US"));
                }
                return "";
            }
            catch
            {
                return number;
            }
        }
        public static DateTime AddBusinessDays(this DateTime current, int days)
        {
            var sign = Math.Sign(days);
            var unsignedDays = Math.Abs(days);
            for (var i = 0; i < unsignedDays; i++)
            {
                do
                {
                    current = current.AddDays(sign);

                } while (current.DayOfWeek == DayOfWeek.Saturday ||
                         current.DayOfWeek == DayOfWeek.Sunday);
            }
            return current;
        }
    }
}
