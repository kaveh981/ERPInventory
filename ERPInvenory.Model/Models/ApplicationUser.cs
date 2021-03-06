﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Objects;
using System.Security.Claims;
using System.Threading.Tasks;


namespace ERPInventory.Model.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public byte Level { get; set; }
        [Required]
        public DateTime JoinDate { get; set; }

        public string Secret { get; set; }
 
        public bool Active { get; set; }

        public string AllowedOrigin { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

    }
}
