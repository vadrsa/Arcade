using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Infrastructure.ChangeTracking
{

    /// <summary>
    /// A wrapper over any class to make it Restorable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Memento<T>
    {
        #region Private

        /// <summary>
        /// Dictionary keeping track of old values
        /// If object is referance type referance is stored
        /// </summary>
        Dictionary<PropertyInfo, object> storedProperties =
                   new Dictionary<PropertyInfo, object>();

        #endregion

        public Memento(T originator)
        {
            // Get all properties
            var propertyInfos =
                typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                       .Where(p => p.CanRead && p.CanWrite);

            // store properties
            foreach (var property in propertyInfos)
            {
                this.storedProperties[property] = property.GetValue(originator, null);
            }
        }

        public void Restore(T originator)
        {
            foreach (var pair in this.storedProperties)
            {
                // If property is IEditableObject call CancelEdit on it
                // else just set the old value
                if (typeof(IEditableObject).IsAssignableFrom(pair.Key.PropertyType) && pair.Value != null)
                {
                    IEditableObject editableObject = (IEditableObject)pair.Value;
                    editableObject?.CancelEdit();
                }
                else
                {
                    pair.Key.SetValue(originator, pair.Value, null);
                }
            }
        }
    }
}
