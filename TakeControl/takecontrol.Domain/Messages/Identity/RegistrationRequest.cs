﻿using takecontrol.Domain.Models.ApplicationUser.Enum;

namespace takecontrol.Domain.Messages.Identity;

public sealed class RegistrationRequest
{
    public string Name { get; private set; } = String.Empty;
    public string Email { get; private set; } = String.Empty;    
    public string Password { get; private set; } = String.Empty;
    public UserType UserType { get; private set; } 

    public RegistrationRequest(string name, string email, string password, UserType userType)
    {
        Name = name;
        Email = email;
        Password = password;
        UserType = userType;
    }
}
