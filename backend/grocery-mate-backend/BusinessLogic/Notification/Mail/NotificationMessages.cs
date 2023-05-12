namespace grocery_mate_backend.BusinessLogic.Notification.Mail;

public static class NotificationMessages
{
    public static string Mail_Subject_RequestAccepted_Client = "Ihr Einkaufs-Auftrag wurde soeben angenommen";
    public static string Mail_Body_RequestAccepted_Client = "<p>Der Auftrag wurde von {0} angenommen. <br>Falls Sie einen Chat beginnen möchten, können Sie dies auf dem Web-Portal.</p>";

    public static string Mail_Subject_RequestAccepted_Contractor = "Vielen Dank, dass Sie den Einkauf übernommen haben";
    public static string Mail_Body_RequestAccepted_Contractor = "<p>Falls Sie noch fragen bezüglich des angenommenen Einkaufes haben, können Sie auf dem Web-Portal einen Chat mit {0} Beginnen.</p>";
    
    public static string Mail_Subject_RequestFulfilled_Client = "Ihr Einkaufs-Auftrag wurde soeben abgeschlossen✅";
    public static string Mail_Body_RequestFulfilled_Client = "<p>Der Auftrag wurde von {0} abgeschlossen.</p>";

    public static string Mail_Subject_RequestFulfilled_Contractor = "Vielen Dank, für en Einfauf 🤗";
    public static string Mail_Body_RequestFulfilled_Contractor = "<p>Der Auftrag für wurde im System {0} abgeschlossen. <br>Sie können ihn auf dem Web-Portag weiterhin in ihren Akzeptieten Aufträgen einsehen.</p>";
}