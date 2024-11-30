using FizzBuzzAPI.Models;

namespace FizzBuzzAPI.Interfaces
{
    public interface IGameService
    {
        // Create a new game
        Task CreateGame(GameRule gameRule);

        // Retrieve game by ID (includes rules, range, etc.)
        Task<GameRule?> GetGameById(int gameId);

        // Delete a game by ID
        Task DeleteGame(int gameId);

        // Add a rule to an existing game
        void AddRuleToGame(int gameId, int divisor, string replacement);

        // Get random number for a game
        int GetRandomNumber(int gameId);

        // Verify a player's input against the game rules
        Task<bool> VerifyAnswer(int gameId, string playerInput, int randomNumber);
    }
}