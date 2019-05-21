using AdSite.Models.CRUDModels;
using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdSite.Models.Mappers
{
    public static class WishlistMapper
    {
        public static List<WishlistGridModel> MapToWishlistGridModel(List<Wishlist> entities)
        {
            List<WishlistGridModel> viewModelList = new List<WishlistGridModel>();

            if (entities != null && entities.Count > 0)
            {
                foreach (var entity in entities)
                {
                    viewModelList.Add(
                        new WishlistGridModel()
                        {
                            ID = entity.ID
                        }
                    );
                }
            }

            return viewModelList;
        }

        public static WishlistGridModel MapToWishlistGridModel(Wishlist entity, WishlistAdGridModel ad)
        {
            WishlistGridModel viewModel = new WishlistGridModel();
            viewModel = new WishlistGridModel()
            {
                ID = entity.ID,
                Ad = ad
            };

            return viewModel;
        }

    }

}
