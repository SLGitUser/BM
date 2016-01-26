using System;
using System.ComponentModel.DataAnnotations;

namespace Bm.Models.Common
{

    public interface IId
    {
        long Id { get; set; }
    }

    public interface ICreateStamp
    {
        [Required]
        string CreatedBy { get; set; }

        [Required]
        DateTime CreatedAt { get; set; }
    }

    public interface IUpdateStamp
    {
        string UpdatedBy { get; set; }

        DateTime? UpdatedAt { get; set; }
    }

    public interface IStamp : ICreateStamp, IUpdateStamp
    {
    }
    
    public interface INo
    {
        string No { get; set; }
    }

    public interface IName
    {
        string Name { get; set; }
    }

    public interface IOrg
    {
        long OrgId { get; set; }
    }
}
