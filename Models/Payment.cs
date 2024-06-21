using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DermDiag.Models;

public partial class Payment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int? SenderID { get; set; }

    public int? ReceiverID { get; set; }

    public string? Status { get; set; }
    
    public string? Method { get; set; }

    public DateTime? Date { get; set; }

    public decimal? Amount { get; set; }
}
