﻿using ScrumAndCo.Domain.BacklogItems;
using ScrumAndCo.Domain.Forum.States;
using ThreadState = ScrumAndCo.Domain.Forum.States.ThreadState;

namespace ScrumAndCo.Domain.Forum;

public abstract class Thread
{
    private ThreadState _threadState;
    public string Topic;
    public string Description;
    public BacklogItem BacklogItem;
    public User Author;
    public List<Comment> Comments;
    
    
    public Thread(string topic, string description, BacklogItem backlogItem, User author)
    {
        this._threadState = new OpenState(this);
        this.Topic = topic;
        this.Description = description;
        this.BacklogItem = backlogItem;
        this.Author = author;
        this.Comments = new List<Comment>();
    }
    
    public void NextThreadState()
    {
        _threadState.NextThreadState();
    }

    public void addComment(Comment comment)
    {
        this.Comments.Add(comment);
        // TODO: notify everyone that left a comment to the thread including author thread
    }
    
    internal void ChangeThreadState(ThreadState threadState)
    {
        Console.WriteLine($"New thread state: {threadState.GetType().FullName}");
        _threadState = threadState;
    }
}