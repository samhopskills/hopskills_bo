﻿using HopSkills.CoreBusiness;
using HopSkills.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopSkills.Plugins.InMemory
{
    public class RoleRepository : IRoleRepository
    {
        public Task CreateAsync(Role role)
        {
            return Task.CompletedTask;
        }

        public Task<List<Role>> GetAllAsync()
        {
            return null;
        }
    }
}
