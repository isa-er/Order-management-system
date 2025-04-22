﻿using AutoMapper;
using SignalR.DtoLayer.MenuTableDto;
using SignalR.EntityLayer.Entities;

namespace SignalRapi.Mapping
{
    public class MenuTableMapping: Profile
    {
        public MenuTableMapping()
        {
            CreateMap<MenuTable, ResultMenuTableDto>();
            CreateMap<MenuTable, CreateMenuTableDto>();
            CreateMap<MenuTable,UpdateMenuTableDto>();
            CreateMap<MenuTable,GetMenuTableDto>();
        }
    }
    
}
