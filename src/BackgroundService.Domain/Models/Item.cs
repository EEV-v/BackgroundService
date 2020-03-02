using System;
namespace BackgroundService.Domain.Models
{
    public class Item
    {
        public Item(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
