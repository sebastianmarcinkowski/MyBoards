namespace MyBoards.Entities
{
    public class State
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public List<WorkItem> WorkItems { get; set; } = new List<WorkItem>();
    }
}
