using AdSite.Models.CRUDModels;
using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdSite.Models.Mappers
{
    public static class UserRoleCountryMapper
    {
        public static List<UserRoleCountryGridModel> MapToViewModel(List<UserRoleCountry> tuples)
        {
            var listViewModel = new List<UserRoleCountryGridModel>();
            if (tuples != null && tuples.Count > 0)
            {
                foreach (UserRoleCountry urc in tuples)
                {
                    var viewModel = new UserRoleCountryGridModel();

                    viewModel.CountryId = urc.CountryId;
                    viewModel.ApplicationUserId = urc.ApplicationUserId;
                    viewModel.RoleId = urc.ApplicationIdentityRoleId;

                    listViewModel.Add(viewModel);
                }
            }
            return listViewModel;
        }

    }
}
