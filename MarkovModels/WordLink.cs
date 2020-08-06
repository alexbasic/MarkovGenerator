namespace Generator.MarkovModels
{
    public class WordLink
    {
        public Word FromWord { get; set; }
        public Word ToWord { get; set; }
        public int Weight { get; set; }
    }
}
