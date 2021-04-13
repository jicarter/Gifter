import React from 'react';
import { Card, CardImg, CardBody } from 'reactstrap';
import { Link } from "react-router-dom";


export const Post = ({ post }) => {
    return (
        <Card className="m-4">
            <p className="text-left px-2">Posted by:
                    <Link to={`/posts/${post.userId}`}>
                    <strong>{post.userProfile.name}</strong>
                </Link>
            </p>
            <CardImg top src={post.imageUrl} alt={post.title} />
            <CardBody>
                <p>
                    <Link to={`/posts/${post.id}`}>
                        <strong>{post.title}</strong>
                    </Link>
                </p>
                <p>{post.caption}</p>
                {post.comments.length > 0 ? (
                    <ul>
                        {post.comments.map((comment, index) => {
                            return <li key={index}>{comment.message}</li>;
                        })}
                    </ul>
                ) : null}
            </CardBody>
        </Card>
    );
};