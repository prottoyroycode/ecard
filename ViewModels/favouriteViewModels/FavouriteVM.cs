using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.favouriteViewModels
{
    public class FavouriteVM
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Sku { get; set; }
        public string ImageUrlPath { get; set; }
    }
    public class RemoveFavouriteVM
    {
        public Guid UserId { get; set; }
        public int Sku { get; set; }
    }
}
