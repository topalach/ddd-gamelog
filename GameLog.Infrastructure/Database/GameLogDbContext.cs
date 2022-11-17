using GameLog.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameLog.Infrastructure.Database;

public class GameLogDbContext : DbContext
{
    public GameLogDbContext(DbContextOptions<GameLogDbContext> options) : base(options)
    {
    }
    
    public DbSet<Gamer> Gamers { get; set; }
    public DbSet<Librarian> Librarians { get; set; }
    public DbSet<GameProfile> GameProfiles { get; set; }
    public DbSet<PlayedGame> PlayedGames { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.ApplyConfigurationsFromAssembly(typeof(GamerEntityTypeConfiguration).Assembly);

        modelBuilder.Entity<Gamer>()
            .ToTable(TableNames.Gamers)
            .HasMany(x => x.PlayedGames)
            .WithOne(x => x.Gamer)
            .HasForeignKey(x => x.GamerId);
        
        modelBuilder.Entity<GameProfile>().ToTable(TableNames.GameProfiles);
        modelBuilder.Entity<Librarian>().ToTable(TableNames.Librarians);

        modelBuilder.Entity<PlayedGame>()
            .ToTable(TableNames.PlayedGames)
            .HasOne(x => x.GameProfile)
            .WithMany()
            .HasForeignKey(x => x.GameProfileId);
    }
    
    //init:
    // using (var scope = app.Services.CreateScope())
    // {
    //     var services = scope.ServiceProvider;
    //
    //     var context = services.GetRequiredService<SchoolContext>();
    //     context.Database.EnsureCreated();
    //     // DbInitializer.Initialize(context);
    // }
    
    
    // services.AddDbContextFactory<ApplicationDbContext>(
    // options =>
    //     options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test"));
}