using System.Globalization;
using Stateless;

namespace MiniVendingMachine;

// stateless

public class VendingMachine
{
    public string Graph => Stateless.Graph.MermaidGraph.Format(_stateMachine.GetInfo());

    public VendingMachineState State => _stateMachine.State;

    private StateMachine<VendingMachineState, VendingMachineTrigger> _stateMachine;

    public VendingMachine()
    {
        _stateMachine =  new StateMachine<VendingMachineState, VendingMachineTrigger>(VendingMachineState.Idle);

        _stateMachine.Configure(VendingMachineState.Idle)
            .OnEntry(()=>Console.WriteLine("Vending machine is idle"))
            .Permit(VendingMachineTrigger.Confirm, VendingMachineState.Awaiting)
            .OnExit(()=>Console.WriteLine("Vending machine is awaiting"));

        _stateMachine.Configure(VendingMachineState.Awaiting)
            .Permit(VendingMachineTrigger.Confirm, VendingMachineState.Processing)
            .Permit(VendingMachineTrigger.Timeout, VendingMachineState.Idle);

        _stateMachine.Configure(VendingMachineState.Processing)
            .Permit(VendingMachineTrigger.Confirm, VendingMachineState.Preparing)
            .Permit(VendingMachineTrigger.Cancel, VendingMachineState.Idle);

        _stateMachine.Configure(VendingMachineState.Preparing)
            .Permit(VendingMachineTrigger.Confirm, VendingMachineState.Idle)
            .Permit(VendingMachineTrigger.Cancel, VendingMachineState.Failure);

        _stateMachine.Configure(VendingMachineState.Failure);
    }

    public void Selection(byte productId)
    {
      Confirm();
    }

    public void Confirm()
    {
        _stateMachine.Fire(VendingMachineTrigger.Confirm);
    }

    public void Cancel()
    {
        _stateMachine.Fire(VendingMachineTrigger.Cancel);
    }

    public void Timeout()
    {
        _stateMachine.Fire(VendingMachineTrigger.Timeout);
    }
}

public enum VendingMachineState
{
    Idle,
    Awaiting,
    Processing,
    Preparing,
    /// <summary>Stan końcowy — brak przejść wychodzących.</summary>
    Failure
}

public enum VendingMachineTrigger
{
    Confirm,
    Cancel,
    Timeout
}
