using AutoMapper;
using Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos.Categories
{
    [AutoMap(typeof(Category))]
    public class BaseCategoryDto
    {
        public string Name { get; set; }

    }
}
