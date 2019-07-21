using StructureMap;
using System.Collections.Generic;
using TestBussiness.Entity;
using TestBussiness.ServiceMessage.Builders;

namespace TestBussiness.ServiceMessage.Responses.Factories
{
    public class ControllerActionListResponseFactory<TDto, TEntity>
        : IControllerActionListResponseFactory<TDto, TEntity>
            where TDto : IDto
            where TEntity : IEntity
    {
        private readonly IDtoBuilder<TDto, TEntity> dtoBuilder;
        private readonly IContainer container;
        private readonly IControllerActionListResponse<TDto> response;

        public ControllerActionListResponseFactory(IContainer container)
        {
            this.container = container;
            dtoBuilder = container.GetInstance<IDtoBuilder<TDto, TEntity>>();
            response = container.GetInstance<IControllerActionListResponse<TDto>>();
        }

        public IControllerActionListResponse<TDto> BuildResponse()
        {
            return response;
        }

        public IControllerActionListResponseFactory<TDto, TEntity> MapDtos(IEnumerable<TEntity> entities)
        {
            response.Result = dtoBuilder.MapToDtoList(entities);
            return this;
        }

        public IControllerActionListResponse<TDto> NotFound(string message = "entity list not found with given identifier(s)")
        {
            return WithResponseCode("1").WithResponseMessage(message).BuildResponse();
        }

        public IControllerActionListResponse<TDto> Success(IEnumerable<TEntity> entities, string responseCode = "0", string responseMessage = "success")
        {
            return MapDtos(entities).WithResponseCode(responseCode).WithResponseMessage(responseMessage).BuildResponse();
        }

        public IControllerActionListResponseFactory<TDto, TEntity> WithResponseCode(string responseCode)
        {
            response.ResponseCode = responseCode;
            return this;
        }

        public IControllerActionListResponseFactory<TDto, TEntity> WithResponseMessage(string responseMessage)
        {
            response.ResponseMessage = responseMessage;
            return this;
        }
    }
}
