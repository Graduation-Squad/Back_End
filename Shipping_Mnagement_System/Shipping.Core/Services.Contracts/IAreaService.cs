﻿using Shipping.Core.DomainModels;
using Shipping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Services.Contracts
{
    public interface IAreaService
    {
        Task<IEnumerable<Area>> GetAllAreasAsync();
        Task<Area?> GetAreaByIdAsync(int id);
        Task<IEnumerable<Area>> GetAreasByCityIdAsync(int cityId);
        Task<Area> CreateAreaAsync(AreaDTO area);
        Task<Area> UpdateAreaAsync(int id, AreaDTO updatedArea);
        Task<bool> ToggleAreaStatusAsync(int id);
        Task<bool> DeleteAreaAsync(int id);
    }
}
