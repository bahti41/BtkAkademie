namespace Book.APı.Repository.Configur

{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book { Id = 1, Title = "Cin Ali", Price = 100 },
                new Book { Id = 2, Title = "İkna Oyunları", Price = 200 },
                new Book { Id = 3, Title = "Kırk Haramiler", Price = 150 }
                );
        }
    }
}
