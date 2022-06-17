using System;
using AutoMapper;
using Insurify.Population.Api.AppStart;
using Microsoft.AspNetCore.Mvc;

namespace Insurify.Population.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        public IMapper Mapper { get; }

        public BaseController()
        {
            Mapper = AutoMapperConfig.Mapper;
        }
    }
}
