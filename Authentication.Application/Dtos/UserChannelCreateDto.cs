using System.ComponentModel.DataAnnotations;
using Authentication.Domain.Entities;
using Authentication.Domain.Enums;

namespace Authentication.Application.Dtos;

public class UserChannelCreateDto
{
    [Required] public Channel Channel { get; set; }

    [Required] public required string Value { get; set; }

    [Required] public bool IsDefault { get; set; }

    [Required] public User? User { get; set; }
}