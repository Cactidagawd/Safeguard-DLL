using System;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace SafeGuard
{
	internal static class Security
	{
		internal static string Encrypt(this string clearText)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(clearText);
			using (Aes aes = Aes.Create())
			{
				Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(Security.EncryptionKey, new byte[]
				{
					73,
					118,
					97,
					110,
					32,
					77,
					101,
					100,
					118,
					101,
					100,
					101,
					118
				});
				aes.Key = rfc2898DeriveBytes.GetBytes(32);
				aes.IV = rfc2898DeriveBytes.GetBytes(16);
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
					{
						cryptoStream.Write(bytes, 0, bytes.Length);
						cryptoStream.Close();
					}
					clearText = Convert.ToBase64String(memoryStream.ToArray());
				}
			}
			return clearText;
		}

		internal static string Decrypt(this string cipherText)
		{
			cipherText = cipherText.Replace(" ", "+");
			byte[] array = Convert.FromBase64String(cipherText);
			using (Aes aes = Aes.Create())
			{
				Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(Security.EncryptionKey, new byte[]
				{
					73,
					118,
					97,
					110,
					32,
					77,
					101,
					100,
					118,
					101,
					100,
					101,
					118
				});
				aes.Key = rfc2898DeriveBytes.GetBytes(32);
				aes.IV = rfc2898DeriveBytes.GetBytes(16);
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
					{
						cryptoStream.Write(array, 0, array.Length);
						cryptoStream.Close();
					}
					cipherText = Encoding.Unicode.GetString(memoryStream.ToArray());
				}
			}
			return cipherText;
		}

		internal static string ComputeHash(string s)
		{
			string result;
			using (MD5 md = MD5.Create())
			{
				using (FileStream fileStream = File.OpenRead(s))
				{
					byte[] array = md.ComputeHash(fileStream);
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i < array.Length; i++)
					{
						stringBuilder.Append(array[i].ToString("X2"));
					}
					result = stringBuilder.ToString();
				}
			}
			return result;
		}

		internal static string GetHID(string drive)
		{
			bool flag = drive == string.Empty;
			if (flag)
			{
				foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
				{
					bool isReady = driveInfo.IsReady;
					if (isReady)
					{
						drive = driveInfo.RootDirectory.ToString();
						break;
					}
				}
			}
			bool flag2 = drive.EndsWith(":\\");
			if (flag2)
			{
				drive = drive.Substring(0, drive.Length - 2);
			}
			string volumeSerial = Security.getVolumeSerial(drive);
			string cpuid = Security.getCPUID();
			return cpuid.Substring(13) + cpuid.Substring(1, 4) + volumeSerial + cpuid.Substring(4, 4);
		}

		internal static string getVolumeSerial(string drive)
		{
			ManagementObject managementObject = new ManagementObject("win32_logicaldisk.deviceid=\"" + drive + ":\"");
			managementObject.Get();
			string result = managementObject["VolumeSerialNumber"].ToString();
			managementObject.Dispose();
			return result;
		}

		internal static string getCPUID()
		{
			string text = "";
			ManagementClass managementClass = new ManagementClass("win32_processor");
			ManagementObjectCollection instances = managementClass.GetInstances();
			foreach (ManagementBaseObject managementBaseObject in instances)
			{
				ManagementObject managementObject = (ManagementObject)managementBaseObject;
				bool flag = text == "";
				if (flag)
				{
					text = managementObject.Properties["processorID"].Value.ToString();
					break;
				}
			}
			return text;
		}

		internal static string EncryptionKey = "2f4980a36b48be1694fb3104b2ff9f00";

		internal static string dllhash = Security.ComputeHash(string.Format("{0}SafeGuard.dll", AppDomain.CurrentDomain.BaseDirectory));

		internal static string newtonhash = Security.ComputeHash(string.Format("{0}Newtonsoft.Json.dll", AppDomain.CurrentDomain.BaseDirectory));
	}
}
