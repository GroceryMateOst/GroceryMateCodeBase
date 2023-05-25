namespace grocery_mate_backend.Utility;

public abstract class LogMessages
{
    /**
     * Method-Names
     */

    // Authentication-Controller
    public static readonly string REST_POST_register = "REST Create User: ";

    public static readonly string REST_POST_login = "REST Log-In: ";
    public static readonly string REST_POST_logout = "REST Log-Out: ";

    // User-Settings-Controller
    public static readonly string REST_GET_settings = "REST Get User-Settings: ";
    public static readonly string REST_POST_settings = "REST Update User-Settings: ";

    // Shopping-Controller
    public static readonly string REST_POST_groceryRequest = "POST Grocery-Request: ";
    public static readonly string REST_GET_groceryRequest = "GET Grocery-Request: ";
    public static readonly string REST_GET_search = "GET Grocery-Request by Zipcode: ";
    public static readonly string REST_PUT_groceryRequestState = "PUT Grocery-Request-state: ";

    public static readonly string REST_GET_groceryRequest_clientRequests = "GET Grocery-Request from a client: ";

    public static readonly string REST_GET_groceryRequest_contractorRequests = "GET Grocery-Request from a contractor: ";
    
    // Chat-Controller
    public static readonly string REST_GET_chat = "Get Chat: ";
    public static readonly string REST_PUT_MessageReadStatus = "PUT Update Messages Read: ";

    // Notification
    public static readonly string Notification_Mail_Send = "Send Mail-Notification: ";


    /**
     * Log-Message
     */
    public static readonly string LogMessage_SuccessfullyCreated = "successfully created!";
    public static readonly string LogMessage_BearerTokenGenerated = "Bearer-Token Successfully generated!";
    public static readonly string LogMessage_TokenRevoked = "Token successfully revoked!";
    public static readonly string LogMessage_UserWithIdDoesntExist = "User with given identityId does not exist!";
    public static readonly string LogMessage_InvalidGroceryRequestState = "Grocery-Request-State is invalid!";
    public static readonly string LogMessage_GroceryRequestSaved = "Grocery-Request successfully saved!";
    public static readonly string LogMessage_BadCredentials = "Invalid ModelState due to bad credentials!";
    public static readonly string LogMessage_GroceryResponseMapped = "Grocery-Response successfully mapped!";
    public static readonly string LogMessage_GroceryResponsePached = "Grocery-Response successfully patched!";
    public static readonly string LogMessage_NoChatMessage = "There is no Chat for this Grocery-Request!";
    public static readonly string LogMessage_ReadChatMessage = "The message with the following id was read: ";
    
    public static readonly string LogMessage_ErrorMailSend = "An error occurred while sending the mail!";
    public static readonly string LogMessage_CreateIdUserFailed = "Creation of the Identity-User failed!";
}