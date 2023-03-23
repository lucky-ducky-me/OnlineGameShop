using System;
using System.Collections.Generic;

namespace DataBaseProvider.Models;

public partial class Game
{
    public Guid Id { get; set; }

    public int Cost { get; set; }

    public Guid? GenreId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Genre? Genre { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<UserScore> UserScores { get; } = new List<UserScore>();
}
