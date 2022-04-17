using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CDA.Models
{
    public class CriminalCodeDto : CriminalCodeBase
    {
        public virtual Status? Status { get; set; } = null;
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public virtual UserDto? CreateUser { get; set; } = null;
        public virtual UserDto? UpdateUser { get; set; } = null;

    }
    public class CriminalCode : CriminalCodeBase
    {
        public virtual Status? Status { get; set; } = null; 
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public virtual User? CreateUser { get; set; } = null;
        public virtual User? UpdateUser { get; set; } = null;

    }
    public class CriminalCodeBase
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public decimal Penalty { get; set; }
        public int PrisionTime { get; set; }
        public int? StatusId { get; set; }
        public int? CreateUserId { get; set; }
        public int? UpdateUserId { get; set; }

    }
}
