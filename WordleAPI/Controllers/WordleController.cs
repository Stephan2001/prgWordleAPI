using Microsoft.AspNetCore.Mvc;

namespace WordleAPI.Controllers
{
    [ApiController]
    [Route("api/wordle")]
    public class WordleController : ControllerBase
    {
        private static List<string> wordList = new List<string> 
        { "foods", "mount", "yield", "yield", "truth", "model", "forum", "chart", "shock", "vital", "there",
        "sheet", "doubt", "upper", "begun", "clear", "click", "chain", "sport", "delay", "guide", "quiet"};
        private static string currentWord = "";

        private readonly ILogger<WordleController> _logger;

        public WordleController(ILogger<WordleController> logger)
        {
            _logger = logger;
        }
        // url: https://localhost:32778/api/wordle
        // get method
        // 
        [HttpGet(Name = "generateWord")]
        public string Get()
        {
            currentWord = wordList[Random.Shared.Next(wordList.Count)];
            return currentWord;
        }

        [HttpPost(Name = "checkWord")]
        public IEnumerable<WordValidations> Post([FromBody] string word)
        {
            // this is a list of all validations for the chars
            List<WordValidations> validations = new List<WordValidations>();

            // assigning the string to a char array
            string temp = word.ToString().ToLower();
            char[] chars = temp.ToCharArray();
            char[] currentChars = currentWord.ToCharArray();
            try
            {
                if (chars.Count() == 5)
                {
                    if (char.IsLetter(chars[0]) && char.IsLetter(chars[1]) && char.IsLetter(chars[2]) && char.IsLetter(chars[3]) && char.IsLetter(chars[4]))
                    {
                        WordValidations wordValidations;
                        int counter = 0;
                        foreach (char c in chars)
                        {
                            wordValidations = new WordValidations();
                            if (currentWord.Contains(c.ToString()))
                            {
                                wordValidations.wordContains = true;
                                if (currentChars[counter] == chars[counter])
                                {
                                    wordValidations.charPosition = true;
                                }
                                else
                                {
                                    wordValidations.charPosition = false;
                                }
                                validations.Add(wordValidations);
                            }
                            else
                            {
                                wordValidations.wordContains = false;
                                wordValidations.charPosition = false;
                                validations.Add(wordValidations);
                            }
                            counter++;
                        }
                    }
                    else
                    {
                        // not all chars are characters
                    }
                }
                else
                {
                    // not enough chars
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured " + e.ToString());
            }
            
            return Enumerable.Range(0, 5).Select(index => new WordValidations
            {
                wordContains = validations[index].wordContains,
                charPosition = validations[index].charPosition
            }).ToArray();
        }
    }
}
