<<<<<<< HEAD
using takecontrol.Domain.Primitives;

namespace takecontrol.Domain.Errors.Players;
=======
﻿using takecontrol.Domain.Primitives;

namespace takecontrol.Domain.Errors.Clubs;
>>>>>>> e4c019e8054464869312e69f9d9156c4d6aeb5c5

public sealed class PlayerError : DomainError
{
    public PlayerError(int codeId, string message) : base(codeId, message)
    {
    }

<<<<<<< HEAD
    public static PlayerError PlayerNotFound = new PlayerError(1801, "Player doesn't exists for this id.");
=======
    
>>>>>>> e4c019e8054464869312e69f9d9156c4d6aeb5c5
}
