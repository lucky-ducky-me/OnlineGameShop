using System;
using System.Collections.Generic;

namespace DataBaseProvider.Models;

/// <summary>
/// Жанр.
/// </summary>
public partial class Genre
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название жанра.
    /// </summary>
    public string GenreName { get; set; } = null!;

    /// <summary>
    /// Игры этого жанра.
    /// </summary>
    public virtual ICollection<Game> Games { get; } = new List<Game>();
}
