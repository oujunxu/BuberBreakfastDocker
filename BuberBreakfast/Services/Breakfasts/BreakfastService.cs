using System.Data;
using System.Text.Json;
using BuberBreakfast.Context;
using BuberBreakfast.Entities;
using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts;

public class BreakfastService: IBreakfastService{

    private BreakfastContext _context;
    public BreakfastService(BreakfastContext context)
    {
        _context = context;
    }
    public ErrorOr<Created> CreateBreakfast(Breakfast breakfast){
        _context.Breakfasts.Add(new BreakfastEntity(){
            Id = breakfast.Id,
            Name = breakfast.Name,
            Description = breakfast.Description,
            StartDateTime = breakfast.StartDateTime,
            EndDateTime = breakfast.EndDateTime,
            Sweet = JsonSerializer.Serialize(breakfast.Sweet),
            Savory = JsonSerializer.Serialize(breakfast.Savory)
        });
        _context.SaveChanges();
        return Result.Created;
    }

    public ErrorOr<Breakfast> GetBreakfast(Guid id){
        var breakfast = _context.Breakfasts.Find(id);
        if(breakfast is not null){
            return new Breakfast(
                breakfast.Id,
                breakfast.Name,
                breakfast.Description,
                breakfast.StartDateTime,
                breakfast.EndDateTime,
                new DateTime(),
                JsonSerializer.Deserialize<List<string>>(breakfast.Savory),
                JsonSerializer.Deserialize<List<string>>(breakfast.Sweet)
            );
        }
        return Errors.Breakfast.NotFound;
    }

    public ErrorOr<UpsertedBreakfast> UpsertBreakfast(Breakfast breakfast){
        bool isNewlyCreated;
        _context.Breakfasts.Find(breakfast.Id) == null ?  isNewlyCreated = false : isNewlyCreated = true;
        _breakfasts[breakfast.Id] = breakfast;
        return new UpsertedBreakfast(isNewlyCreated);
    }

    public ErrorOr<Deleted> DeletedBreakfast(Guid id){
        _breakfasts.Remove(id);
        return Result.Deleted;
    }
}

