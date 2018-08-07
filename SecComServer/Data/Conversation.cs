namespace SecComServer.Data
{
    public class Conversation
    {
        public int ConversationId { get; set; }
        public ApplicationUser FirstUser { get; set; }
        public ApplicationUser SecondUser { get; set; }
        public bool FirstUserAccepted { get; set; }
        public bool SecondUserAccepted { get; set; }
    }

    public class ConversationResponse
    {
        public long Id { get; set; }
        public string FirstUser { get; set; }
        public string SecondUser { get; set; }
        public bool FirstUserAccepted { get; set; }
        public bool SecondUserAccepted { get; set; }

        public ConversationResponse(Conversation convo)
        {
            Id = convo.ConversationId;
            FirstUser = convo.FirstUser.UserName;
            SecondUser = convo.SecondUser.UserName;
            FirstUserAccepted = convo.FirstUserAccepted;
            SecondUserAccepted = convo.SecondUserAccepted;
        }
    }
}