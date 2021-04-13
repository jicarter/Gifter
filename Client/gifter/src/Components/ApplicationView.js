import React from "react";
import { Switch, Route } from "react-router-dom";
import PostList from "./PostList";
import PostForm from "./PostForm";

const ApplicationViews = () => {
  return (
    <Switch>
      <Route path="/" exact>
        <PostList />
      </Route>

      <Route path="/posts/add">
        <PostForm />
      </Route>

      <Route path="/posts/:id">{/* TODO: Post Details Component */}</Route>

      <Route path="users/:id"></Route>
      <UserPosts />
    </Switch>
  );
};

export default ApplicationViews;