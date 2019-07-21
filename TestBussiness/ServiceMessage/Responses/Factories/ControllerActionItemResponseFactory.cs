using StructureMap;
using TestBussiness.Entity;
using TestBussiness.ServiceMessage.Builders;

namespace TestBussiness.ServiceMessage.Responses.Factories
{
    public class ControllerActionItemResponseFactory<TDto, TEntity>
        : IControllerActionItemResponseFactory<TDto, TEntity>
            where TDto : IDto
            where TEntity : IEntity
    {
        private readonly IDtoBuilder<TDto, TEntity> dtoBuilder;
        private readonly IContainer container;
        private readonly IControllerActionItemResponse<TDto> response;

        public ControllerActionItemResponseFactory(IContainer container)
        {
            this.container = container;
            dtoBuilder = container.GetInstance<IDtoBuilder<TDto, TEntity>>();
            response = container.GetInstance<IControllerActionItemResponse<TDto>>();
        }

        public IControllerActionItemResponse<TDto> BuildResponse()
        {
            return response;
        }

        public IControllerActionItemResponseFactory<TDto, TEntity> MapDto(TEntity entity)
        {
            response.Result = dtoBuilder.MapToDto(entity);
            return this;
        }

        public IControllerActionItemResponse<TDto> NotFound(string message = "entity not found with given identifier")
        {
            return WithResponseCode("1").WithResponseMessage(message).BuildResponse();
        }

        public IControllerActionItemResponse<TDto> Success(TEntity entity, string responseCode = "0", string responseMessage = "success")
        {
            return MapDto(entity).WithResponseCode(responseCode).WithResponseMessage(responseMessage).BuildResponse();
        }

        public IControllerActionItemResponseFactory<TDto, TEntity> WithResponseCode(string responseCode)
        {
            response.ResponseCode = responseCode;
            return this;
        }

        public IControllerActionItemResponseFactory<TDto, TEntity> WithResponseMessage(string responseMessage)
        {
            response.ResponseMessage = responseMessage;
            return this;
        }
    }
}
