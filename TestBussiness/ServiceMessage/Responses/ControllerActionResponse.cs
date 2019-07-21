using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.ServiceMessage.Builders;

namespace TestBussiness.ServiceMessage.Responses
{
    public interface IControllerActionResponse
    {
        string ResponseCode { get; set; }
        string ResponseMessage { get; set; }
    }
    public interface IControllerActionItemResponse<TDto> : IControllerActionResponse where TDto : IDto
    {
        TDto Result { get; set; }
    }
    public interface IControllerActionListResponse<TDto> : IControllerActionResponse where TDto : IDto
    {
        IEnumerable<TDto> Result { get; set; }
    }

    public class ControllerActionResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
    }
}
