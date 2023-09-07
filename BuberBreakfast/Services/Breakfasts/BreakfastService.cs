using BuberBreakfast.Models;

namespace BuberBreakfast.Services.Breakfasts;

public class BuberBreakfastService: IBreakfastService{
    private readonly Dictionary<Guid, Breakfast> _breakfasts = new();

    public void Breakfast(Breakfast breakfast){
        _breakfasts.Add(breakfast.Id, breakfast);
    }
}

