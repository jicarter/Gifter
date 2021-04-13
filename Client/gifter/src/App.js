import React from "react";
import { BrowserRouter as Router } from "react-router-dom";
import "./App.css";
import ApplicationViews from "./Components/ApplicationView";
import { SearchForm } from "./Components/SearchPost";
import { PostProvider } from "./Providers/PostProvider";
import { Header } from "./Components/Header";

function App() {
    return (
        <div className="App">
            <Router>
                <PostProvider>
                    <Header />
                    <ApplicationViews />
                    <SearchForm />
                </PostProvider>
            </Router>
        </div>
    );
}

export default App;