﻿using Authentication.Domain.Enums;

namespace Authentication.Domain.Entities;

public class UserChannel : SafeDeleteEntity
{
    public Channel Channel { get; set; }
    public required string Value { get; set; }
    public bool IsDefault { get; set; }
    public int? UserId { get; set; }
    public User? User { get; set; }
    
    public int? VerificationId { get; set; }
    public ICollection<Verification> Verifications { get; set; } = new List<Verification>();

    public ICollection<PasswordReset> PasswordResets { get; set; } = new List<PasswordReset>();
    public ICollection<Login> Logins { get; set; } = new List<Login>();
}