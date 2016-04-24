﻿using System;
using System.Linq;
using System.Configuration;

namespace MaurerConsole
{
	public static class SettingsReader
	{
		public static string[] StringValues_True = new string[] { "true", "t", "yes", "y", "1", "enabled" };
		public static string[] StringValues_False = new string[] { "false", "f", "no", "n", "0", "disabled" };

		public static bool GetSettingBoolean(string SettingName)
		{
			try
			{
				if (!StringValues_True.Contains(GetSettingString(SettingName)))
					return false;
				else
					return true;
			}
			catch
			{
				return false;
			}
		}

		public static string GetSettingString(string SettingName)
		{
			try
			{
				if (!SettingExists(SettingName))
					return string.Empty;
				else
					return ConfigurationManager.AppSettings[SettingName].ToLowerInvariant();
			}
			catch
			{
				return string.Empty;
			}
		}

		public static T GetSetting<T>(string SettingName)
		{
			try
			{
				if (!SettingExists(SettingName))
					return default(T);
				else
					return (T)Convert.ChangeType(ConfigurationManager.AppSettings[SettingName], typeof(T));
			}
			catch
			{
				return default(T);
			}
		}

		private static bool SettingExists(string SettingName, bool CheckForEmptyValue = true)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(SettingName))
					return false;

				if (!ConfigurationManager.AppSettings.AllKeys.Contains(SettingName))
					return false;

				if (CheckForEmptyValue)
				{
					if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings[SettingName]))
						return false;
				}

				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
