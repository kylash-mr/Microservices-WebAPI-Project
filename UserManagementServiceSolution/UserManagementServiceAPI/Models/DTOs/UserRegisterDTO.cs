﻿using System.ComponentModel.DataAnnotations;

namespace UserManagementServiceAPI.Models.DTOs
{
    public class UserRegisterDTO
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
    }
}