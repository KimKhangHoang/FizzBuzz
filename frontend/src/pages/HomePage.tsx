import React from "react";
import { Link } from "react-router-dom";

const HomePage: React.FC = () => {
  return (
    <div className="container text-center mt-5 home">
      <h1 className="mb-4">Welcome to the FizzBuzz Universe</h1>
      <div className="d-flex justify-content-center gap-3">
        <Link to="/create-game" className="btn btn-primary btn-lg">
          Create Your New FizzBuzz Game
        </Link>
        <Link to="/play-game/1" className="btn btn-success btn-lg">
          Play a FizzBuzz Game
        </Link>
      </div>
    </div>
  );
};

export default HomePage;