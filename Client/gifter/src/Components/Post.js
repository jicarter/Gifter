import React from 'react';
import { Card, CardImg, CardBody } from 'reactstrap';

export const Post = ({ post }) => {
    return (
        <Card className="m-4">
            <p className="text-left px-2">Posted by: {post.userProfile.name}</p>
            <CardImg top src={post.imageUrl} alt={post.title} />
            <CardBody>
                <p>
                    <strong>{post.title}</strong>
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