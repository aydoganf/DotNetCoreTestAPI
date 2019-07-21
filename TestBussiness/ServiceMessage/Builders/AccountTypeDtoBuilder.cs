using System.Collections.Generic;
using System.Runtime.Serialization;
using TestBussiness.Entity;

namespace TestBussiness.ServiceMessage.Builders
{
    public class AccountTypeDto : IDto
    {
        [IgnoreDataMember]
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string TypeKey { get; set; }
    }

    public class AccountTypeDtoBuilder : IDtoBuilder<AccountTypeDto, AccountType>
    {
        public AccountTypeDto MapToDto(AccountType entity)
        {
            return new AccountTypeDto()
            {
                Id = entity.Id,
                TypeName = entity.TypeName,
                TypeKey = entity.TypeKey
            };
        }

        public IEnumerable<AccountTypeDto> MapToDtoList(IEnumerable<AccountType> entities)
        {
            List<AccountTypeDto> dtos = new List<AccountTypeDto>();
            foreach (var entity in entities)
            {
                dtos.Add(MapToDto(entity));
            }
            return dtos;
        }
    }
}
