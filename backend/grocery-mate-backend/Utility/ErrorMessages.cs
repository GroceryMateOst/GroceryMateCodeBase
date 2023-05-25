namespace grocery_mate_backend.Utility;

public abstract class ErrorMessages
{
    /**
     * Error-Message
     */
    
    // Validations
    public static readonly string Zipcode_invalid = "Zipcode is invalid!";
    public static readonly string Address_userNotFound = "User with given eMail-Adr. not found!";
    public static readonly string UserPassword_invalid = "Invalid Password!";
    public static readonly string RequestState_incorrect = "Request-State is incorrect!";
    public static readonly string GroceryList_empty = "Shopping list is empty!";
    public static readonly string DateTime_invalidFromat= "Invalid date format!";
    public static readonly string UserMail_invalid= "Invalid Mail-Address!";
    public static readonly string ModelState_badCredentials = "Invalid Model-State due to Bad credentials!";
}