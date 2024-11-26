using Microsoft.EntityFrameworkCore;
using ApiGateway.Models;

namespace ApiGateway.Data;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options): base(options)
    {
    }

    public DbSet<MissionViewModel> Missions { get; set; }
    public DbSet<StaffViewModel> Staffs { get; set; }
    public DbSet<UserViewModel> Users { get; set; }
    public DbSet<RankViewModel> Ranks { get; set; }
    public DbSet<MapShapeViewModel> MapShapes { get; set; }
    public DbSet<MissionAllocationViewModel> MissionAllocations { get; set; }
}