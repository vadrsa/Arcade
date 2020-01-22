using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Linq;

namespace Infrastructure.Utility
{
    public static class INotifyPropertyChangedExtensions
    {
        public static IObservable<EventPattern<PropertyChangedEventArgs>> WhenPropertyChanges<T, TProperty>(
                this T source,
                Expression<Func<T, TProperty>> propertyExpression)
            where T : INotifyPropertyChanged
        {
            var memberExpression = propertyExpression.Body as MemberExpression;
            return memberExpression == null
                ? Observable.Empty<EventPattern<PropertyChangedEventArgs>>()
                : Observable
                    .FromEventPattern<PropertyChangedEventArgs>(source, "PropertyChanged")
                    .Where(e => e.EventArgs.PropertyName == memberExpression.Member.Name);
        }

        public static IObservable<EventPattern<PropertyChangedEventArgs>> FromPropertyChangedPattern(this INotifyPropertyChanged source)
        {
            return Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                x => source.PropertyChanged += x,
                x => source.PropertyChanged -= x);
        }

        public static IObservable<EventPattern<PropertyChangedEventArgs>> WhenAnyPropertyChanges<T, TProperty>(this T source, params Expression<Func<T, TProperty>>[] properties)
            where T : INotifyPropertyChanged
        {
            return WhenAnyPropertyChanges(source, FromPropertyChangedPattern(source), properties);
        }

        public static IObservable<EventPattern<PropertyChangedEventArgs>> WhenAnyPropertyChanges<T, TProperty>(this T source, IObservable<EventPattern<PropertyChangedEventArgs>> observable, params Expression<Func<T, TProperty>>[] properties)
            where T : INotifyPropertyChanged
        {
            return WhenAnyPropertyChanges<T, TProperty>(observable, properties);
        }

        public static IObservable<EventPattern<PropertyChangedEventArgs>> WhenAnyPropertyChanges<T, TProperty>(this IObservable<EventPattern<PropertyChangedEventArgs>> source, params Expression<Func<T, TProperty>>[] properties)
        {
            return WhenAnyPropertyEventOccurs(source, x => x.EventArgs.PropertyName, properties);
        }

        public static IObservable<TObservable> WhenAnyPropertyEventOccurs<T, TProperty, TObservable>(this IObservable<TObservable> source, Func<TObservable, string> propertyNameSelector, params Expression<Func<T, TProperty>>[] properties)
        {
            var propertyNames = properties
                .Select(x => x.Body)
                .OfType<MemberExpression>()
                .Select(x => x.Member.Name);

            return source
                .Where(x => propertyNames.Any(y => EqualityComparer<string>.Default.Equals(y, propertyNameSelector(x))));
        }
    }
}
