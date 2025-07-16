using ClassLibraryDomain.Entity;
using Microsoft.EntityFrameworkCore;

namespace ClassLibraryData1.Context
{


    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ProdutoEntity> Produtos { get; set; }
        public DbSet<CategoriaEntity> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração para ProdutoEntity
            modelBuilder.Entity<ProdutoEntity>()
                .HasKey(p => p.Id); // Chave primária
            modelBuilder.Entity<ProdutoEntity>()
                .Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(255);
            modelBuilder.Entity<ProdutoEntity>()
                .Property(p => p.PrecoBase)
                .IsRequired()
                .HasColumnType("decimal(18,2)"); // Exemplo de precisão
            modelBuilder.Entity<ProdutoEntity>()
                .Property(p => p.Estoque)
                .IsRequired();

            // Configuração para CategoriaEntity
            modelBuilder.Entity<CategoriaEntity>()
                .HasKey(c => c.Id); // Chave primária
            modelBuilder.Entity<CategoriaEntity>()
                .Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100);

            // Relacionamento N:1 entre Produto e Categoria (Um produto pertence a uma categoria)
            modelBuilder.Entity<ProdutoEntity>()
                .HasOne(p => p.CategoriaEntity) // Produto tem uma Categoria
                .WithMany(c => c.Produtos) // Categoria tem muitos Produtos
                .HasForeignKey(p => p.CategoriaId) // Chave estrangeira em Produto
                .OnDelete(DeleteBehavior.Restrict); // Evita exclusão em cascata (ou escolha Cascade)

            // Indices
            modelBuilder.Entity<ProdutoEntity>()
                .HasIndex(p => p.Nome);
            modelBuilder.Entity<CategoriaEntity>()
                .HasIndex(c => c.Nome)
                .IsUnique(); // Nomes de categorias devem ser únicos
        }
    }
}