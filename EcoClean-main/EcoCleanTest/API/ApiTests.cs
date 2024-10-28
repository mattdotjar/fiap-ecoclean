// using NUnit.Framework;
// using System.Net;
// using System.Net.Http;
// using System.Text;
// using Newtonsoft.Json.Linq;
//
// namespace EcoCleanTest.API
// {
//     [TestFixture]
//     public class ApiTests
//     {
//         private HttpClient _httpClient = null!;
//
//         [SetUp]
//         public void Setup()
//         {
//             _httpClient = new HttpClient();
//         }
//
//         [Test]
//         public void TestGetColeta()
//         {
//             var response = _httpClient.GetAsync("https://localhost:5001/api/coleta/1").Result;
//             Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
//
//             var responseContent = response.Content.ReadAsStringAsync().Result;
//             JObject coleta = JObject.Parse(responseContent);
//             Assert.AreEqual(1, (int)coleta["id"]);
//         }
//
//         [Test]
//         public void TestPostColetaWithInvalidData()
//         {
//             var content = new StringContent("{ \"id\": \"abc\", \"nome\": 123 }", Encoding.UTF8, "application/json");
//             var response = _httpClient.PostAsync("https://localhost:5001/api/coleta", content).Result;
//
//             Assert.AreEqual(HttpStatusCode.UnprocessableEntity, response.StatusCode);
//         }
//     }
// }
