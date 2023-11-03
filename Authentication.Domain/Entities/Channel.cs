using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Domain.Entities;

[Table("Channels")]
public class Channel
{
    public required int Id { get; set; }
    public required string Name { get; set; }
}