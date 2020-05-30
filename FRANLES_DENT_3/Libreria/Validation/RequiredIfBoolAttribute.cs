using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FRANLES_DENT_3.Libreria.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredIfBoolAttribute : ValidationAttribute, IClientModelValidator
    {
        private string PropertyName { get; set; }
        //private readonly RequiredAttribute requiredAttribute;

        public RequiredIfBoolAttribute(string propertyName)
        {
            PropertyName = propertyName;
            //  requiredAttribute = new RequiredAttribute();
            ErrorMessage = "The asdasdasd field is required.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            object instance = context.ObjectInstance;
            Type type = instance.GetType();

            bool.TryParse(type.GetProperty(PropertyName).GetValue(instance)?.ToString(), out bool propertyValue);

            // !requiredAttribute.IsValid(value)
            if (propertyValue && string.IsNullOrEmpty(value?.ToString()))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-requiredifbool", FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
            MergeAttribute(context.Attributes, "data-val-requiredifbool-valuebool", context.Attributes.FirstOrDefault(f => f.Key == "id").Value.Replace(context.ModelMetadata.PropertyName, "") + PropertyName);
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