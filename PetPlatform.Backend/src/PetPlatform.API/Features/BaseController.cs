using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace PetPlatform.API.Features;

public class BaseController(IMapper mapper) : ControllerBase
{
    protected readonly IMapper _mapper = mapper;
}