using FizzBuzzAPI.Interfaces;
using FizzBuzzAPI.Models;
using FizzBuzzAPI.Data;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FizzBuzzAPI.Services
{
    public class GameService : IGameService
    {
        private readonly FizzBuzzDbContext _context;

        public GameService(FizzBuzzDbContext context)
        {
            _context = context;
        }

        // Create a new game rule
        public async Task CreateGame(GameRule gameRule)
        {
            if (gameRule == null)
            {
                throw new ArgumentNullException(nameof(gameRule), "Game rule cannot be empty");
            }

            await _context.GameRules.AddAsync(gameRule);
            await _context.SaveChangesAsync();
        }

        // Get a game rule by its ID
        public async Task<GameRule?> GetGameById(int gameId)
        {
            return await _context.GameRules
                .Include(g => g.Rules)
                .FirstOrDefaultAsync(g => g.Id == gameId);
        }

        // Delete a game rule by its ID
        public async Task DeleteGame(int gameId)
        {
            var gameRule = await _context.GameRules.FindAsync(gameId);
            if (gameRule == null)
            {
                throw new ArgumentNullException(nameof(gameId), "Game rule does not exist");
            }

            _context.GameRules.Remove(gameRule);
            await _context.SaveChangesAsync();
        }

        // Add a new rule to an existing game
        public void AddRuleToGame(int gameId, int divisor, string replacement)
        {
            var gameRule = _context.GameRules.Find(gameId);
            if (gameRule == null)
            {
                throw new ArgumentNullException(nameof(gameId), "Game rule does not exist");
            }

            gameRule.AddRule(divisor, replacement);
            _context.SaveChanges();
        }

        // Get a random number for a game
        public int GetRandomNumber(int gameId)
        {
            var game = _context.GameRules.Find(gameId);
            if (game == null)
            {
                throw new ArgumentNullException(nameof(gameId), "Game rule does not exist");
            }

            return new Random().Next(1, game.Range + 1);
        }

        // Verify player answer and return whether it's correct
        public async Task<bool> VerifyAnswer(int gameId, string playerInput)
        {
            var game = await GetGameById(gameId);
            if (game == null)
            {
                throw new ArgumentNullException(nameof(gameId), "Game rule does not exist");
            }

            var gameRules = game.Rules;
            var result = new List<string>();

            // Check if the player input is a number or a replacement string
            bool isNumber = int.TryParse(playerInput, out int inputNumber);

            // Generate the expected result based on the rules
            foreach (var rule in gameRules)
            {
                // If input is a number, check the divisibility
                if (isNumber && inputNumber % rule.Divisor == 0)
                {
                    result.Add(rule.Replacement);
                }
            }

            // Join the expected results from the rules
            string expectedResult = string.Join("", result);

            // Determine if the input is correct
            bool isCorrect = (isNumber && playerInput == inputNumber.ToString()) || playerInput.Equals(expectedResult, StringComparison.OrdinalIgnoreCase);

            return isCorrect;
        }
    }
}
