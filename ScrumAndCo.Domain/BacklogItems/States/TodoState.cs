﻿namespace ScrumAndCo.Domain.BacklogItems.States;

public class TodoState(BacklogItem context) : ItemState(context)
{
    public override void ToInProgress()
    {
        _context.ChangeState(new InProgressState(_context));
    }
}