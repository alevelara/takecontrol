﻿namespace takecontrol.Domain.Messages.Identity;

public sealed class UpdatePasswordRequest
{
    public string Email { get; set; }

    public string NewPassword { get; set; }
}