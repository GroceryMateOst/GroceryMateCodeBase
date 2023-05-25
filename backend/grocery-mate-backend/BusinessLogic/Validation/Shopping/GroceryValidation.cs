using System.ComponentModel;
using grocery_mate_backend.Data.DataModels.Shopping;
using grocery_mate_backend.Models.Shopping;
using grocery_mate_backend.Utility;
using grocery_mate_backend.Utility.Log;
using Microsoft.IdentityModel.Tokens;

namespace grocery_mate_backend.BusinessLogic.Validation;

public abstract class GroceryValidation : ValidationBase
{
    public static bool Validate(GroceryRequestDto requestDto)
    {
        return ValidateGroceryList(requestDto.GroceryList) &&
               ValidateRequestState(requestDto.RequestState) &&
               ValidateDateTime(requestDto.FromDate) &&
               ValidateDateTime(requestDto.ToDate);
    }
    
    public static bool ValidateRequestState(string requestState)
    {
        var validStateNames = GetAllEnumDescriptions<GroceryRequestState>();
        
        return Validate(requestState,
            ErrorMessages.RequestState_incorrect,
            item =>Array.Exists(validStateNames, validState => validState.Equals(requestState, StringComparison.OrdinalIgnoreCase))); 

    }

    public static bool ValidateGroceryList(List<ShoppingListDto> requestDto)
    {
        return Validate(requestDto,
            ErrorMessages.GroceryList_empty,
            item => item.All(groceryList => !groceryList.Description.IsNullOrEmpty()));
    }

    public static bool ValidateDateTime(string date)
    {
        return Validate(date,
            ErrorMessages.DateTime_invalidFromat,
            item => DateTime.TryParse(item, out _));
    }

    private static string[] GetAllEnumDescriptions<TEnum>()
    {
        var enumType = typeof(TEnum);
        
        return Enum.GetValues(enumType)
            .Cast<Enum>()
            .Select(GetEnumDescription)
            .ToArray();
    }
    
    private static string GetEnumDescription(Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }
}