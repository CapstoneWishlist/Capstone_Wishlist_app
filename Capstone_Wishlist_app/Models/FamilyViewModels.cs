﻿using System.ComponentModel.DataAnnotations;

namespace Capstone_Wishlist_app.Models {
    public class RegisterFamilyModel {
        [Required]
        [Display(Name = "Guardian's First Name")]
        public string ParentFirstName { get; set; }

        [Required]
        [Display(Name = "Guardian's Last Name")]
        public string ParentLastName { get; set; }

        [Display(Name = "Shipping Address")]
        public CreateAddressModel ShippingAddress { get; set; }

        [Required]
        [Display(Name = "Use Charity Address")]
        public bool IsShippingToCharity { get; set; }

        [Phone]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }

    public class CreateAddressModel {
        [Display(Name = "Address Line 1", ShortName = "Line 1")]
        public string LineOne { get; set; }

        [Display(Name = "Address Line 2", ShortName = "Line 2")]
        public string LineTwo { get; set; }

        [Display(Name = "City", ShortName = "City")]
        public string City { get; set; }

        [Display(Name = "State or Territory", ShortName = "State")]
        public string State { get; set; }

        [Display(Name = "Postal Code", ShortName = "Zip")]
        public string PostalCode { get; set; }
    }

    public class RegisterChildModel {
        [Required]
        public int FamilyId { get; set; }

        public string FamilyName { get; set; }

        [Required]
        [Display(Name = "Child's First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Child's Last Name")]
        public string LastName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        [RegularExpression("[mMfFuU]", ErrorMessage="Please enter gender as 'm' for male, 'f' for female, or 'u' for unspecified.")]
        public char Gender { get; set; }
    }

    public class RegisteredFamilyViewModel {
        public int FamilyId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class FamilyCredentials {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}