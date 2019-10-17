using Elle.Client.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elle.Client
{
    public interface IStorage
    {
        Task Clear();

        Task DeleteCalculator(int id);

        Task<IReadOnlyList<Calculator>> LoadCalculatorsAsync();
        
        Task<Calculator?> GetCalculatorById(int id);

        Task UpdateCalculatorAsync(Calculator calculator);
        
        Task<int> CreateCalculatorAsync(Calculator calculator);

        event EventHandler CalculatorsUpdated;
    }
}
