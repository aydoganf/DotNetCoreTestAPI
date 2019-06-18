using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using TestBussiness.Entity;

namespace TestBussiness.ServiceMessage
{
    public class AccountTypeInfo
    {
        [IgnoreDataMember]
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string TypeKey { get; set; }
    }

    public class AccountTypeInfoBuilder
    {
        public static AccountTypeInfo MapToInfoInstance(AccountType entity)
        {
            return new AccountTypeInfo()
            {
                Id = entity.Id,
                TypeName = entity.TypeName,
                TypeKey = entity.TypeKey
            };
        }

        public static List<AccountTypeInfo> MapToInfoList(IEnumerable<AccountType> entities)
        {
            List<AccountTypeInfo> dtos = new List<AccountTypeInfo>();
            foreach (var entity in entities)
            {
                dtos.Add(AccountTypeInfoBuilder.MapToInfoInstance(entity));
            }
            return dtos;
        }
    }
}
