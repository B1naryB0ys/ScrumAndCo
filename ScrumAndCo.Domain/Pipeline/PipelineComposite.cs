namespace ScrumAndCo.Domain.Pipeline;

public abstract class PipelineComposite : PipelineStep
{
    public string Name { get; set; }
    private List<PipelineStep> _pipelineSteps = new();
    
    public void AddPipelineComponent(PipelineStep component)
    {
        _pipelineSteps.Add(component);
    }
    
    public override void AcceptVisitor(IPipelineVisitor visitor)
    {
        foreach (var comp in _pipelineSteps)
        {
            comp.AcceptVisitor(visitor);
        }
    }
    
    public override void Execute()
    {   
        Console.WriteLine($"{GetType().Name}: Running");
    }
}