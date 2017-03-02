using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace opieandanthonybot.Collections
{
	public abstract partial class FlexEnum
	{
		protected object baseValue { get; }
		protected string autoFieldName { get; }
		protected int lineNumber { get; }


		public string Name => autoFieldName;

		public static bool IsDefined(Type type, object value)
		{
			// TODO or isinstanceof(?
			if (!typeof(FlexEnum).IsAssignableFrom(type))
				throw new Exception($"Cannot check if isdefined in non-FlexEnum type {type} with val {value}.");

			if (value.GetType().IsInstanceOfType(type))
			{
				return true;
			}

			var name = value as string;
			if (name == null)
				return false;

			var discoveredFields = type.GetFields(discoveryFlags).Select(f => f.GetValue(null));
			return discoveredFields.OfType<FlexEnum>().Any(flexEnumValue => flexEnumValue.autoFieldName == name);
		}

		public static bool TryParse(Type type, string name, out FlexEnum flexEnum)
		{
			try
			{
				flexEnum = Parse(type, name);
				return true;
			}
			catch
			{
				flexEnum = default(FlexEnum);
				return false;
			}
		}

		public static FlexEnum Parse(Type type, string name, bool caseSensitive = true)
		{
			if (!typeof(FlexEnum).IsAssignableFrom(type))
				throw new Exception($"Cannot parse non-FlexEnum type {type} with string {name}.");
			var discoveredFields = type.GetFields(discoveryFlags).Select(f => f.GetValue(null));
			foreach (var flexEnumValue in discoveredFields.OfType<FlexEnum>())
			{
				if (!caseSensitive)
				{
					if (String.Equals(flexEnumValue.autoFieldName, name, StringComparison.CurrentCultureIgnoreCase))
					{
						return flexEnumValue;
					}
				}
				if (flexEnumValue.autoFieldName != name) continue;
				return flexEnumValue;
			}
			throw new Exception($"Cannot parse FlexEnum \"{name}\" in type {type}.");
		}

		protected FlexEnum(object value, string fieldName, int line)
		{
			baseValue = value;
			autoFieldName = fieldName;
			lineNumber = line;
		}

		public int CompareTo(object obj) => String.Compare(autoFieldName, ((FlexEnum)obj).autoFieldName, StringComparison.Ordinal);

		public static bool operator >(FlexEnum left, FlexEnum right)
		{
			return left.lineNumber > right.lineNumber;
		}

		public static bool operator <(FlexEnum left, FlexEnum right)
		{
			return left.lineNumber < right.lineNumber;
		}
	}
	public abstract partial class FlexEnum
	{
		protected static BindingFlags discoveryFlags => BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly;

		protected internal static Dictionary<Type, Array> Cache =
			new Dictionary<Type, Array>();

		
		public static IEnumerable Enumerate(Type enumType)
		{
			if (!Cache.TryGetValue(enumType, out var members))
			{
				members = enumType.GetFields(discoveryFlags)
					.Select(field => field.GetValue(null))
					.ToArray();

				Cache.Add(enumType, members);
			}
			return members;
		}

		public static IEnumerable<TEnum> Enumerate<TEnum>() 
			where TEnum : FlexEnum
		{
			return Enumerate(typeof(TEnum)).Cast<TEnum>();
		}


		//public static IEnumerable<E> Enumerate<E>() where E : FlexEnum =>
		//	typeof(E).GetFields(discoveryFlags).Select(f => f.GetValue(null)).Cast<E>();

		////TODO Fix!! shouldnt be returning defaults if not found!
		//public static TEnum FromValue<TEnum, TValue>(TValue value) where TEnum : FlexEnum
		//{
		//	return Enumerate<TEnum>().FirstOrDefault<>(i => i.baseValue.Equals(value));
		//}

		//public static E FromName<E>(string value) where E : FlexEnum =>
		//	Enumerate<E>().FirstOrDefault<>(i => i.autoFieldName.Equals(value));

	}
	public abstract class FlexEnum<TValue> : FlexEnum, IComparable
	{
		public TValue Value { get; }

		protected FlexEnum(TValue value, string fieldName = null, int line = 0) 
			: base(value, fieldName, line)
		{
			Value = value;
		}

		public override string ToString() => autoFieldName;

		public override bool Equals(object obj)
		{
			var enumeration = obj as FlexEnum<TValue>;
			if (enumeration == null)
				return false;
			return GetType() == obj.GetType() && Value.Equals(enumeration.Value);
		}

		public override int GetHashCode() => autoFieldName.GetHashCode();
	}
}
