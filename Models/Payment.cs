using System;
using System.Collections.Generic;

namespace DermDiag.Models;

public partial class Payment
{
    public int Id { get; set; }

    public string? Sender { get; set; }

    public string? Receiver { get; set; }

    public string? Method { get; set; }

    public DateOnly? Date { get; set; }

    public decimal? Quantity { get; set; }
}
