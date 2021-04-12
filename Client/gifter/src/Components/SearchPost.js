import React, { useState, useContext } from 'react';
import { SearchContext } from '../Providers/SearchProvider';
import { Button, Label, FormGroup, Form, Input } from 'reactstrap';

export const SearchForm = () => {
    const { search, searchTerm, setSearchTerm, setSearchResults } = useContext(
        SearchContext
    );

    const handleControlledInputChange = (evt) => {
        let newSearch = { ...searchTerm };
        newSearch = evt.target.value;
        setSearchTerm(newSearch);
    };

    return (
        <Form className="col-md-2">
            <h2>Search</h2>
            <FormGroup>
                <Input
                    type="search"
                    name="search-term"
                    id="search-term"
                    autoComplete="off"
                    value={searchTerm}
                    onChange={handleControlledInputChange}
                />
            </FormGroup>
            <Button
                onClick={(evt) => {
                    evt.preventDefault();
                    search(searchTerm);
                }}
            >
                Search
            </Button>
            <Button
                onClick={(evt) => {
                    evt.preventDefault();
                    setSearchTerm('');
                    setSearchResults([]);
                }}
            >
                Clear Search
            </Button>
        </Form>
    );
};