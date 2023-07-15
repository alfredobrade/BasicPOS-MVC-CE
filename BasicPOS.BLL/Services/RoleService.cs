using BasicPOS.BLL.ServiceInterfaces;
using BasicPOS.DAL.GenericRepository;
using BasicPOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicPOS.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IGenericRepository<Role> _repository;

        public RoleService(IGenericRepository<Role> repository)
        {
            _repository = repository;
        }

        public async Task<List<Role>> Lista()
        {
            IQueryable<Role> query = await _repository.Consultar();
            return query.ToList();
        }
    }
}
