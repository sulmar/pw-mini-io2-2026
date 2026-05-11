using MiniVendingMachine;

namespace MiniVendingMachine.Tests;

public class VendingMachineTest
{
    // {Method}_{Scenario}_{ExpectedBehavior}

    [Fact]
    public void Init_ShouldStateIsReady()
    {
        // Arrange

        // Act
        var sut = new VendingMachine(); // sut = System Under Control

        // Assert
        Assert.Equal(MachineState.Ready, sut.State);
    }

    [Fact]
    public void GreenButton_Click_StateIsSelection()
    {
        // Arrange
        var sut = new VendingMachine();

        // Act
        sut.GreenButtonClick();

        // Assert
        Assert.Equal(MachineState.Selection, sut.State);

    }

    [Fact]
    public void Cancel_Click_StateIsReady()
    {
        // Arrange
        var sut = new VendingMachine();
        sut.GreenButtonClick();


        // Act
        sut.Cancel();

        // Assert
        Assert.Equal(MachineState.Ready, sut.State);
    }

    [Fact]
    public void Confirm_WaitingPayment_StateIsDelivering()
    {
        // Arrange
        var sut = new VendingMachine();
        sut.AddProduct();
        sut.GreenButtonClick();
        sut.Confirm();

        // Act
        sut.Confirm();

        // Assert
        Assert.Equal(MachineState.Delivering, sut.State);
    }

    [Fact]
    public void Confirm_Delivering_StateIsReady()
    {
        // Arrange
        var sut = new VendingMachine();
        sut.GreenButtonClick();
        sut.Confirm();
        sut.Confirm();

        // Act
        sut.Confirm();

        // Assert
        Assert.Equal(MachineState.Ready, sut.State);
    }

    [Fact]
    public void Cancel_Delivering_StateIsFailed()
    {
        var sut = new VendingMachine();
        sut.GreenButtonClick();
        sut.Confirm();
        sut.Confirm();

        // Act
        sut.Cancel();

        // Assert
        Assert.Equal(MachineState.Failed, sut.State);


    }


}
