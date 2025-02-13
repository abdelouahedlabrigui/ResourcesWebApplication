using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Library.GenerativeAI;
using ResourcesWebApplication.Models;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Models.Documents;
using ResourcesWebApplication.Models.LanguageProcessing;
using ResourcesWebApplication.Models.News;
using ResourcesWebApplication.Models.Politics.Law;
using ResourcesWebApplication.Models.Politics.Law.Bankruptcy;

namespace ResourcesWebApplication.Controllers
{
    public class NewsArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsArticlesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> GetNewsDocumentsTopics()
        {
            try
            {
                var query = await _context.NewsDocuments
                    .Select(s => new {s.Topic})
                    .Distinct()
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query results: {query.Count}"});
                }
                
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = $"Error: {ex.Message}"});
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeletePlainTextNewsDocument(int id)
        {
            try
            {
                PlainTextNewsDocument find = await _context.PlainTextNewsDocuments.FindAsync(id);
                if (find.Id == 0)
                {
                    return Ok(new {Message = $"Query results, no id found: {find.Id}"});
                }
                _context.PlainTextNewsDocuments.Remove(find);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "News document deleted successfully."});    
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = $"Error: {ex.Message}"});
            } 
        }
        [HttpGet]
        public async Task<IActionResult> PostPlainTextNewsDocument(string fullAddress, string topic)
        {
            try
            {
                var fileInfo = new FileInfo(fullAddress);
                PlainTextNewsDocument metadata = new PlainTextNewsDocument
                {
                    Topic = topic,
                    FullAddress = fileInfo.FullName.ToString(),
                    Name = fileInfo.Name.ToString(),
                    LastWriteTime = fileInfo.LastWriteTime.ToString(),
                    LastAccessTime = fileInfo.LastAccessTime.ToString(),
                    DirectoryName = fileInfo.DirectoryName.ToString(),
                    Length = fileInfo.Length.ToString(),
                    CreatedAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                };
                _context.PlainTextNewsDocuments.Add(metadata);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Plaintext news document saved successfully."}); 
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = $"Error: {ex.Message}"});
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPlainTextNewsDocument()
        {
            try
            {
                var query = await _context.PlainTextNewsDocuments
                    .Select(s => new { s.Topic, s.FullAddress, s.Id, s.Name, s.CreatedAT })
                    .Take(10)
                    .OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query results: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = $"Error: {ex.Message}"});
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPlainTextNewsDocumentByFilename(string filename)
        {
            try
            {
                var query = await _context.PlainTextNewsDocuments
                    .Select(s => new { s.Topic, s.FullAddress, s.Id, s.Name, s.CreatedAT })
                    .Where(w => w.Name == filename)
                    .OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query results: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = $"Error: {ex.Message}"});
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteNewsDocumentById(int id)
        {
            try
            {
                NewsDocument find = await _context.NewsDocuments.FindAsync(id);
                if (find.Id == 0)
                {
                    return Ok(new {Message = $"Query results, no id found: {find.Id}"});
                }
                _context.NewsDocuments.Remove(find);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "News document deleted successfully."});    
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = $"Error: {ex.Message}"});
            } 
        }
        [HttpGet]
        public async Task<IActionResult> PostNewsRelatedDocument(string topic, string title, string url)
        {
            try
            {
                NewsDocument newsDocument = new NewsDocument{
                    Topic = topic,
                    Title = title,
                    Url = url,
                    CreatedAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                };
                if (!ModelState.IsValid)
                {
                    return Ok(new {Message = $"Model state may not be valid: {ModelState.IsValid}"});
                }
                _context.NewsDocuments.Add(newsDocument);
                await _context.SaveChangesAsync();
                return Ok(new {Message = $"Data saved to News Document table."});
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = $"Error: {ex.Message}"});
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetNewsDocuments()
        {
            try
            {
                var query = await _context.NewsDocuments
                    .Take(10)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query results: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = $"Error: {ex.Message}"});
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetNewsDocumentsByTopic(string topic)
        {
            try
            {
                var query = await _context.NewsDocuments
                    .Where(w => w.Topic.Contains($"{topic}"))
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query results: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = $"Error: {ex.Message}"});
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetNewsArticlesByTitle(string title)
        {
            try
            {
                var query = await _context.NewsArticles
                    .Where(w => w.Title == title)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query results: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = $"Error: {ex.Message}"});
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetNewsArticlesByGreaterThanDate(string createdAT)
        {
            try
            {
                var query = await _context.NewsArticles
                    .Where(w => w.CreatedAt.Contains($"{createdAT}"))
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return Ok(new {Message = $"Query results: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return Ok(new {Message = $"Error: {ex.Message}"});
            }
        }

        public async Task<IActionResult> DeleteNewsPapperInformationById(int id)
        {
            try
            {
                var newsArticle = await _context.NewsArticles.FindAsync(id);
                _context.NewsArticles.Remove(newsArticle);
                await _context.SaveChangesAsync();
                return Ok(new {Message = "Deleted News Papper Information By Id"});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GETNewsPapperInformationIds()
        {
            try
            {
                var query = await _context.NewsArticles
                    .OrderByDescending(o => o.Id)
                    .Select(s => s.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return NoContent();
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GETNewsPapperInformationById(int id)
        {
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return NotFound();
                }
                var query = await _context.NewsArticles
                    .Where(x => x.Id == id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return NoContent();
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GETNewsPapperInformation()
        {
            try
            {
                var query = await _context.NewsArticles
                    .Take(10)
                    .OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return NoContent();
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> POSTNewsPapperInformation([FromBody] NewsArticle newsArticle)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                _context.NewsArticles.Add(newsArticle);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "News article added successfully." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        static string RemovePunctuation(string text)
        {
            text = Regex.Replace(text, @"[^\w\s]", ""); // Remove all non-alphanumeric characters
            return text.Trim(); // Trim the end of the string if there are any extra spaces
        }
        [HttpGet]
        public async Task<IActionResult> GetMatchTextToSentences(string matcher)
        {
            try
            {
                
                string trdChar = matcher;
                char[] charactersToTake = new char[3];

                // Select first 3 characters from the string
                int index = 0;
                for (int i = 0; i < 3 && index < trdChar.Length; i++) {
                    charactersToTake[i] = trdChar[index++];
                }

                // Output: {'T', 'h', 'i'}
                string searchString = string.Join("", charactersToTake);
                // SELECT COUNT(*) AS C, RootDep, RootText FROM BankruptcyNounChunks WHERE Text LIKE '%cod%' GROUP BY RootDep, RootText ORDER BY C;
                var rootTextNounChunks = await _context.BankruptcyNounChunks
                    .Where(w => w.Text.Contains(searchString))
                    .GroupBy(g => new { g.RootText })
                    .Select(s => 
                        new { 
                            RootText = s.Key.RootText, 
                            Count = s.Count()}
                    ).ToListAsync();
                // SELECT COUNT(*) AS C, RootDep FROM BankruptcyNounChunks WHERE Text LIKE '%cod%' GROUP BY RootDep ORDER BY C;
                var rootDepNounChunks = await _context.BankruptcyNounChunks
                    .Where(w => w.Text.Contains(searchString))
                    .GroupBy(g => new {g.RootDep})
                    .Select(s => new {
                        RootDep = s.Key.RootDep,
                        Count = s.Count()
                    })
                    .ToListAsync();
                // SELECT COUNT(*) AS C, Dep, Ancestors FROM BankruptcyLocalTrees WHERE Ancestors LIKE '%cod%' GROUP BY Dep, Ancestors ORDER BY C;
                var ancestorsLocalTrees = await _context.BankruptcyLocalTrees
                    .Where(w => w.Ancestors.Contains(searchString))
                    .Where(w => w.Dep != "PUNCT")
                    .GroupBy(g => new { g.Ancestors })
                    .Select(s => 
                        new { 
                            Ancestors = s.Key.Ancestors, 
                            Count = s.Count()}
                    ).ToListAsync();
                var ancestorsTokens = ancestorsLocalTrees
                    .Select(s => new {
                        Ancestors = new {
                            Token = s.Ancestors.Replace(" ", "").Replace(")", "").Replace("(", "").Replace(";", "").Replace("'", "").Replace("[","").Replace("]", "").Replace(":", "").Replace(".", "").Replace("‘", "").Replace("’", "")
                                    .Split(',').Distinct(),
                            CountToken = s.Ancestors.Replace(" ", "").Replace(")", "").Replace("(", "").Replace(";", "").Replace("'", "").Replace("[","").Replace("]", "").Replace(":", "").Replace(".", "").Replace("‘", "").Replace("’", "")
                                    .Split(',').Count()
                        },
                        CountOrginal = s.Count
                    });
                var distinctAncestorsToken = new {
                    AncestorsLocalTrees = ancestorsTokens                        
                        .GroupBy(g => new { g.Ancestors })
                        .Select(s => s.Key.Ancestors.Token).Distinct()
                };
                
                // SELECT COUNT(*) AS C, Dep FROM BankruptcyLocalTrees WHERE Ancestors LIKE '%cod%' GROUP BY Dep ORDER BY C;
                var depLocalTrees = await _context.BankruptcyLocalTrees
                    .Where(w => w.Ancestors.Contains(searchString))
                    .Where(w => w.Dep != "PUNCT")
                    .GroupBy(g => new {g.Dep})
                    .Select(s => new {
                        Dep = s.Key.Dep,
                        Count = s.Count()
                    })
                    .ToListAsync();
                // SELECT COUNT(*) AS C, Dep, Child FROM BankruptcyParseTrees WHERE Child LIKE '%cod%' GROUP BY Dep, Child ORDER BY C;
                var childParseTrees = await _context.BankruptcyParseTrees
                    .Where(w => w.Child.Contains(searchString))
                    .GroupBy(g => new { g.Child })
                    .Select(s => 
                        new { 
                            Child = s.Key.Child, 
                            Count = s.Count()}
                    ).ToListAsync();
                var childTokens = childParseTrees
                    .Select(s => new {
                        Child = new {
                            Token = s.Child.Replace(" ", "").Replace(")", "").Replace("(", "").Replace(";", "").Trim().ToLower().Replace("'", "").Replace("[","").Replace("]", "").Replace(":", "").Replace(".", "").Replace("‘", "").Replace("’", "")
                            .Split(',').Distinct(),
                            CountToken = s.Child.Replace(" ", "").Replace(")", "").Replace("(", "").Replace(";", "").Replace("'", "").Replace("[","").Replace("]", "").Replace(":", "").Replace(".", "").Replace("‘", "").Replace("’", "")
                            .Split(',').Count()
                        },
                        CountOrginal = s.Count
                    });
                var distinctChildToken = new {
                    ChildParseTrees = childTokens
                        .GroupBy(g => new { g.Child })
                        .Select(s => s.Key.Child.Token).Distinct()
                };
                // SELECT COUNT(*) AS C, Dep FROM BankruptcyParseTrees WHERE Child LIKE '%cod%' GROUP BY Dep ORDER BY C;
                var depParseTrees = await _context.BankruptcyParseTrees
                    .Where(w => w.Child.Contains(searchString))
                    .GroupBy(g => new {g.Dep})
                    .Select(s => new {
                        Dep = s.Key.Dep,
                        Count = s.Count()
                    })
                    .ToListAsync();
                var data = new {
                    RootTextNounChunks = rootTextNounChunks,
                    RootDepNounChunks = rootDepNounChunks,
                    AncestorsTokensLocalTree = distinctAncestorsToken,
                    DepLocalTrees = depLocalTrees,
                    ChildTokensParseTree = distinctChildToken,
                    DepParseTrees = depParseTrees,
                };
                return Ok(data);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // SELECT COUNT(*) AS CHP, HeadPos FROM dbo.BankruptcyParseTrees GROUP BY HeadPos ORDER BY CHP DESC;
        public async Task<IActionResult> GetPosFromParseAndLocalTrees()
        {
            try
            {
                var posParseTree = await _context.BankruptcyParseTrees
                    .GroupBy(g => g.HeadPos)
                    .Select(s => new {
                        HeadPos = s.Key,
                        Counts = s.Count()
                    })
                    .OrderByDescending(o => o.Counts)
                    .ToListAsync();
                if (posParseTree.Count == 0)
                {
                    return NoContent();
                }
                var posLocalTree = await _context.BankruptcyParseTrees
                    .GroupBy(g => g.HeadPos)
                    .Select(s => new {
                        HeadPos = s.Key,
                        Counts = s.Count()
                    })
                    .OrderByDescending(o => o.Counts)
                    .ToListAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // SELECT
        // (SELECT COUNT(*) AS CC FROM BankruptcyLocalTrees WHERE Dep = 'compound') AS CLocalTrees,
        // (SELECT COUNT(*) AS CC FROM BankruptcyParseTrees WHERE Dep = 'compound') AS CParseTrees
        [HttpGet]
        public async Task<IActionResult> GetLinguisticFeaturesSentencesMatchToDependecies(string text, string dep, string sentiment)
        {
            try
            {
               
                var localTree = await _context.BankruptcyLocalTrees
                    .Where(w => w.Dep == dep)
                    .Where(w => w.Text == text)
                    .GroupBy(g => new {g.Dep, g.Text})
                    .Select(s => new 
                    { 
                        Text = s.Key.Text,
                        Dep = s.Key.Dep, 
                        Count = s.Count()
                    })
                    .ToListAsync();
                
                var parseTree = await _context.BankruptcyParseTrees
                    .Where(w => w.Dep == dep)
                    .Where(w => w.Text == text)
                    .GroupBy(g => new {g.Dep, g.Text})
                    .Select(s => new 
                    { 
                        Text = s.Key.Text,
                        Dep = s.Key.Dep, 
                        Count = s.Count()
                    })
                    .ToListAsync();
                List<object> sentencesMatch = new List<object>();

                switch (sentiment)
                {
                    case "positive":                        
                        var nlpSentencesPositive = await _context.BankruptcyNLPSentences
                            .Where(w => w.Positive > w.Negative)
                            .Where(w => w.Sentence.Contains($"{text}"))
                            .OrderByDescending(o => o.Id)
                            .ToListAsync();
                        var positiveMatch = new 
                        {
                            NlpSentences = nlpSentencesPositive
                        };
                        sentencesMatch.Add(positiveMatch);
                        
                        var dataPos = new 
                        {
                            LocalTreeCounts = localTree,
                            ParseTreeCounts = parseTree,
                            SentencesMatch = sentencesMatch
                        };
                        
                        return Ok(dataPos);
                    case "negative": 
                        var nlpSentencesNegative = await _context.BankruptcyNLPSentences
                            .Where(w => w.Positive < w.Negative)
                            .Where(w => w.Sentence.Contains($"{text}"))
                            .OrderByDescending(o => o.Id)
                            .ToListAsync();
                        var negativeMatch = new 
                        {
                            NlpSentences = nlpSentencesNegative
                        };
                        sentencesMatch.Add(negativeMatch);   
                        
                        var dataNeg = new 
                        {
                            LocalTreeCounts = localTree,
                            ParseTreeCounts = parseTree,
                            SentencesMatch = sentencesMatch
                        };
                        
                        return Ok(dataNeg);
                    case "neutral":                        
                        var nlpSentencesNeutral = await _context.BankruptcyNLPSentences
                            .Where(w => 
                    (w.Positive < w.Neutral) && (w.Negative < w.Neutral))
                            .Where(w => w.Sentence.Contains($"{text}"))
                            .OrderByDescending(o => o.Id)
                            .ToListAsync();
                        var neutralMatch = new 
                        {
                            NlpSentences = nlpSentencesNeutral
                        };
                        sentencesMatch.Add(neutralMatch); 
                        
                        var dataSents = new 
                        {
                            LocalTreeCounts = localTree,
                            ParseTreeCounts = parseTree,
                            SentencesMatch = sentencesMatch
                        };
                        
                        return Ok(dataSents);
                    default:
                        break;                    
                }
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // SELECT Dep, COUNT(*) AS CD FROM dbo.BankruptcyLocalTrees GROUP BY Dep ORDER BY CD DESC;
        // SELECT RootDep, COUNT(*) AS CRootDep FROM dbo.BankruptcyNounChunks GROUP BY RootDep ORDER BY CRootDep DESC;
        // SELECT Dep, COUNT(*) AS CD FROM dbo.BankruptcyParseTrees GROUP BY Dep ORDER BY CD DESC;
        [HttpGet]
        public async Task<IActionResult> GetLinguisticFeaturesDependenciesCount()
        {
            try
            {
                
                var countNounChunks = await _context.BankruptcyNounChunks
                    .GroupBy(g => g.RootDep)
                    .Select(s => new 
                    {
                        Dep = s.Key,
                        Count = s.Count().ToString()
                    })
                    .ToListAsync();
                

                List<object> dataListLocalTree = new List<object>();
                List<object> dataListParseTree = new List<object>();
                List<object> dataListNounChunk = new List<object>();

                foreach (var dep in countNounChunks)
                {
                    var countLocalTrees = await _context.BankruptcyLocalTrees
                        .Where(w => w.Dep == dep.Dep)
                        .GroupBy(g => g.Dep)
                        .Select(s => new 
                        {
                            Dep = s.Key,
                            Count = s.Count().ToString()
                        })
                        .ToListAsync();
                    
                    var countParseTrees = await _context.BankruptcyParseTrees
                        .Where(w => w.Dep == dep.Dep)
                        .GroupBy(g => g.Dep)
                        .Select(s => new 
                        {
                            Dep = s.Key,
                            Count = s.Count().ToString()
                        })
                        .ToListAsync();

                    var countNounChunks_V2 = await _context.BankruptcyNounChunks
                        .GroupBy(g => g.RootDep)
                        .Select(s => new 
                        {
                            Dep = s.Key,
                            Count = s.Count().ToString()
                        })
                        .ToListAsync();
                    
                    dataListLocalTree.Add(countLocalTrees);
                    dataListParseTree.Add(countParseTrees);
                    dataListNounChunk.Add(countNounChunks_V2);
                }
                var verifyShapes = new {
                    TotalParseTree = dataListParseTree.Count(),
                    TotalLocalTree = dataListLocalTree.Count(),
                    TotalNounChunk = dataListNounChunk.Count(),
                };
                var data = new {
                    CountNounChunksDep = countNounChunks,
                    CountParseTree = dataListParseTree,
                    CountLocalTree = dataListLocalTree,
                    Shapes = verifyShapes
                };
                return Ok(data);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetLiguisticFeaturesNSubjRecordsCounts(string nsubj)
        {
            try
            {
                if (string.IsNullOrEmpty(nsubj) || nsubj.Length != 2)
                {
                    return NotFound();
                }
                var countLocalTrees = await _context.BankruptcyLocalTrees
                    .Where(w => w.Text == nsubj)
                    .Select(s => s.Text)
                    .ToListAsync();
                var countNounChunks = await _context.BankruptcyNounChunks
                    .Where(w => w.Text == nsubj)
                    .Select(s => s.Text)
                    .ToListAsync();
                var countParseTrees = await _context.BankruptcyParseTrees
                    .Where(w => w.Text == nsubj)
                    .Select(s => s.Text)
                    .ToListAsync();
                var countNLPSentences = await _context.BankruptcyNLPSentences
                    .Where(w => w.Sentence.Contains($"{nsubj}"))
                    .Select(s => s.Sentence)
                    .ToListAsync();

                List<object> countsList = new List<object>(); 
                var counts = new {
                    Name = "Local Tree",
                    NSubj = nsubj,
                    Count = countLocalTrees.Count(),
                };

                countsList.Add(counts);
                counts = new {
                    Name = "Parse Tree",
                    NSubj = nsubj,
                    Count = countParseTrees.Count(),
                };
                countsList.Add(counts);
                counts = new {
                    Name = "Noun Chunk",
                    NSubj = nsubj,
                    Count = countNounChunks.Count(),
                };
                countsList.Add(counts);
                counts = new {
                    Name = "Sentences",
                    NSubj = nsubj,
                    Count = countNLPSentences.Count(),
                };
                countsList.Add(counts);

                return Ok(countsList);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetLinguisticFeaturesRecordsCounts()
        {
            try
            {

                var countLocalTrees = await _context.BankruptcyLocalTrees
                    .Select(s => s.Text)
                    .ToListAsync();
                var countNounChunks = await _context.BankruptcyNounChunks
                    .Select(s => s.Text)
                    .ToListAsync();
                var countParseTrees = await _context.BankruptcyParseTrees
                    .Select(s => s.Text)
                    .ToListAsync();
                var countNLPSentences = await _context.BankruptcyNLPSentences
                    .Select(s => s.Sentence)
                    .ToListAsync();

                List<object> countsList = new List<object>(); 
                var counts = new {
                    Name = "Local Tree",
                    Count = countLocalTrees.Count(),
                };

                countsList.Add(counts);
                counts = new {
                    Name = "Parse Tree",
                    Count = countParseTrees.Count(),
                };
                countsList.Add(counts);
                counts = new {
                    Name = "Noun Chunk",
                    Count = countNounChunks.Count(),
                };
                countsList.Add(counts);
                counts = new {
                    Name = "Sentences",
                    Count = countNLPSentences.Count(),
                };
                countsList.Add(counts);
                if (countsList.Count == 0)
                {
                    return NoContent();
                }
                return Ok(countsList);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostBankruptcyEntities([FromBody] BankruptcyEntity bankruptcyEntity)
        {
            try
            {
                if (bankruptcyEntity == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.BankruptcyEntities.Add(bankruptcyEntity);
                    await _context.SaveChangesAsync();
                }
                return Ok("Data saved successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostBankruptcyParseTrees([FromBody] BankruptcyParseTree bankruptcyParseTree)
        {
            try
            {
                if (bankruptcyParseTree == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.BankruptcyParseTrees.Add(bankruptcyParseTree);
                    await _context.SaveChangesAsync();
                }
                return Ok("Data saved successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostBankruptcyLocalTrees([FromBody] BankruptcyLocalTree bankruptcyLocalTree)
        {
            try
            {
                if (bankruptcyLocalTree == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.BankruptcyLocalTrees.Add(bankruptcyLocalTree);
                    await _context.SaveChangesAsync();
                }
                return Ok("Data saved successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostBankruptcyNounChunks([FromBody] BankruptcyNounChunk bankruptcyNounChunk)
        {
            try
            {
                if (bankruptcyNounChunk == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.BankruptcyNounChunks.Add(bankruptcyNounChunk);
                    await _context.SaveChangesAsync();
                }
                return Ok("Data saved successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostBankruptcyPartOfSpeech([FromBody] BankruptcyPos bankruptcyPos)
        {
            try
            {
                if (bankruptcyPos == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.BankruptcyPartOfSpeech.Add(bankruptcyPos);
                    await _context.SaveChangesAsync();
                }
                return Ok("Data saved successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostBankruptcyNLPSentences([FromBody] BankruptcyNLPSentence bankruptcyNLPSentence)
        {
            try
            {
                if (bankruptcyNLPSentence == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.BankruptcyNLPSentences.Add(bankruptcyNLPSentence);
                    await _context.SaveChangesAsync();
                }
                return Ok("Data saved successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        // [Route("GetProfessionTerms")]
        public async Task<IActionResult> GetProfessionTerms()
        {
            try
            {
                var query = await _context.Professions.OrderByDescending(d => d.Id).ToListAsync();
                if (query.Count == 0)
                {
                    return NoContent();
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpPost]
        // [Route("PostProfessionTerms")]
        public async Task<IActionResult> PostProfessionTerms([FromBody] Profession profession)
        {
            try
            {
                if (profession == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.Professions.Add(profession);
                    await _context.SaveChangesAsync();
                }
                return Ok("Data saved successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 
        [HttpGet]
        public async Task<IActionResult> GetNewsTitleDescription()
        {
            var articles = await _context.NewsTitleDescriptions.OrderByDescending(d => d.Id).ToListAsync();
            return Ok(articles);
        }
        [HttpGet]
        public async Task<IActionResult> GetNewsQuestions()
        {
            var articles = await _context.NewsQuestions.OrderByDescending(d => d.Id).ToListAsync();
            return Ok(articles);
        }
        [HttpPost]
        public async Task<IActionResult> PostNewsTitleDescription([FromBody] NewsTitleDescription news)
        {
            try
            {
                if (news == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.NewsTitleDescriptions.Add(news);
                    await _context.SaveChangesAsync();
                }
                return Ok("Data saved successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostNewsQuestions([FromBody] NewsQuestion news)
        {
            try
            {
                if (news == null)
                {
                    return NoContent();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model Object");
                }
                else 
                {
                    _context.NewsQuestions.Add(news);
                    await _context.SaveChangesAsync();
                }
                return Ok("Data saved successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult GetNewsTitleDescriptionUsingPython(string title)
        {
            try
            {
                string script = @"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\library\Machine-Learning-for-NLP\GenerateNewsDescription.py";
                List<NewsTitleDescription> news = new List<NewsTitleDescription>();
                // Check if the script has already been executed in the current session
                if (HttpContext.Session.GetString("ScriptExecuted") == null)
                {
                    // Mark the script as executed in the session
                    HttpContext.Session.SetString("ScriptExecuted", "true");

                    // Create the file if it doesn't exist
                    if (!System.IO.File.Exists(script + ".executed"))
                    {
                        System.IO.File.Create(script + ".executed").Dispose();
                    }

                    // Execute the script
                    using (Process process = new Process())
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = "cmd.exe";
                        startInfo.RedirectStandardInput = true;
                        startInfo.UseShellExecute = false;

                        process.StartInfo = startInfo;
                        process.Start();

                        StreamWriter sw = process.StandardInput;
                        if (sw.BaseStream.CanWrite)
                        {
                            // sw.WriteLine(@"cd C:\Users\dell\Entrepreneurship\Engineering\machine_learning");
                            // sw.WriteLine(@"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\ml\Scripts\activate");
                            sw.WriteLine(@"python.exe " + script  + @" """ + title  + @"""");
                            // sw.WriteLine(@"deactivate");
                            sw.Close();
                        }
                        
                    }
                }
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IActionResult SpeechPartsCount()
        {
            return View();
        }
        public async Task<IActionResult> GetSpeechPartsCount(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }
            List<AnswerPos> query = await _context.PartOfSpeech
                .Where(p => p.DatasetName.Contains($"news_questions_{id}"))
                .GroupBy(p => new { p.DatasetName, p.Pos_ })
                .Select(g => new AnswerPos()
                {
                    DatasetName = g.Key.DatasetName,
                    Pos_ = g.Key.Pos_,
                    SpeechPartsCounts = g.Count()
                }).ToListAsync();
            return Ok(query);
        }          
        public async Task<IActionResult> GetSpeechPartsByDependenciesCount(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }            
            List<AnswerPosDependency> query = await _context.PartOfSpeech
                .Where(d => d.DatasetName.Contains($"news_questions_{id}"))
                .GroupBy(d => new {d.DatasetName, d.Pos_, d.Dep_})
                .Select(d => new AnswerPosDependency()
                {
                    DatasetName = d.Key.DatasetName,
                    Pos_ = d.Key.Pos_,
                    Dep_ = d.Key.Dep_,
                    SpeechParts_DepsCounts = d.Count()
                }).ToListAsync();
            return Ok(query);
        }                
        public async Task<IActionResult> GetGroupByPartsOfSpeechByPosTag(string id, string tag)
        {
            if (string.IsNullOrWhiteSpace(tag) || string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Parts of speech tag & id are required.");
            } 
            List<LemmaBySpeechPartsCount> query = await _context.PartOfSpeech
                .Where(d => d.Pos_ == tag)
                .Where(d => d.DatasetName.Contains($"news_questions_{id}"))
                .GroupBy(d => new {d.DatasetName, d.Text_, d.Lemma_, d.Pos_, d.Tag_, d.Dep_, d.Shape_, d.Is_Alpha, d.Is_Stop})
                .Select(d => new LemmaBySpeechPartsCount() {
                    DatasetName = d.Key.DatasetName, 
                    Text_ = d.Key.Text_, 
                    Lemma_ = d.Key.Lemma_, 
                    Pos_ = d.Key.Pos_, 
                    Tag_ = d.Key.Tag_,
                    Dep_ = d.Key.Dep_, 
                    Shape_ = d.Key.Shape_, 
                    Is_Alpha = d.Key.Is_Alpha,
                    Is_Stop = d.Key.Is_Stop,
                    CountLemma_ = d.Count()
                })
                .ToListAsync();
            if (query.Count == 0)
            {
                return NoContent();
            }
            return Ok(query);
        }                        
        public async Task<IActionResult> GetPartsOfSpeechByPosTag(string id, string tag)
        {
            if (string.IsNullOrWhiteSpace(tag) || string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Parts of speech tag & id are required.");
            } 
            List<Pos> query = await _context.PartOfSpeech
                .Where(d => d.Pos_ == tag)
                .Where(d => d.DatasetName.Contains($"news_questions_{id}"))
                .ToListAsync();
            if (query.Count == 0)
            {
                return NoContent();
            }
            return Ok(query);
        }
        public async Task<IActionResult> GetLemmaBySpeechPartsCount(string pos, string id)
        {
            if (string.IsNullOrWhiteSpace(pos) || string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Parts of speech tag & id are required.");
            }            
            List<LemmaBySpeechParts> query = await _context.PartOfSpeech
                .Where(d => d.Pos_.Contains($"{pos}"))
                .Where(d => d.DatasetName.Contains($"news_questions_{id}"))
                .GroupBy(d => new {d.DatasetName, d.Pos_, d.Lemma_})
                .Select(d => new LemmaBySpeechParts()
                {
                    DatasetName = d.Key.DatasetName,
                    Lemma_ = d.Key.Lemma_,
                    Pos_ = d.Key.Pos_,
                    DuplicateVerbsCount = d.Count()
                }).ToListAsync();
            return Ok(query);
        }
        public IActionResult GetLemmaBySpeechPartSentenceMatch(string matcher)
        {
            if (string.IsNullOrWhiteSpace(matcher))
            {
                return BadRequest("A match to a sentence is required.");
            }
            var maxMatchCount = _context.NLPSentences
                .Count(s => s.Sentence.Contains($"{matcher}"));

            MatchSentences matches = new MatchSentences()
            {
                Match = matcher,
                Total = maxMatchCount.ToString()
            };
            return Ok(matches);
        }                                 
        public async Task<IActionResult> GetLemmaBySpeechPartSentenceMatchDisplay(string id, string matcher)
        {
            if (string.IsNullOrWhiteSpace(matcher) || string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("A match & id to a sentence is required.");
            }
            List<AnswerSentiment> query = await _context.AnswerSentiments
                .Where(d => d.DatasetName == $"news_questions_{id}")
                .Where(d => d.Sentence.Contains($"{matcher}"))
                .ToListAsync();
            if (query.Count == 0)
            {
                return NotFound();
            }
            return Ok(query);
        }        
        // tag refers to lemma of a token
        public IActionResult GetLemmaWithPosTagBySpeechPartSentenceMatch(string matcher, string tag)
        {
            if (string.IsNullOrWhiteSpace(matcher) || string.IsNullOrWhiteSpace(tag))
            {
                return BadRequest("A match with pos tag to a sentence is required.");
            }
            var maxMatchCount = _context.NLPSentences
                .Count(s => s.Sentence.Contains($"{matcher}") && s.Sentence.Contains($"{tag}"));

            MatchWithTagSentences matches = new MatchWithTagSentences()
            {
                Match = matcher,
                PosTag = tag,
                Total = maxMatchCount.ToString()
            };
            return Ok(matches);
        }
        public IActionResult GetLemmaWithPosYDepTagsBySpeechPartSentenceMatch(string matcher, string pos, string dep)
        {
            if (string.IsNullOrWhiteSpace(matcher) || string.IsNullOrWhiteSpace(pos) || string.IsNullOrWhiteSpace(dep))
            {
                return BadRequest("A match with pos & dep tags to a sentence is required.");
            }            

            var maxMatchCount = _context.NLPSentences
                .Count(s => s.Sentence.Contains($"{matcher}") && s.Sentence.Contains($"{pos}") && s.Sentence.Contains($"{dep}"));

            MatchWithTagYDepSentences matches = new MatchWithTagYDepSentences()
            {
                Match = matcher,
                PosTag = pos,
                DepTag = dep,
                Total = maxMatchCount.ToString()
            };
            return Ok(matches);
        }
        // GET: NewsArticles
        public async Task<IActionResult> Index()
        {
            return View(await _context.NewsArticles.OrderByDescending(d => d.Id).ToListAsync());
        }

        public async Task<IActionResult> QuestionRecord(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var article = await _context.NewsArticles.FirstOrDefaultAsync(d => d.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }        
        public async Task<IActionResult> QuestionRecordsId()
        {
            var questionsIds = await _context.Questions.OrderByDescending(d => d.Id).Select(d => d.Id).ToListAsync();
            if (questionsIds == null)
            {
                return NotFound();
            }
            return Ok(questionsIds);
        }
        public async Task<IActionResult> AnswersRecordsId()
        {
            var questionsIds = await _context.Questions.Select(d => d.Id).ToListAsync();
            if (questionsIds == null)
            {
                return NotFound();
            }
            return Ok(questionsIds);
        }

        public async Task<IActionResult> PostQuestion([FromBody] Question question)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
            return Ok();
        }
        public IActionResult DeleteNlpDataWhereAnswerId(string answerId)
        {
            string nlp_data_id = $"news_questions_{answerId}";
            return Ok();
        }
        public async Task<IActionResult> GetEntities(string answer_id)
        {
            string nlp_data_id = $"news_questions_{answer_id}";
            return Ok(await _context.Entities.Where(d => d.DatasetName == nlp_data_id).ToListAsync());
        }
        public async Task<IActionResult> ExecuteGenerateAnswerNLP(string answer_id, string paragraph)
        {
            string scriptPath = @"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\library\Machine-Learning-for-NLP\POST_En_LinguisticFeatures.py";
            
            // Check if the script has already been executed in the current session
            if (HttpContext.Session.GetString("ScriptExecuted") == null)
            {
                // Mark the script as executed in the session
                HttpContext.Session.SetString("ScriptExecuted", "true");

                // Create the file if it doesn't exist
                if (!System.IO.File.Exists(scriptPath + ".executed"))
                {
                    System.IO.File.Create(scriptPath + ".executed").Dispose();
                }
                // Execute the script
                using (Process process = new Process())
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "cmd.exe";
                    startInfo.RedirectStandardInput = true;
                    startInfo.UseShellExecute = false;
                    // startInfo.RedirectStandardOutput = true;
                    // startInfo.RedirectStandardError = true;
                    // process.StartInfo.CreateNoWindow = false;

                    process.StartInfo = startInfo;
                    process.Start();

                    StreamWriter sw = process.StandardInput;
                    if (sw.BaseStream.CanWrite)
                    {
                        sw.WriteLine(@"cd C:\Users\dell\Entrepreneurship\Engineering\machine_learning");
                        sw.WriteLine(@"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\ml\Scripts\activate");
                        sw.WriteLine($@"python.exe {scriptPath} ""{paragraph}"" ""{answer_id}""");
                        sw.WriteLine(@"deactivate");
                        sw.Close();
                    }
                    process.WaitForExit();
                }
            }
            string nlp_data_id = $"news_questions_{answer_id}";
            await Task.Delay(10);
            var entity = await _context.Entities.Where(d => d.DatasetName.Contains($"{nlp_data_id}")).ToListAsync();
            var localTree = await _context.LocalTrees.Where(d => d.DatasetName.Contains($"{nlp_data_id}")).ToListAsync();
            var nlpSentence = await _context.AnswerSentiments.Where(d => d.DatasetName.Contains($"{nlp_data_id}")).ToListAsync();
            var nounChunk = await _context.NounChunks.Where(d => d.DatasetName.Contains($"{nlp_data_id}")).ToListAsync();
            var parseTree = await _context.ParseTrees.Where(d => d.DatasetName.Contains($"{nlp_data_id}")).ToListAsync();
            var partsOfSpeech = await _context.PartOfSpeech.Where(d => d.DatasetName.Contains($"{nlp_data_id}")).ToListAsync();
            

            LanguageFeatures languageFeatures = new LanguageFeatures()
            {
                Entity = entity,
                LocalTree = localTree,
                NLPSentence = nlpSentence,
                NounChunk = nounChunk,
                ParseTree = parseTree,
                PartsOfSpeech = partsOfSpeech,
            };
            return Ok(languageFeatures);
        }

        public async Task<IActionResult> ExecuteGenerateAnswer(string articleTitle, string questionString)
        {
            // if (string.IsNullOrWhiteSpace(articleTitle) || string.IsNullOrWhiteSpace(questionString))
            // {
            //     return NotFound();
            // }
            string scriptPath = @"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\library\LLMs\TextGeneration\AnswerGenerator.py";
            
            // Check if the script has already been executed in the current session
            if (HttpContext.Session.GetString("ScriptExecuted") == null)
            {
                // Mark the script as executed in the session
                HttpContext.Session.SetString("ScriptExecuted", "true");

                // Create the file if it doesn't exist
                if (!System.IO.File.Exists(scriptPath + ".executed"))
                {
                    System.IO.File.Create(scriptPath + ".executed").Dispose();
                }
                // Execute the script
                using (Process process = new Process())
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "cmd.exe";
                    startInfo.RedirectStandardInput = true;
                    startInfo.UseShellExecute = false;
                    // startInfo.RedirectStandardOutput = true;
                    // startInfo.RedirectStandardError = true;

                    process.StartInfo = startInfo;
                    process.Start();

                    StreamWriter sw = process.StandardInput;
                    if (sw.BaseStream.CanWrite)
                    {
                        sw.WriteLine(@"cd C:\Users\dell\Entrepreneurship\Engineering\machine_learning");
                        sw.WriteLine(@"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\ml\Scripts\activate");
                        sw.WriteLine($@"python.exe {scriptPath} ""{articleTitle}"" ""{questionString}""");
                        sw.WriteLine(@"deactivate");
                        sw.Close();
                    }
                    process.WaitForExit();
                }
            }
            await Task.Delay(10);
            return Ok();
        }
        public async Task<IActionResult> GetAnswerById(int? id)
        {
            if (id ==  null)
            {
                return NotFound();
            }
            var query = await _context.Questions.Where(d => d.Id == id).ToListAsync();
            if (query.Count == 0)
            {
                return NotFound();
            }
            return Ok(query);
        }
        public async Task<IActionResult> GetAnswers()
        {
            var query = await _context.Questions.OrderByDescending(d => d.Id).ToListAsync();
            if (query.Count == 0)
            {
                return NotFound();
            }
            return Ok(query);
        }
        public async Task<IActionResult> PostAnswerSentiments([FromBody] AnswerSentiment answerSentiment)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            _context.AnswerSentiments.Add(answerSentiment);
            await _context.SaveChangesAsync();
            return Ok();
        }
        public async Task<IActionResult> GetAnswerByIdYPostToAnswerSentiments(int answerId)
        {
            if (string.IsNullOrEmpty(answerId.ToString()))
            {
                return NotFound();
            }
            var answer = await _context.Questions.Where(d => d.Id == answerId).ToListAsync();

            string scriptPath = @"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\library\LLMs\TextGeneration\AnswerSentimentsGenerator.py";
            
            // Check if the script has already been executed in the current session
            if (HttpContext.Session.GetString("ScriptExecuted") == null)
            {
                // Mark the script as executed in the session
                HttpContext.Session.SetString("ScriptExecuted", "true");

                // Create the file if it doesn't exist
                if (!System.IO.File.Exists(scriptPath + ".executed"))
                {
                    System.IO.File.Create(scriptPath + ".executed").Dispose();
                }
                // Execute the script
                using (Process process = new Process())
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "cmd.exe";
                    startInfo.RedirectStandardInput = true;
                    startInfo.UseShellExecute = false;
                    // startInfo.RedirectStandardOutput = true;
                    // startInfo.RedirectStandardError = true;

                    process.StartInfo = startInfo;
                    process.Start();

                    StreamWriter sw = process.StandardInput;
                    if (sw.BaseStream.CanWrite)
                    {
                        sw.WriteLine(@"cd C:\Users\dell\Entrepreneurship\Engineering\machine_learning");
                        sw.WriteLine(@"C:\Users\dell\Entrepreneurship\Engineering\machine_learning\ml\Scripts\activate");
                        sw.WriteLine($@"python.exe {scriptPath} ""{answer[0].Answer.Replace('\n', ' ')}"" ""{answerId}""");
                        sw.WriteLine(@"deactivate");
                        sw.Close();
                    }
                    process.WaitForExit();
                }
            }
            await Task.Delay(10);
            var sentiments = await _context.AnswerSentiments.Where(d => d.DatasetName.Contains($"news_questions_{answerId}")).ToListAsync();
            if (sentiments.Count == 0)
            {
                return NoContent();
            }
            return Ok(sentiments);
        }

        public IActionResult Questions()
        {
            return View();
        }

        public async Task<IActionResult> Answers()
        {
            var query = await _context.Questions.OrderByDescending(d => d.Id).ToListAsync();
            if (query.Count == 0)
            {
                return NotFound();
            }
            return View(query);
        }

        public async Task<IActionResult> AnswerDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        public async Task<IActionResult> AnswerDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.FindAsync(id);
            if (string.IsNullOrWhiteSpace(question.Id.ToString()))
            {
                return RedirectToAction(nameof(Answers));
            }
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Answers));
        }

        // GET: NewsArticles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsArticle = await _context.NewsArticles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            return View(newsArticle);
        }

        // GET: NewsArticles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NewsArticles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Url,CreatedAt")] NewsArticle newsArticle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newsArticle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newsArticle);
        }

        // GET: NewsArticles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsArticle = await _context.NewsArticles.FindAsync(id);
            if (newsArticle == null)
            {
                return NotFound();
            }
            return View(newsArticle);
        }

        // POST: NewsArticles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Url,CreatedAt")] NewsArticle newsArticle)
        {
            if (id != newsArticle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newsArticle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsArticleExists(newsArticle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(newsArticle);
        }

        // GET: NewsArticles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsArticle = await _context.NewsArticles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            return View(newsArticle);
        }

        // POST: NewsArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newsArticle = await _context.NewsArticles.FindAsync(id);
            _context.NewsArticles.Remove(newsArticle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsArticleExists(int id)
        {
            return _context.NewsArticles.Any(e => e.Id == id);
        }

        
    }
}
