using NSubstitute;
using Shouldly;
using StixApi.Contracts.Persistance;
using StixApi.Features.Vulnerabilities.Queries.List.V1;
using StixApi.Persistance.Models;

namespace StixApi.UnitTests.Vulnerabilities.Queries;

public class GetVulnerabilitiesQueryHandlerTests
{
    [Fact]
    public async Task GetVulnerabilitiesTest()
    {
        // Arrange
        var repositoryMock = Substitute.For<IAsyncRepository<VulnerabilityDbModel>>();
        var vulnerability = new VulnerabilityDbModel
        {
            Id = "vulnerability--e9eb06c9-ebc1-47a6-a009-4702bd9f744a",
            Value = System.Text.Json.JsonDocument.Parse(@"{
                ""id"": ""vulnerability--e9eb06c9-ebc1-47a6-a009-4702bd9f744a"",
                ""type"": ""vulnerability"",
                ""spec_version"": ""2.1"",
                ""created_by_ref"": ""identity--eb8aacf7-6148-4cd6-97f9-fda0c83622d7"",
                ""created"": ""2019-05-24T19:41:20.000Z"",
                ""modified"": ""2022-10-10T15:36:32.000Z"",
                ""name"": ""CVE-2018-0798"",
                ""description"": ""Equation Editor in Microsoft Office 2007, Microsoft Office 2010, Microsoft Office 2013, and Microsoft Office 2016 allows a remote code execution vulnerability due to the way objects are handled in memory, aka Microsoft Office Memory Corruption Vulnerability."",
                ""labels"": [""public""],
                ""confidence"": 100,
                ""object_marking_refs"": [""marking-definition--34098fce-860f-48ae-8e50-ebd3cc5e41da""]
            }")
        };
        repositoryMock.ListAllAsync().Returns(new List<VulnerabilityDbModel> { vulnerability });
        var handler = new ListVulnerabilitiesQueryHandler(repositoryMock);

        // Act
        var result = await handler.Handle(new ListVulnerabilitiesQuery(), CancellationToken.None);

        // Assert
        result.ShouldBeOfType<List<VulnerabilityListDTO>>();
        result.Count.ShouldBe(1);
    }
}
