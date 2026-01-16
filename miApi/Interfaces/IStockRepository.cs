using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using miApi.models;

namespace miApi.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
    }
}