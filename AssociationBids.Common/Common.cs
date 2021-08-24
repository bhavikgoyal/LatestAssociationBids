using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Stripe;
using System.Linq;

namespace AssociationBids.Portal.Common
{
    public class LookUpType
    {
        public enum RecordStatus
        {
            PendingApproval = 1,
            Approved = 2,
            Unapproved = 4,
            PendingApprovalOrApproved = 3
        }

        public enum AccessType
        {
            Read = 1,
            Create = 2,
            Update = 4,
            Delete = 8
        }

        public enum EmailStatus
        {
            New = 1,
            Pending = 2,
            Sent = 4
        }

        public enum ProposalRequestStatus
        {
            InProgress = 1,
            Submitted = 2,
            Completed = 4,
            Cancelled = 8
        }

        public enum ProposalStatus
        {
            NotStarted = 1,
            InProgress = 2,
            Completed = 4
        }

        public enum ProposalRequestResourceStatus
        {
            New = 1,
            Notified = 2,
            Interested = 4,
            NotInterested = 8,
            Submitted = 16,
            Accepted = 32,
            Rejected = 64
        }

        public enum TaskStatus
        {
            NotStarted = 1,
            InProgress = 2,
            Waiting = 16,
            Deferred = 8,
            Complete = 4,
            Closed = 32
        }

        public enum TaskPriority
        {
            Low = 1,
            Normal = 2,
            High = 4
        }

        /// <summary>
        /// SELECT replace(Title, ' ', '') + ' = ' + cast(GroupKey AS VARCHAR(5)) + ',' FROM [Group]
        /// </summary>
        public enum Group
        {
            Administrator = 1,
            Supervisor = 2,
            PropertyManager = 3,
            Staff = 4,
            Vendor = 5,
            Guest = 6,
            Other = 7
        }

        /// <summary>
        /// SELECT Controller + ' = ' + cast(ModuleKey AS VARCHAR(5)) + ',' FROM Module
        /// </summary>
        public enum ModuleType
        {
            Home = 1,
            Billing = 2,
            Settings = 3,
            ProposalRequest = 100,
            Proposal = 101,
            Property = 102,
            Staff = 103,
            Vendor = 104,
            VendorVerify = 105,
            WorkOrder = 106,
            Invoice = 200,
            Payment = 201,
            Account = 300,
            CreditCard = 301,
            Insurance = 302,
            Profile = 303,
            CompanyService = 304,
            ServiceArea = 305,
            Company = 701,
            Document = 702,
            Message = 703,
            Note = 704,
            Register = 705,
            Resource = 706,
            Reminder = 707,
            Task = 708,
            Rewards = 709
        }
    }

    /// <summary>
    /// Summary description for Util.
    /// </summary>
    public class Util
    {
        public Util() { }

        #region Format Functions
        public static string FormatPhoneNumber(string phone, string extension)
        {
            if (IsObjectData(phone) && IsObjectData(extension))
            {
                return phone + " x " + extension;
            }
            else
            {
                return WriteObjectData(phone);
            }
        }

        public static string FormatDate(string date)
        {
            if (IsValidDateTime(date))
            {
                return FormatDate(DateTime.Parse(date));
            }
            else
            {
                return "";
            }
        }

        public static string FormatDate(DateTime date)
        {
            if (IsValidDateTime(date))
            {
                return date.ToString("M/d/yyyy");
            }
            else
            {
                return "";
            }
        }

        public static string FormatDateTime(string dateTime)
        {
            if (IsValidDateTime(dateTime))
            {
                return FormatDateTime(DateTime.Parse(dateTime));
            }
            else
            {
                return "";
            }
        }

        public static string FormatDateTime(System.DateTime dateTime)
        {
            if (IsValidDateTime(dateTime))
            {
                return dateTime.ToString("M/d/yy h:mm tt").Replace(" 12:00 AM", "");
            }
            else
            {
                return "";
            }
        }

