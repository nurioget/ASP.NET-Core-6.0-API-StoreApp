using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Models;

namespace WebApi.Repositories.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book { Id = 1, Title = "suç ve ceza", Price = 30 },
                new Book { Id = 2, Title = "mesneviden dersler", Price = 356 },
                new Book { Id = 3, Title = "Devlet", Price = 42 }
             );
        }

    }
}
