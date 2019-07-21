using System.Collections.Generic;
using TestBussiness.ServiceMessage.Builders;

namespace TestBussiness.ServiceMessage.Responses
{
    public class AccountDtoListResponse : ControllerActionResponse, IControllerActionListResponse<AccountDto>
    {
        public IEnumerable<AccountDto> Result { get; set; }
    }
}
