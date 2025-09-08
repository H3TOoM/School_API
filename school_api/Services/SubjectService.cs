using AutoMapper;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Repoistories.Base;
using school_api.Services.Base;

namespace school_api.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IMainRepoistory<Subject> _mainRepoistory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SubjectService( IMainRepoistory<Subject> mainRepoistory, IUnitOfWork unitOfWork, IMapper mapper )
        {
            _mainRepoistory = mainRepoistory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }





        public async Task<IEnumerable<SubjectReadDto>> GetAllSubjectsAsync()
        {
            var subjects = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<SubjectReadDto>>( subjects );
        }

        public async Task<SubjectReadDto> GetSubjectByIdAsync( int id )
        {
            var subject = await _mainRepoistory.GetByIdAsync( id );
            return _mapper.Map<SubjectReadDto>( subject );
        }



        public async Task<SubjectReadDto> CreateSubjectAsync( SubjectCreateDto dto )
        {
            var subject = new Subject
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _mainRepoistory.CreateAsync( subject );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SubjectReadDto>( subject );
        }



        public async Task<SubjectReadDto> UpdateSubjectAsync( int id, SubjectUpdateDto dto )
        {
            var subject = await _mainRepoistory.GetByIdAsync( id );
            if (subject == null)
                throw new KeyNotFoundException( "Subject Not Found!" );

            subject.Name = dto.Name ?? subject.Name;
            subject.Description = dto.Description ?? subject.Description;

            await _mainRepoistory.UpdateAsync( id, subject );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SubjectReadDto>( subject );

        }


        public async Task<bool> DeleteSubjectAsync( int id )
        {
            var subject = await _mainRepoistory.GetByIdAsync( id );
            if (subject == null)
                return false;

            await _mainRepoistory.DeleteAsync( id );
            await _unitOfWork.SaveChangesAsync();

            return true;
        }


    }
}
