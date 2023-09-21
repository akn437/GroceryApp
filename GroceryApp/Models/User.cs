using System;
using System.Collections.Generic;

namespace GroceryApp.Models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string MobileNo { get; set; } = null!;

    public string EmailId { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? Password { get; set; }
}
