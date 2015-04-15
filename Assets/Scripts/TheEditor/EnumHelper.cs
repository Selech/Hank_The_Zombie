using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public static class EnumHelper
{
	/**
	 * To get the description in this next line supply the enum field name as a string, in this case col3
	 * Also supply the name of the Enum to use here EnumHelper<XXX> replace XXX with that name.
	 * string description = EnumHelper<Samplecolumns>.GetEnumDescription("col3");
	 **/
	public static string GetEnumDescription<T>(string value)
	{
		Type type = typeof(T);

		var name = System.Enum.GetNames(type)
			.Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase))
				.Select(d => d).FirstOrDefault();
		
		if (name == null)
		{
			return string.Empty;
		}
		var field = type.GetField(name);
		var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
		
		return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
	}


	// This extension method is broken out so you can use a similar pattern with 
	// other MetaData elements in the future. This is your base method for each.
	public static T GetAttribute<T>(this Enum value) where T : Attribute {
		var type = value.GetType();
		var memberInfo = type.GetMember(value.ToString());
		var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
		return (T)attributes[0];
	}
	
	// This method creates a specific call to the above method, requesting the
	// Description MetaData attribute.
	public static string ToName(this Enum value) {
		var attribute = value.GetAttribute<DescriptionAttribute>();
		return attribute == null ? value.ToString() : attribute.Description;
	}

	public static T ParseEnum<T>(string value, T defaultValue) where T : struct, IConvertible
	{
		if (!typeof(T).IsEnum) throw new ArgumentException("T must be an enumerated type");
		if (string.IsNullOrEmpty(value)) return defaultValue;
		
		foreach (T item in Enum.GetValues(typeof(T)))
		{
			if (item.ToString().ToLower().Equals(value.Trim().ToLower())) return item;
		}
		return defaultValue;
	}

	/**
	 * This is method is called like this: methodName<enumName>("string-to-search-for"). 
	 * @return The corresponding position number for the related enum value if within Enum.
	 **/
	public static int GetCorrespondingEnumPositionFromDescriptionSearhWord<T>(string searchWord)
	{
		var names = System.Enum.GetNames(typeof(T));
		int length = names.Length;
		
		for (int i=0; i<length; i++)
		{
			string desc = EnumHelper.GetEnumDescription<T>(names[i]);
			if(searchWord == desc)
			{
				return i;
			}
		}
		
		return -1;
	}
}