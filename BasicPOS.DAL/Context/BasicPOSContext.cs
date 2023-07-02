using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicPOS.DAL.Context
{
    public class BasicPOSContext : DbContext
    {
        public BasicPOSContext(DbContextOptions<BasicPOSContext> options) : base(options)
        {
            
        }




    }
}
