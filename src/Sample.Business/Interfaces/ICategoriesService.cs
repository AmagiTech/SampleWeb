using Sample.Data.Models.DTOs;

namespace Sample.Business.Interfaces
{
    public interface ICategoriesService
    {
        List<CategoryDto> ListCategoriesAndDetails();
    }
}
