namespace Server.UnitTest
{

    public interface IMessageService
    {
        string GetHelloMessage();
        string GetGreetingMessage();
    }
    public class MessageService : IMessageService
    {
        public string GetHelloMessage()
        {
            return "Hello there!";
        }

        public string GetGreetingMessage()
        {
            return "Good Morning!";
        }
    }
    public class UnitTest1
    {
        private const string Expected1 = "Hello there!";
        private const string Expected2 = "Good Morning!";
        [Fact]
        public void HelloMessageTest()
        {
            var msgService = A.Fake<IMessageService>();
            A.CallTo(() => msgService.GetHelloMessage()).Returns(Expected1);

            var res = msgService.GetHelloMessage();
            Assert.Equal(Expected1, res);
        }
    }
}