﻿using AutoMapper;
using SignalR.DtoLayer.MessageDto;
using SignalR.EntityLayer.Entities;

namespace SignalRapi.Mapping
{
    public class MessageMapping:Profile
    {
        public MessageMapping()
        {
            CreateMap<CreateMessageDto,Message>().ReverseMap();
            CreateMap<ResultMessageDto,Message>().ReverseMap();
            CreateMap<GetMeesageDto,Message>().ReverseMap();
            CreateMap<UpdateMessageDto,Message>().ReverseMap();
        }
    }
}
