using Microsoft.AspNetCore.Mvc;

namespace WordleAPI.Controllers
{
    [ApiController]
    [Route("api/wordle")]
    public class WordleController : ControllerBase
    {
        private static List<string> wordList = new List<string> { "foods", "foods", "foods", "foods", "foods", "foods" };
        private static string currentWord = "";

        private readonly ILogger<WordleController> _logger;

        public WordleController(ILogger<WordleController> logger)
        {
            _logger = logger;
        }

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
            string temp = word;
            char[] chars = temp.ToCharArray();
            char[] currentChars = currentWord.ToCharArray();

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
            return Enumerable.Range(0, 5).Select(index => new WordValidations
            {
                wordContains = validations[index].wordContains,
                charPosition = validations[index].charPosition
            }).ToArray();
        }
    }
}
