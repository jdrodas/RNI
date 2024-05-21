﻿using RNI_CS_SQL_REST_API.Models;

namespace RNI_CS_SQL_REST_API.Interfaces
{
    public interface ITipoReactorRepository
    {
        public Task<List<TipoReactor>> GetAllAsync();
    }
}