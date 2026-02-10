using System;
using System.IO;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using System.Collections.Generic;
using System.Reflection;
using VideoAssetManager.Models;

namespace VideoAssetManager.Encoding
{
    public static class FilterHelper
    {
        public static IEnumerable<T> ApplyFilters<T>(this IEnumerable<T> source, Rootobject filterObject)
        {
            if (filterObject?.rules == null || !filterObject.rules.Any())
                return source;

            foreach (var rule in filterObject.rules)
            {
                var prop = typeof(T).GetProperty(rule.field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (prop == null) continue;

                var propertyType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                object value = null;
                if (propertyType == typeof(DateTime))
                {
                    DateTime.TryParse(rule.data, out DateTime dt);
                    value = dt;
                }
                else if (propertyType == typeof(bool))
                {
                    bool.TryParse(rule.data, out bool b);
                    value = b;
                }
                else if (propertyType == typeof(int))
                {
                    int.TryParse(rule.data, out int i);
                    value = i;
                }
                else
                {
                    value = rule.data.ToLower().Trim();
                }

                source = source.Where(item =>
                {
                    var itemValue = prop.GetValue(item);
                    if (itemValue == null) return false;

                    if (propertyType == typeof(string))
                    {
                        var strVal = itemValue.ToString().ToLower().Trim();
                        switch (rule.op)
                        {
                            case "cn": return strVal.Contains(value.ToString());
                            case "nc": return !strVal.Contains(value.ToString());
                            case "eq": return strVal == value.ToString();
                            case "ne": return strVal != value.ToString();
                            case "nu": return string.IsNullOrEmpty(strVal);
                            case "nn": return !string.IsNullOrEmpty(strVal);
                        }
                    }
                    else if (propertyType == typeof(DateTime))
                    {
                        var dtVal = (DateTime)itemValue;
                        var dtRuleVal = (DateTime)value;
                        switch (rule.op)
                        {
                            case "eq": return dtVal == dtRuleVal;
                            case "ne": return dtVal != dtRuleVal;
                            case "gt": return dtVal > dtRuleVal;
                            case "lt": return dtVal < dtRuleVal;
                            case "ge": return dtVal >= dtRuleVal;
                            case "le": return dtVal <= dtRuleVal;
                        }
                    }
                    else if (propertyType == typeof(bool))
                    {
                        return itemValue.Equals(value);
                    }
                    else if (propertyType == typeof(int))
                    {
                        var intVal = (int)itemValue;
                        var intRuleVal = (int)value;
                        switch (rule.op)
                        {
                            case "eq": return intVal == intRuleVal;
                            case "ne": return intVal != intRuleVal;
                            case "gt": return intVal > intRuleVal;
                            case "lt": return intVal < intRuleVal;
                            case "ge": return intVal >= intRuleVal;
                            case "le": return intVal <= intRuleVal;
                        }
                    }

                    return true;
                }).ToList();
            }

            return source;
        }
    }

}