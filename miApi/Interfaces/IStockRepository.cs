using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using miApi.Dtos.Stock;
using miApi.models;

namespace miApi.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsyn(int id);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequest stockDto);
        Task<Stock?> DelteAsync(int id);
        Task<bool> StockExists(int id);
    }
}