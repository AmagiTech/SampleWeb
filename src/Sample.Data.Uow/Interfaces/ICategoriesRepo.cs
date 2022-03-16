using Sample.Data.Models.DTOs;

namespace Sample.Data.Uow.Interfaces
{
    public interface ICategoriesRepo
    {
        List<CategoryDto> ListCategoriesAndDetails();
    }
}
