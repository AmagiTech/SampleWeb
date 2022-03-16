using AutoMapper;
using Sample.Business.Interfaces;
using Sample.Data.Models.DTOs;
using Sample.Data.Uow.Interfaces;

namespace Sample.Business
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepo _dbRepo;
        private readonly IMapper _mapper;
        public CategoriesService(ICategoriesRepo dbRepo, IMapper mapper)
        {
            _dbRepo = dbRepo;
            _mapper = mapper;
        }
        public List<CategoryDto> ListCategoriesAndDetails()
        {
            return _dbRepo.ListCategoriesAndDetails();
        }
    }
}
