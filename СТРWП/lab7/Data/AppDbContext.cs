using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<LinkModel> Links { get; set; }
    public DbSet<CommentModel> Comments { get; set; } 

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Укажите строку подключения к базе данных
        optionsBuilder.UseSqlServer("Server=N1CE\\FLOPPA;Database=asp7lab;Trusted_Connection=True;");
    }

    //protected AppDbContext()
    //{
    //    Database.EnsureCreated();
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Настройка связи между ссылками и комментариями
        modelBuilder.Entity<CommentModel>()
            .HasOne(c => c.Link)
            .WithMany(l => l.Comments)
            .HasForeignKey(c => c.LinkId)
            .OnDelete(DeleteBehavior.Cascade); // При удалении ссылки удаляются связанные комментарии
    }
}
