using AutoMapper;
using Domain.Entites.ProductModule;
using Microsoft.Extensions.Configuration;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    internal class PictureUrlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductResultDto, string>
    {
        public string Resolve(Product source, ProductResultDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl)) return string.Empty;
            return $"{_configuration.GetSection("URLS")["BaseUrl"]}{source.PictureUrl}";
        }
    }
}
