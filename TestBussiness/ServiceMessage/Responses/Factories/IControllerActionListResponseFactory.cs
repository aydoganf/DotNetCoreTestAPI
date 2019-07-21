using System.Collections.Generic;
using TestBussiness.Entity;
using TestBussiness.ServiceMessage.Builders;

namespace TestBussiness.ServiceMessage.Responses.Factories
{
    public interface IControllerActionListResponseFactory<TDto, TEntity>
        where TDto : IDto
        where TEntity : IEntity
    {
        IControllerActionListResponseFactory<TDto, TEntity> MapDtos(IEnumerable<TEntity> entities);
        IControllerActionListResponse<TDto> BuildResponse();
        IControllerActionListResponseFactory<TDto, TEntity> WithResponseCode(string responseCode);
        IControllerActionListResponseFactory<TDto, TEntity> WithResponseMessage(string responseMessage);
        IControllerActionListResponse<TDto> NotFound(string message = "entity list not found with given identifier(s)");
        IControllerActionListResponse<TDto> Success(IEnumerable<TEntity> entities, string responseCode = "0", string responseMessage = "success");
    }
}
