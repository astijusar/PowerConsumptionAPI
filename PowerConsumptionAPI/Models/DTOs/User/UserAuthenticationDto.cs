﻿using System.ComponentModel.DataAnnotations;

namespace PowerConsumptionAPI.Models.DTOs.User
{
    public class UserAuthenticationDto
    {
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Password name is required")]
        public string Password { get; set; }
    }
}
