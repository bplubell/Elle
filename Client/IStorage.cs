﻿using Elle.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elle.Client
{
    public interface IStorage
    {
        Task<IReadOnlyList<Calculator>> LoadCalculatorsAsync();
        
        Task<Calculator?> GetCalculatorById(string id);

        Task SaveCalculatorsAsync(IList<Calculator> calculators);
    }
}
