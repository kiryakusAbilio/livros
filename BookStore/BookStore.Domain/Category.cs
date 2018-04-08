using System;

namespace BookStore.Domain
{
    public class Category
    {
        public Category()
        {
            this.AcquireDate = DateTime.Now;
        }
        public int Id { get; set; }
        public string Title { get; set; }       
        public DateTime AcquireDate { get; set; }
        public bool IsActive { get; set; }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
