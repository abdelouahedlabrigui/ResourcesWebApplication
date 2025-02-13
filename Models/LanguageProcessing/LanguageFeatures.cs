using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResourcesWebApplication.Models.News;

namespace ResourcesWebApplication.Models.LanguageProcessing
{
    public class LanguageFeatures
    {
        public List<Entity> Entity { get; set; }
        public List<LocalTree> LocalTree { get; set; }
        public List<AnswerSentiment> NLPSentence { get; set; }
        public List<NounChunk> NounChunk { get; set; }
        public List<ParseTree> ParseTree { get; set; }
        public List<Pos> PartsOfSpeech { get; set; }
    }
}