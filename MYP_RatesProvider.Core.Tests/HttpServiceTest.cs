using Moq.Protected;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


//namespace MYP_RatesProvider.Core.Tests
//{
//    public class HttpServiceTest
//    {
    //    [Test]
    //    public async Task GetDataFromSource_ReturnsData()
    //    {
    //        // Arrange
    //        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
    //        mockHttpMessageHandler.Protected()
    //            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
    //            .ReturnsAsync(new HttpResponseMessage
    //            {
    //                StatusCode = HttpStatusCode.OK,
    //                Content = new StringContent("{"key": "value"}")
    //            });

    //        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
    //        var urlFromAppSettings = "https://example.com";

    //        // Act
    //        var result = await GetDataFromSource(urlFromAppSettings);

    //        // Assert
    //        Assert.AreEqual("value", result["key"]);
    //    }

    //    [Test]
    //    public async Task GetDataFromSource_ThrowsException()
    //    {
    //        // Arrange
    //        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
    //        mockHttpMessageHandler.Protected()
    //            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
    //            .ReturnsAsync(new HttpResponseMessage
    //            {
    //                StatusCode = HttpStatusCode.InternalServerError
    //            });

    //        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
    //        var urlFromAppSettings = "https://example.com";

    //        // Act and Assert
    //        await Assert.ThrowsAsync<Exception>(() => GetDataFromSource(urlFromAppSettings));
    //    }
    //}
//}
