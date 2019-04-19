using System;
using System.Reflection;

namespace AdSite.Mappers
{
    public static class AuditedEntityMapper<T>
    {
        public static void FillCreateAuditedEntityFields(T entity, string currentUser, Guid countryId)
        {
            DateTime currentTime = DateTime.Now;

            SetPropertyInfoValue(entity, currentUser, "CreatedBy");
            SetPropertyInfoValue(entity, currentUser, "ModifiedBy");
            SetPropertyInfoValue(entity, currentTime, "CreatedAt");
            SetPropertyInfoValue(entity, currentTime, "ModifiedAt");
            SetPropertyInfoValue(entity, countryId, "CountryId");
        }

        public static void FillModifyAuditedEntityFields(T entity, string currentUser)
        {
            DateTime currentTime = DateTime.Now;

            SetPropertyInfoValue(entity, currentUser, "ModifiedBy");
            SetPropertyInfoValue(entity, currentTime, "ModifiedAt");
        }

        public static void FillCountryEntityField(T entity, Guid countryId)
        {
            SetPropertyInfoValue(entity, countryId, "CountryId");
        }

        private static void SetPropertyInfoValue(T entity, object value, string fieldName)
        {
            PropertyInfo fieldPropertyInfo = entity.GetType()?.GetProperty(fieldName);
            if (fieldPropertyInfo == null)
                throw new Exception($"Property {fieldName} not found");
            fieldPropertyInfo.SetValue(entity, value);
        }
    }
}
