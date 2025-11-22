namespace Dictionary.Models
{
    public static class SynAntMeaning
    {
        public static Dictionary<string, List<string>> Synonyms { get; set; } = new();
        public static Dictionary<string, List<string>> Antonyms { get; set; } = new();
    }
}
