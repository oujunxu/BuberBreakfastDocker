using BuberBreakfast.Contracts.Breafast;
using BuberBreakfast.Models;
using BuberBreakfast.Services.Breakfasts;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using BuberBreakfast.Context;
using BuberBreakfast.Entities;
using Azure.Core.Serialization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace BuberBreakfast.Controllers;

public class BreakfastsController : ApiController
{
    private readonly IBreakfastService _breakfastService;
    private BreakfastContext _context;

    public BreakfastsController(IBreakfastService breakfastService, BreakfastContext context)
    {
        _breakfastService = breakfastService;
        _context = context;
    }

    [HttpPost()]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        ErrorOr<Breakfast> breakfastRequest = Breakfast.Create(
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            request.Savory,
            request.Sweet
        );

        if (breakfastRequest.IsError)
        {
            return Problem(breakfastRequest.Errors);
        }

        var breakfast = breakfastRequest.Value;

        ErrorOr<Created> createBreakfastResult = _breakfastService.CreateBreakfast(breakfast);

        return createBreakfastResult.Match(
            created => CreatedAt(breakfast),
            errors => Problem(errors));     
    }

    [HttpGet("{id:guid}")]
    public JsonResult GetBreakfast(Guid id)
    {

        ErrorOr<Breakfast> getBreakfastResult = _breakfastService.GetBreakfast(id);

        var breakfast = _context.Breakfasts.Find(id);

        return new JsonResult(breakfast);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id ,UpsertBreakfastRequest request)
    {
        ErrorOr<Breakfast> requestToBreakfastResult = Breakfast.From(id, request);

        if (requestToBreakfastResult.IsError)
        {
            return Problem(requestToBreakfastResult.Errors);
        }

        var breakfast = requestToBreakfastResult.Value;
        ErrorOr<UpsertedBreakfast> upsertBreakfastResult = _breakfastService.UpsertBreakfast(breakfast);

        _context.Breakfasts.Update(new BreakfastEntity(){
            Id = breakfast.Id,
            Name = breakfast.Name,
            Description = breakfast.Description,
            StartDateTime = breakfast.StartDateTime,
            EndDateTime = breakfast.EndDateTime
        });

        _context.SaveChanges();

        return upsertBreakfastResult.Match(
            upserted => upserted.IsNewlyCreated ? CreatedAt(breakfast) : NoContent(),
            errors => Problem(errors));
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        ErrorOr<Deleted> deletedResult = _breakfastService.DeletedBreakfast(id);
        var breakfast = _context.Breakfasts.Find(id);

        if(breakfast is not null){
            _context.Breakfasts.Remove(breakfast);
        }

        return deletedResult.Match(
            deleted => NoContent(),
            errors => Problem(errors)
        );
    }

    public CreatedAtActionResult CreatedAt(Breakfast breakfast){
        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new {id = breakfast.Id},
            value: MapBreakfastResponse(breakfast)
        );    
    }

    public static BreakfastResponse MapBreakfastResponse(Breakfast breakfast)
        {
           return new BreakfastResponse(
                breakfast.Id,
                breakfast.Name,
                breakfast.Description,
                breakfast.StartDateTime,
                breakfast.EndDateTime,
                breakfast.LastModifiedDateTime,
                breakfast.Savory,
                breakfast.Sweet
            );
        }
}