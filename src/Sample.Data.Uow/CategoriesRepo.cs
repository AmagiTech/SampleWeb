using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sample.Data.Models.DTOs;
using Sample.Data.Uow.Interfaces;

namespace Sample.Data.Uow
{
    public class CategoriesRepo : ICategoriesRepo
    {
        private readonly IMapper _mapper;
        private readonly SampleDbContext _context;


        public CategoriesRepo(SampleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<CategoryDto> ListCategoriesAndDetails()
        {
            return _context.Categories.Include(x => x.CategoryDetail)
                        .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                        .ToList();
        }
    }
}