        public static string FormatTime(System.DateTime dateTime)
        {
            if (IsValidDateTime(dateTime))
            {
                return dateTime.ToString("h:mm tt");
            }
            else
            {
                return "";
            }
        }

        public static string FormatTime(string dateTime)
        {
            return (dateTime != null && dateTime != "" ? FormatTime(DateTime.Parse(dateTime)) : "");
        }

        public static string FormatNumber(int data)
        {
            return data.ToString("#,###,##0");
        }

        public static string FormatMoney(double data)
        {
            return data.ToString("#,###,##0.00");
        }

        public static string FormatMoney(Decimal data)
        {
            return data.ToString("#,###,##0.00");
        }

        public static string FormatPercentage(double data)
        {
            return data.ToString("#,##0.0000") + "%";
        }

        public static string FormatDollar(double data)
        {
            return String.Format("{0:C}", data);
        }

        public static string FormatDollar(Decimal data)
        {
            return String.Format("{0:C}", data);
        }

        public static string FormatPhone(string phone)
        {
            if (String.IsNullOrEmpty(phone))
            {
                return "";
            }

            string phoneNumber = StripNumber(phone);

            if (phoneNumber.Length == 10)
            {
                try
                {
                    double dblPhone = double.Parse(phoneNumber);
                    return dblPhone.ToString("(###) ###-####");
                }
                catch
                {
                    return phone;
                }
            }
            else if (phoneNumber.Length == 11 && phoneNumber.StartsWith("1"))
            {
                try
                {
                    double dblPhone = double.Parse(phoneNumber.Substring(1));
                    return dblPhone.ToString("(###) ###-####");
                }
                catch
                {
                    return phone;
                }
            }
            else
            {
                return phone;
            }
        }

        public static string FormatFullName(object firstName, object middleName, object lastName)
        {
            string strFullName = WriteObjectData(lastName);
            strFullName += WriteObjectData(", ", firstName);
            strFullName += WriteObjectData(" ", middleName);

            return strFullName;
        }
        public static string FormatFileSize(double bytes)
        {
            double kilobytes = bytes / (double)1024;
            double megabytes = kilobytes / (double)1024;
            return megabytes.ToString("#,###,###.# MB");
        }
        public static string FormatUrl(string data)
        {
            if (data != null)
            {
                // then, find any urls with or without the protocol i.e. with http, https, etc.
                string url = "<a href=\"{0}\" target=\"_blank\">{0}</a>";
                string url2 = "<a href=\"http://{0}\" target=\"_blank\">{0}</a>";
                //string pattern = @"(([\w]+:)?//)?(([\d\w]|%[a-fA-f\d]{2,2})+(:([\d\w]|%[a-fA-f\d]{2,2})+)?@)?([\d\w][-\d\w]{0,253}[\d\w]\.)+[\w]{2,4}(:[\d]+)?(/([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)*(\?(&?([-+_~.\d\w]|%[a-fA-f\d]{2,2})=?)*)?(#([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)?";

                string pattern = @"(?#WebOrIP)((?#protocol)((http|https):\/\/)?(?#subDomain)(([a-zA-Z0-9]+\.(?#domain)[a-zA-Z0-9\-]+(?#TLD)(\.[a-zA-Z]+){1,2})|(?#IPAddress)((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])))+(?#Port)(:[1-9][0-9]*)?)+(?#Path)((\/((?#dirOrFileName)[a-zA-Z0-9_\-\%\~\+]+)?)*)?(?#extension)(\.([a-zA-Z0-9_]+))?(?#parameters)(\?([a-zA-Z0-9_\-]+\=[a-z-A-Z0-9_\-\%\~\+]+)?(?#additionalParameters)(\&([a-zA-Z0-9_\-]+\=[a-z-A-Z0-9_\-\%\~\+]+)?)*)?";

                System.Text.RegularExpressions.Regex exp = new System.Text.RegularExpressions.Regex(pattern, System.Text.RegularExpressions.RegexOptions.ExplicitCapture);

                foreach (System.Text.RegularExpressions.Match match in exp.Matches(data))
                {
                    if (match.Value.ToLower().StartsWith("www"))
                    {
                        data = data.Replace(match.Value, String.Format(url2, match.Value));
                    }
                    else
                    {
                        data = data.Replace(match.Value, String.Format(url, match.Value));
                    }
                }

                return data;
            }
            else
            {
                return "";
            }
        }
        public static string FormatText(string data)
        {
            if (data != null)
            {
                // first, replace new line or carriage return characters with <br/>
                string pattern = @"(\r\n|\r|\n|\n\r)";
                System.Text.RegularExpressions.Regex exp = new System.Text.RegularExpressions.Regex(pattern);
                data = exp.Replace(data, "<br />");

                // reformat &amp; as & for urls
                data = data.Replace("&amp;", "&");

                return data;
            }
            else
            {
                return "";
            }
        }
        public static string FormatTextSummary(string data)
        {
            if (data != null)
            {
                return FormatText(GetTextSummary(data));
            }
            else
            {
                return "";
            }
        }
        public static string GetTextSummary(string data)
        {
            if (data != null)
            {
                if (data.Length > GetAppSettingsInt("MaximumSummaryTextLength"))
                {
                    return data.Substring(0, GetAppSettingsInt("MaximumSummaryTextLength")) + "...";
                }
                else
                {
                    return data;
                }
            }
            else
            {
                return "";
            }
        }

