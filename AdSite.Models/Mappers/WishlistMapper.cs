using AdSite.Models.CRUDModels;
using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdSite.Models.Mappers
{
    public static class WishlistMapper
    {
        public static List<WishlistViewModel> MapToWishlistViewModel(List<Wishlist> entities)
        {
            List<WishlistViewModel> viewModelList = new List<WishlistViewModel>();

            if (entities != null && entities.Count > 0)
            {
                foreach (var entity in entities)
                {
                    viewModelList.Add(
                        new WishlistViewModel()
                        {
                            ID = entity.ID,
                            AdId = entity.AdId
                        }
                    );
                }
            }

            return viewModelList;
        }
    }
}
