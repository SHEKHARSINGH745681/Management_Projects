using System;
using System.ComponentModel.DataAnnotations;

namespace Book_Management.Validation
{
    public class CapitalizeEachWordAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is string title)
            {
                // Check if every word starts with an uppercase letter
                var words = title.Split(' ');
                return words.All(word => !string.IsNullOrEmpty(word) && char.IsUpper(word[0]));
            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must have each word starting with a capital letter.";
        }
    }
}


