namespace  Content
{
    interface IContentServer
    {
        void SSubscribe(IContentListener subscriber);
        List<Thread> SGetAllMessages();
        void SSendAllMessagesToClient(int userId);
    }    
}