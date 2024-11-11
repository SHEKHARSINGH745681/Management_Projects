using System;
using System.ComponentModel.DataAnnotations;
using Book_Management.Validation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Book_Management.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required]                   // necessary
        [MinLength(5)]               // Min lenght of String 5 char
        [MaxLength(20)]              // Max lenght of String 20 char
        [CapitalizeEachWord]         // Each Word First letter Should be Capital
        public required string Title { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        [CapitalLetterAuthor]
        public required string Author { get; set; }

        [Required]
        [ValidPhoneNumber]            // Validates if the phone number is in a valid phone format
        [MaxLength(13)]               // You can adjust the max length based on the format (e.g., international phone numbers)
        [MinLength(13)]               // 13 number bcz contain +91(3 char more)
        public required string PhoneNumber { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal price { get; set; }

        public required DateTime PublishedDate { get; set; }


    }
}

