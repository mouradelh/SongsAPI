using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ApiSongs.Controllers;
using ApiSongs.Models;
using ApiSongs.Services;
using System.Collections.Generic;
using ApiSongs.View;
using Assert = Xunit.Assert;

public class ArtistControllerTests
{
    private readonly Mock<IArtistData> mockArtistData;
    private readonly ArtistController controller;

    public ArtistControllerTests()
    {
        mockArtistData = new Mock<IArtistData>();
        controller = new ArtistController(mockArtistData.Object);
    }

    [Fact]
    public void Index_ReturnsListOfArtists()
    {
        mockArtistData.Setup(data => data.GetAll()).Returns(new List<ArtistModel> { new ArtistModel() });

        var result = controller.Index();

        var objectResult = Assert.IsType<ObjectResult>(result);
        var artists = Assert.IsType<List<ArtistModel>>(objectResult.Value);
        Assert.NotEmpty(artists);
    }

    [Fact]
    public void Detail_WithValidId_ReturnsOkResult()
    {
        var existingArtist = new ArtistModel { Id = 1, Name = "Test Artist", NumberOfSongs = 10, MonthlyStreams = 1000, Popularity = 8 };
        mockArtistData.Setup(data => data.Get(1)).Returns(existingArtist);

        var result = controller.Detail(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void Create_WithValidData_ReturnsCreatedAtActionResult()
    {
        var createArtistView = new CreateArtistView { Name = "New Artist", NumberOfSongs = 5, MonthlyStreams = 500, Popularity = 7 };

        var result = controller.Create(createArtistView);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal("Detail", createdAtActionResult.ActionName);
        Assert.NotNull(createdAtActionResult.RouteValues["id"]);
    }

    [Fact]
    public void Delete_WithValidId_ReturnsNoContentResult()
    {
        var existingArtist = new ArtistModel { Id = 1, Name = "Test Artist", NumberOfSongs = 10, MonthlyStreams = 1000, Popularity = 8 };
        mockArtistData.Setup(data => data.Get(1)).Returns(existingArtist);

        var result = controller.Delete(1);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void Update_WithValidData_ReturnsNoContentResult()
    {
        var updateArtistView = new UpdateArtistView { Name = "Updated Artist", NumberOfSongs = 15, MonthlyStreams = 1500, Popularity = 9 };

        var result = controller.Update(1, updateArtistView);

        Assert.IsType<NoContentResult>(result);
    }

}
