using AutoMapper;
using EventBus.Message.Events;
using Ordering.Application.Features.Orders.Commands.CheckOutOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Mappings
{
   class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<BasketCheckOutEvent, CheckoutOrderCommand>().ReverseMap();
        }
    }
}
