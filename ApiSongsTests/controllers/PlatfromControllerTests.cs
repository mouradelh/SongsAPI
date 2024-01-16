using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ApiSongs.Controllers;
using ApiSongs.Models;
using ApiSongs.Services;
using System.Collections.Generic;
using ApiSongs.View;
using Assert = Xunit.Assert;

public class PlatformControllerTests
{
    private readonly Mock<IPlatfromData> mockPlatformData;
    private readonly PlatformController controller;

    public PlatformControllerTests()
    {
        mockPlatformData = new Mock<IPlatfromData>();
        controller = new PlatformController(mockPlatformData.Object);
    }

    [Fact]
    public void Index_ReturnsListOfPlatforms()
    {
        mockPlatformData.Setup(data => data.GetAll()).Returns(new List<PlatformModel> { new PlatformModel() });

        var result = controller.Index();

        var objectResult = Assert.IsType<ObjectResult>(result);
        var platforms = Assert.IsType<List<PlatformModel>>(objectResult.Value);
        Assert.NotEmpty(platforms);
    }

    [Fact]
    public void Detail_WithValidId_ReturnsOkResult()
    {
        var existingPlatform = new PlatformModel { Id = 1, Name = "Test Platform" };
        mockPlatformData.Setup(data => data.Get(1)).Returns(existingPlatform);

        var result = controller.Detail(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void Create_WithValidData_ReturnsCreatedAtActionResult()
    {
        var createPlatformView = new CreatePlatformView { Name = "New Platform" };

        var result = controller.Create(createPlatformView);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal("Detail", createdAtActionResult.ActionName);
        Assert.NotNull(createdAtActionResult.RouteValues["id"]);
    }

    [Fact]
    public void Delete_WithValidId_ReturnsNoContentResult()
    {
        var existingPlatform = new PlatformModel { Id = 1, Name = "Test Platform" };
        mockPlatformData.Setup(data => data.Get(1)).Returns(existingPlatform);

        var result = controller.Delete(1);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void Update_WithValidData_ReturnsNoContentResult()
    {
        var updatePlatformView = new UpdatePlatformView { Name = "Updated Platform" };

        var result = controller.Update(1, updatePlatformView);

        Assert.IsType<NoContentResult>(result);
    }

}
