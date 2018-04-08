using BookStore.Domain;
using BookStore.Infra.Mappings;
using System.Data.Entity;

namespace BookStore.Infra.DataContexts
{
    public class BookStoreDataContext : DbContext
    {
        public BookStoreDataContext()
            : base("BookStoreConnectionString")
        {
            //Database.SetInitializer<BookStoreDataContext>(new BookStoreDataContextInitializer());
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
        public IDbSet<Book> Books { get; set; }
        public IDbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BookMap());
            modelBuilder.Configurations.Add(new CategoryMap());

            base.OnModelCreating(modelBuilder);
        }
    }
    public class BookStoreDataContextInitializer : DropCreateDatabaseIfModelChanges<BookStoreDataContext>
    {
        protected override void Seed(BookStoreDataContext context)
        {
            base.Seed(context);
        }
    }
}
