using Sample.Data.Models;
using Sample.Data.Models.DTOs;

namespace Sample.Data.Uow.Interfaces
{
    public interface IItemsRepo
    {
        List<Item> GetItems();
        List<ItemDto> GetItemsByDateRange(DateTime minDateValue, DateTime
        maxDateValue);
        List<GetItemsForListingDto> GetItemsForListingFromProcedure();
        List<GetItemsTotalValueDto> GetItemsTotalValues(bool isActive);
        List<FullItemDetailDto> GetItemsWithGenresAndCategories();
        int UpsertItem(Item item);
        void UpsertItems(List<Item> items);
        void DeleteItem(int id);
        void DeleteItems(List<int> itemIds);

    }
}
