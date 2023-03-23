using System;
using System.Collections.Generic;

namespace DataBaseProvider.Models;

public partial class Genre
{
    public Guid Id { get; set; }

    public string GenreName { get; set; } = null!;

    public virtual ICollection<Game> Games { get; } = new List<Game>();
}
