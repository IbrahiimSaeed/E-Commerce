using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.BasketModule
{
    public record BasketDto
    {
        public string Id { get; init; }
        public ICollection<BasketItemDto> BasketItems { get; init; } = [];
    }
}
