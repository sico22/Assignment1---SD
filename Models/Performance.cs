using System;
using System.Collections.Generic;

namespace Assignment1.DAL.Models;

public partial class Performance
{
    public int PerformanceId { get; set; }

    public string Artist { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public string Title { get; set; } = null!;

    public DateTime Date { get; set; }

    public int NrOfTickets { get; set; }

    public virtual ICollection<Ticket> Tickets { get; } = new List<Ticket>();
}
