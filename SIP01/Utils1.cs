using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIP01
{
    static class Utils1
    {

		public static string GetField(string message, string fieldName)
		{
			char[] Sep = { '\r', '\n' };
			string[] Fields = message.Split(Sep);
			foreach (string str1 in message.Split(Sep)) if (str1.StartsWith(fieldName)) return str1.Substring(fieldName.Length+1).Trim();
			return null;
		}


		// *******************************************************************************************************

		public static string GetParam(string message, string fieldName)
		{
			char[] Sep = { ',' };
			foreach (string str1 in message.Split(Sep))
			{
				if ((str1.Trim()).StartsWith(fieldName))
				{
					string fieldData = str1.Substring(fieldName.Length);
					fieldData = fieldData.Replace("=", "");
					fieldData = fieldData.Replace("\"", "");
					return fieldData;
				}
			}

			return null;

		}

		// *******************************************************************************************************
		public static string GenerateTag1(int length)

		{

			Random random = new Random();
			string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
			StringBuilder result = new StringBuilder(length);
			for (int i = 0; i < length; i++)
			{
				result.Append(characters[random.Next(characters.Length)]);
			}
			return result.ToString();
		}

		// *******************************************************************************************************
		public static string GenerateTag2(int length)

		{

			Random random = new Random();
			string characters = "0123456789abcdefghijklmnopqrstuvwxyz";
			StringBuilder result = new StringBuilder(length);
			for (int i = 0; i < length; i++)
			{
				result.Append(characters[random.Next(characters.Length)]);
			}
			return result.ToString();
		}

		// *******************************************************************************************************
		public static string GenerateBrachLong1()
		{
			return "branch="+ Const.BranchStart + GenerateTag2(10) + "-" + GenerateTag2(4) + "-" + GenerateTag2(4) + "-" + GenerateTag2(4) + "-" + GenerateTag2(12) ;
		}

		// *******************************************************************************************************
		public static string GenerateBrachShort1()
		{
			return "branch=" + Const.BranchStart +"."+ GenerateTag1(9);
		}

		// *******************************************************************************************************
		// *******************************************************************************************************
		// *******************************************************************************************************





	}
}
