using Microsoft.AspNetCore.Http;
using Moq;

namespace Project.Test.Mocks;

public class MockHttpContextAccessor
{
    public static IHttpContextAccessor GetMockedHttpContextAccessor()
    {
        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        var context = new DefaultHttpContext();
        mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(context);
        return mockHttpContextAccessor.Object;
    }
}