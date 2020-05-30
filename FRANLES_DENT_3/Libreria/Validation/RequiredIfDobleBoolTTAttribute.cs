using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FRANLES_DENT_3.Libreria.Validation
{
    public class RequiredIfDobleBoolTTAttribute : ValidationAttribute, IClientModelValidator
    {
        private string PropertyName { get; set; }
        private string PropertyName2 { get; set; }

        //private readonly RequiredAttribute requiredAttribute;

        public RequiredIfDobleBoolTTAttribute(string propertyName, string propertyName2)
        {
            PropertyName = propertyName;
            PropertyName2 = propertyName2;
            //requiredAttribute = new RequiredAttribute();
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            object instance = context.ObjectInstance;
            Type type = instance.GetType();

            bool.TryParse(type.GetProperty(PropertyName).GetValue(instance)?.ToString(), out bool propertyValue);
            bool.TryParse(type.GetProperty(PropertyName2).GetValue(instance)?.ToString(), out bool propertyValue2);

            if (propertyValue && propertyValue2 && !string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-requiredifdoblebooltt", FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
            MergeAttribute(context.Attributes, "data-val-requiredifdoblebooltt-valuebool", context.Attributes.FirstOrDefault(f => f.Key == "id").Value.Replace(context.ModelMetadata.PropertyName, "") + PropertyName);
            MergeAttribute(context.Attributes, "data-val-requiredifdoblebooltt-valuebool2", context.Attributes.FirstOrDefault(f => f.Key == "id").Value.Replace(context.ModelMetadata.PropertyName, "") + PropertyName2);
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