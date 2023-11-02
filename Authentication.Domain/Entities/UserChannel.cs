﻿using Authentication.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Domain.Entities;

[Index(nameof(Channel),nameof(Value), IsUnique = true)]
[Index(nameof(User),nameof(Value), IsUnique = true)]
public class UserChannel : BaseEntity
{
    public Channel Channel { get; set; }
    public required string Value { get; set; }
    public bool IsDefault { get; set; }
    
    public User? User { get; set; }
    public Verification? Verification { get; set; }
    
    public ICollection<PasswordReset> PasswordResets { get; set; } = new List<PasswordReset>();
    public ICollection<Login> Logins { get; set; } = new List<Login>();
}