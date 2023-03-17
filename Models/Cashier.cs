using System;
using System.Collections.Generic;

namespace Assignment1.DAL.Models;

public partial class Cashier
{
    public int CashierId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}
