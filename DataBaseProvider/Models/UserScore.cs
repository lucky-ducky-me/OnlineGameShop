using System;
using System.Collections.Generic;

namespace DataBaseProvider.Models;

/// <summary>
/// Оценка игры.
/// </summary>
public partial class UserScore
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Оценка.
    /// </summary>
    public short Score { get; set; }

    /// <summary>
    /// Id пользователя, сделавшего оценку.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Id оценённой игры.
    /// </summary>
    public Guid? GameId { get; set; }

    /// <summary>
    /// Оценённая игра.
    /// </summary>
    public virtual Game? Game { get; set; }

    /// <summary>
    /// Сделавший оценку пользователь.
    /// </summary>
    public virtual User? User { get; set; }
}
