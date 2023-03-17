using System;
using System.Collections.Generic;

namespace Assignment1.DAL.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int PerformanceId { get; set; }

    public int NrOfPlaces { get; set; }

    public virtual Performance Performance { get; set; } = null!;
}
