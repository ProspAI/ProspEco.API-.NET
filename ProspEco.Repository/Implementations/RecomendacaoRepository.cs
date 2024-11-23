﻿using Microsoft.EntityFrameworkCore;
using ProspEco.Model.Entities;
using ProspEco.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProspEco.Database.Contexts;

namespace ProspEco.Repository.Implementations
{
    public class RecomendacaoRepository : Repository<Recomendacao>, IRecomendacaoRepository
    {
        public RecomendacaoRepository(ProspEcoContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Recomendacao>> GetByUsuarioIdAsync(long usuarioId)
        {
            return await _dbSet.Where(r => r.UsuarioId == usuarioId).ToListAsync();
        }
    }
}