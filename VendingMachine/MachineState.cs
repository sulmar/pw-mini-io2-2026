using System.Globalization;
using Stateless;

namespace MiniVendingMachine;

public enum MachineState
{
    Ready,
    Selection,
    WaitingForPayment,
    Delivering,
    Failed
}

public enum MachineTrigger
{
    Confirm,
    Cancel,
    Timeout
}

public class VendingMachine
{
    // dotnet add package Stateless

    public MachineState State => _stateMachine.State;

    private StateMachine<MachineState, MachineTrigger> _stateMachine;

    public string Graph => Stateless.Graph.MermaidGraph.Format(_stateMachine.GetInfo());

    private int qty = 0;

    public void AddProduct() => qty++;

    public VendingMachine()
    {
        _stateMachine = new StateMachine<MachineState, MachineTrigger>(MachineState.Ready);

        _stateMachine.Configure(MachineState.Ready)
            .Permit(MachineTrigger.Confirm, MachineState.Selection);

        _stateMachine.Configure(MachineState.Selection)
            .OnEntry(()=>Console.WriteLine("Wybierz produkt"), "Wybierz")
            .PermitIf(MachineTrigger.Confirm, MachineState.WaitingForPayment, () => qty > 0)
            .PermitIf(MachineTrigger.Confirm, MachineState.Ready, () => qty <= 0)
            .Permit(MachineTrigger.Cancel, MachineState.Ready)
            .Permit(MachineTrigger.Timeout, MachineState.Ready)
            .OnExit(()=>Console.WriteLine("Wybrano produkt"), "Wybrano");

        _stateMachine.Configure(MachineState.WaitingForPayment)
            .Permit(MachineTrigger.Confirm, MachineState.Delivering)
            .Permit(MachineTrigger.Cancel, MachineState.Ready)
            .Permit(MachineTrigger.Timeout, MachineState.Ready);

        _stateMachine.Configure(MachineState.Delivering)
            .Permit(MachineTrigger.Confirm, MachineState.Ready)
            .Permit(MachineTrigger.Cancel, MachineState.Failed);
    }

    public void Confirm() => _stateMachine.Fire(MachineTrigger.Confirm);

    public void GreenButtonClick() => Confirm();
    public void Cancel() => _stateMachine.Fire(MachineTrigger.Cancel);
    private void Timeout() => _stateMachine.Fire(MachineTrigger.Timeout);
}
