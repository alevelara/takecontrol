﻿namespace Takecontrol.Credential.Domain.Messages.Identity;

public sealed record class UpdatePasswordRequest(string Email, string NewPassword);
