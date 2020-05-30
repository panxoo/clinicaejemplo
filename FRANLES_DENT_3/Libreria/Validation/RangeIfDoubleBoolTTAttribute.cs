using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FRANLES_DENT_3.Libreria.Validation
{
    public class RangeIfDoubleBoolTTAttribute : ValidationAttribute, IClientModelValidator
    {
        private string PropertyNameBool { get; set; }
        private string PropertyNameBool2 { get; set; }
        private int Min { get; set; }
        private int Max { get; set; }
        private readonly RangeAttribute rangeAttribute;

        public RangeIfDoubleBoolTTAttribute(string propertyNameBool, string propertyNameBool2, int min, int max)
        {
            PropertyNameBool = propertyNameBool;
            PropertyNameBool2 = propertyNameBool2;
            Min = min;
            Max = max;
            rangeAttribute = new RangeAttribute(Min, Max);
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            object instance = context.ObjectInstance;
            Type type = instance.GetType();

            bool.TryParse(type.GetProperty(PropertyNameBool).GetValue(instance)?.ToString(), out bool propertyValue);
            bool.TryParse(type.GetProperty(PropertyNameBool2).GetValue(instance)?.ToString(), out bool propertyValue2);

            if (propertyValue && propertyValue2 && !rangeAttribute.IsValid(value))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-rangeifdoblebooltt", FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
            MergeAttribute(context.Attributes, "data-val-rangeifdoblebooltt-valuebool", context.Attributes.FirstOrDefault(f => f.Key == "id").Value.Replace(context.ModelMetadata.PropertyName, "") + PropertyNameBool);
            MergeAttribute(context.Attributes, "data-val-rangeifdoblebooltt-valuebool2", context.Attributes.FirstOrDefault(f => f.Key == "id").Value.Replace(context.ModelMetadata.PropertyName, "") + PropertyNameBool2);
            MergeAttribute(context.Attributes, "data-val-rangeifdoblebooltt-min", Min.ToString());
            MergeAttribute(context.Attributes, "data-val-rangeifdoblebooltt-max", Max.ToString());
        }

        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;
        }
    }
}