
using System.Security.Claims;

namespace NewWordle
{
    public class Program
    {
        private static List<string> wordList = new List<string>
        { "foods", "mount", "yield", "yield", "truth", "model", "forum", "chart", "shock", "vital", "there",
        "sheet", "doubt", "upper", "begun", "clear", "click", "chain", "sport", "delay", "guide", "quiet"};
        private static string currentWord = "";
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            
                app.UseSwagger();
                app.UseSwaggerUI();
            

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("/word", () =>
            {
                currentWord = wordList[Random.Shared.Next(wordList.Count)];
                return currentWord;

            })
            .WithName("GetRandomWord")
            .WithOpenApi();

            app.MapPost("/validate", (string word) =>

            {
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

            })
            .WithName("ValidateWord")
            .WithOpenApi();

            app.Run();
        }
    }
}
