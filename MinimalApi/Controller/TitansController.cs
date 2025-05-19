using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MinimalApi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TitansController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public TitansController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}