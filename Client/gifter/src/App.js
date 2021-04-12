import React from 'react';
import './App.css';
import { PostProvider } from './Providers/PostProvider';
import { PostList } from './Components/PostList';
import { SearchProvider } from './Providers/SearchProvider';
import { SearchForm } from './Components/SearchPost';


function App() {
    return (
        <div className="App">
            <SearchProvider>
                <PostProvider>
                    <div className="row align-items-center">
                        <SearchForm />
                    </div>
                    <PostList />
                </PostProvider>
            </SearchProvider>
        </div>
    );
}

export default App;