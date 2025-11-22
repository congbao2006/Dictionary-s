namespace Dictionary.Models
{
    public class Meaning
    {
        public string Definition { get; set; }
        public string Description { get; set; }
        public string Example { get; set; }

        public Meaning(string definition, string description, string example)
        {
            Definition = definition;
            Description = description;
            Example = example;
        }
    }
}
