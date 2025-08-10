using School_API.DTOs;
using School_API.Models;
using School_API.Repoistory.Base;
using School_API.Services.Base;

namespace School_API.Services
{
    public class ManagerService : IManagerService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepoistory<Manager> _repoistory;

        public ManagerService(IUnitOfWork unitOfWork, IRepoistory<Manager> repoistory)
        {
            _repoistory = repoistory;
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<Manager>> GetAllManagersAsync()
        {
            return await _repoistory.GetAllAsync();
        }


        public async Task<Manager> GetManagerByIdAsync(int id)
        {
            var manager = await _repoistory.GetByIdAsync(id);
            return manager;
        }

        public async Task<Manager> CreateManagerAsync(ManagerDto dto)
        {
            var manager = new Manager
            {
                Name = dto.Name,
                Email = dto.Email,
            };

            await _repoistory.Add(manager);
            await _unitOfWork.SaveChangesAsync();
            
            return manager;
        }
        public async Task<Manager> UpdateManagerAsync(int id, Manager manager)
        {

            var existing = await _repoistory.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Manager with Id {id} not found.");



            await _repoistory.UpdateAsync(id, manager);
            await _unitOfWork.SaveChangesAsync();
            return manager;
        }

        public async Task<string> DeleteManagerAsync(int id)
        {

            var existing = await _repoistory.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Manager with Id {id} not found.");



            await _repoistory.DeleteByIdAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return "Manager Deleted Successfully!";


        }



    }
}
