import React, { useState } from 'react';

export const SearchContext = React.createContext();

export const SearchProvider = (props) => {
    const [searchTerm, setSearchTerm] = useState('');
    const [searchResults, setSearchResults] = useState([]);

    const search = (searchTerm) => {
        return fetch(`/api/post/search?q=${searchTerm}`)
            .then((res) => res.json())
            .then(setSearchResults);
    };

    return (
        <SearchContext.Provider
            value={{
                searchTerm,
                setSearchTerm,
                searchResults,
                setSearchResults,
                search,
            }}
        >
            {props.children}
        </SearchContext.Provider>
    );
};