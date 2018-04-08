using BookStore.Domain;
using System.Data.Entity.ModelConfiguration;

namespace BookStore.Infra.Mappings
{
    public class BookMap : EntityTypeConfiguration<Book>
    {
        public BookMap()
        {
            ToTable("Book");
            HasKey(x => x.Id);
            Property(x => x.Title).HasMaxLength(160).IsRequired();
            Property(x => x.Price).IsRequired();
            Property(x => x.AcquireDate).IsRequired();

            HasRequired(x => x.Category);


        }
    }
}
