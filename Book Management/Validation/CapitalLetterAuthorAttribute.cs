using System;
using System.ComponentModel.DataAnnotations;

namespace Book_Management.Validation
{
    public class CapitalLetterAuthorAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is string author)
            {
                // Check if the first letter is uppercase
                return char.IsUpper(author[0]);
            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must start with a capital letter.";
        }
    }
}

