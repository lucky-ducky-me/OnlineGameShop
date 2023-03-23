using System;
using System.Collections.Generic;

namespace DataBaseProvider.Models;

public partial class UserScore
{
    public Guid Id { get; set; }

    public short Score { get; set; }

    public Guid? UserId { get; set; }

    public Guid? GameId { get; set; }

    public virtual Game? Game { get; set; }

    public virtual User? User { get; set; }
}
