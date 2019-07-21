using TestBussiness.Entity;
using TestBussiness.ServiceMessage.Builders;

namespace TestBussiness.ServiceMessage.Responses.Factories
{
    public interface IControllerActionItemResponseFactory<TDto, TEntity> 
        where TDto : IDto
        where TEntity : IEntity
    {
        IControllerActionItemResponseFactory<TDto, TEntity> MapDto(TEntity entity);
        IControllerActionItemResponse<TDto> BuildResponse();
        IControllerActionItemResponseFactory<TDto, TEntity> WithResponseCode(string responseCode);
        IControllerActionItemResponseFactory<TDto, TEntity> WithResponseMessage(string responseMessage);
        IControllerActionItemResponse<TDto> NotFound(string message = "entity not found with given identifier(s)");
        IControllerActionItemResponse<TDto> Success(TEntity entity, string responseCode = "0", string responseMessage = "success");
    }
}