        public static string GetSafeFileName(string data)
        {
            return data.Replace(" ", "-").Replace("#", "-").Replace("&", "-").Replace("%", "-").Replace("--", "-");
        }
        #endregion

        #region Misc Functions
        /// <summary>
        /// Uses reflection to get current directory based on the location of this assembly
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentDirectory()
        {
            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            dir = dir.Replace(@"file:\", "");
            dir = dir.Replace(@"\Debug", "");
            dir = dir.Replace(@"\Release", "");
            dir = dir.Replace(@"\bin", "");
            dir = dir.Replace(@"\x86", "");
            dir = dir.Replace(@"\x64", "");

            return dir;
        }

        public static string StripNumber(string data)
        {
            System.Text.StringBuilder newData = new System.Text.StringBuilder();
            string validNumbers = ".0123456789";
            for (int i = 0; i < data.Length; i++)
            {
                if (validNumbers.IndexOf(data[i]) > -1)
                {
                    newData.Append(data[i]);
                }
            }
            return newData.ToString();
        }

        public static DataSet ToDataSet<T>(IList<T> list, string name, IList<string> properties)
        {
            Type elementType = typeof(T);
            DataSet ds = new DataSet();
            DataTable t = new DataTable(name);
            ds.Tables.Add(t);

            //add a column to table for each public property on T
            foreach (var propInfo in elementType.GetProperties())
            {
                Type ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

                if (properties.Contains(propInfo.Name))
                {
                    t.Columns.Add(propInfo.Name, ColType);
                }
            }

            //go through each property on T and add each value to the table
            foreach (T item in list)
            {
                DataRow row = t.NewRow();

                foreach (var propInfo in elementType.GetProperties())
                {
                    if (properties.Contains(propInfo.Name))
                    {
                        row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                    }
                }

                t.Rows.Add(row);
            }

            return ds;
        }
        #endregion

        #region Date Functions
        public static System.DateTime GetBeginingOfMonth(System.DateTime date)
        {
            return Util.GetValueDateTime(date.Month.ToString() + "/1/" + date.Year.ToString());
        }
        public static System.DateTime GetBeginingOfNextMonth(System.DateTime date)
        {
            return GetBeginingOfMonth(date).AddMonths(1);
        }
        public static System.DateTime GetEndOfMonth(System.DateTime date)
        {
            return GetBeginingOfMonth(date.AddMonths(1)).AddDays(-1);
        }
        public static System.DateTime GetBeginingOfDay(string date)
        {
            return GetBeginingOfDay(GetValueDateTime(date));
        }
        public static System.DateTime GetBeginingOfDay(System.DateTime date)
        {
            return Util.GetValueDateTime(date.ToShortDateString() + " 12:00 AM");
        }
        public static System.DateTime GetEndOfDay(string date)
        {
            return GetEndOfDay(GetValueDateTime(date));
        }
        public static System.DateTime GetEndOfDay(System.DateTime date)
        {
            return GetBeginingOfDay(date.AddDays(1)).AddMinutes(-1);
        }
        public static System.DateTime AddBusinessDays(System.DateTime date, int days)
        {
            DateTime returnDate = GetEndOfDay(date);

            for (int i = 0; i < days; i++)
            {
                returnDate = returnDate.AddDays(1);

                if (returnDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    returnDate = returnDate.AddDays(2);
                }
                else if (returnDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    returnDate = returnDate.AddDays(1);
                }
            }

            return returnDate;
        }
        public static System.DateTime GetNext15Minutes(System.DateTime date)
        {
            int minutes = date.Minute;

            if (minutes < 15)
            {
                minutes = 15;
            }
            else if (minutes >= 15 && minutes < 30)
            {
                minutes = 30;
            }
            else if (minutes >= 30 && minutes < 45)
            {
                minutes = 45;
            }
            else
            {
                minutes = 60;
            }

            return System.DateTime.Today.AddHours(date.Hour).AddMinutes(minutes);
        }
        public static System.DateTime StartOfWeek(System.DateTime date)
        {
            System.DayOfWeek startOfWeek = System.DayOfWeek.Monday;
            int diff = date.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return date.AddDays(-1 * diff).Date;
        }
        #endregion

        #region Data Functions
        public static bool IsObjectData(object data)
        {
            return (data != null && data.ToString().Trim().Length > 0);
        }

        public static string WriteObjectData(object data)
        {
            return WriteObjectData("", data, "");
        }

        public static string WriteObjectData(string prefix, object data)
        {
            return WriteObjectData(prefix, data, "");
        }

        public static string WriteObjectData(object data, string suffix)
        {
            return WriteObjectData("", data, suffix);
        }

        public static string WriteObjectData(string prefix, object data, string suffix)
        {
            return (IsObjectData(data) ? prefix + data + suffix : "");
        }
        #endregion

        #region App Settings Functions
        public static bool AppSettingsExist(string name)
        {
            return (System.Configuration.ConfigurationManager.AppSettings[name] != null);
        }
        public static string GetAppSettings(string name)
        {
            if (AppSettingsExist(name))
            {
                return System.Configuration.ConfigurationManager.AppSettings[name];
            }
            else
            {
                throw new Exception("web.config key \"" + name + "\" is missing!");
            }
        }
        public static int GetAppSettingsInt(string name)
        {
            return (GetValueInt(GetAppSettings(name)));
        }
        public static double GetAppSettingsDouble(string name)
        {
            return (GetValueDouble(GetAppSettings(name)));
        }
        public static bool GetAppSettingsBool(string name)
        {
            return (GetValueBool(GetAppSettings(name)));
        }
        #endregion

        #region Validation Functions
        public static bool IsValidText(string data)
        {
            return (GetValue(data) != null && GetValue(data).Length > 0);
        }

        public static bool IsValidInt(int data)
        {
            return (data > 0);
        }

        public static bool IsValidInt(string data)
        {
            if (IsValidText(data))
            {
                int value;
                return int.TryParse(GetValue(data), out value);
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidDouble(string data)
        {
            if (IsValidText(data))
            {
                double value;
                return double.TryParse(GetValue(data), out value);
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidDouble(double data)
        {
            return (data > 0.0);
        }

        public static bool IsValidDecimal(string data)
        {
            if (IsValidText(data))
            {
                Decimal value;
                return Decimal.TryParse(GetValue(data), out value);
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidDecimal(Decimal data)
        {
            return (data > 0);
        }

        public static bool IsValidDateTime(DateTime data)
        {
            return (data != DateTime.MinValue);
        }
        public static bool IsValidDateTime(string data)
        {
            if (IsValidText(data))
            {
                DateTime value;
                return DateTime.TryParse(GetValue(data), out value);
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidDateTime(string date, string time)
        {
            if (IsValidText(date))
            {
                string data = GetValue(date) + " " + GetValue(time);
                DateTime value;
                return DateTime.TryParse(GetValue(data), out value);
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidBool(string data)
        {
            if (IsValidText(data))
            {
                bool value;
                return bool.TryParse(GetValue(data), out value);
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidGUID(Guid data)
        {
            return !Guid.Empty.Equals(data);
        }

        public static bool IsValidGUID(string data)
        {
            if (IsValidText(data))
            {
                Guid value;
                return Guid.TryParse(GetValue(data), out value);
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidMoney(string data)
        {
            string newData = StripNumber(data);
            return (IsValidDouble(newData) || IsValidInt(newData));
        }
        public static bool IsValidPassword(string password)
        {
            if (password == "" || password.Length < 5)
            {
                return false;
            }
            else
            {
                string validNumbers = "0123456789";
                string validCharactersLower = "abcdefghijklmnopqrstuvwxyz";
                string validCharactersUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                string validCharacters = validNumbers + validCharactersLower + validCharactersUpper;

                bool numberExists = false;
                bool upperCharExists = false;

                string letter = "";

                for (int i = 0; i < password.Length; i++)
                {
                    letter = password.Substring(i, 1);
                    if (validCharacters.IndexOf(letter) < 0)
                    {
                        return false;
                    }
                    if (validNumbers.IndexOf(letter) >= 0)
                    {
                        numberExists = true;
                    }
                    if (validCharactersUpper.IndexOf(letter) >= 0)
                    {
                        upperCharExists = true;
                    }
                }

                return (numberExists && upperCharExists);
            }
        }

        public static bool IsValidEmail(string data)
        {
            if (IsValidText(data))
            {
                string pattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

                System.Text.RegularExpressions.Regex exp = new System.Text.RegularExpressions.Regex(pattern);

                return exp.IsMatch(data);
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidEmailList(string data)
        {
            if (IsValidText(data))
            {
                data = data.Replace(';', ',');
                string[] emailList = data.Split(',');

                foreach (string email in emailList)
                {
                    if (!IsValidEmail(email))
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region GetValue Functions
        private static string GetValue(string data)
        {
            return (data != null ? data : "");
        }

        public static string GetValueText(string data)
        {
            return (IsValidText(data) ? GetValue(data) : "");
        }

        public static int GetValueInt(string data)
        {
            return (IsValidInt(data) ? int.Parse(GetValue(data)) : 0);
        }

        public static double GetValueDouble(string data)
        {
            return (IsValidDouble(data) ? double.Parse(GetValue(data)) : 0);
        }
        public static Decimal GetValueDecimal(string data)
        {
            return (IsValidDecimal(data) ? Decimal.Parse(GetValue(data)) : 0);
        }
        public static DateTime GetValueDateTime(string data)
        {
            return (IsValidDateTime(data) ? DateTime.Parse(GetValue(data)) : DateTime.MinValue);
        }

        public static DateTime GetValueDateTime(string date, string time)
        {
            if (IsValidDateTime(date) && IsValidDateTime(date, time))
            {
                string dateTime = GetValue(date) + " " + GetValue(time);
                return DateTime.Parse(GetValue(dateTime));
            }
            else if (IsValidDateTime(date))
            {
                return DateTime.Parse(GetValue(date));
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        public static bool GetValueBool(string data)
        {
            return (IsValidBool(data) ? bool.Parse(GetValue(data)) : GetValue(data) == "on");
        }

        public static Guid GetValueGUID(string name)
        {
            return (IsValidGUID(name) ? new Guid(GetValue(name)) : Guid.NewGuid());
        }
        #endregion

        #region Mail Functions
        public static void AddEmailAddressList(System.Net.Mail.MailAddressCollection addressCol, string emailList)
        {
            if (IsValidText(emailList))
            {
                emailList = emailList.Replace(";", ",");

                addressCol.Add(emailList);
            }
        }

        public static bool SendMail(System.Net.Mail.MailMessage message)
        {
            try
            {
                Email email = new Email(message);

                email.SendMail();

                return true;
            }
            catch (Exception e)
            {
                Error.LogError(e, false);
                return false;
            }
        }

        public static bool SendMailAsync(System.Net.Mail.MailMessage message)
        {
            try
            {
                Email email = new Email(message);

                email.SendMailAsync();

                return true;
            }
            catch (Exception e)
            {
                Error.LogError(e, false);
                return false;
            }
        }

        #endregion
    }

    public class Error
    {
        public Error() { }

        public static void LogError(Exception e)
        {
            LogError(e, true);
        }

        public static void LogError(Exception e, string sessionXML)
        {
            LogError(e, sessionXML, true);
        }

        public static void LogError(Exception e, bool sendEmail)
        {
            LogError(e, null, sendEmail);
        }

        public static void LogError(Exception e, string sessionXML, bool sendEmail)
        {
            try
            {
                if (sendEmail)
                {
                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

                    message.From = new System.Net.Mail.MailAddress(Util.GetAppSettings("Mail.SiteAdministrator"), "Association Bids");
                    Util.AddEmailAddressList(message.To, Util.GetAppSettings("Mail.ErrorMessageTo"));
                    message.Subject = Util.GetAppSettings("Mail.ErrorMessageTitle");
                    message.Body = e.ToString();

                    if (sessionXML != null)
                    {
                        message.Body += "\n\n---------------------------------";
                        message.Body += sessionXML;
                    }

                    Util.SendMail(message);
                }
            }
            catch
            {
                WriteErrorsToFile(e.Message);
            }
        }

        public static void WriteErrorsToFile(string message)
        {
            StreamWriter writer = null;

            try
            {
                string fileName = String.Format(@"{0}\errors.log", Util.GetAppSettings("ErrorLogDirectory"));
                writer = new StreamWriter(fileName, true);
                writer.WriteLine("--------------------------------------------------");
                writer.WriteLine(String.Format("{0}: {1}", System.DateTime.Now, message));
                writer.WriteLine("--------------------------------------------------");
            }
            catch
            {
                // Do nothing
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
    }

    public class Randomize
    {
        private int __initializer;

        public Randomize()
        {
            __initializer = DateTime.Now.Millisecond;
        }

        public Randomize(int initializer)
        {
            __initializer = initializer;
        }

        #region Randomize Functions
        public string GetRandomCharacters(string stringToRandomize, int numberOfCharacters)
        {
            string randomString = "";
            Random randomizer = new Random(__initializer);

            for (int i = 0; i < numberOfCharacters; i++)
            {
                randomString += stringToRandomize.Substring(randomizer.Next(0, stringToRandomize.Length), 1);
            }
            return randomString;
        }

        public string GetRandomNumbers(int numberOfNumbers)
        {
            string randomString = "";
            Random randomizer = new Random(__initializer);
            string validNumbers = "0123456789";

            for (int i = 0; i < numberOfNumbers; i++)
            {
                randomString += validNumbers.Substring(randomizer.Next(0, 10), 1);
            }
            return randomString;
        }

        public string RandomizeString(string stringToRandomize)
        {
            int position = 0;
            string newString = "";
            Random randomizer = new Random(__initializer);

            while (stringToRandomize.Length > 1)
            {
                position = randomizer.Next(0, stringToRandomize.Length);
                newString += stringToRandomize.Substring(position, 1);

                if (position == 0)
                {
                    // first letter
                    stringToRandomize = stringToRandomize.Substring(1, stringToRandomize.Length - 1);
                }
                else if (position == (stringToRandomize.Length - 1))
                {
                    // last letter
                    stringToRandomize = stringToRandomize.Substring(0, stringToRandomize.Length - 1);
                }
                else
                {
                    // middle letter
                    stringToRandomize = stringToRandomize.Substring(0, position) + stringToRandomize.Substring(position + 1);
                }
            }

            return newString + stringToRandomize;
        }

        public string GeneratePassword()
        {
            string validNumbers = "123456789";
            string validCharactersUpper = "ABCDEFGHIJKLMNPQRSTUVWXYZ";
            string validCharactersLower = "abcdefghijkmnpqrstuvwxyz";
            string password = "";

            // select 2 random numbers
            password += GetRandomCharacters(validNumbers, 2);

            // select 2 random upper case letters
            password += GetRandomCharacters(validCharactersUpper, 2);

            // select 2 random lower case letters
            password += GetRandomCharacters(validCharactersLower, 2);

            // randomize selected password
            return RandomizeString(password);
        }
        #endregion
    }


    public class Payment
    {
        public static async Task<dynamic> PayAsync(string CardNumber, int Month, int Year, string CVC, int Value,string name,string addressline1,string addressline2, string zip,string city,string state)
        {
            try
            {
                
                StripeConfiguration.ApiKey = System.Configuration.ConfigurationManager.AppSettings["PaymentApikey"];
                var optionstoken = new TokenCreateOptions
                {
                    Card = new CreditCardOptions
                    {
                        Number = CardNumber,
                        ExpMonth = Month,
                        ExpYear = Year,
                        Cvc = CVC,
                        Name = name,
                        AddressLine1 = addressline1,
                        AddressLine2 = addressline2,
                        AddressZip = zip,
                        AddressCity = city,
                        AddressState = state,
                        AddressCountry = "US",
                        Currency = "usd",
                        
                    }
                };

                var servicetoken = new TokenService();
                Token stripetoken = await servicetoken.CreateAsync(optionstoken);

                var options = new ChargeCreateOptions
                {
                    Amount = Value,
                    Currency = "usd",
                    Description = "Auto dynamiv",
                    Source = stripetoken.Id
                };

                var service = new ChargeService();

                Charge charge = await service.CreateAsync(options);
            
                if (charge.Paid)
                {
                    return "Success " +"?" + charge.Id;
                }
                else
                {
                    return "Failed " + "?" + charge.Id;
                }

            }
            catch (Exception e)
            {
                return e.Message;
            }

        }


        /* ---------------------------- */
        public static async Task<dynamic> GenerateTokenForCC(string CardNumber, int Month, int Year, string CVC, string name, string addressline1, string addressline2, string zip, string city, string state)
        {
            try
            {

                StripeConfiguration.ApiKey = System.Configuration.ConfigurationManager.AppSettings["PaymentApikey"];

                var cust = new CustomerCreateOptions()
                {
                    Address = new AddressOptions()
                    {
                        Line1 = addressline1,
                        Line2 = addressline2,
                        City = city,
                        State = state,
                        PostalCode = zip,
                        Country = "US"
                    },
                    Description = name,
                    Name = name
                };
                var custService = new CustomerService();
                var cRes = await custService.CreateAsync(cust);

                var pmOption = new PaymentMethodCreateOptions()
                {
                    Card = new PaymentMethodCardCreateOptions()
                    {
                        Number = CardNumber,
                        ExpMonth = Month,
                        ExpYear = Year,
                        Cvc = CVC
                    },
                    Type = "card"
                };
                var pmService = new PaymentMethodService();
                var pmRes = await pmService.CreateAsync(pmOption);
                await pmService.AttachAsync(pmRes.Id, new PaymentMethodAttachOptions() { Customer = cRes.Id });

                return "Success?toc=" + cRes.Id + "&pmid=" + pmRes.Id;

            }
            catch(Exception ex)
            {
                return "failed";
            }
        }

        public static async Task<dynamic> PayAsyncNew(int Value, string TokenId,string PMId,string Description)
        {
            try
            {

                StripeConfiguration.ApiKey = System.Configuration.ConfigurationManager.AppSettings["PaymentApikey"];

                var optionsList = new PaymentMethodListOptions
                {
                    Customer = TokenId,
                    Type = "card",
                };

                //var PMservice = new PaymentMethodService();
                //var paymentMethods = PMservice.List(optionsList);
                //string pId = paymentMethods.Data[0].Id;
                if (Description == "")
                    Description = "Association Bids";
                var pi = new PaymentIntentCreateOptions()
                {
                    Customer = TokenId,
                    Amount = Value,
                    Currency = "usd",
                    OffSession = true,
                    PaymentMethod = PMId,
                    Confirm = true,
                    Description = Description
                };
                var piService = new PaymentIntentService();
                var piRes = await piService.CreateAsync(pi);
                if (piRes.Status == "succeeded")
                {
                    return "Success " + "?" + piRes.Id;//charge.Id;
                }
                else
                {
                    return "Failed ";
                }

            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        public static async Task<dynamic> AddPaymentMethod(string TokenId, string CardNumber, int Month, int Year, string CVC,bool isPrimary)
        {
            if (TokenId != "")
            {
                StripeConfiguration.ApiKey = System.Configuration.ConfigurationManager.AppSettings["PaymentApikey"];
                var PMListOption = new PaymentMethodListOptions()
                {
                    Customer = TokenId,
                    Type = "card"
                };
                var PMService = new PaymentMethodService();

                var pmOption = new PaymentMethodCreateOptions()
                {
                    Card = new PaymentMethodCardCreateOptions()
                    {
                        Number = CardNumber,
                        ExpMonth = Month,
                        ExpYear = Year,
                        Cvc = CVC
                    },
                    Type = "card"
                };
                var pmRes = await PMService.CreateAsync(pmOption);
                if (pmRes.Id != null)
                {
                    //var PMList = await PMService.ListAsync(PMListOption);
                    //if (PMList != null && PMList.Data.Count > 0)
                    //{
                    //    string old_pmId = PMList.Data[0].Id;
                    //    await PMService.DetachAsync(old_pmId);
                    //}
                    await PMService.AttachAsync(pmRes.Id, new PaymentMethodAttachOptions() { Customer = TokenId });
                    return "Success?PmId=" + pmRes.Id;
                }
            }
            return "Failed";

        }
        //public static async Task<dynamic> ChangePrimaryMethod(string CToken,String PMToken)
        //{
        //    try
        //    {
        //        StripeConfiguration.ApiKey = System.Configuration.ConfigurationManager.AppSettings["PaymentApikey"];
        //        string oldPMID = "";
        //        var PMService = new PaymentMethodService();
        //        var PM = PMService.Get(PMToken);
        //        var PMListOption = new PaymentMethodListOptions()
        //        {
        //            Customer = CToken,
        //            Type = "card"
        //        };
                
        //        if (PM != null && PM.Id != "")
        //        {
        //            var p = await PMService.AttachAsync(PMToken, new PaymentMethodAttachOptions() { Customer = CToken });
        //            return "Success" + "?" + p.Id;
        //        }
        //        return "Failed";
        //    }
        //    catch(Exception ex)
        //    {
        //        return "Failed";
        //    }
        //}
        /* ---------------------------- */
        public static async Task<dynamic> RefundPayAsync(string Strip_tokenId)
        {
            try
            {

                StripeConfiguration.ApiKey = System.Configuration.ConfigurationManager.AppSettings["PaymentApikey"];

                //var cService = new CustomerService();
                //var cdata = cService.Get(Strip_tokenId);
                //var piOption = new PaymentIntentListOptions()
                //{
                //    Customer = Strip_tokenId
                //};
                //var pService = new PaymentIntentService();
                //var listPI = await pService.ListAsync(piOption);
                //var len = listPI.Data.Count;
                //var lastPayment = listPI.Data.ToList().LastOrDefault();

                //var options = new RefundCreateOptions
                //{
                //    Charge = Strip_tokenId,
                //};
                var options = new RefundCreateOptions
                {
                    PaymentIntent = Strip_tokenId
                };

                var service = new RefundService();
                  Refund refund  =await service.CreateAsync(options);
      
               
                if (refund.Status == "succeeded")
                {
                    return "Success " + "?" + refund.Id;
                }
                else
                {
                    return "Fail" + "?" + refund.Id;
                }

            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
    }
}




