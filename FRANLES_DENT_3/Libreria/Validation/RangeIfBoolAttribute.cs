using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FRANLES_DENT_3.Libreria.Validation
{
    public class RangeIfBoolAttribute : ValidationAttribute, IClientModelValidator
    {
        private string PropertyNameBool { get; set; }
        private int Min { get; set; }
        private int Max { get; set; }
        private bool Opc { get; set; }
        private readonly RangeAttribute rangeAttribute;

        public RangeIfBoolAttribute(string propertyNameBool, int min, int max, bool opc)
        {
            PropertyNameBool = propertyNameBool;
            Min = min;
            Max = max;
            Opc = opc;
            rangeAttribute = new RangeAttribute(Min, Max);
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            object instance = context.ObjectInstance;
            Type type = instance.GetType();

            bool.TryParse(type.GetProperty(PropertyNameBool).GetValue(instance)?.ToString(), out bool propertyValue);

            if (!rangeAttribute.IsValid(value) && propertyValue == Opc)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-rangeifbool", FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
            MergeAttribute(context.Attributes, "data-val-rangeifbool-valuebool", context.Attributes.FirstOrDefault(f => f.Key == "id").Value.Replace(context.ModelMetadata.PropertyName, "") + PropertyNameBool);
            MergeAttribute(context.Attributes, "data-val-rangeifbool-min", Min.ToString());
            MergeAttribute(context.Attributes, "data-val-rangeifbool-max", Max.ToString());
            MergeAttribute(context.Attributes, "data-val-rangeifbool-opc", Opc.ToString());
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