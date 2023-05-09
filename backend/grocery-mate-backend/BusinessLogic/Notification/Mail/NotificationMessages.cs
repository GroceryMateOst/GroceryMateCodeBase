namespace grocery_mate_backend.BusinessLogic.Notification.Mail;

public static class NotificationMessages
{
    public static string Mail_Subject_RequestAccepted_Client = "Ihr Einkaufs-Auftrag wurde soeben angenommen";
    public static string Mail_Body_RequestAccepted_Client = "Der Auftrag wurde von {contractor} angenommen. \nFalls Sie einen CHat beginnen möchten, können Sie dies auf dem Web-Portal.";

    public static string Mail_Subject_RequestAccepted_Contractor = "";
    public static string Mail_Body_RequestAccepted_Contractor = "";
    
    public static string Mail_Subject_RequestFulfilled_Client = "Ihr Einkaufs-Auftrag wurde soeben abgeschlossen✅";
    public static string Mail_Body_RequestFulfilled_Client = "Der Auftrag wurde von {contractor} abgeschlossen.";


    public static string Mail_Subject_RequestFulfilled_Contractor = "";
    public static string Mail_Body_RequestFulfilled_Contractor = "";
}