﻿using GestaoTarefa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Domain.Interfaces.Repositories
{
    public interface ISetorRepository : IBaseRepository<Setor>
    {
        Task<Setor?> GetByName(string name);
    }
}
