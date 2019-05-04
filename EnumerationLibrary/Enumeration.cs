using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EnumerationLibrary
{
    [JsonConverter(typeof(EnumerationJsonConverter))]
    public class Enumeration<T> : IEnumerationJson, IComparable
    {
        // Originally based upon the concept described here:
        // https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types

        public T Code { get; protected set; }
        public string Description { get; protected set; }

        public Enumeration()
        { }

        public Enumeration(T code, string description)
        {
            Code = code;
            Description = description;
        }

        public virtual object ReadJson(string jsonValue)
        {
            var type = GetType();
            var instance = Activator.CreateInstance(type);
            var fields = type.GetTypeInfo().GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            foreach (var info in fields)
            {
                var locatedValue = info.GetValue(instance);
                if(locatedValue != null && locatedValue.GetType().IsAssignableFrom(type))
                {
                    var serializedValue = info.FieldType.GetMethod("WriteJson")?.Invoke(locatedValue, null);
                    if (serializedValue != null && ((string)serializedValue).Equals(jsonValue)) return locatedValue;
                }
            }
            return null;
        }

        public virtual string WriteJson()
            => Code.ToString();

        public override string ToString() => Description;

        public static E Parse<E>(T code) where E : Enumeration<T>, new()
            => GetAll<E>()
                .Where(n => 
                    (typeof(T) == typeof(string)) ?
                    (code as string).Equals(n.Code as string, StringComparison.OrdinalIgnoreCase) 
                    : n.Code.Equals(code))
                .FirstOrDefault();

        public static E ParseDescription<E>(string description) where E : Enumeration<T>, new()
            => GetAll<E>()
                .Where(n => n.Description.Equals(description, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

        public static IEnumerable<E> GetAll<E>() where E : Enumeration<T>, new()
        {
            var type = typeof(E);
            var instance = new E();
            var fields = type.GetTypeInfo().GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            foreach(var info in fields)
            {
                var locatedValue = info.GetValue(instance) as E;
                if (locatedValue != null) yield return locatedValue;
            }
        }

        public override bool Equals(object other)
        {
            var otherValue = other as Enumeration<T>;
            if (otherValue == null) return false;
            var typeMatches = GetType().Equals(other.GetType());
            var valueMatches = Code.Equals(otherValue.Code);
            return typeMatches && valueMatches;
        }

        public override int GetHashCode() 
            => Code.GetHashCode();

        public int CompareTo(object other)
            => (other.GetType() != GetType()) ? -1 : CompareTo(other as Enumeration<T>);

    }
}
