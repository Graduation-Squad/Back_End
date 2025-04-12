using Shipping.Core.DomainModels;
using Shipping.Core.Repositories.Contracts;
using Shipping.Core.Services.Contracts;
using Shipping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Service
{
    public class AreaService : IAreaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AreaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Area>> GetAllAreasAsync()
        {
            return await _unitOfWork.Repository<Area>().GetAllAsync();
        }

        public async Task<Area?> GetAreaByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Area>().GetByIdAsync(id);
        }

        public async Task<IEnumerable<Area>> GetAreasByCityIdAsync(int cityId)
        {
            return await _unitOfWork.Repository<Area>().FindAsync(a => a.CityId == cityId);
        }

        public async Task<Area> CreateAreaAsync(AreaDTO area)
        {
            Area NewArea = new Area()
            {
                Name = area.Name,
                IsActive = area.IsActive,
                CityId = area.CityId
            };

            await _unitOfWork.Repository<Area>().AddAsync(NewArea);
            await _unitOfWork.CompleteAsync();
            return NewArea;
        }

        public async Task<Area> UpdateAreaAsync(int id, AreaDTO updatedArea)
        {
            Area area = await _unitOfWork.Repository<Area>().GetByIdAsync(id);
            if (area == null) throw new Exception("Area not found");

            area.Name = updatedArea.Name;
            area.IsActive = updatedArea.IsActive;
            area.CityId = updatedArea.CityId;

            _unitOfWork.Repository<Area>().Update(area);
            await _unitOfWork.CompleteAsync();
            return area;
        }

        public async Task<bool> ToggleAreaStatusAsync(int id)
        {
            var area = await _unitOfWork.Repository<Area>().GetByIdAsync(id);
            if (area == null) return false;

            area.IsActive = !area.IsActive;
            _unitOfWork.Repository<Area>().Update(area);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteAreaAsync(int id)
        {
            var area = await _unitOfWork.Repository<Area>().GetByIdAsync(id);
            if (area == null) return false;

            _unitOfWork.Repository<Area>().Delete(area);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
