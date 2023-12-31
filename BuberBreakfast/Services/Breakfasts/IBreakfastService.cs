using BuberBreakfast.Contracts.Breafast;
using BuberBreakfast.Models;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts;
public interface IBreakfastService{
    ErrorOr<Created> CreateBreakfast(Breakfast breakfast);
    ErrorOr<Breakfast> GetBreakfast(Guid id);
    ErrorOr<Updated> UpsertBreakfast(Breakfast breakfast);
    ErrorOr<Deleted> DeletedBreakfast(Guid id);
}