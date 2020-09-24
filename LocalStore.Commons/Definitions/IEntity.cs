﻿using System;

namespace LocalStore.Commons.Definitions
{
    public interface IEntity
    {
        Guid Id { get; }
        DateTime CreationTime { get; }
    }
}
