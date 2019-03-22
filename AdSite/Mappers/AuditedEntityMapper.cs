using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AdSite.Mappers
{
    public static class AuditedEntityMapper<T>
    {
        public static void FillCreateAuditedEntityFields(T entity, string currentUser)
        {
            DateTime currentTime = DateTime.Now;
            Guid countryId = new Guid("9B0CBFD6-0070-4285-B353-F13189BD2291");

            Type entityType = entity.GetType();

            PropertyInfo fieldPropertyInfo = entityType.GetProperty("CreatedBy");
            if (fieldPropertyInfo == null)
                throw new Exception(string.Format("Property CreatedBy not found"));
            fieldPropertyInfo.SetValue(entity, currentUser, null);

            fieldPropertyInfo = entityType.GetProperty("ModifiedBy");
            if (fieldPropertyInfo == null)
                throw new Exception(string.Format("Property ModifiedBy not found"));
            fieldPropertyInfo.SetValue(entity, currentUser, null);

            fieldPropertyInfo = entityType.GetProperty("CreatedAt");
            if (fieldPropertyInfo == null)
                throw new Exception(string.Format("Property CreatedAt not found"));
            fieldPropertyInfo.SetValue(entity, currentTime, null);

            fieldPropertyInfo = entityType.GetProperty("ModifiedAt");
            if (fieldPropertyInfo == null)
                throw new Exception(string.Format("Property ModifiedAt not found"));
            fieldPropertyInfo.SetValue(entity, currentTime, null);

            fieldPropertyInfo = entityType.GetProperty("CountryId");
            if (fieldPropertyInfo == null)
                throw new Exception(string.Format("Property CountryId not found"));
            fieldPropertyInfo.SetValue(entity, countryId, null);
        }

        public static void FillModifyAuditedEntityFields(T entity, string currentUser)
        {
            DateTime currentTime = DateTime.Now;

            Type entityType = entity.GetType();
            PropertyInfo fieldPropertyInfo = entityType.GetProperty("ModifiedBy");
            if (fieldPropertyInfo == null)
                throw new Exception(string.Format("Property not found"));
            fieldPropertyInfo.SetValue(entity, currentUser, null);

            fieldPropertyInfo = entityType.GetProperty("ModifiedAt");
            if (fieldPropertyInfo == null)
                throw new Exception(string.Format("Property not found"));
            fieldPropertyInfo.SetValue(entity, currentTime, null);
        }

    }
}
