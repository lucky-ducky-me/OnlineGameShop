using System;
using System.Collections.Generic;

namespace DataBaseProvider.Models;

/// <summary>
/// Заказ.
/// </summary>
public partial class Order
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Время заказа.
    /// </summary>
    public DateTime OrderDate { get; set; }

    /// <summary>
    /// Id пользователя, сделавшего заказ.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Id заказанной игры.
    /// </summary>
    public Guid? GameId { get; set; }

    /// <summary>
    /// Заказанная игра.
    /// </summary>
    public virtual Game? Game { get; set; }

    /// <summary>
    /// Сделавший заказа пользователь.
    /// </summary>
    public virtual User? User { get; set; }
}
