import React, { useState } from "react";
import axios from "axios";

const CreateGamePage: React.FC = () => {
  const [gameName, setGameName] = useState("");
  const [authorName, setAuthorName] = useState("");
  const [range, setRange] = useState<number | "">("");
  const [rules, setRules] = useState([{ divisor: "", replacement: "" }, { divisor: "", replacement: "" }]);

  const handleInputChange = (index: number, field: string, value: string) => {
    const updatedRules = [...rules];
    updatedRules[index][field as keyof (typeof updatedRules)[number]] = value;
    setRules(updatedRules);
  };

  const addRuleRow = () =>
    setRules([...rules, { divisor: "", replacement: "" }]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const gameData = {
      id: 0,
      name: gameName,
      author: authorName,
      range: Number(range),
      rules: rules.map((rule) => ({
        id: 0,
        divisor: Number(rule.divisor),
        replacement: rule.replacement,
        gameRuleId: 0,
      })),
    };

    // Log the data being sent
    console.log("Game data to send:", gameData);

    try {
      const response = await axios.post(
        `${import.meta.env.VITE_API_BASE_URL}/Game/create`,
        gameData
      );
      console.log("Game created:", response.data);
      alert("Game created successfully!");

      // Reset form fields after successful submission
      resetForm();
    } catch (error) {
      console.error("Error creating game:", error);
      alert("Failed to create game. Please try again.");
    }
  };

  const resetForm = () => {
    setGameName("");
    setAuthorName("");
    setRange("");
    setRules([{ divisor: "", replacement: "" }, { divisor: "", replacement: "" }]);
  };

  return (
    <div className="container mt-5">
      <h1>Create a New FizzBuzz Game</h1>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          className="form-control mb-3"
          placeholder="Game name"
          style={{ width: "40%" }}
          value={gameName}
          onChange={(e) => setGameName(e.target.value)}
          required
        />
        <input
          type="text"
          className="form-control mb-3"
          placeholder="Author name"
          style={{ width: "40%" }}
          value={authorName}
          onChange={(e) => setAuthorName(e.target.value)}
          required
        />
        <input
          type="number"
          className="form-control mb-3"
          placeholder="Range (e.g. 1000)"
          style={{ width: "40%" }}
          value={range}
          min="0"
          onChange={(e) => {
            const newValue = Number(e.target.value);
            if (!isNaN(newValue) && newValue >= 0) {
              setRange(newValue);
            }
          }}
          required
        />

        <h5>Rules</h5>
        <button
          type="button"
          className="btn btn-secondary mb-3"
          onClick={addRuleRow}
        >
          Add Rule
        </button>
        <table className="table table-bordered mb-3">
          <thead>
            <tr>
              <th>Divisor</th>
              <th>Replacement</th>
            </tr>
          </thead>
          <tbody>
            {rules.map((rule, index) => (
              <tr key={index}>
                <td>
                  <input
                    type="text"
                    className="form-control"
                    value={rule.divisor}
                    placeholder={index === 0 ? "7" : index === 1 ? "11" : "Divisor"}
                    onChange={(e) =>
                      handleInputChange(index, "divisor", e.target.value)
                    }
                    required
                  />
                </td>
                <td>
                  <input
                    type="text"
                    className="form-control"
                    value={rule.replacement}
                    placeholder={index === 0 ? "Fizz" : index === 1 ? "Buzz" : "Placeholder"}
                    onChange={(e) =>
                      handleInputChange(index, "replacement", e.target.value)
                    }
                    required
                  />
                </td>
              </tr>
            ))}
          </tbody>
        </table>

        <button type="submit" className="btn btn-primary">
          Create Game
        </button>
        <button type="reset" className="btn btn-danger" onClick={resetForm}>
          Reset Form
        </button>
      </form>
    </div>
  );
};

export default CreateGamePage;
