using System;

namespace BookStore.Domain
{
    public class Book
    {
        public Book()
        {
            this.AcquireDate = DateTime.Now;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime AcquireDate { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
