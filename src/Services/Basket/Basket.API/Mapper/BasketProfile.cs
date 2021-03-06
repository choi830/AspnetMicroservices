using AutoMapper;
using Basket.API.Entities;
using EventBus.Message.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Mapper
{
    public class BasketProfile:Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketCheckOut, BasketCheckOutEvent>().ReverseMap();
        }
    }
}
