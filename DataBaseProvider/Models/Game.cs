using System;
using System.Collections.Generic;

namespace DataBaseProvider.Models;

/// <summary>
/// Игра.
/// </summary>
public partial class Game
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Стоимость.
    /// </summary>
    public int Cost { get; set; }

    /// <summary>
    /// Id жанра.
    /// </summary>
    public Guid? GenreId { get; set; }

    /// <summary>
    /// Название.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Жанр.
    /// </summary>
    public virtual Genre? Genre { get; set; }

    /// <summary>
    /// Список заказов игры.
    /// </summary>
    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    /// <summary>
    /// Список оценок игры.
    /// </summary>
    public virtual ICollection<UserScore> UserScores { get; } = new List<UserScore>();
}
