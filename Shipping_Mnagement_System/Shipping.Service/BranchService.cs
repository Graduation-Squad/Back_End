using Microsoft.EntityFrameworkCore;
using Shipping.Core.DomainModels;
using Shipping.Core.Repositories.Contracts;
using Shipping.Core.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Service
{
    public class BranchService : IBranchService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BranchService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Branch>> GetAllAsync()
        {
            return await _unitOfWork.Repository<Branch>().GetAllAsync();
        }

        public async Task<Branch?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Branch>().GetByIdAsync(id);
        }

        public async Task AddAsync(Branch branch)
        {
            await _unitOfWork.Repository<Branch>().AddAsync(branch);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(Branch branch)
        {
            _unitOfWork.Repository<Branch>().Update(branch);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var branch = await _unitOfWork.Repository<Branch>().GetByIdAsync(id);
            if (branch != null)
            {
                _unitOfWork.Repository<Branch>().Delete(branch);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task AssignUsersToBranch(int branchId, List<string> userIds)
        {
            var branch = await _unitOfWork.Repository<Branch>().GetByIdAsync(branchId);
            if (branch == null) throw new Exception("Branch not found");

            var existingUserBranches = await _unitOfWork.Repository<UserBranch>()
                                          .FindAsync(ub => ub.BranchId == branchId);

            var newUserBranches = userIds
                .Where(userId => existingUserBranches.All(ub => ub.UserId != userId))
                .Select(userId => new UserBranch { UserId = userId, BranchId = branchId })
                .ToList();

            foreach (var userBranch in newUserBranches)
            {
                await _unitOfWork.Repository<UserBranch>().AddAsync(userBranch);
            }

            await _unitOfWork.CompleteAsync();
        }


        public async Task RemoveUserFromBranch(int branchId, string userId)
        {
            var userBranch = (await _unitOfWork.Repository<UserBranch>().FindAsync(ub => ub.BranchId == branchId && ub.UserId == userId)).FirstOrDefault();
            if (userBranch == null) throw new Exception("User not found in this branch");

            _unitOfWork.Repository<UserBranch>().Delete(userBranch);
            await _unitOfWork.CompleteAsync();
        }

        public Task AssignUsersToBranch(int branchId, List<int> userIds)
        {
            throw new NotImplementedException();
        }

        public Task RemoveUserFromBranch(int branchId, int userId)
        {
            throw new NotImplementedException();
        }
    }

}
