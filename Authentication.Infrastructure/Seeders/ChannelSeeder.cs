using Authentication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Seeders;

public class ChannelSeeder : Seeder
{
    public ChannelSeeder(ModelBuilder modelBuilder, int count = 1) : base(modelBuilder, count)
    {
    }

    public override void Seed()
    {
        foreach (var channel in (Domain.Enums.Channel[])Enum.GetValues(typeof(Domain.Enums.Channel)))
        {
            ModelBuilder.Entity<Channel>().HasData(
                new Channel { Name = channel.ToString() }
            );
        }
    }
}