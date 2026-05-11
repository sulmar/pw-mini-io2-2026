using MiniVendingMachine;

namespace MiniVendingMachine.Tests;

public class VendingMachineTests
{
    [Fact]
    public void Initial_state_is_Idle()
    {
        var sut = new VendingMachine();

        Assert.Equal(VendingMachineState.Idle, sut.State);
    }

    [Fact]
    public void Selection_from_Idle_calls_Confirm_and_enters_Awaiting()
    {
        var sut = new VendingMachine();

        sut.Selection(1);

        Assert.Equal(VendingMachineState.Awaiting, sut.State);
    }

    [Fact]
    public void Happy_path_Confirm_sequence_goes_through_all_states_and_returns_to_Idle()
    {
        var sut = new VendingMachine();

        sut.Selection(42);
        Assert.Equal(VendingMachineState.Awaiting, sut.State);

        sut.Confirm();
        Assert.Equal(VendingMachineState.Processing, sut.State);

        sut.Confirm();
        Assert.Equal(VendingMachineState.Preparing, sut.State);

        sut.Confirm();
        Assert.Equal(VendingMachineState.Idle, sut.State);
    }

    [Fact]
    public void Timeout_from_Awaiting_returns_to_Idle()
    {
        var sut = new VendingMachine();
        sut.Selection(1);
        Assert.Equal(VendingMachineState.Awaiting, sut.State);

        sut.Timeout();

        Assert.Equal(VendingMachineState.Idle, sut.State);
    }

    [Fact]
    public void Cancel_from_Processing_returns_to_Idle()
    {
        var sut = new VendingMachine();
        sut.Selection(1);
        sut.Confirm();
        Assert.Equal(VendingMachineState.Processing, sut.State);

        sut.Cancel();

        Assert.Equal(VendingMachineState.Idle, sut.State);
    }

    [Fact]
    public void Cancel_from_Preparing_enters_Failure()
    {
        var sut = new VendingMachine();
        sut.Selection(1);
        sut.Confirm();
        sut.Confirm();
        Assert.Equal(VendingMachineState.Preparing, sut.State);

        sut.Cancel();

        Assert.Equal(VendingMachineState.Failure, sut.State);
    }

    [Fact]
    public void Graph_is_non_empty_Mermaid()
    {
        var sut = new VendingMachine();

        Assert.False(string.IsNullOrWhiteSpace(sut.Graph));
        Assert.Contains("stateDiagram", sut.Graph);
    }

    [Fact]
    public void Cancel_from_Idle_throws()
    {
        var sut = new VendingMachine();

        Assert.Throws<InvalidOperationException>(() => sut.Cancel());
        Assert.Equal(VendingMachineState.Idle, sut.State);
    }

    [Fact]
    public void Timeout_from_Idle_throws()
    {
        var sut = new VendingMachine();

        Assert.Throws<InvalidOperationException>(() => sut.Timeout());
    }

    [Fact]
    public void Cancel_from_Awaiting_throws()
    {
        var sut = new VendingMachine();
        sut.Selection(1);

        Assert.Throws<InvalidOperationException>(() => sut.Cancel());
        Assert.Equal(VendingMachineState.Awaiting, sut.State);
    }

    [Fact]
    public void Failure_is_terminal_no_further_triggers()
    {
        var sut = new VendingMachine();
        sut.Selection(1);
        sut.Confirm();
        sut.Confirm();
        sut.Cancel();
        Assert.Equal(VendingMachineState.Failure, sut.State);

        Assert.Throws<InvalidOperationException>(() => sut.Confirm());
        Assert.Throws<InvalidOperationException>(() => sut.Cancel());
        Assert.Throws<InvalidOperationException>(() => sut.Timeout());
        Assert.Equal(VendingMachineState.Failure, sut.State);
    }
}
