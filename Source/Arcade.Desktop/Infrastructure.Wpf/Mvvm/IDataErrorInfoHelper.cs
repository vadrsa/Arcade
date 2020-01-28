using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mvvm
{
	public static class IDataErrorInfoHelper
	{
		public static bool HasErrors<TOwner>(TOwner owner, int deep = 2, params Expression<Func<TOwner, object>>[] excludedProperties)
		{
			var exProperties = excludedProperties.Select(x => ExpressionHelper.GetPropertyName(x)).ToList();
			Func<PropertyDescriptor, bool> propertyFilter = x => !exProperties.Contains(x.Name);
			return HasErrors((IDataErrorInfo)owner, false, deep, propertyFilter);
		}
		public static bool HasErrors(IDataErrorInfo owner, bool ignoreOwnerError, int deep = 2, Func<PropertyDescriptor, bool> propertyFilter = null)
		{
			if (owner == null) throw new ArgumentNullException("owner");
			if (--deep < 0) return false;
			if (propertyFilter == null) propertyFilter = x => true;
			var properties = TypeDescriptor.GetProperties(owner).Cast<PropertyDescriptor>().Where(propertyFilter);
			var errorProperty = properties.FirstOrDefault(p => p.Name == "Error");
			bool hasImplicitImplementation = ExpressionHelper.PropertyHasImplicitImplementation(owner, o => o.Error, false);
			if (errorProperty != null && hasImplicitImplementation)
			{
				properties = properties.Except(new[] { errorProperty });
			}
			bool propertiesHaveError = properties.Any(p => PropertyHasError(owner, p, deep));
			return propertiesHaveError || (!ignoreOwnerError && !string.IsNullOrEmpty(owner.Error));
		}
		static bool PropertyHasError(IDataErrorInfo owner, PropertyDescriptor property, int deep)
		{
			string simplePropertyError = owner[property.Name];
			if (!string.IsNullOrEmpty(simplePropertyError)) return true;
			object propertyValue;
			if (!TryGetPropertyValue(owner, property.Name, out propertyValue))
				return false;
			IDataErrorInfo nestedDataErrorInfo = propertyValue as IDataErrorInfo;
			return nestedDataErrorInfo != null && HasErrors(nestedDataErrorInfo, deep);
		}
		static string GetNestedPropertyErrorText(object owner, string path, int pathDelimiterIndex)
		{
			string propertyName = path.Remove(pathDelimiterIndex);
			object propertyValue;
			if (!TryGetPropertyValue(owner, propertyName, out propertyValue))
				return string.Empty;
			IDataErrorInfo nestedDataErrorInfo = propertyValue as IDataErrorInfo;
			if (nestedDataErrorInfo == null)
				return string.Empty;
			return nestedDataErrorInfo[path.Substring(pathDelimiterIndex + 1, path.Length - pathDelimiterIndex - 1)];
		}
		static bool TryGetPropertyValue(object owner, string propertyName, out object propertyValue)
		{
			propertyValue = null;
			PropertyInfo pi = owner.GetType().GetProperty(propertyName);
			if (pi == null)
				return false;
			MethodInfo getter = pi.GetGetMethod();
			if (getter == null)
				return false;
			propertyValue = getter.Invoke(owner, null);
			return true;
		}
	}
}
