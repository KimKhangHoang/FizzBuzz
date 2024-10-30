import React from "react";
import { Routes, Route } from "react-router-dom";
import Navbar from "./components/Navbar";
import HomePage from "./pages/HomePage";
import GamePage from "./pages/PlayGame";
import CreateGamePage from "./pages/CreateGamePage";

const App: React.FC = () => {
  return (
    <>
      <Navbar />
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/play-game/:gameId" element={<GamePage />} />
        <Route path="/create-game" element={<CreateGamePage/>} />
      </Routes>
    </>
  );
};

export default App;
