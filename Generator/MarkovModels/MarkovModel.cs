using System.Collections.Generic;

namespace Generator.MarkovModels
{
    public class MarkovModel
    {
        public Word[] Words { get; set; }
        public Word[] StartWords { get; set; }
        public IEnumerable<WordLink> WordLinks { get; set; }
    }
}
