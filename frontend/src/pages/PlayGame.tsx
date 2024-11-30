import React, { useState, useEffect } from "react";
import axios from "axios";

const PlayGame: React.FC<{ gameId?: number }> = ({ gameId = 2 }) => {
  const [gameDescription, setGameDescription] = useState<string>(""); 
  const [rulesDescription, setRulesDescription] = useState<string>(""); 
  const [timeLimit, setTimeLimit] = useState<number | string >("");
  const [input, setInput] = useState<string>("");
  const [remainingTime, setRemainingTime] = useState<number | null>(null); 
  const [score, setScore] = useState<number>(0); 
  const [gameStarted, setGameStarted] = useState(false); 
  const [randomNumber, setRandomNumber] = useState<number | null>(null);

  // Fetch game rules when the component loads
  useEffect(() => {
    const fetchGameRules = async () => {
      try {
        const response = await axios.get(
          `${import.meta.env.VITE_API_BASE_URL}/Game/${gameId}`
        );
        const game = response.data;

        // Build a game description from the rules
        const rulesSummary = game.rules
          .map(
            (rule: any) => `If divisible by ${rule.divisor}, enter "${rule.replacement}"`
          )
          .join(". ") + ". Else type the number";

        setGameDescription(`Game: ${game.name} by ${game.author}`);
        setRulesDescription(`Rules: ${rulesSummary}`);
      } catch (error) {
        console.error("Error fetching game rules:", error);
      }
    };

    fetchGameRules();
  }, [gameId]);

  // Timer effect when the game starts
  useEffect(() => {
    if (remainingTime !== null && remainingTime > 0) {
      const timer = setInterval(() => {
        setRemainingTime((prevTime) => (prevTime ? prevTime - 1 : 0));
      }, 1000);
      return () => clearInterval(timer);
    }
  }, [remainingTime]);

  const handleStartGame = async () => {
    if (Number(timeLimit) > 0) {
      setRemainingTime(Number(timeLimit));
      setGameStarted(true); // Mark the game as started

      try {
        // Fetch the first random number from the backend
        const response = await axios.post(
          `${import.meta.env.VITE_API_BASE_URL}/Game/random`,
          gameId, // Send gameId directly (as an integer)
          {
            headers: {
                'Content-Type': 'application/json'
            }
          }
      );

        setRandomNumber(response.data.randomNumber);
      } catch (error) {
        console.error("Error starting the game:", error);
      }
    }
  };

  // Submit the number to the backend
  const handleNumberSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (input !== "" && remainingTime && remainingTime > 0) {
      try {
        // Send the number to the backend to verify the result
        const response = await axios.post(`${import.meta.env.VITE_API_BASE_URL}/Game/verify`, 
          null, // Set the first argument to null to avoid sending a body object
          {
            params: {
              gameId: gameId,
              playerInput: input,
            }
          }
        );

        // Update the score if the number is correct
        console.log(response.data.result);
        if (response.data.result) {
          setScore((prevScore) => prevScore + 1);
        }

        // Fetch a new random number
        const randomResponse = await axios.post(
            `${import.meta.env.VITE_API_BASE_URL}/Game/random`,
            gameId, // Send gameId directly (as an integer)
            {
                headers: {
                    'Content-Type': 'application/json'
                }
            }
        );

        setInput("");
        setRandomNumber(randomResponse.data.randomNumber);
      } catch (error) {
        console.error("Error submitting number:", error);
      }
    }
  };

  // Handle time limit input change
  const handleTimeLimitChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setTimeLimit(Number(e.target.value));
  };

  // Restart the game to the initial state
  const handleRestartGame = () => {
    setTimeLimit("");
    setRemainingTime(null);
    setGameStarted(false);
    setInput("");
    setScore(0);
    setRandomNumber(null);
  };

  return (
    <div className="container mt-5">
        <h1>Play Game</h1>

        {/* If the game hasn't started, show the description and time input */}
        {!gameStarted ? (
            <>
                <p>{gameDescription}</p>
                <p>{rulesDescription}</p>

                <form
                    onSubmit={(e) => {
                        e.preventDefault();
                        handleStartGame();
                    }}
                >
                    <label>
                        Set Time Limit (in seconds):
                        <input
                            type="number"
                            style={{ width: "20%", marginLeft: "0.5rem" }}
                            value={timeLimit}
                            min="1"
                            max="300"
                            onChange={handleTimeLimitChange}
                            required
                        />
                    </label>
                    <button type="submit" className="btn btn-info">
                        Start
                    </button>
                </form>
            </>
        ) : (
            <>
                <p>Time Remaining: {remainingTime} seconds</p>
                
                {/* Only show random number if game is ongoing */}
                {remainingTime !== null && remainingTime > 0 && randomNumber !== null && (
                    <p>Random Number: {randomNumber}</p>
                )}

                {/* Player Input for Numbers */}
                {remainingTime !== null && remainingTime > 0 ? (
                  <form onSubmit={handleNumberSubmit}>
                    <label>
                        Enter a number or replacement:
                        <input
                            type="text"
                            value={input}
                            onChange={(e) => setInput(e.target.value)}
                            style={{ width: "20%", marginLeft: "0.5rem"}}
                            required
                        />
                    </label>
                    <button type="submit" className="btn btn-info">
                        Submit
                    </button>
                  </form>
                ) : (
                    // Show final score and restart button when time is up
                    <>
                        <p>Final Score: {score}</p>
                        <button className="btn btn-secondary" onClick={handleRestartGame}>
                            Restart Game
                        </button>
                    </>
                )}
            </>
        )}
    </div>
  );

};

export default PlayGame;
