﻿namespace Authentication.Domain.Entities;

public class Login : BaseEntity
{
    public required string Ip { get; set; }
    public bool IsSuccess { get; set; }
    
    public int UserChannelId { get; set; }
    public UserChannel? UserChannel { get; set; }
}