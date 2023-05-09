namespace grocery_mate_backend.Utility;

public class LogMessages
{
    /**
     * Method-Names
     */
    
    // Authentication-Controller
    public static string MethodName_REST_POST_register = "REST Create User: ";
    public static string MethodName_REST_POST_login = "REST Log-In: ";
    public static string MethodName_REST_POST_logout = "REST Log-Out: ";

    // User-Settings-Controller
    public static string MethodName_REST_GET_settings = "REST Get User-Settings: ";
    public static string MethodName_REST_POST_settings = "REST Update User-Settings: ";
    
    // Shopping-Controller
    public static string MethodName_REST_POST_groceryRequest = "POST Grocery-Request: ";
    public static string MethodName_REST_GET_groceryRequest = "GET Grocery-Request: ";
    public static string MethodName_REST_GET_search = "GET Grocery-Request by Zipcode: ";
    public static string MethodName_REST_PUT_groceryRequestState = "PUT Grocery-Request-state: ";
    public static string MethodName_REST_GET_groceryRequest_clientRequests = "GET Grocery-Request from a client: ";
    public static string MethodName_REST_GET_groceryRequest_contractorRequests = "GET Grocery-Request from a contractor: ";

    
    /**
     * Log-Message
     */
    public static string LogMessage_SuccessfullyCreated = "successfully created!";
    public static string LogMessage_BearerTokenGenerated = "Bearer-Token Successfully generated!";
    public static string LogMessage_TokenRevoked = "Token successfully revoked!";
    public static string LogMessage_UserWithIdDoesntExist = "User with given identityId does not exist!";
    public static string LogMessage_InvalidGroceryRequestState = "Grocery-Request-State is invalid!";
    public static string LogMessage_GroceryRequestSaved = "Grocery-Request successfully saved!";
    public static string LogMessage_BadCredentials = "Invalid ModelState due to bad credentials!";
    public static string LogMessage_GroceryResponseMapped = "Grocery-Response successfully mapped!";
    public static string LogMessage_GroceryResponsePached = "Grocery-Response successfully patched!";

}