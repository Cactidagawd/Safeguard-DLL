using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace SafeGuard
{
	internal static class Utilities
	{
		internal static string getJSON(string url, string programId = "", string userName = "", string password = "")
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.Proxy = null;
			httpWebRequest.Accept = "application/json; charset=utf-8";
			httpWebRequest.Method = "GET";
			httpWebRequest.Headers["programid"] = programId;
			httpWebRequest.Headers["username"] = userName;
			httpWebRequest.Headers["password"] = password;
			httpWebRequest.Headers["version"] = Assembly.GetExecutingAssembly().GetName().Version.ToString().Encrypt();
			httpWebRequest.Headers["ipaddress"] = Tools.GetClientIP().Encrypt();
			httpWebRequest.Headers["signature"] = Config.Name;
			httpWebRequest.Headers["dllmd5"] = Security.dllhash.Encrypt();
			httpWebRequest.Headers["newtonmd5"] = Security.newtonhash.Encrypt();
			httpWebRequest.Headers["hid"] = Security.GetHID("C").Encrypt();
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			string result;
			using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
			{
				string text = streamReader.ReadToEnd();
				result = text;
			}
			return result;
		}

		internal static string postJSON(string url, object postObject, string programId = "", string userName = "", string password = "")
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.ContentType = "application/json; charset=utf-8";
			httpWebRequest.Accept = "application/json; charset=utf-8";
			httpWebRequest.Method = "POST";
			httpWebRequest.Proxy = null;
			httpWebRequest.Headers["programid"] = programId;
			httpWebRequest.Headers["username"] = userName;
			httpWebRequest.Headers["password"] = password;
			httpWebRequest.Headers["version"] = Assembly.GetExecutingAssembly().GetName().Version.ToString().Encrypt();
			httpWebRequest.Headers["ipaddress"] = Tools.GetClientIP().Encrypt();
			httpWebRequest.Headers["signature"] = Config.Name;
			httpWebRequest.Headers["dllmd5"] = Security.dllhash.Encrypt();
			httpWebRequest.Headers["newtonmd5"] = Security.newtonhash.Encrypt();
			httpWebRequest.Headers["hid"] = Security.GetHID("C").Encrypt();
			string value = JsonConvert.SerializeObject(postObject, new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore
			});
			using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
			{
				streamWriter.Write(value);
				streamWriter.Flush();
				streamWriter.Close();
			}
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			string result;
			using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
			{
				string text = streamReader.ReadToEnd();
				result = text;
			}
			return result;
		}

		internal static string postJSONNoHeaders(string url, object postObject)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.ContentType = "application/json; charset=utf-8";
			httpWebRequest.Accept = "application/json; charset=utf-8";
			httpWebRequest.Method = "POST";
			string value = JsonConvert.SerializeObject(postObject, new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore
			});
			using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
			{
				streamWriter.Write(value);
				streamWriter.Flush();
				streamWriter.Close();
			}
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			string result;
			using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
			{
				string text = streamReader.ReadToEnd();
				result = text;
			}
			return result;
		}

		public static bool validateEmail(string Email)
		{
			bool result;
			try
			{
				new MailAddress(Email);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool IsUrlValid(string url)
		{
			string pattern = "^(http|https|ftp|)\\://|[a-zA-Z0-9\\-\\.]+\\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\\-\\._\\?\\,\\'/\\\\\\+&amp;%\\$#\\=~])*[^\\.\\,\\)\\(\\s]$";
			Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
			return regex.IsMatch(url);
		}

		internal static string GetEnumDescription(this Enum value)
		{
			FieldInfo field = value.GetType().GetField(value.ToString());
			DescriptionAttribute[] array = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
			string result;
			if (array != null && array.Length != 0)
			{
				result = array[0].Description;
			}
			else
			{
				result = value.ToString();
			}
			return result;
		}
	}
}
