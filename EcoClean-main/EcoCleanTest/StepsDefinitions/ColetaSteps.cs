using NUnit.Framework;
using TechTalk.SpecFlow;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

[Binding]
public class ColetaSteps
{
    private HttpResponseMessage _response = null!;

    [Given(@"que estou autenticado no sistema")]
    public void DadoQueEstouAutenticadoNoSistema()
    {
        // Simular autenticação
        // Exemplo: Gerar token JWT válido (simulação para teste)
        // _token = "Bearer token_simulado";
    }

    [When(@"eu envio uma solicitação de criação de coleta com dados válidos")]
    public void QuandoEnvioUmaSolicitacaoDeCriacaoDeColetaComDadosValidos()
    {
        var httpClient = new HttpClient();
        var content = new StringContent("{ \"id\": 1, \"nome\": \"Coleta A\" }", Encoding.UTF8, "application/json");
        _response = httpClient.PostAsync("https://localhost:5001/api/coleta", content).Result;
    }

    [When(@"eu envio uma solicitação de criação de coleta sem dados")]
    public void QuandoEnvioUmaSolicitacaoDeCriacaoDeColetaSemDados()
    {
        var httpClient = new HttpClient();
        var content = new StringContent("{}", Encoding.UTF8, "application/json");
        _response = httpClient.PostAsync("https://localhost:5000/api/coleta", content).Result;
    }

    [When(@"eu envio uma solicitação de criação de coleta com dados inválidos")]
    public void QuandoEnvioUmaSolicitacaoDeCriacaoDeColetaComDadosInvalidos()
    {
        var httpClient = new HttpClient();
        var content = new StringContent("{ \"id\": \"abc\", \"nome\": 123 }", Encoding.UTF8, "application/json");
        _response = httpClient.PostAsync("https://localhost:5001/api/coleta", content).Result;
    }

    [Then(@"a coleta deve ser criada com sucesso")]
    public void EntaoAColetaDeveSerCriadaComSucesso()
    {
        Assert.AreEqual(HttpStatusCode.Created, _response.StatusCode);
    }

    [Then(@"devo receber um status (.*)")]
    public void EntaoDevoReceberUmStatus(int statusCode)
    {
        Assert.AreEqual((HttpStatusCode)statusCode, _response.StatusCode);
    }

    [Then(@"o sistema deve retornar um erro de validação")]
    public void EntaoOSistemaDeveRetornarUmErroDeValidacao()
    {
        Assert.IsTrue(_response.StatusCode == HttpStatusCode.BadRequest || _response.StatusCode == HttpStatusCode.UnprocessableEntity);
    }
}
