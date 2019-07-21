using TestBussiness.ServiceMessage.Builders;

namespace TestBussiness.ServiceMessage.Responses
{
    public class AccountDtoItemResponse : ControllerActionResponse, IControllerActionItemResponse<AccountDto>
    {
        public AccountDto Result { get; set; }
    }
}
