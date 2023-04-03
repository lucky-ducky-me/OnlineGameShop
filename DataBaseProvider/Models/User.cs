using System;
using System.Collections.Generic;

namespace DataBaseProvider.Models;

/// <summary>
/// Пользователь.
/// </summary>
public partial class User
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Логин.
    /// </summary>
    public string Login { get; set; } = null!;

    /// <summary>
    /// Пароль.
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// Телефон.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Список заказов, сделанных пользователем.
    /// </summary>
    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    /// <summary>
    /// Список оценок игр, сделанных пользователем.
    /// </summary>
    public virtual ICollection<UserScore> UserScores { get; } = new List<UserScore>();
}
