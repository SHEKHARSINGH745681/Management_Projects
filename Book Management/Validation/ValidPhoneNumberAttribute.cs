using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Book_Management.Validation
{
    public class ValidPhoneNumberAttribute : ValidationAttribute
    {
        // Constructor to set a custom error message
        public ValidPhoneNumberAttribute() : base("Phone number must start with +91 and " +
            "be followed by a valid number (starting with 9, 8, 7, or 6).")
        {
        }

        // Override the IsValid method to implement custom logic
        public override bool IsValid(object? value)
        {
            if (value == null) return false;

            var phoneNumber = value.ToString();

            if (phoneNumber == null)
                return false;
            var regex = new Regex(@"^\+91[9876]\d{9}$");
            // Matches +91 followed by 9, 8, 7, or 6 and then exactly 9 digits

            return regex.IsMatch(phoneNumber);
        }
    }
}

