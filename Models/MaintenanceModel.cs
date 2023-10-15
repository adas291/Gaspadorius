using System.ComponentModel.DataAnnotations;

namespace Gaspadorius.Models;

public class MaintenanceModel
{
    public int? Id { get; set; }
    public DateTime Date{ get; set; }
    public string EventName { get; set; } = string.Empty;
    public int FkObject { get; set; }
}