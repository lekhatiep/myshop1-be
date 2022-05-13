using AutoMapper;
using Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos.Orders
{
    [AutoMap(typeof(Order))]
    public class BaseOrder
    {
    }
}
