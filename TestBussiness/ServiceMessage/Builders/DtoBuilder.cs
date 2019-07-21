using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.Entity;

namespace TestBussiness.ServiceMessage.Builders
{
    public interface IDto
    {
    }

    public interface IDtoBuilder<TDto, TEntity>
        where TDto : IDto
        where TEntity : IEntity
    {
        TDto MapToDto(TEntity entity);

        IEnumerable<TDto> MapToDtoList(IEnumerable<TEntity> entities);
    }
}
