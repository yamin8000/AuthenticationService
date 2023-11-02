using System.ComponentModel.DataAnnotations;
using Authentication.Domain.Entities;

namespace Authentication.Application.Dtos;

public class UserChannelCreateDto
{
    [Required] public Channel Channel { get; set; }

    [Required] public string Value { get; set; }
}