namespace ScrumAndCo.Domain.Pipeline;

public abstract class PipelineComposite : PipelineStep
{
    public string Name { get; set; }
    private List<PipelineStep> _pipelineSteps = new();
    
    public void AddPipelineComponent(PipelineStep component)
    {
        _pipelineSteps.Add(component);
    }
    
    public void RemovePipelineComponent(PipelineStep component)
    {
        _pipelineSteps.Remove(component);
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
        foreach (var comp in _pipelineSteps)
        {
            comp.Execute();
        }
    }
}