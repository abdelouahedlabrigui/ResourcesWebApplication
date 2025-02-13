using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResourcesWebApplication.Models.Processing;

namespace ResourcesWebApplication.Library
{
    public class Tokens
    {
        public List<Token> GetTokenCountsAndLength(string text)
        {
            string[] words = text.Split(' ');
            var counts = words.GroupBy(token => token)
                .ToDictionary(group => group.Key, group => group.Count());
            var length = counts.Select(pair => new { Token = pair.Key, Count = pair.Value })
                .ToList();
            List<Token> tokens = new List<Token>();
            foreach (var item in length)
            {
                Token token = new Token
                {
                    Word = item.Token,
                    Count = item.Count.ToString(),
                    Length = item.Token.Length.ToString()
                };
                tokens.Add(token);
            }
            return tokens;
        }
    }
}