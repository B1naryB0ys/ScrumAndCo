namespace ScrumAndCo.Domain.Pipeline;

public abstract class PipelineStep
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public virtual void Execute()
    {
        Console.WriteLine($"{GetType().Name}: Running");
    }
    public abstract void AcceptVisitor(IPipelineVisitor visitor);
}