using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ApiSongs.Controllers;
using ApiSongs.Models;
using ApiSongs.Services;
using System.Collections.Generic;
using ApiSongs.View;
using Assert = Xunit.Assert;

public class SongControllerTests
{
    private readonly Mock<ISongData> mockSongData;
    private readonly SongController controller;

    public SongControllerTests()
    {
        mockSongData = new Mock<ISongData>();
        controller = new SongController(mockSongData.Object);
    }

    [Fact]
    public void Index_ReturnsListOfSongs()
    {
        mockSongData.Setup(data => data.GetAll()).Returns(new List<SongModel> { new SongModel() });

        var result = controller.Index();

        var objectResult = Assert.IsType<ObjectResult>(result);
        var songs = Assert.IsType<List<SongModel>>(objectResult.Value);
        Assert.NotEmpty(songs);
    }

    [Fact]
    public void Detail_WithValidId_ReturnsOkResult()
    {
        var existingSong = new SongModel { Id = 1, Name = "Test Song", Artist = new ArtistModel { Name = "Test Artist" }, Streams = 100 };
        mockSongData.Setup(data => data.Get(1)).Returns(existingSong);

        var result = controller.Detail(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void Create_WithValidData_ReturnsCreatedAtActionResult()
    {
        var createSongView = new CreateSongView
        {
            Name = "New Song",
            Artist = new ArtistModel { Name = "Test Artist" },
            Streams = 50
        };

        var result = controller.Create(createSongView);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal("Detail", createdAtActionResult.ActionName);
        Assert.NotNull(createdAtActionResult.RouteValues["id"]);
    }

    [Fact]
    public void Delete_WithValidId_ReturnsNoContentResult()
    {
        var existingSong = new SongModel { Id = 1, Name = "Test Song", Artist = new ArtistModel { Name = "Test Artist" }, Streams = 100 };
        mockSongData.Setup(data => data.Get(1)).Returns(existingSong);

        var result = controller.Delete(1);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void Update_WithValidData_ReturnsNoContentResult()
    {
        var updateSongView = new UpdateSongView { Name = "Updated Song", Artist = new ArtistModel { Name = "Updated Artist" }, Streams = 75 };

        var result = controller.Update(1, updateSongView);

        Assert.IsType<NoContentResult>(result);
    }
}
