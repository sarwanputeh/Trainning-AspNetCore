using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OrixNetCoreApp.Areas.Identity.Data;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser
{
    [Required(ErrorMessage ="ชื่อสกุล ห้ามว่าง")]
    public string FullName { get; set; } = null!;

    public string Photo { get; set; } = "nopic.png";
}

