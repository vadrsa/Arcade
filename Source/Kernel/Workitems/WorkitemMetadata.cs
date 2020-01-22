namespace Kernel.Workitems
{
    public class WorkitemMetadata
    {
        public WorkitemMetadata(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}
